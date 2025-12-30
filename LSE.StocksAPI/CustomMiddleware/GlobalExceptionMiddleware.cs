using LSE.Application.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

namespace LSE.StocksAPI.CustomMiddleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomValidationException ex)
            {
                Log.Warning(ex, "Validation error");
                await WriteErrorResponse(context, HttpStatusCode.BadRequest, ex.Errors);
            }
            catch (ArgumentException ex)
            {
                Log.Warning(ex, "Business validation error");
                await WriteErrorResponse(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Warning(ex, "Resource not found");
                await WriteErrorResponse(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled internal server error");
                await WriteErrorResponse(context, HttpStatusCode.InternalServerError,
                    "An unexpected error occurred. Please try again later.");
            }
        }

        private static Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, object errorDetails)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = statusCode,
                errors = errorDetails,
                timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
