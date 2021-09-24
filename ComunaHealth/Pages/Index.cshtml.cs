using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Identity;

namespace ComunaHealth.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger, UserManager<ModeloUsuario> userManager)
		{
			_logger = logger;
			
		}

		public void OnGet()
		{

		}
	}
}
