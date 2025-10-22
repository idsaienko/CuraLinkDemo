using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Services;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces
{
    public interface ILLMService
    {
        Task<ReportAnalysisResult> ExtractReportDataAsync(string reportText);
    }
}
