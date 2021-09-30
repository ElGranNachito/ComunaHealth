using System.Collections.Generic;
using ComunaHealth.Relaciones;

namespace ComunaHealth.Modelos
{
    /// <summary>
    /// Modelo de datos que representa a un contenedor de entradas.
    /// </summary>
    public class ModeloContenedorDeEntradas : ModeloBase
    {
        /// <summary>
        /// Indica si el contenedor puede ser modificado.
        /// </summary>
        public bool PuedeSerModificado { get; set; }

        /// <summary>
        /// Clave para desencriptar el contenedor con sus entradas.
        /// </summary>
        public string ClaveEncriptado { get; set; }
        
        /// <summary>
        /// Entradas contenidas.
        /// </summary>
        public List<TIContenedorDeEntradasEntrada> Entradas { get; set; }
    }

    /// <summary>
    /// Modelo de datos que representa a un contenedor de entradas.
    /// </summary>
    public class ModeloChat : ModeloContenedorDeEntradas
    {
        /// <summary>
        /// Entradas contenidas.
        /// </summary>
        public List<TIChatUsuario> Participantes { get; set; }
    }
}