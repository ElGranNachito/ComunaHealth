using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;

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
                    RequiredLength = 8
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
                    AllowedUserNameCharacters = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789@.",
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
    }
}
