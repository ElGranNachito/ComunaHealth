using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ComunaHealth.Modelos
{
	/// <summary>
	/// Modelo que representa a un usuario de tipo medico
	/// </summary>
	public class ModeloMedico : ModeloUsuarioNoAdministrador
	{
		[NotMapped]
		public List<EEspecializacion> Especializaciones { get; private set; } = new List<EEspecializacion>();

		/// <summary>
		/// Especializaciones del medico
		/// </summary>
		[Required]
		[Column("Especializaciones")]
		public string StringEspecializaciones
		{
			get
			{
				StringBuilder bld = new StringBuilder();

				for (var i = 0; i < Especializaciones.Count; i++)
				{
					bld.Append(Especializaciones[i].ToString() + (i < Especializaciones.Count - 1 ? ", " : string.Empty));
				}

				return bld.ToString();
			}
			set
			{
				string[] especializaciones = value.Split(',');

				foreach (var especializacion in especializaciones)
				{
					if (Enum.TryParse<EEspecializacion>(especializacion, out var especializacionParseada))
						Especializaciones.Add(especializacionParseada);
				}
			}
		}

		/// <summary>
		/// Matricula del medico
		/// </summary>
		public int Matricula { get; set; }
	}
}
