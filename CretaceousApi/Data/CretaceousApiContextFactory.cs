// Using the CretaceousApiContextFactory file to provide more information to Ef Core on how to create the CretaceousApiContext

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CretaceousApi.Data;
    public class CretaceousApiContextFactory : IDesignTimeDbContextFactory<CretaceousApiContext>
    {
        public CretaceousApiContext CreateDbContext(string[] args)
        {
            // Create a new configuration builder and set the path to appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the connection string from the configuration
            var optionsBuilder = new DbContextOptionsBuilder<CretaceousApiContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Use MySQL or SQL Server depending on your setup
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new CretaceousApiContext(optionsBuilder.Options);
        }
    }


// Since the error persisted, I added a Design time factory for my DbContext
// This factory ensures EF Core can instantiate your DbContext when running design-time commands like dotnet ef migrations add.
// The Design-Time Factory (IDesignTimeDbContextFactory) is a handy tool for cases like this where EF Core needs a bit more guidance on how to create an instance of your DbContext at design time, especially when using dotnet ef commands.

