using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using SignalRChat.Controllers;

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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
