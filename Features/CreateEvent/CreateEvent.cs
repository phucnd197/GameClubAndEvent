using FluentValidation;
using GameClubAndEvent.Api.Common;
using GameClubAndEvent.Api.Contracts.Request;
using GameClubAndEvent.Api.Contracts.Response;
using GameClubAndEvent.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameClubAndEvent.Api.Features.CreateEvent;

public record CreateEventCommand(Guid ClubId, CreateEventRequest RequestData) : IRequest<Result<CreateEventResponse>>;
public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<CreateEventResponse>>
{
    private readonly GameDbContext _dbContext;
    private readonly IValidator<CreateEventRequest> _validator;
    private readonly ILogger<CreateEventCommandHandler> _logger;

    public CreateEventCommandHandler(GameDbContext dbContext, IValidator<CreateEventRequest> validator, ILogger<CreateEventCommandHandler> logger)
    {
        _dbContext = dbContext;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<CreateEventResponse>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.RequestData, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<CreateEventResponse>(validationResult.Errors.Select(x => x.ErrorMessage).ToArray(), 400);
        }

        if (!await _dbContext.Clubs.AnyAsync(x => x.Id == request.ClubId, cancellationToken: cancellationToken))
        {
            return Result.Fail<CreateEventResponse>("Club does not exist, please recheck.", 400);
        }

        var newEvent = request.RequestData.ToEventEntity();
        newEvent.ClubId = request.ClubId;
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                await _dbContext.AddAsync(newEvent, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.Fail<CreateEventResponse>("Error creating events for club");
            }
        }

        return Result.Success(new CreateEventResponse(newEvent.Title, newEvent.Description), 201);
    }
}
