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

namespace ComunaHealth.Pages
{
	/// <summary>
	/// Modelo de la pagina encargada de lidiar con el registro de usuarios
	/// </summary>
	[AllowAnonymous]
	public class BuscadorModel : PageModel
	{
		private readonly ComunaDbContext _dbcontext;
		private readonly UserManager<ModeloUsuario> _userManager;
		private readonly SignInManager<ModeloUsuario> _signInManager;
		private readonly IConfiguration _config;

		public BuscadorModel(ComunaDbContext dbcontext, UserManager<ModeloUsuario> userManager, SignInManager<ModeloUsuario> signInManager, IConfiguration config)
		{
			_dbcontext = dbcontext;
			_userManager = userManager;
			_config = config;
			_signInManager = signInManager;
		}

		public void OnGet()
		{

		}

		public async Task<IActionResult> BuscarMedicos(
			[FromQuery(Name = "nombre")] string nombre, 
			[FromQuery(Name = "dni")]string dni, 
			[FromQuery(Name = "profesion")] string profesion,
			[FromQuery(Name = "municipio")] string municipio)
		{
			return Page();
		}

	}
}