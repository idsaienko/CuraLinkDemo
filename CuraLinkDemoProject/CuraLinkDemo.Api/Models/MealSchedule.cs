using CuraLinkDemoProject.CuraLinkDemo.Domain.Entities;

namespace CuraLinkDemoProject.CuraLinkDemo.Api.Models
{
    public class MealSchedule
    {
        public int Id { get; set; }

        public int ResidentId { get; set; }
        public Resident Resident { get; set; } = null!;

        public string MealType { get; set; } = string.Empty;

        public DateTime MealTime { get; set; }

        public string DietaryNotes { get; set; } = string.Empty;

        public string Menu { get; set; } = string.Empty;
    }
}
