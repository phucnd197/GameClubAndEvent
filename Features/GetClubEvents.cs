using GameClubAndEvent.Api.Common;
using GameClubAndEvent.Api.Contracts.Response;
using GameClubAndEvent.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameClubAndEvent.Api.Features;

public record GetClubEventsQuery(Guid ClubId) : IRequest<Result<IList<GetClubEventsResponse>>>;
public class GetClubEventsQueryHandler : IRequestHandler<GetClubEventsQuery, Result<IList<GetClubEventsResponse>>>
{
    private readonly GameDbContext _dbContext;

    public GetClubEventsQueryHandler(GameDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IList<GetClubEventsResponse>>> Handle(GetClubEventsQuery request, CancellationToken cancellationToken)
    {
        IList<GetClubEventsResponse> events = await _dbContext.Events
            .Where(x => x.ClubId == request.ClubId)
            .Select(x => new GetClubEventsResponse(x.Title, x.Description, x.ScheduledTime))
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
        return Result.Success(events);
    }
}
