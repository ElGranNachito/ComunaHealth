using System.ComponentModel.DataAnnotations.Schema;
using ComunaHealth.Modelos;

namespace ComunaHealth.Relaciones
{
    /// <summary>
    /// Representa una relacion con un <see cref="ModeloContenedorDeEntradas"/>
    /// </summary>
    public abstract class TIContenedorDeEntradas
    {
        [ForeignKey(nameof(ContenedorDeEntradas))]
        public int IdContenedorDeEntradas { get; set; }
        public ModeloContenedorDeEntradas ContenedorDeEntradas { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloContenedorDeEntradas"/> con el <see cref="TEntrada"/> que contenga.
    /// </summary>
    public class TIContenedorDeEntradasEntrada<TEntrada>
    {
        [ForeignKey(nameof(ContenedorDeEntradas))]
        public int IdContenedorDeEntradas { get; set; }
        public ModeloContenedorDeEntradas<TEntrada> ContenedorDeEntradas { get; set; }

        [ForeignKey(nameof(Entrada))]
        public int IdEntrada { get; set; }
        public TEntrada Entrada { get; set; }
    }


    // Chat:


    /// <summary>
    /// Representa una relacion con un <see cref="ModeloChat"/>
    /// </summary>
    public abstract class TIChat
    {
        [ForeignKey(nameof(Chat))]
        public int IdChat { get; set; }
        public ModeloChat Chat { get; set; }
    }

    /// <summary>
    /// Representa una relacion de un <see cref="ModeloChat"/> con el <see cref="ModeloUsuario"/> que participa del mismo.
    /// </summary>
    public class TIChatUsuario : TIChat
    {
        [ForeignKey(nameof(Usuario))]
        public string IdUsuario { get; set; }
        public ModeloUsuario Usuario { get; set; }
    }
}