namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string KeyHash { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
