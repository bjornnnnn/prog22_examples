using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
    


        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("ConnectionID: {0}", Context.ConnectionId);
        }


        public async Task AddToGroup(string groupName){
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine("Joined a new client to group {0}", groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task SendMessage(string user, string message)
        {
           await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessage2(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage2", user, message);
        }

        public async Task SendPrivateMessage(string groupName, string user, string message ){
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

    }
}
