using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComunaHealth.Pages.Chat
{
    [Authorize]
    public class ChatModel : PageModel
    {
        public string IdChatActual { get; set; }

        public void OnGet()
        {
	        
        }

        public async Task<IActionResult> OnPost()
        {
	        if (Request.Form["idChat"] != string.Empty)
	        {
		        return Partial("_MensajesChat", this);
	        }
            
	        return Page();
        }
    }
}
