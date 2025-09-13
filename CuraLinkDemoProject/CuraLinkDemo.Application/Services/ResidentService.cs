using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class ResidentService : IResidentService
    {
        private readonly CuraLinkDbContext _context;

        public ResidentService(CuraLinkDbContext context)
        {
            _context = context;
        }

        // Alle Bewohner
        public async Task<IEnumerable<ResidentDto>> GetAllAsync()
        {
            return await _context.Residents
                .Select(r => new ResidentDto
                {
                    Id = r.ResidentId,
                    FullName = r.FullName,
                    RoomNumber = r.RoomNumber,
                    CareLevel = r.CareLevel,
                    DateOfBirth = r.DateOfBirth,
                    Notes = r.Notes
                })
                .ToListAsync();
        }

        // Bewohner nach Id finden
        public async Task<ResidentDto?> GetByIdAsync(int id)
        {
            var r = await _context.Residents.FindAsync(id);
            if (r == null) return null;

            return new ResidentDto
            {
                Id = r.ResidentId,
                FullName = r.FullName,
                RoomNumber = r.RoomNumber,
                CareLevel = r.CareLevel,
                DateOfBirth = r.DateOfBirth,
                Notes = r.Notes
            };
        }

        public async Task<ResidentWithAppointmentsDto?> GetResidentWithAppointmentsAsync(int id)
        {
            var resident = await _context.Residents
                .Include(r => r.Appointments) // Laden die Aufgaben
                .FirstOrDefaultAsync(r => r.ResidentId == id);

            if (resident == null) return null;

            return new ResidentWithAppointmentsDto
            {
                Id = resident.ResidentId,
                FullName = resident.FullName,
                RoomNumber = resident.RoomNumber,
                CareLevel = resident.CareLevel,
                DateOfBirth = resident.DateOfBirth,
                Notes = resident.Notes,
                Appointments = resident.Appointments
                    .Select(a => new AppointmentDto
                    {
                        Id = a.AppointmentId,
                        ResidentName = resident.FullName,
                        DateTime = a.DateTime,
                        Type = a.Type,
                        Notes = a.Notes
                    }).ToList()
            };
        }

        // Создать нового жителя
        public async Task<ResidentDto> CreateAsync(CreateResidentDto dto)
        {
            var resident = new Resident
            {
                FullName = dto.FullName,
                RoomNumber = dto.RoomNumber,
                CareLevel = dto.CareLevel,
                DateOfBirth = dto.DateOfBirth,
                Notes = dto.Notes
            };

            _context.Residents.Add(resident);
            await _context.SaveChangesAsync();

            return new ResidentDto
            {
                Id = resident.ResidentId,
                FullName = resident.FullName,
                RoomNumber = resident.RoomNumber,
                CareLevel = resident.CareLevel,
                DateOfBirth = resident.DateOfBirth,
                Notes = resident.Notes
            };
        }

        // Обновить данные жителя
        public async Task<bool> UpdateAsync(int id, CreateResidentDto dto)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident == null) return false;

            resident.FullName = dto.FullName;
            resident.RoomNumber = dto.RoomNumber;
            resident.CareLevel = dto.CareLevel;
            resident.DateOfBirth = dto.DateOfBirth;
            resident.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return true;
        }

        // Удалить жителя
        public async Task<bool> DeleteAsync(int id)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident == null) return false;

            _context.Residents.Remove(resident);
            await _context.SaveChangesAsync();
            return true;
        }

        Task<ResidentDto> IResidentService.CreateAsync(CreateResidentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
