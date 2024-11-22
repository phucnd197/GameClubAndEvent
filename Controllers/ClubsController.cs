using GameClubAndEvent.Api.Common;
using GameClubAndEvent.Api.Contracts.Request;
using GameClubAndEvent.Api.Features.CreateClub;
using GameClubAndEvent.Api.Features.GetAllClubs;
using GameClubAndEvent.Api.Features.SearchClubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameClubAndEvent.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClubsController
{
    private readonly IMediator _mediator;

    public ClubsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClub(CreateClubRequest club, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateClubCommand(club), cancellationToken);
        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetClubs([FromQuery] ClubQuery? queryParams, CancellationToken cancellationToken)
    {
        var result = queryParams is null
            ? await _mediator.Send(new GetAllClubsQuery(), cancellationToken)
            : await _mediator.Send(new SearchClubsQuery(queryParams), cancellationToken);
        return result.ToActionResult();
    }

}
