using FluentValidation;
using MediatR;
using NewsScrapingMonolithic.Application.Common.Exceptions;

namespace NewsScrapingMonolithic.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        
        var context = new ValidationContext<TRequest>(request);
        var validationTasks = _validators.Select(x => x.ValidateAsync(context, cancellationToken)).ToList();

        var errors = validationTasks
            .SelectMany(task => task.Result.Errors)
            .Where(error => error != null)
            .Select(error => error)
            .Distinct()
            .ToArray();

        var errorsCodes = errors.Select(e => e.ErrorCode).ToArray();
        var errorsMessages = errors.Select(e => e.ErrorMessage).ToArray();
        
        if (errorsCodes.Contains("409")) throw new ConflictException(errorsMessages);
        if (errors.Any()) throw new BadRequestException(errorsMessages);
        return await next();
    }
}