using Application.Features.Movie.CreateMovie;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Application.auMapper;

// using moviesGestion.auMapper; // Cet using pourrait changer selon l'emplacement de MappingProfile
using moviesGestion.Hubs;
using moviesGestion.repositories;
using System.Reflection;

// --- 1. D�finir un nom pour la politique CORS --- (INCHANG�)
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// --- 2. Ajouter les services CORS --- (INCHANG�)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5500", "null")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

// --- Configuration des Services ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// (INCHANG�) - Enregistrement du DbContext
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseNpgsql(connectionString));

// (NOUVEAU) - Enregistrement de MediatR
// Cette ligne va scanner le projet "Application" et trouver tous vos CommandHandlers et QueryHandlers.
// On utilise typeof(CreateMovieCommand) pour donner � MediatR une r�f�rence vers l'assembly "Application".
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateMovieCommand).Assembly));

// (INCHANG�) - Injection de votre Repository g�n�rique
builder.Services.AddScoped(typeof(ITRepository<>), typeof(Trepository<>));

// (INCHANG�) - Injection d'AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// (INCHANG�) - Services restants
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- Le reste du fichier est identique ---

// --- Configuration du Pipeline de Requ�tes HTTP ---

// Appliquer les migrations de la base de donn�es au d�marrage
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MovieDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");

app.Run();