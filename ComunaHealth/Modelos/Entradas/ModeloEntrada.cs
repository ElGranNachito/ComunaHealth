using System;
using System.ComponentModel.DataAnnotations;
using ComunaHealth.Relaciones;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo que representa a una entrada de datos.
	/// </summary>
	public class ModeloEntrada : ModeloBase
	{
		/// <summary>
		/// Contenido dentro de la entrada.
		/// </summary>
		[StringLength(2000)]
		public string Contenido { get; set; }
	
		/// <summary>
		/// Fecha de creacion de la entrada.
		/// </summary>
		[Required]
		public DateTimeOffset FechaDeCreacion { get; set; }
	}

	/// <summary>
	/// Modelo que representa a un reporte de usuario.
	/// </summary>
	public class ModeloReporte : ModeloEntrada
    {
		/// <summary>
		/// Estado en el que se encuentra el reporte.
		/// </summary>
		[Required]
		public EEstadoReporte EstadoReporte { get; set; }
    }

	/// <summary>
	/// Modelo que representa el mensaje de un chat entre usuarios.
	/// </summary>
	public class ModeloMensajeChat : ModeloEntrada
    {
		/// <summary>
		/// Usuario no administrador remitente del mensaje.
		/// </summary>
		[Required]
		public ModeloUsuario Remitente { get; set; }
    }

	/// <summary>
	/// Modelo que representa el historial medico de un paciente para un medico especifico.
	/// </summary>
	public class ModeloEntradaHistorialMedico : ModeloEntrada
	{
		/// <summary>
		/// Medico que crea el historial medico del paciente para cierta especialidad.
		/// </summary>
		public ModeloMedico MedicoCreador { get; set; }

		/// <summary>
		/// Paciente del que se trata el historial medico.
		/// </summary>
		public ModeloPaciente Paciente { get; set; }

		/// <summary>
		/// Especializacion medica que abarga el historial.
		/// </summary>
		[Required]
		public EEspecializacion Especializacion { get; set; }
	}

	/// <summary>
	/// Modelo que representa un log de un administrador.
	/// </summary>
	public class ModeloLogAdministrador : ModeloEntrada
    {
		/// <summary>
		/// Administrador que realizo el log.
		/// </summary>
		[Required]
		public ModeloAdministrador Administrador { get; set; }
	}
}