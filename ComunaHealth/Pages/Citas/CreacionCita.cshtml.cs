using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ComunaHealth.Pages.Citas
{
    /// <summary>
    /// Modelo de la pagina encargada de lidiar con la creacion de citas.
    /// </summary>
    public class CreacionCitaModel : PageModel
    {
        private readonly ComunaDbContext _dbcontext;
        private readonly UserManager<ModeloUsuario> _userManager;


        public CreacionCitaModel(ComunaDbContext dbContext, UserManager<ModeloUsuario> userManager)
        {
            _dbcontext   = dbContext;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if(!int.TryParse(DuracionMinutos, out var duracionParseada))
                ModelState.AddModelError(nameof(DuracionMinutos), "Duracion solo puede contener caracteres numericos");

            if(ModelState.ErrorCount > 0)
                return Page();

            //Obtenemos al usuario medico actual.
            var usuarioMedico = (ModeloMedico) ((ModeloUsuarioNoAdministrador) await _userManager.GetUserAsync(User));
            
            //Obenermos al usuario paciente al que se le decide crear la cita.
            var usuarioPaciente = usuarioMedico.Pacientes.Single(p => p.Id == PacienteId);

            //Creamos la nueva cita.
            ModeloCita nuevaCita = new ModeloCita
            {
                Medico              = usuarioMedico,
                Paciente            = usuarioPaciente,
                EspecializacionCita = Especializacion,
                Fecha               = Fecha,
                Duracion            = int.Parse(DuracionMinutos),
                Descripcion         = Descripcion
            };

            //Intentamos crear la cita y guardarla en la base de datos
            try
            {
                _dbcontext.Attach(nuevaCita).State = EntityState.Added;

                usuarioMedico.Citas.Add(nuevaCita);
                usuarioPaciente.Citas.Add(nuevaCita);

                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //Si la cita ya se guardo en la base de datos, la borramos ya que fallo en los pasos anteriores.
                if (nuevaCita.Id != 0)
                    _dbcontext.Remove(nuevaCita);

                await _dbcontext.SaveChangesAsync();

                return new JsonResult("Algo salio mal");
            }

            //De llegar hasta aqui, significa que la creacion de la cita fue exitosa.
            return RedirectToPage("MenuCitas");
        }


        #region Propiedades para la creacion de citas.

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
        [BindProperty]
        public int PacienteId { get; set; }

        [Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
        [BindProperty]
        public EEspecializacion Especializacion { get; set; }

        [Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = Constantes.MensajeErrorEsteCampoNoPuedeQuedarVacio)]
        [Display(Name = "Duracion en minutos")]
        [BindProperty]
        public string DuracionMinutos { get; set; }

        [StringLength(500)]
        [BindProperty]
        public string Descripcion { get; set; }

        #endregion
    }
}
