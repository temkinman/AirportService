using Airport.Application.Interfaces;
using Airport.Infrastructure.AppDbContext;
using Airport.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Airport.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IAirportRepository, AirportRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        services.AddDbContext<IAirportDbContext, AirportDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(nameof(AirportDbContext)))
                .LogTo(Console.WriteLine));

        return services;
    }
}