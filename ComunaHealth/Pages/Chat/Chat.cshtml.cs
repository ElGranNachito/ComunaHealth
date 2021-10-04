using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize]
    public class ChatModel : PageModel
    {
	    private readonly IHubContext<ChatHub> _chatHub;

	    private readonly UserManager<ModeloUsuario> _userManager;

	    private readonly ComunaDbContext _dbContext;

	    private string mIdChatAcutal;

		public ModeloChat ChatActual { get; private set; }

	    [TempData]
	    public Guid IdChatActual
	    {
		    get => string.IsNullOrEmpty(mIdChatAcutal) ? Guid.Empty : Guid.Parse(mIdChatAcutal);
		    set => mIdChatAcutal = value.ToString();
	    }

        public ChatModel(IHubContext<ChatHub> chatHub, UserManager<ModeloUsuario> userManager, ComunaDbContext dbContext)
        {
	        _chatHub = chatHub;
	        _userManager = userManager;
	        _dbContext = dbContext;
        }

        public void OnGet()
        {
	        
        }

        public async Task<IActionResult> OnPost()
        {
	        if (Request.Form["idChat"] != string.Empty)
	        {
		        IdChatActual = Guid.Parse( Request.Form["idChat"]);

				ChatActual = await _dbContext.Chats.Where(c => c.GuidChat == mIdChatAcutal).Include(c => c.Entradas).FirstOrDefaultAsync();

				return Partial("_MensajesChat", this);
	        }
            
	        return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostEnviarMensaje()
        {
	        var usuariosActual = await _userManager.GetUserAsync(User);

	        ChatActual = await _dbContext.Chats.Where(c => c.GuidChat == mIdChatAcutal).FirstOrDefaultAsync();

			var nuevoMensaje = new ModeloMensajeChat
	        {
		        Contenido = Request.Form["Mensaje"],
		        FechaDeCreacion = DateTimeOffset.Now,
		        Remitente = usuariosActual
	        };

			ChatActual.Entradas.Add(nuevoMensaje);

			await _dbContext.AddAsync(nuevoMensaje);
			await _dbContext.SaveChangesAsync();

	        await _chatHub.Clients.Group(IdChatActual.ToString()).SendAsync("RecibirMensaje", IdChatActual.ToString(), nuevoMensaje.Contenido, nuevoMensaje.FechaDeCreacion, nuevoMensaje.Remitente.UserName);

	        return Page();
        }
    }
}
