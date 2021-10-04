using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Hubs;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ComunaHealth.Pages.Chat
{
	/// <summary>
	/// Modelo de la pagina del chat
	/// </summary>
    [Authorize]
    public class ChatModel : PageModel
    {
	    private readonly UserManager<ModeloUsuario> _userManager;

	    private readonly ComunaDbContext _dbContext;

	    public ModeloChat chatActual;

		public ChatModel(IHubContext<ChatHub> chatHub, UserManager<ModeloUsuario> userManager, ComunaDbContext dbContext)
        {
	        _userManager = userManager;
	        _dbContext   = dbContext;
        }

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPost()
		{
			string guidChatActual = Request.Form["idChat"].ToString();

			chatActual = await _dbContext.Chats.Where(c => c.GuidChat == guidChatActual).Include(c => c.Entradas)
				.Include(c => c.Participantes).FirstOrDefaultAsync();

			return Partial("_MensajesChat", this);
		}

		/// <summary>
		/// Metodo encargado de crear y guardar un mensaje 
		/// </summary>
		/// <returns></returns>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEnviarMensaje()
        {
			//Obtenemos el usuario actual
	        var usuariosActual = await _userManager.GetUserAsync(User);

			//Guid del chat actual
	        var idChatActual = Request.Form["idChatActual"].ToString();

	        if (string.IsNullOrWhiteSpace(idChatActual))
		        return BadRequest();

			//Intentamos encontrar el chat con la guid especificada
	        chatActual = await _dbContext.Chats.Where(c => c.GuidChat == idChatActual).FirstOrDefaultAsync();

			if (chatActual == null)
				return BadRequest();

			//Creamos el nuevo mensaje
			var nuevoMensaje = new ModeloMensajeChat
	        {
		        Contenido = Request.Form["Mensaje"],
		        FechaDeCreacion = DateTimeOffset.UtcNow,
		        Remitente = usuariosActual
	        };

			//Añadimos el nuevo mensaje al chat
			chatActual.Entradas.Add(nuevoMensaje);

			//Añadimos el nuevo mensaje a la base de datos y guardamos
			await _dbContext.AddAsync(nuevoMensaje);
			await _dbContext.SaveChangesAsync();

			return new AcceptedResult();
        }
    }
}