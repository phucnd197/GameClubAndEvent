namespace GameClubAndEvent.Api.Infrastructure.Models;

public class Event
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }

    public virtual Club Club { get; set; }
}
