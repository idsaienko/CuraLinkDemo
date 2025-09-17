namespace CuraLinkDemoProject.CuraLinkDemo.Application.DTOs
{
    public class MealScheduleDto
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public string MealType { get; set; } = string.Empty;
        public DateTime MealTime { get; set; }
        public string DietaryNotes { get; set; } = string.Empty;
        public string Menu { get; set; } = string.Empty;
    }
}
