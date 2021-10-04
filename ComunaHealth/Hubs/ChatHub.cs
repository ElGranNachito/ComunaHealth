using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ComunaHealth.Hubs
{
	public class ChatHub : Hub
	{
		private readonly UserManager<ModeloUsuario> _userManager;

		public ChatHub(ComunaDbContext dbContext, UserManager<ModeloUsuario> userManager)
		{
			_userManager = userManager;
		}

		public async Task ConectarAChat(string guidChat)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, guidChat);
		}

		public async Task EnviarMensaje(string guidChat, string mensaje)
		{
			await Clients.Group(guidChat).SendAsync("RecibirMensaje", mensaje, DateTime.UtcNow.ToString("d"), _userManager.GetUserName(Context.User),_userManager.GetUserId(Context.User));
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await Task.FromResult("a");
		}
	}
}
