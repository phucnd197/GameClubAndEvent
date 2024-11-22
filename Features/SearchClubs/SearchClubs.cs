using GameClubAndEvent.Api.Common;
using GameClubAndEvent.Api.Contracts.Response;
using GameClubAndEvent.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameClubAndEvent.Api.Features.SearchClubs;

public record SearchClubsQuery(ClubQuery QueryParams) : IRequest<Result<IList<GetClubResponse>>>;
public class SearchClubsQueryHandler : IRequestHandler<SearchClubsQuery, Result<IList<GetClubResponse>>>
{
    private readonly GameDbContext _dbContext;

    public SearchClubsQueryHandler(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IList<GetClubResponse>>> Handle(SearchClubsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Clubs.AsNoTracking();
        var (name, description, created) = request.QueryParams;
        if (!string.IsNullOrEmpty(name))
        {
            // search for string ignore case
            name = name.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(name));
        }
        if (!string.IsNullOrEmpty(description))
        {
            description = description.ToLower();
            query = query.Where(x => x.Description != null && x.Description.ToLower().Contains(description));
        }
        if (created is not null)
        {
            var createdDate = new DateTime(created.Value.Year, created.Value.Month, created.Value.Day);
            query = query.Where(x => x.Created.Date == createdDate);
        }

        IList<GetClubResponse> clubs = await query
            .Select(x => new GetClubResponse(x.Id, x.Name, x.Description, x.Created))
            .ToArrayAsync(cancellationToken);
        return Result.Success(clubs);
    }
}
