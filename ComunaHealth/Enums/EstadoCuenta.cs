namespace ComunaHealth
{
	/// <summary>
	/// Representa el estado de una cuenta
	/// </summary>
	public enum EEstadoCuenta
	{
		/// <summary>
		/// Cuenta esta verificada, habilitada y no hay reportes pendientes
		/// </summary>
		Habilitada,

		/// <summary>
		/// Cuenta creada pero falta que un administrador confirme la identidad del usuario
		/// </summary>
		VerificacionPendiente,

		/// <summary>
		/// Cuenta tiene reportes pendientes de revision
		/// </summary>
		ConProblemitas,

		/// <summary>
		/// Cuenta ha sido deshabilitada
		/// </summary>
		Deshabilitada
	}
}
