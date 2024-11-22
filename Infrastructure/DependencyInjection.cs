using System.Reflection;
using FluentValidation;
using GameClubAndEvent.Api.Features.CreateClub;
using Microsoft.EntityFrameworkCore;

namespace GameClubAndEvent.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
        return services;
    }
}
