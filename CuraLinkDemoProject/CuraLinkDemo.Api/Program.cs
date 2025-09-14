using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Application.Services;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// 1. DB Verbindung
// --------------------
builder.Services.AddDbContext<CuraLinkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --------------------
// 2. Service Regisration
// --------------------
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IStaffService, StaffService>();

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

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();