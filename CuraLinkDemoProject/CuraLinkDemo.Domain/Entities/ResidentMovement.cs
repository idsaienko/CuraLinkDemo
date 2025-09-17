namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class ResidentMovement
    {
        public int Id { get; set; }

        public int ResidentId { get; set; }
        public Resident Resident { get; set; } = null!;

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        public string Room { get; set; } = string.Empty;       // помещение (например "столовая")
        public string Object { get; set; } = string.Empty;     // объект ("кресло", "кровать")
        public double? Angle { get; set; }                     // угол расположения в градусах
        public DateTime MovementTime { get; set; } = DateTime.Now;

        public string Notes { get; set; } = string.Empty;
    }
}
