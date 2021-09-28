using System;
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
		public string Contenido { get; set; }
	
		/// <summary>
		/// Fecha de creacion de la entrada.
		/// </summary>
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
		public TIUsuarioNoAdministradorMensajeChat Remitente { get; set; }
    }

	/// <summary>
	/// Modelo que representa el historial medico de un paciente para un medico especifico.
	/// </summary>
	public class ModeloEntradaHistorialMedico : ModeloEntrada
	{
		/// <summary>
		/// Medico que crea el historial medico del paciente para cierta especialidad.
		/// </summary>
		public TIMedicoEntradaHistorialMedico MedicoCreador { get; set; }

		/// <summary>
		/// Paciente del que se trata el historial medico.
		/// </summary>
		public TIPacienteEntradaHistorialMedico Paciente { get; set; }

		/// <summary>
		/// Especializacion medica que abarga el historial.
		/// </summary>
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
		public TIAdministradorLogAdministrador Administrador { get; set; }
	}
}