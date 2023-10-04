using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
    
        private readonly IConfiguration configuration;

        public ChatHub(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("ConnectionID: {0}", Context.ConnectionId);
        }

        [Authorize]
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

        public async Task CreateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, "test"),
                new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            

            await Clients.All.SendAsync("CreateToken", tokenHandler.WriteToken(tokenHandler.CreateToken(securityTokenDescriptor)));
        }

    }
}
