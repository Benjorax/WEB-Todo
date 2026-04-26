using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// Singleton для TaskManager
builder.Services.AddSingleton<TaskManager>();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseStaticFiles();
app.MapRazorPages();
app.Run();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
