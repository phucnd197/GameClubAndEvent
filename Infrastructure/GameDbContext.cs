using System.Reflection;
using GameClubAndEvent.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameClubAndEvent.Api.Infrastructure;

public class GameDbContext : DbContext
{
    public virtual DbSet<Club> Clubs { get; set; }
    public virtual DbSet<Event> Events { get; set; }

    public GameDbContext() : base() { }
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
