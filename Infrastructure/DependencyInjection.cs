using System.Reflection;
using FluentValidation;
using GameClubAndEvent.Api.Features.CreateClub;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GameClubAndEvent.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContext<GameDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("GameConnection"));
        });

        services.AddValidatorsFromAssemblyContaining<CreateClubRequestValidator>();

        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        if (environment.IsDevelopment())
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.SQLite($"{Environment.CurrentDirectory}/logs.db", batchSize: 1)
                .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.SQLite($"{Environment.CurrentDirectory}/logs.db", batchSize: 1)
                .CreateLogger();
        }

        services.AddSerilog();

        return services;
    }
}
