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
		public static List<TEnum> ObtenerValoresFlag<TEnum>(TEnum excepciones)
			where TEnum: struct, Enum
		{
			var valoresEnum = Enum.GetValues<TEnum>().ToList();

			valoresEnum.RemoveAll(e => (Convert.ToInt32(e) & Convert.ToInt32(excepciones)) != 0);
			
			return valoresEnum;
		}

		/// <summary>
		/// Obtiene todos los valores de un <see cref="Enum"/>
		/// </summary>
		/// <typeparam name="TEnum">Tipo del enum del que obtener los valores</typeparam>
		/// <param name="enumExcepciones">Valores del enum que exceptuar de la obtenencion</param>
		/// <returns><see cref="List{T}"/> con los valores de <typeparamref name="TEnum"/></returns>
		public static List<TEnum> ObtenerValores<TEnum>(IEnumerable<TEnum> enumExcepciones = null)
			where TEnum : struct, Enum
		{
			var excepciones = enumExcepciones?.ToList();
			var valoresEnum = Enum.GetValues<TEnum>().ToList();

			if(excepciones != null)
				valoresEnum.RemoveAll(e => excepciones.Contains(e));

			return valoresEnum;
		}

		public static List<SelectListItem> ToSelectListItemList<TEnum>(List<TEnum> valoresEnum)
			where TEnum : struct, Enum
		{
			return valoresEnum.Select(v => new SelectListItem(v.ToString(), v.ToString())).ToList();
		}

		public static ERegionSanitariaBSAS ObtenerRegionDeSaludCorrespondiente(EMunicipio municipio)
		{
			switch (municipio)
			{
				case EMunicipio.AdolfoAlsina:
				case EMunicipio.BahiaBlanca:
				case EMunicipio.Patagones:
				case EMunicipio.AdolfoGonzalesChaves:
				case EMunicipio.CoronelDorrego:
				case EMunicipio.CoronelPringles:
				case EMunicipio.CoronelRosales:
				case EMunicipio.CoronelSuárez:
				case EMunicipio.Guaminí:
				case EMunicipio.MonteHermoso:
				case EMunicipio.Puán:
				case EMunicipio.Saavedra:
				case EMunicipio.Tornquist:
				case EMunicipio.TresArroyos:
				case EMunicipio.Villarino:
					return ERegionSanitariaBSAS.Region1;

				case EMunicipio.CarlosTejedor:
				case EMunicipio.Pehuajó:
				case EMunicipio.Daireaux:
				case EMunicipio.GeneralVillegas:
				case EMunicipio.NueveDeJulio:
				case EMunicipio.CarlosCasares:
				case EMunicipio.TrenqueLauquen:
				case EMunicipio.Rivadavia:
				case EMunicipio.Pellegrini:
				case EMunicipio.TresLomas:
				case EMunicipio.Salliquelo:
				case EMunicipio.HipólitoYrigoyen:
					return ERegionSanitariaBSAS.Region2;

				case EMunicipio.GeneralViamonte:
				case EMunicipio.Chacabuco:
				case EMunicipio.Junín:
				case EMunicipio.GeneralArenales:
				case EMunicipio.LeandroNAlem:
				case EMunicipio.GeneralPinto:
				case EMunicipio.FlorentinoAmeghino:
				case EMunicipio.Lincoln:
					return ERegionSanitariaBSAS.Region3;

				case EMunicipio.CapitánSarmiento:
				case EMunicipio.CarmenDeAreco:
				case EMunicipio.SanAndrésDeGiles:
				case EMunicipio.SanAntonioDeAreco:
				case EMunicipio.Baradero:
				case EMunicipio.SanPedro:
				case EMunicipio.Ramallo:
				case EMunicipio.SanNicolás:
				case EMunicipio.Arrecifes:
				case EMunicipio.Salto:
				case EMunicipio.Pergamino:
				case EMunicipio.Colón:
				case EMunicipio.Rojas:
					return ERegionSanitariaBSAS.Region4;

				case EMunicipio.ExaltaciónDeLaCruz:
				case EMunicipio.Zárate:
				case EMunicipio.Campana:
				case EMunicipio.Pilar:
				case EMunicipio.Escobar:
				case EMunicipio.Tigre:
				case EMunicipio.JoseCPaz:
				case EMunicipio.MalvinasArgentinas:
				case EMunicipio.SanFernando:
				case EMunicipio.SanIsidro:
				case EMunicipio.VicenteLópez:
				case EMunicipio.GeneralSanMartín:
				case EMunicipio.SanMiguel:
				case EMunicipio.IslaMartínGarcía:
					return ERegionSanitariaBSAS.Region5;

				case EMunicipio.Avellaneda:
				case EMunicipio.Lanús:
				case EMunicipio.Quilmes:
				case EMunicipio.LomasDeZamora:
				case EMunicipio.AlmiranteBrown:
				case EMunicipio.Ezeiza:
				case EMunicipio.FlorencioVarela:
				case EMunicipio.EstebanEcheverría:
				case EMunicipio.Berazategui:
					return ERegionSanitariaBSAS.Region6;

				case EMunicipio.Moreno:
				case EMunicipio.Hurlingham:
				case EMunicipio.Morón:
				case EMunicipio.Ituzaingó:
				case EMunicipio.TresDeFebrero:
				case EMunicipio.Merlo:
				case EMunicipio.GeneralLasHeras:
				case EMunicipio.GeneralRodríguez:
				case EMunicipio.MarcosPaz:
				case EMunicipio.Lujan:
					return ERegionSanitariaBSAS.Region7;

				case EMunicipio.GeneralLavalle:
				case EMunicipio.Maipú:
				case EMunicipio.LaCosta:
				case EMunicipio.Pinamar:
				case EMunicipio.VillaGesell:
				case EMunicipio.GeneralMadariaga:
				case EMunicipio.GeneralGuido:
				case EMunicipio.Ayacucho:
				case EMunicipio.Tandil:
				case EMunicipio.Balcarce:
				case EMunicipio.MarChiquita:
				case EMunicipio.GeneralPueyrredón:
				case EMunicipio.GeneralAlvarado:
				case EMunicipio.Lobería:
				case EMunicipio.Necochea:
				case EMunicipio.SanCayetano:
					return ERegionSanitariaBSAS.Region8;

				case EMunicipio.LasFlores:
				case EMunicipio.GeneralAlvear:
				case EMunicipio.Tapalqué:
				case EMunicipio.Bolivar:
				case EMunicipio.Azul:
				case EMunicipio.BenitoJuarez:
				case EMunicipio.Laprida:
				case EMunicipio.Olavarria:
				case EMunicipio.GeneralLaMadrid:
				case EMunicipio.Rauch:
					return ERegionSanitariaBSAS.Region9;

				case EMunicipio.Mercedes:
				case EMunicipio.Lobos:
				case EMunicipio.Suipacha:
				case EMunicipio.Bragado:
				case EMunicipio.Alberti:
				case EMunicipio.Chivilcoy:
				case EMunicipio.Navarro:
				case EMunicipio.RoquePérez:
				case EMunicipio.VeinticincoDeMayo:
				case EMunicipio.Saladillo:
					return ERegionSanitariaBSAS.Region10;

				case EMunicipio.Pila:
				case EMunicipio.Dolores:
				case EMunicipio.Tordillo:
				case EMunicipio.Castelli:
				case EMunicipio.GeneralBelgrano:
				case EMunicipio.Chascomús:
				case EMunicipio.PuntaIndio:
				case EMunicipio.Monte:
				case EMunicipio.GeneralPaz:
				case EMunicipio.Brandsen:
				case EMunicipio.Berisso:
				case EMunicipio.LaPlata:
				case EMunicipio.Ensenada:
				case EMunicipio.SanVicente:
				case EMunicipio.Cañuelas:
				case EMunicipio.PresidentePerón:
				case EMunicipio.Magdalena:
				case EMunicipio.Lezama:
					return ERegionSanitariaBSAS.Region11;

				case EMunicipio.LaMatanza:
					return ERegionSanitariaBSAS.Region12;
			}

			return ERegionSanitariaBSAS.Region1;
		}
	}
}
