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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComunaHealth.Pages.Citas
{
    /// <summary>
    /// Modelo de la pagina encargada de lidiar con la muestra y gestion de citas.
    /// </summary>
    public class MenuCitasModel : PageModel
    {
        private readonly ComunaDbContext _dbContext;
        private readonly UserManager<ModeloUsuario> _userManager;

        /// <summary>
        /// Estado de cita seleccionado para mostrar.
        /// </summary>
        [Display(Name = "Filtrar por estado:")]
        public EEstadoCita FiltroEstadoCitas { get; set; } = EEstadoCita.Pendiente;

        /// <summary>
        /// Tipos de estados de citas seleccionables.
        /// </summary>
        public List<SelectListItem> EstadosDeCitaDisponibles { get; init; }

        /// <summary>
        /// Cita que el usuario selecciona para mostrar en la vista _InformacionCita>
        /// </summary>
        public ModeloCita CitaSeleccionada { get; set; } = null;

        /// <summary>
        /// Constructor del modelo.
        /// </summary>
        public MenuCitasModel(ComunaDbContext dbContext, UserManager<ModeloUsuario> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;

            EstadosDeCitaDisponibles = EnumHelpers.ToSelectListItemList<EEstadoCita>(Enum.GetValues<EEstadoCita>().ToList());
        }

        public async void OnGet()
        {
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostFiltradoCitas()
        {
            return Partial("_Citas", this);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostVerInformacionCita()
        {
            if (await _userManager.GetUserAsync(User) is ModeloMedico usuarioMedicoActual && 
                User.IsInRole(Constantes.NombreRolMedico))
            {
                CitaSeleccionada = usuarioMedicoActual.Citas.Single(p => p.Id == Request.Form["idCita"]);
            }
            else if (await _userManager.GetUserAsync(User) is ModeloPaciente usuarioPacienteActual && 
                User.IsInRole(Constantes.NombreRolPaciente))
            {
                CitaSeleccionada = usuarioPacienteActual.Citas.Single(p => p.Id == Request.Form["idCita"]);
            }

            return Partial("_InformacionCita", this);
        }
    }
}
