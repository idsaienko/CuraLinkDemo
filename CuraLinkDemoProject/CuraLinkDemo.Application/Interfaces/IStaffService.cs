using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetAllAsync();

        Task<StaffDto?> GetByIdAsync(int id);

        Task<StaffDto> CreateAsync(CreateStaffDto dto);

        Task<bool> UpdateAsync(int id, CreateStaffDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
