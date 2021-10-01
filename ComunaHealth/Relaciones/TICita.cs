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

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMedico"/> con la <see cref="ModeloCita"/> que tenga apuntada
    /// </summary>
    public class TICitaMedico : TICita
    {
        [ForeignKey(nameof(Medico))]
        public int IdMedico { get; set; }
        public ModeloMedico Medico { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPaciente"/> con la <see cref="ModeloCita"/> que tenga reservada
    /// </summary>
    public class TICitaPaciente : TICita
    {
        [ForeignKey(nameof(Paciente))]
        public int IdPaciente { get; set; }
        public ModeloPaciente Paciente { get; set; }
    }

}
