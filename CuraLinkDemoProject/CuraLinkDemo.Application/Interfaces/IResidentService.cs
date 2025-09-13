using CuraLinkDemoProject.CuraLinkDemo.Application.DTOs;

namespace CuraLinkDemoProject.CuraLinkDemo.Application.Interfaces
{
    public interface IResidentService
    {
        // List der allen Bewohner
        Task<IEnumerable<ResidentDto>> GetAllAsync();

        // Bewohner mit dem Id finden
        Task<ResidentDto?> GetByIdAsync(int id);

        Task<ResidentWithAppointmentsDto?> GetResidentWithAppointmentsAsync(int id);

        // Neue Bewohner erstellen
        Task<ResidentDto> CreateAsync(CreateResidentDto dto);

        // Daten über den Bewohner aktualisieren
        Task<bool> UpdateAsync(int id, CreateResidentDto dto);

        // Bewohner löschen
        Task<bool> DeleteAsync(int id);
    }
}
