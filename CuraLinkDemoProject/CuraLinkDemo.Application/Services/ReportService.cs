using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly CuraLinkDbContext _context;
        private readonly ILLMService _llmService;

        public ReportService(CuraLinkDbContext context, ILLMService llmService)
        {
            _context = context;
            _llmService = llmService;
        }

        public async Task<object> ProcessReportAsync(ReportDto dto)
        {
            var structured = await _llmService.ExtractReportDataAsync(dto.TextReport);

            foreach (var med in structured.Medications)
            {
                _context.Medications.Add(new Medication
                {
                    Name = med.Name,
                    Dosage = med.Dosage,
                    StartDate = med.StartDate,
                    ResidentId = dto.ResidentId
                });
            }

            foreach (var pain in structured.PainObservations)
            {
                _context.PainObservations.Add(new PainObservation
                {
                    ResidentId = dto.ResidentId,
                    Score = pain.Score,
                    Location = pain.Location,
                    Notes = pain.Notes,
                    Time = pain.Time
                });
            }

            foreach (var move in structured.Movements)
            {
                _context.ResidentMovements.Add(new ResidentMovement
                {
                    ResidentId = dto.ResidentId,
                    StaffId = dto.StaffId,
                    Room = move.Room,
                    Object = move.Object,
                    Angle = move.Angle,
                    MovementTime = move.MovementTime,
                    Notes = move.Notes
                });
            }

            await _context.SaveChangesAsync();

            return structured;
        }
    }
}
