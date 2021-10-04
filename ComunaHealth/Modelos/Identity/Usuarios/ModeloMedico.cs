using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ComunaHealth.Relaciones;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo que representa a un usuario de tipo medico
	/// </summary>
	public class ModeloMedico : ModeloUsuarioNoAdministrador
	{
		/// <summary>
		/// Especializacion sanitaria del medico.
		/// </summary>
		[NotMapped]
		public List<EEspecializacion> Especializaciones { get; private set; } = new List<EEspecializacion>();

        /// <summary>
        /// Notas de el medico.
        /// </summary>
        public List<TIMedicoContenedorEntrada> Notas { get; set; } = new List<TIMedicoContenedorEntrada>();

        /// <summary>
        /// Historial medico del paciente.
        /// </summary>+
        public List<TIMedicoContenedorEntradaHistorialMedico> NotasPacientes { get; set; } = new List<TIMedicoContenedorEntradaHistorialMedico>();

        /// <summary>
		/// Pacientes del medico.
		/// </summary>
        public List<ModeloPaciente> Pacientes { get; set; } = new List<ModeloPaciente>();

		/// <summary>
		/// Citas con sus pacientes.
		/// </summary>
        public List<ModeloCita> Citas { get; set; } = new List<ModeloCita>();

		/// <summary>
		/// Especializaciones del medico
		/// </summary>
		[Required]
		[Column("Especializaciones")]
		public string StringEspecializaciones
		{
			get => EnumHelpers.ListaValoresEnumACadena(Especializaciones);
			set => Especializaciones = EnumHelpers.CadenadaAListaEnums<EEspecializacion>(value);
		}

		/// <summary>
		/// Matricula del medico
		/// </summary>
		[Required]
		public string Matricula { get; set; }
	}
}
