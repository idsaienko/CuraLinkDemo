using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Application.Services;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- Настройка сервисов ----------

// Подключение к базе данных (SQL Server)
/*builder.Services.AddDbContext<CuraLinkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Подключаем контроллеры (Web API)
builder.Services.AddControllers();

// Swagger для теста API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Подключение CORS (чтобы фронтенд мог дергать API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Регистрация сервисов приложения (DI)
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();
builder.Services.AddScoped<IVitalSignsService, VitalSignsService>();
builder.Services.AddScoped<IPainObservationService, PainObservationService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// LLM-сервис
builder.Services.AddScoped<ILLMService, LLMService>();

// HTTP-клиент для вызова внешнего API LLM (например, OpenAI)
builder.Services.AddHttpClient<OpenAILLMClient>();
*/
var app = builder.Build();

// ---------- Middleware pipeline ----------

// Swagger только в Dev-режиме
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();