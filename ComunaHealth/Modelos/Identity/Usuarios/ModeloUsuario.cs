using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Clase base para el modelo de un usuario
	/// </summary>
	public class ModeloUsuario : IdentityUser<int>
	{
		[NotMapped]
		public List<ETipoCuenta> TiposCuenta { get; private set; }

		/// <summary>
		/// DNI del usuario
		/// </summary>
		[Required]
		public int DNI { get; set; }

		/// <summary>
		/// Id
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override int Id { get; set; }

		/// <summary>
		/// Tipos de esta cuenta
		/// </summary>
		[Required]
		[Column(name: "TiposCuenta")]
		public string StringTiposCuenta
		{
			get => EnumHelpers.ListaValoresEnumACadena(TiposCuenta);
			set => TiposCuenta = EnumHelpers.CadenadaAListaEnums<ETipoCuenta>(value);
		}

		/// <summary>
		/// Estado de esta cuenta
		/// </summary>
		[Required]
		public EEstadoCuenta EstadoCuenta { get; set; }

		public ModeloUsuario()
		{
			SecurityStamp = Guid.NewGuid().ToString();
		}
	}
}
