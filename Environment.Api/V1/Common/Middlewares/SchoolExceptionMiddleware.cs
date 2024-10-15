using Environment.Api.V1.Common.ExceptionError;
using Environment.Api.V1.Common.ResponseType;

namespace Environment.Api.V1.Common.Middlewares;

public class SchoolExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SchoolExceptionMiddleware> _logger;

    public SchoolExceptionMiddleware(RequestDelegate next, ILogger<SchoolExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (SchoolException ex)
        {
            await HandleException(context, ex.Code, ex.Message, ex.Global);
        }
        catch (Exception ex)
        {
            //Log
            _logger.LogError(ex.ToString());

            await HandleException(context, 500, "", true);
        }
    }

    public async Task HandleException(HttpContext context, int code, string message, bool? Global)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(
            new ResponseModel<string>
            {
                Status = false,
                Error = message,
                Data = null,
                GlobalError = Global
            }
        );
    }
}
