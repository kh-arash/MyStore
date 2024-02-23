using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyStore.Database
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var basePath = AppContext.BaseDirectory;

            var builder = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var build = new DbContextOptionsBuilder<ApplicationDbContext>();
            build.UseSqlServer(configuration.GetConnectionString("MSSqlServer"));
            return new ApplicationDbContext(build.Options);
        }
    }
}
