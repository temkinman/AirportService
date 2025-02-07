using Airport.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Airport.Application;

public static  class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AirportMappingProfile).Assembly);

        return services;
    }
}