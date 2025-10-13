using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Application.Services;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Comprehensive debug
Console.WriteLine("=== CONFIGURATION DEBUG ===");
Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);
Console.WriteLine("Content Root: " + builder.Environment.ContentRootPath);
Console.WriteLine("Base Path: " + AppContext.BaseDirectory);

// Check if files exist
var settingsPath = Path.Combine(builder.Environment.ContentRootPath, "appsettings.json");
var devSettingsPath = Path.Combine(builder.Environment.ContentRootPath, "appsettings.Development.json");
Console.WriteLine($"appsettings.json exists: {File.Exists(settingsPath)}");
Console.WriteLine($"appsettings.Development.json exists: {File.Exists(devSettingsPath)}");

// Try to read the file directly
if (File.Exists(devSettingsPath))
{
    Console.WriteLine("Content of appsettings.Development.json:");
    Console.WriteLine(File.ReadAllText(devSettingsPath));
}

// Check all configuration sources
Console.WriteLine("\nConfiguration sources:");
foreach (var source in ((IConfigurationRoot)builder.Configuration).Providers)
{
    Console.WriteLine($"  - {source.GetType().Name}");
}

// Try to get connection string
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"\nConnection String: {connStr ?? "NULL"}");

// Also try alternate method
var connStr2 = builder.Configuration["ConnectionStrings:DefaultConnection"];
Console.WriteLine($"Alternate method: {connStr2 ?? "NULL"}");

Console.WriteLine("===========================\n");

// --------------------
// 1. DB Verbindung
// --------------------
builder.Services.AddDbContext<CuraLinkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
Console.WriteLine("DB Connection: " + builder.Configuration.GetConnectionString("DefaultConnection"));

// --------------------
// 2. Service Regisration
// --------------------
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IAusscheidungService, AusscheidungService>();


// --------------------
// 3. Controllers + Swagger
// --------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --------------------
// 4. CORS — Erlaubnis fürs Front zum kommunizieren mit API
// --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// --------------------
// 5. Einstellung des middleware
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();