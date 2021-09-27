using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		/// <summary>
		/// Arreglo de bytes que contiene la foto de perfil del usuario
		/// </summary>
		[Column(TypeName = "image")]
		public byte[] FotoDePerfil { get; set; }

		/// <summary>
		/// Arreglo de bytes que contiene el anverso del DNI del usuario
		/// </summary>
		[Required]
		[Column(TypeName = "image")]
		public byte[] FotoAnversoDNI { get; set; }

		/// <summary>
		/// Arreglo de bytes que contiene el reverso del DNI del usuario
		/// </summary>
		[Required]
		[Column(TypeName = "image")]
		public byte[] FotoReversoDNI { get; set; }
	}
}
