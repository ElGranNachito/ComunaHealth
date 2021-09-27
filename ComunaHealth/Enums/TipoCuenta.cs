using System;

namespace ComunaHealth
{
	/// <summary>
	/// Describe el tipo de una cuenta
	/// </summary>
	[Flags]
	public enum ETipoCuenta
	{
		/// <summary>
		/// Cuenta de un paciente
		/// </summary>
		Paciente = 1<<0,

		/// <summary>
		/// Cuenta de un medico
		/// </summary>
		Medico = 1<<1,

		/// <summary>
		/// Cuenta de un administrador
		/// </summary>
		Administrador = 1<<2,

		/// <summary>
		/// Cuenta de un administrador jefe
		/// </summary>
		AdministradorJefe = 1<<3,

		/// <summary>
		/// Nada
		/// </summary>
		NINGUNO = 0
	}
}
