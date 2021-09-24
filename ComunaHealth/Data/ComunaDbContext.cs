using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ComunaHealth.Modelos;
using ComunaHealth.Modelos.Identity;
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
		public DbSet<ModeloPaciente> Medicos { get; set; }
		public DbSet<ModeloPaciente> Administradores { get; set; }
		public DbSet<ModeloPaciente> AdministradoresJefe { get; set; }

		public ComunaDbContext(DbContextOptions<ComunaDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
