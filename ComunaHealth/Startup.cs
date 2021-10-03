using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using ComunaHealth.Modelos.Identity.Usuarios;

namespace ComunaHealth
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<ComunaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                services.AddDbContext<ComunaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AzureConnection")));
            }

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ModeloUsuario>()
                .AddRoles<ModeloRol>()
                .AddEntityFrameworkStores<ComunaDbContext>()
                .AddDefaultTokenProviders();

            services.AddAntiforgery(config => config.HeaderName = "RequestValidationToken");

            services.AddAuthentication(config =>
            {
                config.RequireAuthenticatedSignIn = false;

            });

            services.AddRazorPages();

            //Configuramos el servicio de autenticacion
            services.Configure<IdentityOptions>(configIdentity =>
            {
                //Configuracion de los requisitos de la contraseña
                configIdentity.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = true,
                    RequiredLength = 8,
                    RequiredUniqueChars = 3
                };

                //Configuracion de los requisitos para loguearse
                configIdentity.SignIn = new SignInOptions
                {
                    RequireConfirmedAccount = true,
                    RequireConfirmedEmail = true
                };

                //Configuracion de las restricciones para crearse un usuario
                configIdentity.User = new UserOptions
                {
                    AllowedUserNameCharacters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789@. ",
                    RequireUniqueEmail = true
                };

                //Configuracion de la resctriccion en caso de muchos intentos de logueo fallidos
                configIdentity.Lockout = new LockoutOptions
                {
                    MaxFailedAccessAttempts = 5,
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5)
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider servicios)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            CrearRolesSiNoExisten(servicios.GetRequiredService<RoleManager<ModeloRol>>(), servicios.GetRequiredService<UserManager<ModeloUsuario>>()).Wait();
            CrearBaseDeDatos(servicios).Wait();
            CrearAdministradorJefeSiNoExiste(servicios).Wait();
        }

        /// <summary>
        /// Crea los roles utilizados por la aplicacion en caso de que aun no existan
        /// </summary>
        private async Task CrearRolesSiNoExisten(RoleManager<ModeloRol> roleManager, UserManager<ModeloUsuario> userManager)
        {
	        //Creamos el rol paciente si no existe
            if (!await roleManager.RoleExistsAsync(Constantes.NombreRolPaciente))
            {
                await roleManager.CreateAsync(new ModeloRol(Constantes.NombreRolPaciente, ETipoCuenta.Paciente));
            }
            //Creamos el rol medico si no existe
            if (!await roleManager.RoleExistsAsync(Constantes.NombreRolMedico))
            {
                await roleManager.CreateAsync(new ModeloRol(Constantes.NombreRolMedico, ETipoCuenta.Medico));
            }
            //Creamos el rol administrador si no existe
            if (!await roleManager.RoleExistsAsync(Constantes.NombreRolAdministrador))
            {
                await roleManager.CreateAsync(new ModeloRol(Constantes.NombreRolAdministrador, ETipoCuenta.Administrador));
            }
            //Creamos el rol administrador jefe si no existe
            if (!await roleManager.RoleExistsAsync(Constantes.NombreRolAdministradorjefe))
            {
                await roleManager.CreateAsync(new ModeloRol(Constantes.NombreRolAdministradorjefe, ETipoCuenta.AdministradorJefe));
            }
        }

        /// <summary>
        /// Crea la base de datos y aplica las migraciones
        /// </summary>
        /// <param name="servicios">Inyeccion de dependencias</param>
        private async Task CrearBaseDeDatos(IServiceProvider servicios)
        {
	        await using (var context = servicios.CreateScope().ServiceProvider.GetRequiredService<ComunaDbContext>())
	        {
		        await context.Database.MigrateAsync();
	        }
        }

        /// <summary>
        /// Crea una cuenta para el administrador jefe con credenciales por defecto
        /// </summary>
        /// <param name="servicios"></param>
        /// <returns></returns>
        private async Task CrearAdministradorJefeSiNoExiste(IServiceProvider servicios)
        {
	        await using (var context = servicios.CreateScope().ServiceProvider.GetRequiredService<ComunaDbContext>())
	        {
                //Si el administrador jefe ya ha sido creado nos pegamos la vuelta
		        if (await context.AdministradoresJefe.AnyAsync())
                    return;
		        
			    var userManager = servicios.GetRequiredService<UserManager<ModeloUsuario>>();
                
			    ModeloAdministradorJefe administradorJefe = new ModeloAdministradorJefe
			    {
				    UserName = "Admin Jefe",

				    StringTiposCuenta  = ETipoCuenta.AdministradorJefe.ToString(),
				    EstadoCuenta = EEstadoCuenta.Habilitada,
                    Email = "nomail@nada.com"
			    };

			    administradorJefe.PasswordHash = userManager.PasswordHasher.HashPassword(administradorJefe, @"*&//--ElJefe--\\=¿?");
                
			    try
			    {
				    await userManager.CreateAsync(administradorJefe);
                    
                    //Esta en rol de administrador y administrador jefe
				    await userManager.AddToRoleAsync(administradorJefe, Constantes.NombreRolAdministradorjefe);
				    await userManager.AddToRoleAsync(administradorJefe, Constantes.NombreRolAdministrador);

                    await context.SaveChangesAsync();
			    }
			    catch (Exception)
			    {
				    context.Remove(administradorJefe);

				    await context.SaveChangesAsync();
                }
	        }
        }
    }
}
