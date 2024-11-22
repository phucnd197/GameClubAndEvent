using GameClubAndEvent.Api.Common;
using GameClubAndEvent.Api.Contracts.Response;
using GameClubAndEvent.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameClubAndEvent.Api.Features.GetAllClubs;

public record GetAllClubsQuery : IRequest<Result<IList<GetClubResponse>>>;
public class GetAllClubsQueryHandler : IRequestHandler<GetAllClubsQuery, Result<IList<GetClubResponse>>>
{
    private readonly GameDbContext _dbContext;

    public GetAllClubsQueryHandler(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IList<GetClubResponse>>> Handle(GetAllClubsQuery request, CancellationToken cancellationToken)
    {
        IList<GetClubResponse> clubs = await _dbContext.Clubs
            .Select(x => new GetClubResponse
            (
                x.Id,
                x.Name,
                x.Description,
                x.Created
            ))
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
        return Result.Success(clubs);
    }
}
