using Microsoft.AspNetCore.SignalR;
using SignalRChat.Controllers;
using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatHub>("/ChatHub");
app.MapControllers();

/* app.Use(async (context, next) =>
{
   var hubContext = context.RequestServices.GetRequiredService<IHubContext<ChatHub>>();
    hubContext.Index();
    
    if (next != null)
    {
        await next.Invoke();
    }
});
 */
app.Run();
