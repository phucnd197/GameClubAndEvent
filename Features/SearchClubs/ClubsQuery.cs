namespace GameClubAndEvent.Api.Features.SearchClubs;

public record ClubQuery(string? Name, string? Description, DateOnly? Created);
