using EMG_MED1000_BACKEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//Ajout de la configuration de la base de données; donc du DbContext
builder.Services.AddDbContext<VoitureContext>(
    Options => Options.UseMySql(
        builder.Configuration.GetConnectionString("VoitureDb"),
        new MySqlServerVersion(new Version(8, 0, 40))
    )
);

// Enregistrement des services
builder.Services.AddScoped<VoitureService>();
builder.Services.AddScoped<MarqueService>();
builder.Services.AddScoped<ModeleService>();

//Ajout des controllers
builder.Services.AddControllers();

//Ajout des services nécessaires pour API
builder.Services.AddControllersWithViews();

//Ajout de Cors pour les échanges de connexions depuis notre front-end
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseStaticFiles();

app. UseRouting();

app.UseAuthorization();

app.MapControllers();
    
app.Run();
