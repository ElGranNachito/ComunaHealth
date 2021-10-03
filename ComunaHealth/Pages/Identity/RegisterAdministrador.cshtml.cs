using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ComunaHealth.Pages.Identity
{
	/// <summary>
	/// Modelo de la pagina de registro de administradores
	/// </summary>
	[Authorize(Roles = Constantes.NombreRolAdministradorjefe)]
	public class RegisterAdministradorModel : PageModel
	{
		private readonly ComunaDbContext _dbcontext;
		private readonly UserManager<ModeloUsuario> _userManager;

		public RegisterAdministradorModel(ComunaDbContext dbContext, UserManager<ModeloUsuario> userManager)
	    {
		    _dbcontext   = dbContext;
		    _userManager = userManager;
	    }

		/// <summary>
		/// Post que crea el administrador
		/// </summary>
		/// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
			if (!ModelState.IsValid)
				return Page();

			//Verificamos los datos del modelo
			if (!int.TryParse(DNI, out var dniParseado))
				ModelState.AddModelError(nameof(DNI), "DNI solo puede contener caracteres numericos");

			if (await _dbcontext.Users.AnyAsync(p => p.DNI == dniParseado))
				ModelState.AddModelError(nameof(DNI), "Ya existe un usuario con ese DNI");

			if (await _dbcontext.Users.AnyAsync(p => p.Email == Mail))
				ModelState.AddModelError(nameof(Mail), "Ya existe un usuario con esa direccion de mail");

			if (await _dbcontext.Users.AnyAsync(p => p.PhoneNumber == Telefono))
				ModelState.AddModelError(nameof(Telefono), "Ya existe un usuario con ese numero de telefono");

			//Si se añadio algun error al modelo nos pegamos la vuelta
			if (ModelState.ErrorCount > 0)
				return Page();

			//Verificamos que la contraseña cumpla con los requisitos de segurdad
			foreach (var pValidator in _userManager.PasswordValidators)
			{
				//Si no cumple con alguno de los validators añadimos el error al modelo y recargamos la pagina
				if (!(await pValidator.ValidateAsync(_userManager, null, Contraseña)).Succeeded)
				{
					ModelState.AddModelError(nameof(Contraseña), $"Contraseña demasiado debil. Asegurese de que:{Environment.NewLine}-Contenga al menos 8 caracteres{Environment.NewLine}-Contenga al menos una mayuscula y una minuscula{Environment.NewLine}-Contenga al menos tres caracteres unicos{Environment.NewLine}-Contenga al menos un caracter no alfa-numerico");

					return Page();
				}
			}

			//Creamos el nuevo administrador
			ModeloAdministrador nuevoAdministrador = new ModeloAdministrador
			{
				UserName    = NombreAdministrador,
				DNI         = dniParseado,
				PhoneNumber = Telefono,
				Email       = Mail,

				StringTiposCuenta  = ETipoCuenta.Administrador.ToString(),
				EstadoCuenta = EEstadoCuenta.Habilitada,
				RegionSanitaria = Enum.Parse<ERegionSanitariaBSAS>(RegionSanitaria)
			};

			//Hasheamos la contraseña y la guardamos en el modelo
			nuevoAdministrador.PasswordHash = _userManager.PasswordHasher.HashPassword(nuevoAdministrador, Contraseña);

			//Creamos los claims para el nuevo administrador
			var claimsNuevoAdministrador = new List<Claim>
			{
				new Claim(ClaimTypes.Email, Mail),
				new Claim(ClaimTypes.Name, NombreAdministrador),
				new Claim(nameof(ModeloUsuario.DNI), DNI)
			};

			//Intentamos crear el administrador y guardarlo en la base de datos
			try
			{
				_dbcontext.Attach(nuevoAdministrador).State = EntityState.Added;

				await _userManager.CreateAsync(nuevoAdministrador);

				await _dbcontext.SaveChangesAsync();

				//Añadimos los claims al usuario
				await _userManager.AddClaimsAsync(nuevoAdministrador, claimsNuevoAdministrador);

				//Añadimos el rol de administrador al usuario
				await _userManager.AddToRoleAsync(nuevoAdministrador, Constantes.NombreRolAdministrador);

				await _dbcontext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				//Si el usuario se guardo en la base de datos entonces lo borramos ya que algo fallo en los pasos posteriores
				if (nuevoAdministrador.Id != 0)
					_dbcontext.Remove(nuevoAdministrador);

				await _dbcontext.SaveChangesAsync();

				return new JsonResult("Algo salio mal");
			}

			//Si llegamos hasta aqui entonces se pudo registrar al nuevo administrador
			return new RedirectToPageResult("/Index");
        }

		#region Propiedades para la registracion

		[Display(Name = "Nombre")]
		[DataType(DataType.Text)]
		[StringLength(50, ErrorMessage = "Nombre demasiado largo")]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[BindProperty]
		public string NombreAdministrador { get; set; }

		[Display(Name = "Mail")]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "Mail demasiado largo")]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[EmailAddress(ErrorMessage = "Mail no valido")]
		[BindProperty]
		public string Mail { get; set; }

		[Display(Name = "Telefono")]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "Telefono demasiado largo")]
		[Required(AllowEmptyStrings = false,ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[BindProperty]
		public string Telefono { get; set; }

		[Display(Name = "Contraseña")]
		[DataType(DataType.Password)]
		[StringLength(50, ErrorMessage = "Contraseña demasiado larga")]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[PasswordPropertyText]
		[BindProperty]
		public string Contraseña { get; set; }

		[Display(Name = "Confirmar contraseña")]
		[DataType(DataType.Password)]
		[StringLength(50, ErrorMessage = "Contraseña demasiado larga")]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[Compare(nameof(Contraseña), ErrorMessage = "Las contraseñas no coinciden")]
		[PasswordPropertyText]
		[BindProperty]
		public string ConfirmacionContraseña { get; set; }

		[Display(Name = "DNI")]
		[DataType(DataType.Text)]
		[StringLength(9, ErrorMessage = "DNI demasiado largo")]
		[Required(AllowEmptyStrings = false, ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[BindProperty]
		public string DNI { get; set; }

		[Display(Name = "Confirmar DNI")]
		[DataType(DataType.Text)]
		[StringLength(9, ErrorMessage = "DNI demasiado largo")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[Compare(nameof(DNI), ErrorMessage = "Los DNIs no coinciden")]
		[BindProperty]
		public string ConfirmacionDNI { get; set; }

		[Display(Name = "Region sanitaria")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		[BindProperty]
		public string RegionSanitaria { get; set; }

		#endregion
	}
}