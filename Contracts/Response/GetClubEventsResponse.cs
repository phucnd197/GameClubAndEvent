namespace GameClubAndEvent.Api.Contracts.Response;

public record GetClubEventsResponse(string Title, string? Description, DateTime ScheduledTime);
