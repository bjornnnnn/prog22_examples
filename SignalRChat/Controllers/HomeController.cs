using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

namespace SignalRChat.Controllers;

public class HomeController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public HomeController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send(string message)
    {
        Console.WriteLine("Rec message: {0}", message);
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"Message at: {DateTime.Now}>> {message} ");
        return Ok();
    }

}