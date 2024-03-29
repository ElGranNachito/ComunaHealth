﻿using ComunaHealth.Relaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace ComunaHealth.Modelos
{
    /// <summary>
    /// Modelo de datos que representa a una cita medica.
    /// </summary>
    public class ModeloCita : ModeloBase
    {
        /// <summary>
        /// Medico con el que se tiene la cita.
        /// </summary>
        [Required]
        public ModeloMedico Medico { get; set; }

        /// <summary>
        /// Paciente apuntado a la cita.
        /// </summary>
        [Required]
        public ModeloPaciente Paciente { get; set; }

        /// <summary>
        /// Especializaciones abarcadas en el motivo de la cita.
        /// </summary>
        [Required]
        public EEspecializacion EspecializacionCita { get; set; }

        /// <summary>
        /// Especializaciones abarcadas en el motivo de la cita.
        /// </summary>
        [Required]
        public EEstadoCita EstadoCita { get; set; }

        /// <summary>
        /// Una breve d0escripcion y motivo de la cita.
        /// </summary>
        [StringLength(500)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Duracion total en segundos de la cita.
        /// </summary>
        [Required]
        public int Duracion { get; set; }

        /// <summary>
        /// Fecha apuntada para la cita.
        /// </summary>
        [Required]
        public DateTimeOffset Fecha { get; set; }
        
        /// <summary>
        /// Solicitud para la postergacion de la fecha apuntada para la cita.
        /// </summary>
        public ModeloSolicitudCambioHorarioDeCita SolicitudCambioHorario { get; set; }
    }
}
