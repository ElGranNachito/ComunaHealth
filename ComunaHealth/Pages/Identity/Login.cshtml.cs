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
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Authentication;
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
    public class LoginModel : PageModel
    {
	    private readonly ComunaDbContext _dbcontext;
	    private readonly UserManager<ModeloUsuario> _userManager;
		private readonly SignInManager<ModeloUsuario> _signInManager;
	    private readonly IConfiguration _config;

		[BindProperty]
		public ViewModelLogin ViewModelLogin { get; set; } = new ViewModelLogin();

		public LoginModel(ComunaDbContext dbcontext, UserManager<ModeloUsuario> userManager, SignInManager<ModeloUsuario> signInManager, IConfiguration config)
	    {
		    _dbcontext   = dbcontext;
		    _userManager = userManager;
		    _config      = config;
			_signInManager = signInManager;
	    }

	    public void OnGet()
        {

        }

		[ValidateAntiForgeryToken]
	    public async Task<IActionResult> OnPost()
	    {
			if(!ModelState.IsValid)
				return await Task.FromResult(Page());

			var resultado = (await Task.Run(() =>
			{
				return _dbcontext.Users.Where(u => u.Email == ViewModelLogin.EMail);
			})).FirstOrDefault();

			if (resultado == null)
            {
				ModelState.AddModelError("ViewModelLogin.EMail", "Mail o usuario incorrecto");

				return Page();
			}

			if(_userManager.PasswordHasher.VerifyHashedPassword(resultado, resultado.PasswordHash, ViewModelLogin.Contraseña) != PasswordVerificationResult.Success) 
			{
				ModelState.AddModelError("ViewModelLogin.EMail", "Mail o usuario incorrecto");

				return Page();
			}

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, resultado.Email),
				new Claim(ClaimTypes.Name, resultado.UserName),
				new Claim("DNI", resultado.DNI.ToString()),
				new Claim(ClaimTypes.Role, resultado.TiposCuenta == ETipoCuenta.Paciente ? Constantes.NombreRolPaciente : Constantes.NombreRolMedico)
			};

			var identity = new ClaimsIdentity(claims);

			await _userManager.AddClaimsAsync(resultado, claims);
			await _signInManager.SignInAsync(resultado, new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow + TimeSpan.FromMinutes(30) });
			await _signInManager.CreateUserPrincipalAsync(resultado);

			return RedirectToPage("/Index");
		}
    }

	#region ViewModelLogin

	public class ViewModelLogin
    {
		[Display(Name = "Mail")]
		[StringLength(320)]
		[EmailAddress(ErrorMessage = "Este campo debe ser una direccion de mail")]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string EMail { get; set; }

		[StringLength(50)]
		[PasswordPropertyText]
		[Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
		public string Contraseña { get; set; }
    }

	#endregion
}