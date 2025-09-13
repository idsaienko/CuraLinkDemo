using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly CuraLinkDbContext _context;

        public AppointmentService(CuraLinkDbContext context)
        {
            _context = context;
        }

        // Aufgaben herstellen
        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
        {
            var resident = await _context.Residents.FindAsync(dto.ResidentId);
            var staff = await _context.Staff.FindAsync(dto.StaffId);

            if (resident == null) throw new Exception("Resident not found");
            if (staff == null) throw new Exception("Staff not found");

            var appointment = new Appointment
            {
                ResidentId = dto.ResidentId,
                StaffId = dto.StaffId,
                DateTime = dto.DateTime,
                Type = dto.Type,
                Notes = dto.Notes
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return new AppointmentDto
            {
                Id = appointment.AppointmentId,
                ResidentName = resident.FullName,
                StaffName = staff.FullName,
                DateTime = appointment.DateTime,
                Type = appointment.Type,
                Notes = appointment.Notes
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IEnumerable<AppointmentDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        // Alle Aufgaben des Bewohners bekommen
        public async Task<IEnumerable<AppointmentDto>> GetByResidentIdAsync(int residentId)
        {
            return await _context.Appointments
                .Where(a => a.ResidentId == residentId)
                .Include(a => a.Resident)
                .Select(a => new AppointmentDto
                {
                    Id = a.AppointmentId,
                    ResidentName = a.Resident.FullName,
                    DateTime = a.DateTime,
                    Type = a.Type,
                    Notes = a.Notes
                })
                .ToListAsync();
        }

        public Task<bool> UpdateAsync(int id, CreateAppointmentDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
