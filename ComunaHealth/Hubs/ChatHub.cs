using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComunaHealth.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ComunaHealth.Hubs
{
	public class ChatHub : Hub
	{
		public ChatHub(ComunaDbContext dbContext)
		{
			
		}

		public async Task ConectarAChat(string guidChat)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, guidChat);
		}

		public async Task EnviarMensajeChat(string guidChat, string mensaje)
		{
			await Clients.Group(guidChat).SendAsync("RecibirMensaje", mensaje);
		}
	}
}
