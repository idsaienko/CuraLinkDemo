using CuraLinkDemoProject.CuraLinkDemo.Api.Models;
using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;
using CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces;
using CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Services
{
    public class AusscheidungService : IAusscheidungService
    {
        private readonly CuraLinkDbContext _db;

        public AusscheidungService(CuraLinkDbContext db)
        {
            _db = db;
        }

        public async Task<Ausscheidung> AddAsync(AusscheidungDto dto)
        {
            var entity = new Ausscheidung
            {
                ResidentId = dto.ResidentId,
                StaffId = dto.StaffId,
                Time = dto.Time,
                Abstand = dto.Abstand,
                Menge = dto.Menge,
                Konsistenz = dto.Konsistenz
            };

            _db.Ausscheidungen.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Ausscheidung>> GetByResidentAsync(int residentId)
        {
            return await _db.Ausscheidungen
                .Where(x => x.ResidentId == residentId)
                .Include(x => x.Staff)
                .ToListAsync();
        }
    }
}
