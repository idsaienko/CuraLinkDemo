using Microsoft.EntityFrameworkCore;

namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int StaffId { get; set; }
        public Staff Staff { get; set; }
    }
}
