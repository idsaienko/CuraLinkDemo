using CuraLinkDemoProject.CuraLinkDemo.Api.Models;
using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Application.Services;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class MockLLMService : ILLMService
    {
        public Task<ReportAnalysisResult> ExtractReportDataAsync(string reportText)
        {
            Console.WriteLine("MockLLMService: Returning fake data");

            // Return fake data for testing
            var result = new ReportAnalysisResult
            {
                MealSchedules = new List<MealScheduleDto>
                {
                    new MealScheduleDto
                    {
                        MealType = "Frühstück",
                        MealTime = DateTime.Now.Date.AddHours(8),
                        Comments = "From report: " + reportText.Substring(0, Math.Min(50, reportText.Length)),
                        MealName = "Breakfast"
                    },
                    new MealScheduleDto
                    {
                        MealType = "Mittagessen",
                        MealTime = DateTime.Now.Date.AddHours(12),
                        Comments = "Lunch",
                        MealName = "Lunch"
                    }
                },
                Movements = new List<ResidentMovementDto>
                {
                    new ResidentMovementDto
                    {
                        StaffId = 1,
                        Room = "Zimmer",
                        Object = "Stuhl",
                        Angle = 45,
                        MovementTime = DateTime.Now.AddHours(-2),
                        Notes = "Movement test"
                    }
                },
                Ausscheidungen = new List<AusscheidungDto>
                {
                    new AusscheidungDto
                    {
                        StaffId = 1,
                        Abstand = "normal",
                        Menge = "mittel",
                        Konsistenz = "fest",
                        Time = DateTime.Now.AddHours(-1)
                    }
                }
            };

            return Task.FromResult(result);
        }

        public Task<ReportAnalysisResult> AnalyzeReportAsync(string reportText)
        {
            throw new NotImplementedException("Use ExtractReportDataAsync instead");
        }

        public Task<string> TranscribeAudioAsync(Stream audioStream)
        {
            return Task.FromResult("Mock transcription");
        }
    }
}