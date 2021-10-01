using System.ComponentModel.DataAnnotations.Schema;
using ComunaHealth.Modelos;

namespace ComunaHealth.Relaciones
{
    /// <summary>
    /// Representa una relacion con un <see cref="ModeloAdministrador"/>
    /// </summary>
    public abstract class TIAdministrador
    {
        [ForeignKey(nameof(Administrador))]
        public string IdAdministrador { get; set; }
        public ModeloAdministrador Administrador { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloAdministrador"/> con un <see cref="ModeloLogAdministrador"/> que revisa.
    /// </summary>
    public class TIAdministradorLogAdministrador : TIAdministrador
    {
        [ForeignKey(nameof(LogAdministrador))]
        public int IdLogAdministrador { get; set; }
        public ModeloLogAdministrador LogAdministrador { get; set; }
    }
}
