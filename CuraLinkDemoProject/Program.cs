using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Application.Services;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Check if files exist
var settingsPath = Path.Combine(builder.Environment.ContentRootPath, "appsettings.json");
var devSettingsPath = Path.Combine(builder.Environment.ContentRootPath, "appsettings.Development.json");


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
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ILLMService, LLMService>();

// Configure HttpClient for OpenAI
builder.Services.AddHttpClient("OpenAI", client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/");
    var apiKey = builder.Configuration["LLM:ApiKey"];
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
});

var llmApiKey = builder.Configuration["LLM:ApiKey"];
Console.WriteLine($"LLM API Key loaded: {(string.IsNullOrEmpty(llmApiKey) ? "NULL/EMPTY" : "Found (length: " + llmApiKey.Length + ")")}");

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
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "https://www.curalink.care",
            "https://curalink.care",
            "http://www.curalink.care",
            "http://curalink.care",
            "http://localhost:57378"
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
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

app.UseCors("AllowFrontend");

//app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();