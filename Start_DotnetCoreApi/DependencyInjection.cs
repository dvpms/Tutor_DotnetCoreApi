
using DotnetCoreApi.Data;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (connectionString is null)
        {
            Environment.Exit(1);
        }
        
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 36)); // Sesuaikan versi MySQL yang Anda gunakan
        
        services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, serverVersion));
        
        return services;
    }
}