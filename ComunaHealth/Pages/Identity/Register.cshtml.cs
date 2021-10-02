using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ComunaHealth.Pages.Identity
{
	/// <summary>
    /// Modelo de la pagina encargada de lidiar con el registro de usuarios
    /// </summary>
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
	    private readonly ComunaDbContext _dbcontext;
	    private readonly UserManager<ModeloUsuario> _userManager;
	    private readonly IConfiguration _config;

		[BindProperty]
	    public ViewModelRegistro_DatosGenerales RegistroDatosGenerales { get; set; } = new ViewModelRegistro_DatosGenerales();

		[BindProperty]
	    public ViewModelRegistro_DatosMedico RegistroDatosMedico { get; set; } = new ViewModelRegistro_DatosMedico();

		/// <summary>
		/// Tipos de cuentas seleccionables
		/// </summary>
		public List<SelectListItem> TiposDeCuentaDisponibles { get; init; }

		/// <summary>
		/// Tipos de cuentas seleccionables
		/// </summary>
		public List<SelectListItem> MunicipiosDisponibles { get; init; }

		public RegisterModel(ComunaDbContext dbcontext, UserManager<ModeloUsuario> userManager, IConfiguration config)
	    {
		    _dbcontext   = dbcontext;
		    _userManager = userManager;
		    _config      = config;

		    TiposDeCuentaDisponibles = EnumHelpers.ToSelectListItemList(EnumHelpers.ObtenerValoresFlag(ETipoCuenta.Administrador | ETipoCuenta.AdministradorJefe));
		    MunicipiosDisponibles    = EnumHelpers.ToSelectListItemList(EnumHelpers.ObtenerValores<EMunicipio>());
	    }

	    public void OnGet()
        {
        }

		[ValidateAntiForgeryToken]
	    public async Task<IActionResult> OnPost()
	    {
			if (RegistroDatosGenerales.TipoCuenta == ETipoCuenta.Paciente)
				ModelState.Remove($"{nameof(RegistroDatosMedico)}.{nameof(RegistroDatosMedico.MatriculaMedico)}");
				
			if (!TryValidateModel(RegistroDatosGenerales))
			    return Page();

			if(!int.TryParse(RegistroDatosGenerales.DNI, out var nada))
				ModelState.AddModelError("RegistroDatosGenerales.DNI", "DNI solo puede contener caracteres numericos");

			if (await _dbcontext.Users.AnyAsync(p => p.Email == RegistroDatosGenerales.Mail)) 
				ModelState.AddModelError("RegistroDatosGenerales.Mail", "Ya existe un usuario con esa direccion de mail");

			if(await _dbcontext.Users.AnyAsync(p => p.DNI == int.Parse(RegistroDatosGenerales.DNI)))
				ModelState.AddModelError("RegistroDatosGenerales.DNI", "Ya existe un usuario con esa direccion de mail");

			//Obtenemos el tamaño maximo permitido para un archivo desde la configuracion
			var tamañoMaximoImagenes = int.Parse(_config["TamanioMaximoArchivo"]);

		    //Nos aseguramos de que el tamaño de los archivos subidos no sea mayor al maximo
			if(RegistroDatosGenerales.FotoAnversoDNI.Length > tamañoMaximoImagenes)
				ModelState.AddModelError(nameof(ViewModelRegistro_DatosGenerales.FotoAnversoDNI), "Imagen demasiado grande");

			if (RegistroDatosGenerales.FotoAnversoDNI.Length > tamañoMaximoImagenes)
				ModelState.AddModelError(nameof(ViewModelRegistro_DatosGenerales.FotoReversoDNI), "Imagen demasiado grande");

			//Nos aseguramos de que el formato de los archivos subido sea valido
			if (!RegistroDatosGenerales.FotoAnversoDNI.ImagenEsValida())
				ModelState.AddModelError(nameof(ViewModelRegistro_DatosGenerales.FotoAnversoDNI), "Imagen no valida");

		    if (!RegistroDatosGenerales.FotoReversoDNI.ImagenEsValida()) 
			    ModelState.AddModelError(nameof(ViewModelRegistro_DatosGenerales.FotoReversoDNI), "Imagen no valida");

			if(ModelState.ErrorCount > 0)
				return Page();

			//Creamos un usuario utilizando lo datos ingresados
			var usuarioCreado = RegistroDatosGenerales.CrearUsuario(_userManager);

			//Si el tipo de cuenta siendo creado es para un medico...
			if (usuarioCreado is ModeloMedico medico)
			{
				//Validamos el modelo
				if (!TryValidateModel(RegistroDatosMedico))
					return Page();

				//Obtenemos las especialzaciones seleccionadas
				StringValues especializaciones = Request.Form["RegistroDatosMedico.Especializaciones"];

				//Si no selecciono ninguna especialidad, tiramos error
				if (especializaciones.Count == 0)
				{
					ModelState.AddModelError(nameof(ViewModelRegistro_DatosMedico.Especializaciones), "Debes especificar al menos una especializacion");

				    return Page();
				}

				//Guardamos las selecciones
				medico.StringEspecializaciones = Regex.Replace(string.Join(',', especializaciones), @"\s", "");
				medico.Especializaciones.RemoveAll(e => e == EEspecializacion.NINGUNA);

				medico.Matricula = int.Parse(RegistroDatosMedico.MatriculaMedico);
			}

			//Establecemos el estado de la cuenta creada como verificacion pendiente
			usuarioCreado.EstadoCuenta = EEstadoCuenta.VerificacionPendiente;

			try
			{
				_dbcontext.Attach(usuarioCreado).State = EntityState.Added;

				await _userManager.CreateAsync(usuarioCreado);

				await _dbcontext.SaveChangesAsync();

				await _userManager.AddToRoleAsync(usuarioCreado,
					RegistroDatosGenerales.TipoCuenta == ETipoCuenta.Paciente
						? Constantes.NombreRolPaciente
						: Constantes.NombreRolMedico);

				await _dbcontext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return new JsonResult("Algo salio mal");
			}

			return RedirectToPage("/Index");
		}

		public async Task<IActionResult> OnPostVerificarEmailDisponible([FromQuery(Name = "mail")] string mail)
		{
		
			return new JsonResult(await _dbcontext.Users.AnyAsync(p => p.Email == mail) ? "false" : "true");
		}
	}

	#region ViewModelRegistro

	/// <summary>
	/// View model que contiene los datos necesarios para el registro
	/// </summary>
	[Serializable]
	public class ViewModelRegistro_DatosGenerales
	{
		[Display(Name = "Nombres")]
		[DataType(DataType.Text)]
		[StringLength(50, ErrorMessage = "Nombre demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string Nombre { get; set; }

		[Display(Name = "Apellidos")]
		[DataType(DataType.Text)]
		[StringLength(50, ErrorMessage = "Apellido demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string Apellido { get; set; }

		[Display(Name = "Descripcion")]
		[DataType(DataType.Text)]
		[StringLength(500, ErrorMessage = "Descripcion demasiado larga")]
		public string Descripcion { get; set; }

		[Display(Name = "DNI")]
		[DataType(DataType.Text)]
		[StringLength(9, ErrorMessage = "DNI demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string DNI { get; set; }

		[Display(Name = "Confirmar DNI")]
		[DataType(DataType.Text)]
		[StringLength(9, ErrorMessage = "DNI demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[Compare(nameof(DNI), ErrorMessage = "Los DNIs no coinciden")]
		public string ConfirmacionDNI { get; set; }

		[Display(Name = "EMail")]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "Mail demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[EmailAddress(ErrorMessage = "Mail no valido")]
		public string Mail { get; set; }

		[Display(Name = "Telefono")]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "Telefono demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string Telefono { get; set; }

		[Display(Name = "Contraseña")]
		[DataType(DataType.Password)]
		[StringLength(50, ErrorMessage = "Contraseña demasiado larga")]
		[PasswordPropertyText]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string Contraseña { get; set; }

		[Display(Name = "Confirmar contraseña")]
		[DataType(DataType.Password)]
		[StringLength(50, ErrorMessage = "Contraseña demasiado larga")]
		[Compare(nameof(Contraseña), ErrorMessage = "Las contraseñas no coinciden")]
		[PasswordPropertyText]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string ConfirmacionContraseña { get; set; }

		[Display(Name = "Mostrar mail publicamente", Description = "Indica si el mail debe ser mostrado dentro de tu informacion publica")]
		public bool MailEsPublico { get; set; }

		[Display(Name = "Mostrar telefono publicamente", Description = "Indica si el telefono debe ser mostrado dentro de tu informacion publica")]
		public bool TelefonoEsPublico { get; set; }

		[Display(Name = "Habilitar autenticacion de dos factores", Description = "Cuando alguien intente entrar a tu cuenta se enviara un correo a tu mail para confirmar que eres tu")]
		public bool AutenticacionDeDosFactoresActiva { get; set; }

		/// <summary>
		/// Tipo de la cuenta que se esta creando
		/// </summary>
		[Display(Name = "Tipo de cuenta")]
		[Required]
		public ETipoCuenta TipoCuenta { get; set; }

		[Display(Name = "Municipio")]
		[Required]
		public EMunicipio Municipio { get; set; }

		[Display(Name = "Anverso DNI")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public IFormFile FotoAnversoDNI { get; set; }

		[Display(Name = "Reverso DNI")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public IFormFile FotoReversoDNI { get; set; }

		/// <summary>
		/// Crea un <see cref="ModeloUsuarioNoAdministrador"/> con los datos de este viewmodel
		/// </summary>
		/// <param name="userManager">User manager</param>
		/// <returns><see cref="ModeloUsuarioNoAdministrador"/> creado</returns>
		public ModeloUsuarioNoAdministrador CrearUsuario(UserManager<ModeloUsuario> userManager)
		{
			//Creamos el tipo de cuenta correspondiente en base al tipo elegido
			ModeloUsuarioNoAdministrador usuarioCreado = TipoCuenta == ETipoCuenta.Paciente ? new ModeloPaciente() : new ModeloMedico();

			//Guardamos nombre y apellido en el nombre de usuario
			usuarioCreado.UserName = $"{Nombre} {Apellido}";

			usuarioCreado.Descripcion = Descripcion;

			usuarioCreado.DNI = int.Parse(DNI);

			usuarioCreado.Email = Mail;

			usuarioCreado.PhoneNumber = Telefono;

			usuarioCreado.MailEsPublico     = MailEsPublico;
			usuarioCreado.TelefonoEsPublico = TelefonoEsPublico;
			usuarioCreado.TwoFactorEnabled  = AutenticacionDeDosFactoresActiva;

			usuarioCreado.Municipio   = Municipio;
			usuarioCreado.TiposCuenta = TipoCuenta;

			//Guardamos los bytes de las fotos
			using (var bReader = new BinaryReader(FotoAnversoDNI.OpenReadStream()))
			{
				usuarioCreado.FotoAnversoDNI = bReader.ReadBytes((int) FotoAnversoDNI.Length);
			}

			using (var bReader = new BinaryReader(FotoReversoDNI.OpenReadStream()))
			{
				usuarioCreado.FotoReversoDNI = bReader.ReadBytes((int)FotoReversoDNI.Length);
			}

			//Hasheamos la contraseña y guardamos el resultado
			usuarioCreado.PasswordHash = userManager.PasswordHasher.HashPassword(usuarioCreado, Contraseña);

			return usuarioCreado;
		}
	}

	/// <summary>
	/// Viewmodel que contiene los datos extra necesarios para el registro de un medico
	/// </summary>
	public class ViewModelRegistro_DatosMedico
	{
		/// <summary>
		/// Matricula del medico
		/// </summary>
		[Display(Name = "Numero de matricula")]
		[StringLength(12, ErrorMessage = "Numero demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string MatriculaMedico { get; set; }

		/// <summary>
		/// Especializaciones del medico
		/// </summary>
		public string[] Especializaciones { get; set; }
	}

	#endregion
}