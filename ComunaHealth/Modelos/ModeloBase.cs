using System.ComponentModel.DataAnnotations;

namespace ComunaHealth.Modelos
{
    /// <summary>
    /// Clase base de todos los modelos con una key
    /// </summary>
    public class ModeloBase
    {   
        //Id
        [Key]
        public int Id { get; set; }
    }
}
