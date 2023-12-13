using Microsoft.IdentityModel.Tokens;
using System.Net.Mime;
using System.Net;

namespace Tasks.API.Services;

public class GlobalExceptionHandlingMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<GlobalExceptionHandlingMiddleWare> logger;

    public GlobalExceptionHandlingMiddleWare(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleWare> logger)
    {
        this.next = next;
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex);

        }

        async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            //TODO make an http static class to hold this value


            var (statusCode, message) = ex switch
            {
                // custom exceptions can be written here
                _ => (HttpStatusCode.InternalServerError, "Something bad happened please try again!")
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(new { StatusCode = (int)statusCode, Message = message });
        }
    }
}

