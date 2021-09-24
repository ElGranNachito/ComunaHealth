using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo que representa a un usuario no administrador
	/// </summary>
	public abstract class ModeloUsuarioNoAdministrador : ModeloUsuario
	{
		/// <summary>
		/// Descripcion que aparecera en el perfil publico del usuario
		/// </summary>
		[StringLength(500)]
		[PersonalData]
		public string Descripcion { get; set; }

		/// <summary>
		/// Indica si el mail de este usuario debe mostrarse como informacion publica
		/// </summary>
		[Required]
		public bool MailEsPublico { get; set; }

		/// <summary>
		/// Indica si el telefono de este usuario debe mostrarse como informacion publica
		/// </summary>
		public bool? TelefonoEsPublico { get; set; }

	}
}
