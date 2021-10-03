using System.ComponentModel.DataAnnotations;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo que representa a un usuario administrador
	/// </summary>
	public class ModeloAdministrador : ModeloUsuario
	{
		[Required(ErrorMessage = "Region sanitaria no puede ser nula")]
		public ERegionSanitariaBSAS RegionSanitaria { get; set; }
	}
}