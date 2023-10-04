using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Validation.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VehiclesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VehiclesContext") ?? throw new InvalidOperationException("Connection string 'VehiclesContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var MinFinaPolicy = "Min fina policy";
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MinFinaPolicy,
//                      policy =>
//                      {
//                          policy.WithOrigins("http://example.com",
//                                             "http://www.google.com");
//                      });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseCors(MinFinaPolicy);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehicles}/{action=Index}/{id?}");

app.Run();
