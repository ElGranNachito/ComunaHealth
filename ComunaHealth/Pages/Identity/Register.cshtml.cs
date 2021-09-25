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
using Microsoft.AspNetCore.Mvc.Rendering;

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

		[BindProperty]
		public ViewModelRegistro Registro { get; init; } = new ViewModelRegistro
		{
			TiposDeCuentaDisponibles =  EnumHelpers.ToSelectItemList(EnumHelpers.ObtenerValores<ETipoCuenta>(ETipoCuenta.Administrador | ETipoCuenta.AdministradorJefe))
		};

	    public RegisterModel(ComunaDbContext dbcontext, UserManager<ModeloUsuario> userManager)
	    {
		    _dbcontext   = dbcontext;
		    _userManager = userManager;
	    }

	    public void OnGet()
        {
        }

	    public async Task<IActionResult> OnPost()
	    {
		    return await Task.FromResult(Page());
	    }
	}

	#region ViewModel Registro

	public class ViewModelRegistro
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
		public string Mail { get; set; }

		[Display(Name = "Telefono")]
		[DataType(DataType.Text)]
		[StringLength(100, ErrorMessage = "Telefono demasiado largo")]
		public string Telefono { get; set; }

		[Display(Name = "Contraseña")]
		[DataType(DataType.Password)]
		[StringLength(50, ErrorMessage = "Contraseña demasiado larga")]
		[PasswordPropertyText]
		public string Contraseña { get; set; }

		[Display(Name = "Confirmar contraseña")]
		[DataType(DataType.Password)]
		[StringLength(50, ErrorMessage = "Contraseña demasiado larga")]
		[Compare(nameof(Contraseña), ErrorMessage = "Las contraseñas no coinciden")]
		[PasswordPropertyText]
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
		public ETipoCuenta TipoCuenta { get; set; }

		public List<SelectListItem> TiposDeCuentaDisponibles { get; init; }
	} 

	#endregion
}
