using GameClubAndEvent.Api.Infrastructure.Models;

namespace GameClubAndEvent.Api.Contracts.Request;

public record CreateEventRequest(string Title, string Description, DateTime ScheduledTime)
{
    public Event ToEventEntity()
    {
        return new Event
        {
            Title = Title,
            Description = Description,
            ScheduledTime = ScheduledTime,
            Created = DateTime.UtcNow,
        };
    }
}
