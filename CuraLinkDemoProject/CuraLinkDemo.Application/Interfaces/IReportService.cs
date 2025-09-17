using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces
{
    public interface IReportService
    {
        Task<object> ProcessReportAsync(ReportDto dto);
    }
}
