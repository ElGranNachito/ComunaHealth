using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ComunaHealth.Helpers
{
	/// <summary>
	/// Contiene metodos para ayudar a realizar operaciones criptograficas
	/// </summary>
	public static class CryptoHelpers
	{
		/// <summary>
		/// Genera un <see cref="string"/> aleatorio
		/// </summary>
		/// <param name="longitud">Longitud de la cadena</param>
		/// <returns><see cref="string"/> aleatorio</returns>
		public static string GenerarStringAleatorio(int longitud)
		{
			byte[] bytesGenerados = new byte[24];

			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				rng.GetBytes(bytesGenerados);
			}

			return Convert.ToBase64String(bytesGenerados);
		}
	}
}
