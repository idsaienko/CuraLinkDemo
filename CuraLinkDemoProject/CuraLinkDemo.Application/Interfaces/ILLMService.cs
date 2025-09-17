using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces
{
    public interface ILLMService
    {
        Task<ReportAnalysisResult> ExtractReportDataAsync(string reportText);
    }
}
