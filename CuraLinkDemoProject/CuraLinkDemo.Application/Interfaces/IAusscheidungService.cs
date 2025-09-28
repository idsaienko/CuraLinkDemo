using CuraLinkDemoProject.CuraLinkDemo.Api.Models;
using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces
{
    public interface IAusscheidungService
    {
        Task<Ausscheidung> AddAsync(AusscheidungDto dto);
        Task<IEnumerable<Ausscheidung>> GetByResidentAsync(int residentId);
    }
}
