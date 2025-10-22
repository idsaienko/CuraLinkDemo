using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using CuraLinkDemoProject.CuraLinkDemo.Api.Models;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly ILLMService _llmService;
        private readonly CuraLinkDbContext _context;

        public ReportService(ILLMService llmService, CuraLinkDbContext context)
        {
            _llmService = llmService;
            _context = context;
        }

        public async Task<ReportAnalysisResult> ProcessReportAsync(ReportDto dto)
        {
            // 1. Analyze the report with ChatGPT
            var analysis = await _llmService.ExtractReportDataAsync(dto.TextReport);

            // 2. Save MealSchedules to database
            if (analysis.MealSchedules?.Any() == true)
            {
                MealSchedule mealSchedule = new();
                foreach (var meal in analysis.MealSchedules)
                {
                    mealSchedule.ResidentId = dto.ResidentId;
                    mealSchedule.Id = meal.Id;
                    mealSchedule.MealName = meal.MealName;
                    mealSchedule.MealType = meal.MealType;
                    mealSchedule.Comments = meal.Comments;
                    mealSchedule.MealTime = meal.MealTime;
                    _context.MealSchedules.Add(mealSchedule);
                }
            }

            // 3. Save Movements to database
            if (analysis.Movements?.Any() == true)
            {
                ResidentMovement residentMovement = new();
                foreach (var movement in analysis.Movements)
                {
                    residentMovement.ResidentId = dto.ResidentId;
                    residentMovement.Resident = movement.Resident;
                    residentMovement.StaffId = movement.StaffId;
                    residentMovement.Staff = movement.Staff;
                    residentMovement.Room = movement.Room;
                    residentMovement.Object = movement.Object;
                    residentMovement.Angle = movement.Angle;
                    residentMovement.Notes = movement.Notes;
                    _context.ResidentMovements.Add(residentMovement);
                }
            }

            // 4. Save Ausscheidungen to database
            if (analysis.Ausscheidungen?.Any() == true)
            {
                Ausscheidung ausscheidungTemp = new();
                foreach (var ausscheidung in analysis.Ausscheidungen)
                {
                    ausscheidungTemp.ResidentId = dto.ResidentId;
                    ausscheidungTemp.StaffId = ausscheidung.StaffId;
                    ausscheidungTemp.Time = ausscheidung.Time;
                    ausscheidungTemp.Abstand = ausscheidung.Abstand;
                    ausscheidungTemp.Menge = ausscheidung.Menge;
                    ausscheidungTemp.Konsistenz = ausscheidung.Konsistenz;
                    _context.Ausscheidungen.Add(ausscheidungTemp);
                }
            }

            // 5. Save all changes
            await _context.SaveChangesAsync();

            return analysis;
        }
    }
}