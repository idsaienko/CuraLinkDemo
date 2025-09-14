using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        public int ResidentId { get; set; }
        public Resident Resident { get; set; }
    }
}
