using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
	public class ComunaDbContext : IdentityDbContext<ModeloUsuario, ModeloRol, string>
	{
		public DbSet<ModeloPaciente> Pacientes { get; set; }
		public DbSet<ModeloMedico> Medicos { get; set; }
		public DbSet<ModeloAdministrador> Administradores { get; set; }
		public DbSet<ModeloAdministradorJefe> AdministradoresJefe { get; set; }

		public ComunaDbContext(DbContextOptions<ComunaDbContext> options)
			: base(options)
		{}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ModeloUsuario>().HasAlternateKey(u => u.DNI);

			// Medico:
            
            // - Medico Paciente:
            modelBuilder.Entity<TIMedicoPaciente>().HasKey(e => new { e.IdMedico, e.IdPaciente});

            modelBuilder.Entity<TIMedicoPaciente>()
                .HasOne(i => i.Paciente);

            modelBuilder.Entity<TIMedicoPaciente>()
                .HasOne(i => i.Medico)
                .WithMany(p => p.Pacientes)
                .HasForeignKey(ip => ip.IdMedico);

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

            // - Paciente Medico:
            modelBuilder.Entity<TIMedicoPaciente>().HasKey(e => new { e.IdPaciente, e.IdMedico});

            modelBuilder.Entity<TIMedicoPaciente>()
                .HasOne(i => i.Medico);

            modelBuilder.Entity<TIMedicoPaciente>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Medicos)
                .HasForeignKey(ip => ip.IdPaciente);

            // - Paciente Cita:
            modelBuilder.Entity<TICitaPaciente>().HasKey(e => new { e.IdPaciente, e.IdCita});

            modelBuilder.Entity<TICitaPaciente>()
                .HasOne(i => i.Cita);

            modelBuilder.Entity<TICitaPaciente>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(ip => ip.IdPaciente);

            // - Paciente ContenedorEntrada (Diario):
            modelBuilder.Entity<TIPacienteContenedorEntrada>().HasKey(e => new { e.IdPaciente, e.IdEntrada});

            modelBuilder.Entity<TIPacienteContenedorEntrada>()
                .HasOne(i => i.Entrada);

            modelBuilder.Entity<TIPacienteContenedorEntrada>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.Diario)
                .HasForeignKey(ip => ip.IdPaciente);

            // - Paciente ContenedorEntrada (EstiloDeVida):
            //modelBuilder.Entity<TIPacienteContenedorEntrada>().HasKey(e => new { e.IdPaciente, e.IdEntrada});

            //modelBuilder.Entity<TIPacienteContenedorEntrada>()
            //    .HasOne(i => i.Entrada);

            //modelBuilder.Entity<TIPacienteContenedorEntrada>()
            //    .HasOne(i => i.Paciente)
            //    .WithMany(p => p.CambiosEstiloDeVida)
            //    .HasForeignKey(ip => ip.IdPaciente);

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

            // - Cita Medico:
            modelBuilder.Entity<TICitaPaciente>().HasKey(e => new { e.IdCita, e.IdPaciente});

            modelBuilder.Entity<TICitaPaciente>()
                .HasOne(i => i.Paciente);

            modelBuilder.Entity<TICitaPaciente>()
                .HasOne(i => i.Cita)
                .WithOne(p => p.Paciente)
                .HasForeignKey<TICitaPaciente>(ip => ip.IdCita);

            // - Cita SolicitudPostergacion:
            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>().HasKey(e => new { e.IdCita, e.IdSolicitudPostergacionDeCita});

            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>()
                .HasOne(i => i.SolicitudPostergacionDeCita);

            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>()
                .HasOne(i => i.Cita)
                .WithOne(p => p.SolicitudDePostergacion)
                .HasForeignKey<TICitaSolicitudPostergacionDeCita>(ip => ip.IdCita);


            // SolicitudPostergacionDeCita: 

            // - SolicitudPostergacion Cita:
            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>().HasKey(e => new { e.IdSolicitudPostergacionDeCita, e.IdCita});

            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>()
                .HasOne(i => i.Cita);


            // ContenedorDeEntradas:


        }
	}
}
