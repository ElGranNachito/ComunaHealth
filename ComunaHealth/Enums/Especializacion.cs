using System;

namespace ComunaHealth
{
	/// <summary>
	/// Representa la especializacion de un <see cref="Modelos.ModeloMedico"/>
	/// </summary>
	[Flags]
	public enum EEspecializacion
	{
		Proctologo = 1<<0,
		Oftalmologo = 1<<1,
		Ortodoncista = 1<<2,
		Oncologo =1<<3,
		Otorrinonaringologo = 1<<4,
		Cardiologo = 1<<5,
		Dermatologo = 1<<6,
		Neumologo = 1<<7,
		Ginecologo = 1<<8
	}
}
