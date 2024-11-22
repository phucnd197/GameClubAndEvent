using FluentValidation;
using GameClubAndEvent.Api.Common;
using GameClubAndEvent.Api.Contracts.Request;
using GameClubAndEvent.Api.Contracts.Response;
using GameClubAndEvent.Api.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameClubAndEvent.Api.Features.CreateClub;

public record CreateClubCommand(CreateClubRequest CreateClubData) : IRequest<Result<CreateClubResponse>>;

public class CreateClubCommandHandler : IRequestHandler<CreateClubCommand, Result<CreateClubResponse>>
{
    private readonly GameDbContext _dbContext;
    private readonly IValidator<CreateClubRequest> _validator;
    private readonly ILogger<CreateClubCommandHandler> _logger;

    public CreateClubCommandHandler(GameDbContext dbContext, IValidator<CreateClubRequest> validator, ILogger<CreateClubCommandHandler> logger)
    {
        _dbContext = dbContext;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<CreateClubResponse>> Handle(CreateClubCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.CreateClubData, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Fail<CreateClubResponse>(validationResult.Errors.Select(x => x.ErrorMessage).ToArray(), 400);
        }

        if (await _dbContext.Clubs.AnyAsync(x => x.Name == request.CreateClubData.Name, cancellationToken))
        {
            return Result.Fail<CreateClubResponse>(["Game club already existed."], 400);
        }

        var newClub = request.CreateClubData.ToClubEntity();
        using (var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var insertResult = await _dbContext.AddAsync(newClub, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Exception: {Message}", ex.Message);
                return Result.Fail<CreateClubResponse>(["Error creating club."]);
            }
        }

        return Result.Success(new CreateClubResponse(newClub.Name, newClub.Description), 201);
    }
}
