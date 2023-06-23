using BankRateAggregator.Application.Exceptions;
using BankRateAggregator.WebAPI.HttpResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BankRateAggregator.WebAPI.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ForbiddenException), HandleForbiddenException },
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(OperationCanceledException), HandleOperationCanceledException }
        };

    }

    public override Task OnExceptionAsync(ExceptionContext context)
    {
        HandleException(context);
        return base.OnExceptionAsync(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out var value))
        {
            value.Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
        }
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
        var details = new BadRequest(exception.Errors);
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleForbiddenException(ExceptionContext context)
    {
        var exception = (ForbiddenException)context.Exception;
        var details = new Forbidden(exception.Message);

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
        context.ExceptionHandled = true;
    }


    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
        var details = new BadRequest(exception.Message);
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;
        var details = new NotFound(exception.Message);
        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleOperationCanceledException(ExceptionContext context)
    {
        var details = new CancelledRequest();
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

}