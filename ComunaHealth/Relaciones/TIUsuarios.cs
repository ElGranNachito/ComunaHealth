using ComunaHealth.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

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
    /// Representa una relacion de un <see cref="ModeloUsuarioNoAdministrador"/> con un <see cref="ModeloMensajeChat"/> que haya remitido.
    /// </summary>
    public class TIUsuarioNoAdministradorMensajeChat : TIUsuarioNoAdministrador
    {
        [ForeignKey(nameof(MensajeChat))]
        public int IdMensajeChat { get; set; }
        public ModeloMensajeChat MensajeChat { get; set; }
    }

    // Usuario medico.

    /// <summary>
    /// Representa una relacion con un <see cref="ModeloMedico"/>
    /// </summary>
    public abstract class TIMedico
    {
        [ForeignKey(nameof(Medico))]
        public int IdMedico { get; set; }
        public ModeloMedico Medico { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMedico"/> con el <see cref="ModeloPaciente"/> que tiene contacto
    /// </summary>
    public class TIMedicoPaciente : TIMedico
    {
        [ForeignKey(nameof(Paciente))]
        public int IdPaciente { get; set; }
        public ModeloPaciente Paciente { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMedico"/> con un <see cref="ModeloEntradaHistorialMedico"/> que crea.
    /// </summary>
    public class TIMedicoEntradaHistorialMedico : TIMedico
    {
        [ForeignKey(nameof(EntradaHistorialMedico))]
        public int IdEntradaHistorialMedico { get; set; }
        public ModeloEntradaHistorialMedico EntradaHistorialMedico { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMedico"/> con un <see cref="ModeloContenedorDeEntradas<ModeloEntrada>"/> que crea.
    /// </summary>
    public class TIMedicoContenedorEntrada : TIMedico
    {
        [ForeignKey(nameof(Entrada))]
        public int IdEntrada { get; set; }
        public ModeloContenedorDeEntradas<ModeloEntrada> Entrada { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloMedico"/> con un <see cref="ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico>"/> que crea para un paciente.
    /// </summary>
    public class TIMedicoContenedorEntradaHistorialMedico : TIMedico
    {
        [ForeignKey(nameof(EntradaHistorialMedico))]
        public int IdEntradaHistorialMedico { get; set; }
        public ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico> EntradaHistorialMedico { get; set; }
    }

    // Usuario paciente.

    /// <summary>
    /// Representa una relacion con un <see cref="ModeloPaciente"/>
    /// </summary>
    public abstract class TIPaciente
    {
        [ForeignKey(nameof(Paciente))]
        public int IdPaciente { get; set; }
        public ModeloPaciente Paciente { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPaciente"/> con un <see cref="ModeloEntradaHistorialMedico"/> que crea.
    /// </summary>
    public class TIPacienteEntradaHistorialMedico : TIPaciente
    {
        [ForeignKey(nameof(EntradaHistorialMedico))]
        public int IdEntradaHistorialMedico { get; set; }
        public ModeloEntradaHistorialMedico EntradaHistorialMedico { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPaciente"/> con un <see cref="ModeloContenedorDeEntradas<ModeloEntrada>"/> que crea.
    /// </summary>
    public class TIPacienteContenedorEntrada : TIPaciente
    {
        [ForeignKey(nameof(Entrada))]
        public int IdEntrada { get; set; }
        public ModeloContenedorDeEntradas<ModeloEntrada> Entrada { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloPaciente"/> con el <see cref="ModeloEntradaHistorialMedico"/> que se le cree por un medico.
    /// </summary>
    public class TIPacienteContenedorEntradaHistorialMedico : TIPaciente
    {
        [ForeignKey(nameof(EntradaHistorialMedico))]
        public int IdEntradaHistorialMedico { get; set; }
        public ModeloContenedorDeEntradas<ModeloEntradaHistorialMedico> EntradaHistorialMedico { get; set; }
    }
}
