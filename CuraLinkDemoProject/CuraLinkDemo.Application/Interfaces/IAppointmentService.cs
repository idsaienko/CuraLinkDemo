using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAsync();
        Task<AppointmentDto?> GetByIdAsync(int id);
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto);
        Task<IEnumerable<AppointmentDto>> GetByResidentIdAsync(int residentId);
        Task<bool> UpdateAsync(int id, CreateAppointmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
