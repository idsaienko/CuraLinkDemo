using CuraLinkDemoProject.CuraLinkDemo.Api.Models;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class ReportAnalysisResult
    {
        public List<MedicationDto> Medications { get; set; } = new();
        public List<PainObservationDto> PainObservations { get; set; } = new();
        public List<ResidentMovementDto> Movements { get; set; } = new();
        public List<MealScheduleDto>? MealSchedules { get; set; }
        public List<AusscheidungDto>? Ausscheidungen { get; set; }
    }
}
