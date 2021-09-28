namespace ComunaHealth
{
    /// <summary>
    /// Representa el estado de un reporte
    /// </summary>
    public enum EEstadoReporte
    {
        /// <summary>
        /// El reporte aun no ha sido revisado.
        /// </summary>
        PendienteDeRevision,
        
        /// <summary>
        /// El reporte ya ha sido revisado por su administrador correspondiente.
        /// </summary>
        Revisado
    }
}
