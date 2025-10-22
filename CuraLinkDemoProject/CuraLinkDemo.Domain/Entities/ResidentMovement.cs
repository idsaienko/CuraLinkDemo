namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class ResidentMovement
    {
        public int Id { get; set; }

        public int ResidentId { get; set; }
        public Resident Resident { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        public string Room { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
        public double? Angle { get; set; }
        public DateTime MovementTime { get; set; } = DateTime.Now;

        public string Notes { get; set; } = string.Empty;
    }
}
