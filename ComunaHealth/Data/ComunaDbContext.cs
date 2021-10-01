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
            //modelBuilder.Entity<TIMedicoPaciente>().HasKey(e => new { e.IdMedico, e.IdPaciente});

            //modelBuilder.Entity<TIMedicoPaciente>()
            //    .HasOne(i => i.Paciente)
            //    .WithMany(m => m.Medicos)
            //    .HasForeignKey(i => i.IdPaciente);

            //modelBuilder.Entity<TIMedicoPaciente>()
            //    .HasOne(i => i.Medico)
            //    .WithMany(p => p.Pacientes)
            //    .HasForeignKey(ip => ip.IdMedico);


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
            //modelBuilder.Entity<TIPacienteMedico>().HasKey(e => new { e.IdPaciente, e.IdMedico});

            //modelBuilder.Entity<TIPacienteMedico>()
            //    .HasOne(i => i.Medico);

            //modelBuilder.Entity<TIPacienteMedico>()
            //    .HasOne(i => i.Paciente)
            //    .WithMany(p => p.Medicos)
            //    .HasForeignKey(ip => ip.IdPaciente);

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
            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>().HasKey(e => new { e.IdCita, e.IdSolicitudPostergacionDeCita});

            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>()
                .HasOne(i => i.SolicitudPostergacionDeCita);

            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>()
                .HasOne(i => i.Cita)
                .WithOne(p => p.SolicitudDePostergacion)
                .HasForeignKey<TICitaSolicitudPostergacionDeCita>(ip => ip.IdCita);


            // SolicitudPostergacionDeCita: 

            // - SolicitudPostergacion UsuarioNoAdministrador:
            modelBuilder.Entity<TIUsuarioNoAdministradorSolicitudPostergacionCita>().HasKey(e => new { e.IdSolicitudPostergacionCita, e.IdUsuarioNoAdministrador });

            modelBuilder.Entity<TIUsuarioNoAdministradorSolicitudPostergacionCita>()
                .HasOne(i => i.UsuarioNoAdministrador);

            modelBuilder.Entity<TIUsuarioNoAdministradorSolicitudPostergacionCita>()
                .HasOne(i => i.SolicitudPostergacionCita)
                .WithOne(p => p.Solicitante)
                .HasForeignKey<TIUsuarioNoAdministradorSolicitudPostergacionCita>(ip => ip.IdSolicitudPostergacionCita);

            // - SolicitudPostergacion Cita:
            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>().HasKey(e => new { e.IdSolicitudPostergacionDeCita, e.IdCita});

            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>()
                .HasOne(i => i.Cita);

            modelBuilder.Entity<TICitaSolicitudPostergacionDeCita>()
                .HasOne(i => i.SolicitudPostergacionDeCita)
                .WithOne(p => p.Cita)
                .HasForeignKey<TICitaSolicitudPostergacionDeCita>(ip => ip.IdSolicitudPostergacionDeCita);

            // ContenedorDeEntradas:

            // - ContenedorEntrada Entrada:
            modelBuilder.Entity<TIContenedorDeEntradasEntrada<ModeloEntrada>>().HasKey(e => new { e.IdContenedorDeEntradas, e.IdEntrada });

            modelBuilder.Entity<TIContenedorDeEntradasEntrada<ModeloEntrada>>()
                .HasOne(i => i.Entrada);

            modelBuilder.Entity<TIContenedorDeEntradasEntrada<ModeloEntrada>>()
                .HasOne(i => i.ContenedorDeEntradas)
                .WithMany(p => p.Entradas)
                .HasForeignKey(ip => ip.IdContenedorDeEntradas);

            // - ContenedorEntrada EntradaHistorialMedico:
            modelBuilder.Entity<TIContenedorDeEntradasEntrada<ModeloEntradaHistorialMedico>>().HasKey(e => new { e.IdContenedorDeEntradas, e.IdEntrada });

            modelBuilder.Entity<TIContenedorDeEntradasEntrada<ModeloEntradaHistorialMedico>>()
                .HasOne(i => i.Entrada);

            modelBuilder.Entity<TIContenedorDeEntradasEntrada<ModeloEntradaHistorialMedico>>()
                .HasOne(i => i.ContenedorDeEntradas)
                .WithMany(p => p.Entradas)
                .HasForeignKey(ip => ip.IdContenedorDeEntradas);

            // Chat:

            // - Chat Usuario:
            modelBuilder.Entity<TIChatUsuario>().HasKey(e => new { e.IdChat, e.IdUsuario });

            modelBuilder.Entity<TIChatUsuario>()
                .HasOne(i => i.Usuario);

            modelBuilder.Entity<TIChatUsuario>()
                .HasOne(i => i.Chat)
                .WithMany(p => p.Participantes)
                .HasForeignKey(ip => ip.IdChat);


            // EntradaHistorialMedico:

            // - EntradaHistorialMedico Medico:
            modelBuilder.Entity<TIMedicoEntradaHistorialMedico>().HasKey(e => new { e.IdEntradaHistorialMedico, e.IdMedico });

            modelBuilder.Entity<TIMedicoEntradaHistorialMedico>()
                .HasOne(i => i.Medico);

            modelBuilder.Entity<TIMedicoEntradaHistorialMedico>()
                .HasOne(i => i.EntradaHistorialMedico)
                .WithOne(p => p.MedicoCreador)
                .HasForeignKey<TIMedicoEntradaHistorialMedico>(ip => ip.IdEntradaHistorialMedico);

            // - EntradaHistorialMedico Medico:
            modelBuilder.Entity<TIPacienteEntradaHistorialMedico>().HasKey(e => new { e.IdEntradaHistorialMedico, e.IdPaciente });

            modelBuilder.Entity<TIPacienteEntradaHistorialMedico>()
                .HasOne(i => i.Paciente);

            modelBuilder.Entity<TIPacienteEntradaHistorialMedico>()
                .HasOne(i => i.EntradaHistorialMedico)
                .WithOne(p => p.Paciente)
                .HasForeignKey<TIPacienteEntradaHistorialMedico>(ip => ip.IdEntradaHistorialMedico);

            // MensajeChat:

            // - MensajeChat UsuarioNoAdministrador:
            modelBuilder.Entity<TIUsuarioNoAdministradorMensajeChat>().HasKey(e => new { e.IdMensajeChat, e.IdUsuarioNoAdministrador });

            modelBuilder.Entity<TIUsuarioNoAdministradorMensajeChat>()
                .HasOne(i => i.UsuarioNoAdministrador);

            modelBuilder.Entity<TIUsuarioNoAdministradorMensajeChat>()
                .HasOne(i => i.MensajeChat)
                .WithOne(p => p.Remitente)
                .HasForeignKey<TIUsuarioNoAdministradorMensajeChat>(ip => ip.IdMensajeChat);

            // LogAdministrador:

            // - LogAdministrador Administrador:
            modelBuilder.Entity<TIAdministradorLogAdministrador>().HasKey(e => new { e.IdLogAdministrador, e.IdAdministrador });

            modelBuilder.Entity<TIAdministradorLogAdministrador>()
                .HasOne(i => i.Administrador);

            modelBuilder.Entity<TIAdministradorLogAdministrador>()
                .HasOne(i => i.LogAdministrador)
                .WithOne(p => p.Administrador)
                .HasForeignKey<TIAdministradorLogAdministrador>(ip => ip.IdLogAdministrador);


            
        }
    }
}
