using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ComunaHealth.Pages
{
	[AllowAnonymous]
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly SignInManager<ModeloUsuario> _signInManager;

		public IndexModel(ILogger<IndexModel> logger, SignInManager<ModeloUsuario> signInManager)
		{
			_logger = logger;
			_signInManager = signInManager;
		}

		public void OnGet()
		{

		}

		public async Task<IActionResult> OnPostLogout()
		{
			await _signInManager.SignOutAsync();

			return RedirectToPage("/Index");
		}
	}
}
