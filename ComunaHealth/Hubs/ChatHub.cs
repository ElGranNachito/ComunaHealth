using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComunaHealth.Data;
using ComunaHealth.Modelos;
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
	}
}
