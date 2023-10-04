using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using SignalRChat.Hubs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRChat.Controllers;

public class HomeController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IConfiguration configuration;

    public HomeController(IHubContext<ChatHub> hubContext, IConfiguration configuration)
    {
        _hubContext = hubContext;
        this.configuration = configuration;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send(string message)
    {
        Console.WriteLine("Rec message: {0}", message);
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"Message at: {DateTime.Now}>> {message} ");
        return Ok();
    }

 



}