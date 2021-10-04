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
using ComunaHealth.Helpers;
using ComunaHealth.Modelos;
using ComunaHealth.Modelos.Identity.Usuarios;
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

		/// <summary>
		/// Tipo del buscador. Se utiliza para diferenciar el tipo de buscador que desea
		/// ver el administrador jefe
		/// </summary>
		public string? TipoBuscador { get; set; }

		/// <summary>
		/// Nombre del usuario buscado
		/// </summary>
		[BindProperty]
		public string Nombre { get; set; }

		/// <summary>
		/// DNI del usuario buscado
		/// </summary>
		[BindProperty]
		public string DNI { get; set; }

		/// <summary>
		/// Municipio donde reside el usuario buscado
		/// </summary>
		[BindProperty]
		public EMunicipio Municipio { get; set; }

		/// <summary>
		/// Especializaciones del usuario (medico) buscado
		/// </summary>
		[BindProperty]
		public string[] Especializaciones { get; set; }

		/// <summary>
		/// Especializaciones del usuario (medico) buscado
		/// </summary>
		[BindProperty]
		[DisplayName("Regiones sanitarias")]
		public string[] RegionesSanitarias { get; set; }

		/// <summary>
		/// <see cref="ModeloMedico"/> encontrados que coinciden con los criterios ingresados
		/// </summary>
		public List<ModeloMedico> MedicosEncontrados { get; set; } = new List<ModeloMedico>();

		/// <summary>
		/// <see cref="ModeloMedico"/> encontrados que coinciden con los criterios ingresados
		/// </summary>
		public List<ModeloAdministrador> AdministradoresEncontrados { get; set; } = new List<ModeloAdministrador>();

		/// <summary>
		/// <see cref="ModeloPaciente"/> encontrados que coinciden con los criterios ingresados
		/// </summary>
		public List<ModeloPaciente> PacientesEncontrados { get; set; } = new List<ModeloPaciente>();

		public BuscadorModel(ComunaDbContext dbcontext, UserManager<ModeloUsuario> userManager, SignInManager<ModeloUsuario> signInManager, IConfiguration config)
		{
			_dbcontext = dbcontext;
			_userManager = userManager;
			_config = config;
			_signInManager = signInManager;
		}

		public void OnGet(string? tipoBuscador)
		{
			//Si no nos especificaron el tipo de buscador entonces colocamos el por defecto
			if (tipoBuscador is null)
			{
				TipoBuscador = Constantes.NombreBuscadorDefault;

				return;
			}

			//Si nos lo especificaron lo establecemos
			TipoBuscador = tipoBuscador;
		}

		/// <summary>
		/// Metodo que obtiene todos los medicos que cumplen con los parametros especificados
		/// </summary>
		/// <returns></returns>
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Constantes.NombreRolPaciente + ", " + Constantes.NombreRolAdministrador)]
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

			return Partial("_BuscadorMedicos", this);
		}

		[ValidateAntiForgeryToken]
		[Authorize(Roles = Constantes.NombreRolMedico + ", " + Constantes.NombreRolAdministrador)]
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

			return Partial("_BuscadorPacientes", this);
		}

		[ValidateAntiForgeryToken]
		[Authorize(Roles = Constantes.NombreRolAdministradorjefe)]
		public async Task<IActionResult> OnPostBuscarAdministradores()
		{
			AdministradoresEncontrados?.Clear();

			//Consulta apra obtener los medicos que cumplen con los parametros especificados
			IQueryable<ModeloAdministrador> consultaAdministradores = 
				from administrador in _dbcontext.Administradores 
				where !administrador.StringTiposCuenta.Contains(ETipoCuenta.AdministradorJefe.ToString()) 
				select administrador;

			//Si el usuario ingreso un DNI entonces aplicamos filtro por DNI
			if (!string.IsNullOrWhiteSpace(DNI))
			{
				consultaAdministradores = consultaAdministradores.Where(administrador => administrador.DNI == int.Parse(DNI));
			}

			//Si el usuario ingreso un nombre entonces aplicamos filtro por nombre
			if (!string.IsNullOrWhiteSpace(Nombre))
			{
				consultaAdministradores = consultaAdministradores.Where(administrador => administrador.UserName == Nombre);
			}

			var valoresRegionesBSAS = RegionesSanitarias.Select(r => Enum.Parse<ERegionSanitariaBSAS>(r)).ToArray();

			if (valoresRegionesBSAS.Length != 0)
			{
				consultaAdministradores = consultaAdministradores.Where(administrador => valoresRegionesBSAS.Contains(administrador.RegionSanitaria));
			}

			AdministradoresEncontrados = await consultaAdministradores.ToListAsync();

			return Partial("_BuscadorAdministradores", this);
		}

		[ValidateAntiForgeryToken]
		[Authorize(Roles = Constantes.NombreRolAdministradorjefe)]
		public async Task<IActionResult> OnPostEliminarAdministrador()
		{
			if (!int.TryParse(Request.Form["idModelo"], out int idParseada))
				return new JsonResult("Algo salio mal");

			//Buscamos el administrador con la id especificada y nos aseguramos de que no sea el administrador jefe
			var administradorEncontrado = await _dbcontext.Administradores.FirstOrDefaultAsync(a =>
				a.Id == idParseada && !a.StringTiposCuenta.Contains(ETipoCuenta.AdministradorJefe.ToString()));

			if (administradorEncontrado == null)
				return new JsonResult("Algo salio mal");
			
			//Deshabilitamos la cuenta
			administradorEncontrado.EstadoCuenta = EEstadoCuenta.Deshabilitada;

			//Guardamos los datos
			await _dbcontext.SaveChangesAsync();

			return Page();
		}

		[ValidateAntiForgeryToken]
		[Authorize(Roles = Constantes.NombreRolAdministradorjefe)]
		public async Task<IActionResult> OnPostRehabilitarAdministrador()
		{
			if (!int.TryParse(Request.Form["idModelo"], out int idParseada))
				return new JsonResult("Algo salio mal");

			var administradorEncontrado = await _dbcontext.Administradores.FirstOrDefaultAsync(a => a.Id == idParseada);

			if (administradorEncontrado == null)
				return new JsonResult("Algo salio mal");

			administradorEncontrado.EstadoCuenta = EEstadoCuenta.Habilitada;

			//Guardamos los datos
			await _dbcontext.SaveChangesAsync();

			return Page();
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostComenzarChat()
		{
			if (!int.TryParse(Request.Form["ID"], out int idParseada))
				return new JsonResult("Algo salio mal");

			var usuarioActual = await _userManager.GetUserAsync(User);

			if(usuarioActual == null)
				return new JsonResult("Algo salio mal");

			if(usuarioActual.Chats.Any(m => m.Participantes.Any(p => p.Id == idParseada)))
				return RedirectToPage("/Chat/Chat");

			var otroUsuario = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == idParseada);

			if (otroUsuario == null)
				return new JsonResult("Algo salio mal");

			var nuevoChat = new ModeloChat
			{
				Participantes = new List<ModeloUsuario>
				{
					usuarioActual,
					otroUsuario
				},
				
				//Generamos la clave de encriptado
				ClaveEncriptado = CryptoHelpers.GenerarStringAleatorio(Constantes.LongitudKeyEncriptadoEntradas)
			};

			usuarioActual.Chats.Add(nuevoChat);
			otroUsuario.Chats.Add(nuevoChat);

			await _dbcontext.AddAsync(nuevoChat);
			await _dbcontext.SaveChangesAsync();

			return RedirectToPage("/Chat/Chat");
		}
	}
}