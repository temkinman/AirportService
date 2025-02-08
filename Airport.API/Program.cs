using Airport.API;
using Airport.Application;
using Airport.Application.Middlewares;
using Airport.Infrastructure;
using Airport.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<CustomExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider
            .GetRequiredService<AirportDbContext>();

        dbContext.Database.Migrate();
    }
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
