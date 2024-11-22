namespace GameClubAndEvent.Api.Infrastructure.Models;

public class Club
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Created { get; set; }

    public virtual IList<Event> Events { get; set; } = Array.Empty<Event>();
}
