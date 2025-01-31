using System.Text;
using EMG_MED1000_BACKEND.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configuration de JWT, on charge les donnnees de la variable d'environnement
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

//Ajout de la configuration de la base de données; donc du DbContext pour VoitureDb
builder.Services.AddDbContext<VoitureContext>(
    Options => Options.UseMySql(
        builder.Configuration.GetConnectionString("VoitureDb"),
        new MySqlServerVersion(new Version(8, 0, 40))
    )
);

//Ajout de la configuration de la base de données MySql pour ApplicationDbContext, donc pour Identity
builder.Services.AddDbContext<ApplicationDbContext>(
    Options => Options.UseMySql(
        builder.Configuration.GetConnectionString("IdentityDb"),
        new MySqlServerVersion(new Version(8, 0, 40))
    )
);

//Configuration pour Jwt, pour l'authtentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

//Ajout d'Identity avec ApplicationDbContext
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();        

//Ajout du service pour Identity
builder.Services.AddScoped<AuthenticationService>();

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

//Ajout de l'authentication pour Identity
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
    
app.Run();
