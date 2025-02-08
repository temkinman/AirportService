using System.Text.Json.Serialization;
using System.Text.Json;
using Airport.API.Mapping;
using Airport.Application.Airports.Queries.GetAirportInfoByIata;
using Airport.Application.Behaviour;
using FluentValidation;

namespace Airport.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddSingleton(s =>
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null
            };
            options.Converters.Add(new JsonStringEnumConverter());

            return options;
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(GetAirportInfoByIataHandler).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining<GetAirportInfoByIataValidator>(ServiceLifetime.Transient);

        services.AddAutoMapper(typeof(AirportRequestProfile).Assembly);

        services.AddHttpClient();

        return services;
    }
}
