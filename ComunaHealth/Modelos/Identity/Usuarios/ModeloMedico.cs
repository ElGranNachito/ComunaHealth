using System.ComponentModel.DataAnnotations;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo que representa a un usuario de tipo medico
	/// </summary>
	public class ModeloMedico : ModeloUsuarioNoAdministrador
	{
		/// <summary>
		/// Especializaciones del medico
		/// </summary>
		[Required]
		public EEspecializacion Especializaciones { get; set; }
	}
}
