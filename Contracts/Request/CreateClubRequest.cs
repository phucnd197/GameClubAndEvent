using GameClubAndEvent.Api.Infrastructure.Models;

namespace GameClubAndEvent.Api.Contracts.Request;

public record CreateClubRequest(string Name, string? Description)
{
    public Club ToClubEntity()
    {
        return new Club
        {
            Name = Name,
            Description = Description,
            Created = DateTime.UtcNow,
        };
    }
}

