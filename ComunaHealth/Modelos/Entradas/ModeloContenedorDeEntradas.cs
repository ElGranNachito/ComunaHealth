using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using ComunaHealth.Relaciones;

namespace ComunaHealth.Modelos
{
    /// <summary>
    /// Modelo de datos que representa a un contenedor de entradas.
    /// </summary>
    public abstract class ModeloContenedorDeEntradas : ModeloBase
    {
	    /// <summary>
	    /// Indica si el contenedor puede ser modificado.
	    /// </summary>
	    public bool PuedeSerModificado { get; set; } = true;

        /// <summary>
        /// Clave para desencriptar el contenedor con sus entradas.
        /// </summary>
        [Required]
        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string ClaveEncriptado { get; set; }
    }

    /// <summary>
    /// Modelo de datos que representa a un contenedor de entradas.
    /// </summary>
    public class ModeloContenedorDeEntradas<TIEntrada> : ModeloContenedorDeEntradas
		where TIEntrada: ModeloEntrada
    {
        /// <summary>
        /// Entradas contenidas.
        /// </summary>
        [Required]
        public virtual List<TIEntrada> Entradas { get; set; } = new List<TIEntrada>();
    }

    /// <summary>
    /// Modelo de datos que representa a un contenedor de entradas.
    /// </summary>
    public class ModeloChat : ModeloContenedorDeEntradas<ModeloMensajeChat>
    {
        /// <summary>
        /// Guid del chat
        /// </summary>
        [Required]
        [StringLength(255)]
        public string GuidChat { get; set; }

        /// <summary>
        /// Usuarios que participan de este chat
        /// </summary>
        [Required]
        public virtual List<ModeloUsuario> Participantes { get; set; } = new List<ModeloUsuario>();

        public ModeloChat()
        {
	        GuidChat = Guid.NewGuid().ToString();
        }
    }
}