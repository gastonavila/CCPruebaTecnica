using Application.Interfaces;
using Application.Solicitudes;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Seed;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Database
// -----------------------------
var dbFolder = Path.Combine(AppContext.BaseDirectory, "Database");
Directory.CreateDirectory(dbFolder); // crea la carpeta si no existe
var dbPath = Path.Combine(dbFolder, "app.db");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});

// -----------------------------
// DI repositories
// -----------------------------
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IPrestadorRepository, PrestadorRepository>();
builder.Services.AddScoped<IEstudioRepository, EstudioRepository>();

// Handler
builder.Services.AddScoped<CrearSolicitudHandler>();
builder.Services.AddScoped<ConsultarDatos>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // crea la base si no existe
    var seedPath = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seed", "seed-data.json");

    if (File.Exists(seedPath))
    {
        var json = File.ReadAllText(seedPath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var seed = JsonSerializer.Deserialize<SeedModel>(json, options);

        // Solo insertar si la tabla está vacía para evitar duplicados por Id
        if (seed?.Pacientes != null && seed.Pacientes.Any() && !db.Pacientes.Any())
            db.Pacientes.AddRange(seed.Pacientes);

        if (seed?.Medicos != null && seed.Medicos.Any() && !db.Medicos.Any())
            db.Medicos.AddRange(seed.Medicos);

        if (seed?.Prestadores != null && seed.Prestadores.Any() && !db.Prestadores.Any())
            db.Prestadores.AddRange(seed.Prestadores);

        if (seed?.Estudios != null && seed.Estudios.Any() && !db.Estudios.Any())
            db.Estudios.AddRange(seed.Estudios);

        db.SaveChanges();
    }
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;
});
app.MapControllers();
app.Run();
