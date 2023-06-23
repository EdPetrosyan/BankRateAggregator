using BankRateAggregator.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BankRateAggregator.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex) when (ex.GetType() != typeof(OperationCanceledException) &&
                                   ex.GetType() != typeof(NotFoundException) &&
                                   ex.GetType() != typeof(ValidationException) &&
                                   ex.GetType() != typeof(ForbiddenException))
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "BankRateAggregator Request: Unhandled Exception for Request {Name} {Request}", requestName, request);

            throw;
        }
    }
}
