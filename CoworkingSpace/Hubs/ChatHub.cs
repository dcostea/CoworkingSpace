using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpace.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string user)
        {
            await Clients.All.SendAsync("ReceiveChatMessage", message, Context.User.Identity.Name);
        }

        // Clients.User is buggy
        public async Task SendMessageToUser(string message, string user)
        {
            await Clients.User(Context.UserIdentifier).SendAsync("ReceiveDirectMessage", message, Context.UserIdentifier);
        }

        public Task SendMessageToGroup(string groupName, string message)
        {
            return Clients.Group(groupName).SendAsync("ReceiveChatMessage", $"{Context.ConnectionId}: {message}", groupName);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveSystemMessage", $"{Context.ConnectionId} has joined the group {groupName}.", groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveSystemMessage", $"{Context.ConnectionId} has left the group {groupName}.", groupName);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveSystemMessage", "joined", Context.User.Identity.Name);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("ReceiveSystemMessage", "left", Context.User.Identity.Name);
        }
    }
}
