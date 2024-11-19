using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProjectV_Group7_UI_API.Data;
using ProjectV_Group7_UI_API.Pages;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<CriticalNotificationService>();
builder.Services.AddSingleton<AddNotificationModal>();

// Register HttpClient with the application's BaseAddress
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["BaseApiUrl"] ?? "https://localhost:5001/") // Default to localhost if BaseApiUrl is not configured
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
