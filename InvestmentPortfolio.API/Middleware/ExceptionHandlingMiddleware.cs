using InvestmentPortfolio.API.Response;
using InvestmentPortfolio.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace InvestmentPortfolio.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        //catch (ValidationException ex)
        //{
        //    _logger.LogWarning("Validation error occurred");

        //    var problemDetails = new ProblemDetails
        //    {
        //        Status = StatusCodes.Status400BadRequest,
        //        Type = "ValidationFailure",
        //        Title = "Validation error",
        //        Detail = "One or more validation errors has occurred"
        //    };

        //    if (ex.Errors is not null)
        //    {
        //        problemDetails.Extensions["errors"] = ex.Errors;
        //    }

        //    await WriteResponseAsync(context, problemDetails, HttpStatusCode.BadRequest);
        //}
        catch (Exception ex) when (ex is ProductIsInativeException || ex is ResourceNotFoundException || ex is InvalidOperationException)
        {
            _logger.LogWarning(ex.Message);

            var response = ResponseBase.ResponseBaseFactory(HttpStatusCode.BadRequest, ex.Message);

            await WriteResponseAsync<ResponseBase>(context, response, HttpStatusCode.NotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            var response = ResponseBase.ResponseBaseFactory(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.");

            await WriteResponseAsync<ResponseBase>(context, response, HttpStatusCode.InternalServerError);
        }
    }

    public async Task WriteResponseAsync<T>(HttpContext context, T response, HttpStatusCode httpStatusCode)
    {

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}
