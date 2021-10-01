using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo para un rol de comuna health
	/// </summary>
	public class ModeloRol : IdentityRole<int>
	{
		/// <summary>
		/// Tipo de cuenta representado por este rol
		/// </summary>
		public ETipoCuenta EspecializacionRepresentada { get; set; }

		/// <summary>
		/// Constructor por defecto
		/// </summary>
		public ModeloRol(){}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="nombre">Nombre del rol</param>
		/// <param name="especializacion">Tipo de cuenta representado por este rol</param>
		public ModeloRol(string nombre, ETipoCuenta tipoCienta)
		{
			Name                        = nombre;
			EspecializacionRepresentada = tipoCienta;
		}
	}
}
