using Microsoft.EntityFrameworkCore;
using AzureMapsControl.Components;
using NearMe.Data;
using NearMe.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Connection string from User Secrets or appsettings.json
builder.Services.AddDbContext<BlazorStoreFinderContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.UseNetTopologySuite()));

builder.WebHost.UseStaticWebAssets();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddGeolocationServices();


var azureMapsConfig = builder.Configuration.GetSection("AzureMaps");

builder.Services.AddAzureMapsControl(configuration => configuration.ClientId =
    azureMapsConfig.GetValue<string>("ClientId"));

builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<StoreLocationService>();

var app = builder.Build();

NearMe.Services.AuthService.SetAuthSettings(azureMapsConfig);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to chan=ge this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
