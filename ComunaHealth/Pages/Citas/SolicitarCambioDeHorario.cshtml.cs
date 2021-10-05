using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ComunaHealth.Relaciones;
using ComunaHealth.Data;
using Microsoft.AspNetCore.Identity;
using ComunaHealth.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ComunaHealth.Pages.Citas
{
    /// <summary>
    /// Modelo de la pagina encargada de lidiar con la creacion de solicitud de cambio de horario para una cita.
    /// </summary>
    public class SolicitarCambioDeHorario : PageModel
    {
        private readonly ComunaDbContext _dbcontext;
        private readonly UserManager<ModeloUsuario> _userManager;


        public SolicitarCambioDeHorario(ComunaDbContext dbContext, UserManager<ModeloUsuario> userManager)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
        }

        public void OnGet([FromQuery(Name = "idCita")] string id)
        {
            CitaId = int.Parse(id);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (!int.TryParse(NuevaDuracion, out var nuevaDuracionParseada))
                ModelState.AddModelError(nameof(NuevaDuracion), "Duracion solo puede contener caracteres numericos");

            if (ModelState.ErrorCount > 0)
                return Page();

            //Obtenemos al usuario solicitante actual.
            var usuarioSolicitante = (ModeloUsuarioNoAdministrador)await _userManager.GetUserAsync(User);

            //Creamos la nueva cita.
            ModeloSolicitudCambioHorarioDeCita modeloSolicitudCambioHorarioDeCita = new ModeloSolicitudCambioHorarioDeCita
            {
                //Solicitante = usuarioSolicitante,
                //Cita = User.IsInRole(Constantes.NombreRolMedico) ? ((ModeloMedico)usuarioSolicitante).Citas.Select(p => p.Id == CitaId),
                //NuevaFecha = NuevaFecha,
                //NuevaDuracion = int.Parse(NuevaDuracion),
                //Razon = Razon
            };

            //Intentamos crear la cita y guardarla en la base de datos
            try
            {
                _dbcontext.Attach(modeloSolicitudCambioHorarioDeCita).State = EntityState.Added;

                
                
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ////Si la cita ya se guardo en la base de datos, la borramos ya que fallo en los pasos anteriores.
                //if (nuevaCita.Id != 0)
                //    _dbcontext.Remove(nuevaCita);

                await _dbcontext.SaveChangesAsync();

                return new JsonResult("Algo salio mal");
            }

            //De llegar hasta aqui, significa que la creacion de la cita fue exitosa.
            return RedirectToPage("MenuCitas");
        }

        #region Propiedades para la creacion de la solicitud

        [Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
        [BindProperty]
        public int CitaId { get; set; }

        [StringLength(500)]
        [BindProperty]
        public string Razon { get; set; }

        [Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
        [Display(Name = "Nueva duracion en minutos")]
        [BindProperty]
        public string NuevaDuracion { get; set; }


        [Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public DateTime NuevaFecha { get; set; }

        #endregion
    }
}
