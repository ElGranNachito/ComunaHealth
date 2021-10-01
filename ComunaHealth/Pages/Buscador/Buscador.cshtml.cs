using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
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
	[Authorize]
	public class BuscadorModel : PageModel
	{
		private readonly ComunaDbContext _dbcontext;
		private readonly UserManager<ModeloUsuario> _userManager;
		private readonly SignInManager<ModeloUsuario> _signInManager;
		private readonly IConfiguration _config;

		[BindProperty]
		public string Nombre { get; set; }

		[BindProperty]
		public string DNI { get; set; }

		[BindProperty]
		public EMunicipio Municipio { get; set; }

		[BindProperty]
		public string[] Especializaciones { get; set; }

		public List<ModeloMedico> MedicosEncontrados { get; set; } = new List<ModeloMedico>();
		public List<ModeloPaciente> PacientesEncontrados { get; set; } = new List<ModeloPaciente>();

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

		/// <summary>
		/// Metodo que obtiene todos los medicos que cumplen con los parametros especificados
		/// </summary>
		/// <returns></returns>
		[ValidateAntiForgeryToken]
		[Authorize(Roles = $"{Constantes.NombreRolPaciente}, {Constantes.NombreRolAdministrador}"]
		public async Task<IActionResult> OnPostBuscarMedicos()
		{
			MedicosEncontrados?.Clear();

			//Consulta apra obtener los medicos que cumplen con los parametros especificados
			IQueryable<ModeloMedico> consultaMedicos = from medico in _dbcontext.Medicos select medico;

			//Si el usuario ingreso un DNI entonces aplicamos filtro por DNI
			if(!string.IsNullOrWhiteSpace(DNI))
			{
				consultaMedicos = consultaMedicos.Where(medico => medico.DNI == int.Parse(DNI));
			}
			
			//Si el usuario ingreso un nombre entonces aplicamos filtro por nombre
			if(!string.IsNullOrWhiteSpace(Nombre))
			{
				consultaMedicos = consultaMedicos.Where(medico => medico.UserName == Nombre);
			}

			//Si el usuario ingreso un municipio entonces aplicamos filtro por municipio
			if(Municipio != EMunicipio.NINGUNO)
			{
				consultaMedicos = consultaMedicos.Where(medico => medico.Municipio == Municipio);
			}

			//Si el usuario ingreso especializaciones entonces aplicamos filtro por especializaciones
			if(Especializaciones != null && Especializaciones.Length > 0)
			{
				consultaMedicos = consultaMedicos.Where(medico => medico.StringEspecializaciones == string.Join(", ", Especializaciones));
			}
			
			//Ejecutamos la consulta
			MedicosEncontrados = await consultaMedicos.ToListAsync();

			return Page();
		}

		[ValidateAntiForgeryToken]
		[Authorize(Roles = $"{Constantes.NombreRolMedico}, {Constantes.NombreRolAdministrador}")]
		public async Task<IActionResult> OnPostBuscarPacientes()
		{
			PacientesEncontrados?.Clear();

			var usuario = await _userManager.GetUserAsync(User);

			//Consulta apra obtener los medicos que cumplen con los parametros especificados
			IQueryable<ModeloPaciente> consultaPaciente = from medico in _dbcontext.Pacientes select medico;

			//Si el usuario ingreso un DNI entonces aplicamos filtro por DNI
			if (!string.IsNullOrWhiteSpace(DNI))
			{
				consultaPaciente = consultaPaciente.Where(paciente => paciente.DNI == int.Parse(DNI));
			}

			//Si el usuario ingreso un nombre entonces aplicamos filtro por nombre
			if (!string.IsNullOrWhiteSpace(Nombre))
			{
				consultaPaciente = consultaPaciente.Where(paciente => paciente.UserName == Nombre);
			}

			PacientesEncontrados = await consultaPaciente.ToListAsync();

			return Page();
		}

		public async Task<IActionResult> OnPostComenzarChat()
		{
			var idUsuario = Request.Form["ID"];
			
			return Page();
		}
	}
}