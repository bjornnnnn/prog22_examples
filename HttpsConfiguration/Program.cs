using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpsRedirection(options =>
{
    //options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect; <-- detta kommando fungerar ocks�
    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
    //options.HttpsPort = <portnummer>; <-- skriv in portnumret du vill s�nda vidare otill�tna http-anrop till
});


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

app.Run();
