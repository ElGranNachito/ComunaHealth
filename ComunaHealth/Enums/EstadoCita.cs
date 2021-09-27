namespace ComunaHealth
{
    /// <summary>
    /// Representa el estado de una cita
    /// </summary>
    public enum EEstadoCita
    {
        /// <summary>
        /// La cita ha sido efectuada y finalizada.
        /// </summary>
        Completada,

        /// <summary>
        /// Se ha reservado la cita y se esta en espera para ser atendida.
        /// </summary>
        Pendiente,
        
        /// <summary>
        /// La cita esta siendo efectuada.
        /// </summary>
        EnCurso,
        
        /// <summary>
        /// La cita fue efectivamente cancelada y no se ha acordado ninguna fecha de postergacion.
        /// </summary>
        Cancelada,
        
        /// <summary>
        /// Se ha solicitado un cambio en la fecha acordada para la ficha.
        /// </summary>
        Suspendida
    }
}
