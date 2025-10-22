using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Data
{
    public class CuraLinkDbContextFactory : IDesignTimeDbContextFactory<CuraLinkDbContext>
    {
        public CuraLinkDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("C:\\Users\\idsai\\dev\\CuraLinkDemo\\CuraLinkDemoProject\\appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CuraLinkDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new CuraLinkDbContext(optionsBuilder.Options);
        }
    }
}
