using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComunaHealth.Modelos;

namespace ComunaHealth.Helpers
{
    /// <summary>
    /// Clase que contiene metodos para facilitar operaciones con usuarios
    /// </summary>
    public class UserHelpers
    {
        public static List<SelectListItem> ToSelectListItemListUsuarioId<TUsuario>(List<TUsuario> usuarios)
            where TUsuario : ModeloUsuario
        {
            return usuarios.Select(v => new SelectListItem( $"{v.UserName} DNI: {v.DNI}", v.Id.ToString())).ToList();
        }
    }
}
