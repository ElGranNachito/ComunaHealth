using System.ComponentModel.DataAnnotations.Schema;
using ComunaHealth.Modelos;

namespace ComunaHealth.Relaciones
{
    /// <summary>
    /// Representa una relacion con un <see cref="<see cref="ModeloCita"/>
    /// </summary>
    public abstract class TICita
    {
        [ForeignKey(nameof(Cita))]
        public int IdCita { get; set; }
        public ModeloCita Cita { get; set; }
    }

    /// <summary>
    /// Representa una relacion de una <see cref="ModeloSolicitudCambioHorarioDeCita"/> con la <see cref="ModeloCita"/> a postergar.
    /// </summary>
    public class TICitaSolicitudCambioHorarioCita : TICita
    {
        [ForeignKey(nameof(SolicitudCambioHorarioDeCita))]
        public int IdSolicitudCambioHorarioDeCita { get; set; }
        public ModeloSolicitudCambioHorarioDeCita SolicitudCambioHorarioDeCita { get; set; }
    }

}
