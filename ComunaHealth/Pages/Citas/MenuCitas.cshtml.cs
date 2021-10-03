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
    /// Modelo de la pagina del menu de citas.
    /// </summary>
    public class MenuCitasModel : PageModel
    {
        private readonly ComunaDbContext _dbContext;
        private readonly UserManager<ModeloUsuario> _userManager;


        /// <summary>
        /// Usuario actual.
        /// </summary>
        public ModeloUsuarioNoAdministrador UsuarioActual { get; set; }

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
        public async Task<IActionResult> OnPost()
        {
            return Partial("_Citas", this);
        }
    }
}
