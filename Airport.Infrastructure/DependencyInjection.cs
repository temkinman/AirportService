using Airport.Application.Interfaces;
using Airport.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airport.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddScoped<IPatientRepository, PatientRepository>();
        // services.AddScoped<INameRepository, NameRepository>();

        services.AddDbContext<IAirportDbContext, AirportDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(nameof(AirportDbContext)))
                .LogTo(Console.WriteLine));

        return services;
    }
}