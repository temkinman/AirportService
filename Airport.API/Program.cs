using System.Text.Json;
using System.Text.Json.Serialization;
using Airport.API.Mapping;
using Airport.Application;
using Airport.Application.Airports.Queries;
using Airport.Application.Behaviour;
using Airport.Application.Middlewares;
using Airport.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;

// using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// builder.Services.Configure<JsonSerializerOptions>(options =>
// {
//     options.PropertyNameCaseInsensitive = true;
//     options.PropertyNamingPolicy = null;
//     options.Converters.Add(new JsonStringEnumConverter());
// });
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(GetAirportInfoByIataHandler).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<GetAirportInfoByIataValidator>(ServiceLifetime.Transient);

builder.Services.AddAutoMapper(typeof(AirportRequestProfile).Assembly);

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseMiddleware<CustomExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
