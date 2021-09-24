using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
		public ViewModelRegistro Registro { get; set; }

	    public RegisterModel(ComunaDbContext dbcontext, UserManager<ModeloUsuario> userManager)
	    {
		    _dbcontext   = dbcontext;
		    _userManager = userManager;
	    }

	    public async Task<IActionResult> OnPost()
	    {
		    return await Task.FromResult(Page());
	    }

        public void OnGet()
        {
        }
    }

	public class ViewModelRegistro
	{
		[Display(Name = "Nombres")]
		[DataType(DataType.Text)]
		[StringLength(50, ErrorMessage = "Nombre demasiado largo")]
		public string Nombre { get; set; }

		[Display(Name = "Apellidos")]
		[DataType(DataType.Text)]
		[StringLength(50, ErrorMessage = "Apellido demasiado largo")]
		public string Apellido { get; set; }

		public string TipoCuenta { get; set; }
	}
}
