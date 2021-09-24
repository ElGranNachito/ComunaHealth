using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Clase base para el modelo de un usuario
	/// </summary>
	public class ModeloUsuario : IdentityUser
	{
		/// <summary>
		/// DNI del usuario
		/// </summary>
		[Required]
		public int DNI { get; set; }

		/// <summary>
		/// Tipos de esta cuenta
		/// </summary>
		[Required]
		public ETipoCuenta TiposCuenta { get; set; }

		/// <summary>
		/// Estado de esta cuenta
		/// </summary>
		[Required]
		public EEstadoCuenta EstadoCuenta { get; set; }
	}
}
