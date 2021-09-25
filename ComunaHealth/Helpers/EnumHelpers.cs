using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComunaHealth
{
	/// <summary>
	/// Clase que contiene metodos para facilitar operaciones con enums
	/// </summary>
	public class EnumHelpers
	{
		/// <summary>
		/// Obtiene todos los valores de un <see cref="Enum"/>
		/// </summary>
		/// <typeparam name="TEnum">Tipo del enum del que obtener los valores</typeparam>
		/// <param name="excepciones">Flag que contiene los valores que exceptuar de la obtencion</param>
		/// <returns><see cref="List{T}"/> con los valores de <typeparamref name="TEnum"/></returns>
		public static List<TEnum> ObtenerValores<TEnum>(TEnum excepciones)
			where TEnum: struct, Enum
		{
			var valoresEnum = Enum.GetValues<TEnum>().ToList();

			valoresEnum.RemoveAll(e => excepciones.HasFlag(e));

			return valoresEnum;
		}

		public static List<SelectListItem> ToSelectItemList<TEnum>(List<TEnum> valoresEnum)
			where TEnum : struct, Enum
		{
			return valoresEnum.Select(v => new SelectListItem(v.ToString(), v.ToString())).ToList();
		}
	}
}
