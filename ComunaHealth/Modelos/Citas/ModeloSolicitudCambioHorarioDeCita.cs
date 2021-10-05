using ComunaHealth.Relaciones;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComunaHealth.Modelos
{
    /// <summary>
    /// Modelo de datos que representa a la solicitud de postergacion de una cita medica.
    /// </summary>
    public class ModeloSolicitudCambioHorarioDeCita : ModeloBase
    {
        [ForeignKey(nameof(Cita))]
        public int IdCita { get; set; }

        /// <summary>
        /// Usuario no administrador que solicita para postergacion de la cita.
        /// </summary>
        [Required]
        public ModeloUsuarioNoAdministrador Solicitante { get; set; }

        /// <summary>
        /// Cita que se pretende postergar.
        /// </summary>
        [Required]
        public ModeloCita Cita { get; set; }

        /// <summary>
        /// Razon de la solicitud.
        /// </summary>
        [StringLength(500)]
        public string Razon { get; set; }

        /// <summary>
        /// Nueva duracion total en segundos propuesta para la cita.
        /// </summary>
        [Required]
        public int NuevaDuracion { get; set; }

        /// <summary>
        /// Nueva fecha propuesta para la cita.
        /// </summary>
        [Required]
        public DateTimeOffset NuevaFecha { get; set; }
    }
}
