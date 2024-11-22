namespace GameClubAndEvent.Api.Contracts.Response;

public record GetClubResponse(Guid Id, string Name, string? Description, DateTime Created);
