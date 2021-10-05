using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ComunaHealth.Modelos;
using ComunaHealth.Modelos.Identity;
using ComunaHealth.Modelos.Identity.Usuarios;
using ComunaHealth.Relaciones;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ComunaHealth.Data
{
	/// <summary>
	/// Data context de la aplicacion
	/// </summary>
	public class ComunaDbContext : IdentityDbContext<ModeloUsuario, ModeloRol, int>
	{
        /// <summary>
        /// Pacientes registrados
        /// </summary>
		public DbSet<ModeloPaciente> Pacientes { get; set; }

        /// <summary>
        /// Medicos registrados
        /// </summary>
		public DbSet<ModeloMedico> Medicos { get; set; }

        /// <summary>
        /// Administradores registrados
        /// </summary>
		public DbSet<ModeloAdministrador> Administradores { get; set; }

        /// <summary>
        /// Administradores jefe
        /// </summary>
		public DbSet<ModeloAdministradorJefe> AdministradoresJefe { get; set; }

        /// <summary>
        /// Chats
        /// </summary>
        public DbSet<ModeloChat> Chats { get; set; }

        /// <summary>
        /// Mensajes chat
        /// </summary>
        public DbSet<ModeloMensajeChat> MensajesChat { get; set; }

		public ComunaDbContext(DbContextOptions<ComunaDbContext> options)
			: base(options)
		{}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ModeloUsuario>().HasAlternateKey(u => u.DNI);
			modelBuilder.Entity<IdentityUser<int>>().Property(u => u.Id).HasAnnotation("SqlServer:Identity", "(1, 1)");

			modelBuilder.Entity<ModeloUsuario>()
				.HasMany<ModeloChat>(u => u.Chats)
				.WithMany(c => c.Participantes);

			modelBuilder.Entity<ModeloChat>()
				.HasMany<ModeloUsuario>(c => c.Participantes)
				.WithMany(u => u.Chats);
            
            // Medico:

            // - Medico ContenedorEntrada:
            modelBuilder.Entity<TIMedicoContenedorEntrada>().HasKey(e => new { e.IdMedico, e.IdEntrada});

            modelBuilder.Entity<TIMedicoContenedorEntrada>()
                .HasOne(i => i.Entrada);

            modelBuilder.Entity<TIMedicoContenedorEntrada>()
                .HasOne(i => i.Medico)
                .WithMany(p => p.Notas)
                .HasForeignKey(ip => ip.IdMedico);

            // - Medico ContenedorEntradaHistorialMedico:
            modelBuilder.Entity<TIMedicoContenedorEntradaHistorialMedico>().HasKey(e => new { e.IdMedico, e.IdEntradaHistorialMedico});

            modelBuilder.Entity<TIMedicoContenedorEntradaHistorialMedico>()
                .HasOne(i => i.EntradaHistorialMedico);

            modelBuilder.Entity<TIMedicoContenedorEntradaHistorialMedico>()
                .HasOne(i => i.Medico)
                .WithMany(p => p.NotasPacientes)
                .HasForeignKey(ip => ip.IdMedico);


			// Paciente:

            // - Paciente ContenedorEntrada (Diario):
            modelBuilder.Entity<TIPacienteContenedorEntrada>().HasKey(e => new { e.IdPaciente, e.IdContenedorEntrada});

            modelBuilder.Entity<TIPacienteContenedorEntrada>()
                .HasOne(i => i.ContenedorEntrada);

            modelBuilder.Entity<TIPacienteContenedorEntrada>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Diario)
                .HasForeignKey(ip => ip.IdPaciente);

            // - Paciente ContenedorEntrada (Diario):
            modelBuilder.Entity<TIPacienteContenedorEntradaCambiosEstiloVida>().HasKey(e => new { e.IdPaciente, e.IdContenedorEntrada });

            modelBuilder.Entity<TIPacienteContenedorEntradaCambiosEstiloVida>()
                .HasOne(i => i.ContenedorEntrada);

            modelBuilder.Entity<TIPacienteContenedorEntradaCambiosEstiloVida>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.CambiosEstiloDeVida)
                .HasForeignKey(ip => ip.IdPaciente);

            // - Paciente ContenedorEntradaHistorialMedico:
            modelBuilder.Entity<TIPacienteContenedorEntradaHistorialMedico>().HasKey(e => new { e.IdPaciente, e.IdEntradaHistorialMedico});

            modelBuilder.Entity<TIPacienteContenedorEntradaHistorialMedico>()
                .HasOne(i => i.EntradaHistorialMedico);
            
            modelBuilder.Entity<TIPacienteContenedorEntradaHistorialMedico>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.HistorialMedico)
                .HasForeignKey(ip => ip.IdPaciente);

            // Cita:

            modelBuilder.Entity<ModeloSolicitudCambioHorarioDeCita>().HasOne(s => s.Cita).WithOne(c => c.SolicitudCambioHorario);
            modelBuilder.Entity<ModeloSolicitudCambioHorarioDeCita>().HasOne(s => s.Solicitante);
        }
    }
}
