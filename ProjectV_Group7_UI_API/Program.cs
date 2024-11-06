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
builder.Services.AddSingleton<SurvivorInformationService>(); // Register SurvivorInformationService here
builder.Services.AddSingleton<AddNotificationModal>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
