
using DotnetCoreApi.Data;
using Microsoft.EntityFrameworkCore;

public  static class DependencyInjection
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            if (connectionString is null)
            {
                Environment.Exit(1);
            }
            services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(connectionString));
            return services;
        }
    }
