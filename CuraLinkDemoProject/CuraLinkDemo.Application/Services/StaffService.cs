using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly CuraLinkDbContext _context;

        public StaffService(CuraLinkDbContext context)
        {
            _context = context;
        }

        // Получить всех сотрудников
        public async Task<IEnumerable<StaffDto>> GetAllAsync()
        {
            return await _context.Staff
                .Select(s => new StaffDto
                {
                    Id = s.StaffId,
                    FullName = s.FullName,
                    Role = s.Role,
                    PhoneNumber = s.PhoneNumber
                })
                .ToListAsync();
        }

        // Получить сотрудника по Id
        public async Task<StaffDto?> GetByIdAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return null;

            return new StaffDto
            {
                Id = staff.StaffId,
                FullName = staff.FullName,
                Role = staff.Role,
                PhoneNumber = staff.PhoneNumber
            };
        }

        // Добавить нового сотрудника
        public async Task<StaffDto> CreateAsync(CreateStaffDto dto)
        {
            var staff = new Staff
            {
                FullName = dto.FullName,
                Role = dto.Role,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return new StaffDto
            {
                Id = staff.StaffId,
                FullName = staff.FullName,
                Role = staff.Role,
                PhoneNumber = staff.PhoneNumber
            };
        }

        // Обновить данные сотрудника
        public async Task<bool> UpdateAsync(int id, CreateStaffDto dto)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;

            staff.FullName = dto.FullName;
            staff.Role = dto.Role;
            staff.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить сотрудника
        public async Task<bool> DeleteAsync(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return false;

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
