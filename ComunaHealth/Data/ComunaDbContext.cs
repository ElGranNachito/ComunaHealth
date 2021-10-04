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

            // - Medico Cita:
            modelBuilder.Entity<TICitaMedico>().HasKey(e => new { e.IdMedico, e.IdCita});

            modelBuilder.Entity<TICitaMedico>()
                .HasOne(i => i.Cita);

            modelBuilder.Entity<TICitaMedico>()
                .HasOne(i => i.Medico)
                .WithMany(p => p.Citas)
                .HasForeignKey(ip => ip.IdMedico);

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

            // - Paciente Cita:
            modelBuilder.Entity<TICitaPaciente>().HasKey(e => new { e.IdPaciente, e.IdCita});

            modelBuilder.Entity<TICitaPaciente>()
                .HasOne(i => i.Cita);

            modelBuilder.Entity<TICitaPaciente>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(ip => ip.IdPaciente);

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

			// - Cita Medico:
            modelBuilder.Entity<TICitaMedico>().HasKey(e => new { e.IdCita, e.IdMedico});

            modelBuilder.Entity<TICitaMedico>()
                .HasOne(i => i.Medico);

            modelBuilder.Entity<TICitaMedico>()
                .HasOne(i => i.Cita)
                .WithOne(p => p.Medico)
                .HasForeignKey<TICitaMedico>(ip => ip.IdCita);

            // - Cita SolicitudPostergacion:
            modelBuilder.Entity<TICitaSolicitudCambioHorarioCita>().HasKey(e => new { e.IdCita, e.IdSolicitudCambioHorarioDeCita});

            modelBuilder.Entity<TICitaSolicitudCambioHorarioCita>()
                .HasOne(i => i.SolicitudCambioHorarioDeCita);

            modelBuilder.Entity<TICitaSolicitudCambioHorarioCita>()
                .HasOne(i => i.Cita)
                .WithOne(p => p.SolicitudCambioHorario)
                .HasForeignKey<TICitaSolicitudCambioHorarioCita>(ip => ip.IdCita);


            // SolicitudPostergacionDeCita: 

            // - SolicitudPostergacion UsuarioNoAdministrador:
            modelBuilder.Entity<TIUsuarioNoAdministradorSolicitudCambioHorarioCita>().HasKey(e => new { e.IdSolicitudPostergacionCita, e.IdUsuarioNoAdministrador });

            modelBuilder.Entity<TIUsuarioNoAdministradorSolicitudCambioHorarioCita>()
                .HasOne(i => i.UsuarioNoAdministrador);

            modelBuilder.Entity<TIUsuarioNoAdministradorSolicitudCambioHorarioCita>()
                .HasOne(i => i.SolicitudPostergacionCita)
                .WithOne(p => p.Solicitante)
                .HasForeignKey<TIUsuarioNoAdministradorSolicitudCambioHorarioCita>(ip => ip.IdSolicitudPostergacionCita);

            // - SolicitudPostergacion Cita:
            modelBuilder.Entity<TICitaSolicitudCambioHorarioCita>().HasKey(e => new { e.IdSolicitudCambioHorarioDeCita, e.IdCita});

            modelBuilder.Entity<TICitaSolicitudCambioHorarioCita>()
                .HasOne(i => i.Cita);

            modelBuilder.Entity<TICitaSolicitudCambioHorarioCita>()
                .HasOne(i => i.SolicitudCambioHorarioDeCita)
                .WithOne(p => p.Cita)
                .HasForeignKey<TICitaSolicitudCambioHorarioCita>(ip => ip.IdSolicitudCambioHorarioDeCita);
		}
    }
}
