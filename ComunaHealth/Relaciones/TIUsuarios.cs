using System.ComponentModel.DataAnnotations.Schema;
using ComunaHealth.Modelos;

namespace ComunaHealth.Relaciones
{
    /// <summary>
    /// Representa una relacion con un <see cref="ModeloUsuarioNoAdministrador"/>
    /// </summary>
    public abstract class TIUsuarioNoAdministrador
    {
        [ForeignKey(nameof(UsuarioNoAdministrador))]
        public int IdUsuarioNoAdministrador { get; set; }
        public ModeloUsuarioNoAdministrador UsuarioNoAdministrador { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloUsuarioNoAdministrador"/> con la <see cref="ModeloCita"/> que tenga reservada
    /// </summary>
    public class TIUsuarioNoAdministradorCita : TIUsuarioNoAdministrador
    {
        [ForeignKey(nameof(Cita))]
        public int IdCita { get; set; }
        public ModeloCita Cita { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloUsuarioNoAdministrador"/> con la <see cref="ModeloSolicitudPostergacionDeCita"/> que tenga pendiente
    /// </summary>
    public class TIUsuarioNoAdministradorSolicitudPostergacionCita : TIUsuarioNoAdministrador
    {
        [ForeignKey(nameof(SolicitudPostergacionCita))]
        public int IdSolicitudPostergacionCita { get; set; }
        public ModeloSolicitudPostergacionDeCita SolicitudPostergacionCita { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMedico"/> con el <see cref="ModeloPaciente"/> que tiene contacto
    /// </summary>
    public class TIMedicoPaciente
    {
        [ForeignKey(nameof(Medico))]
        public int IdMedico { get; set; }
        public ModeloMedico Medico { get; set; }

        [ForeignKey(nameof(Paciente))]
        public int IdPaciente { get; set; }
        public ModeloPaciente Paciente { get; set; }
    }
}
