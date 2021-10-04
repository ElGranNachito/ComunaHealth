

using System.Collections.Generic;
using ComunaHealth.Relaciones;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo que representa a un usuario de tipo paciente
	/// </summary>
	public class ModeloPaciente : ModeloUsuarioNoAdministrador
    {
        /// <summary>
        /// Diario de experiencias del paciente.
        /// </summary>
        public List<TIPacienteContenedorEntrada> Diario { get; set; } = new List<TIPacienteContenedorEntrada>(); 

        /// <summary>
        /// Notas sobre cambios en el estilo de vida del paciente.
        /// </summary>
        public List<TIPacienteContenedorEntradaCambiosEstiloVida> CambiosEstiloDeVida { get; set; } = new List<TIPacienteContenedorEntradaCambiosEstiloVida>(); 

		/// <summary>
		/// Historial medico del paciente.
		/// </summary>
        public List<TIPacienteContenedorEntradaHistorialMedico> HistorialMedico { get; set; } = new List<TIPacienteContenedorEntradaHistorialMedico>(); 

        /// <summary>
		/// Medicos añadidos por el paciente.
		/// </summary>
        public List<ModeloMedico> Medicos { get; set; } = new List<ModeloMedico>();

		/// <summary>
		/// Citas con sus medicos.
		/// </summary>
        public List<ModeloCita> Citas { get; set; } = new List<ModeloCita>();
    }
}
