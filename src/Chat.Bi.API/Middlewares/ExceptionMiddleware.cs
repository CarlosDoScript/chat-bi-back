using Chat.Bi.Infrastructure.Logging;
using System.Net;
using System.Text.Json;

namespace Chat.Bi.API.Middlewares;

public class ExceptionMiddleware
{
    readonly RequestDelegate _next;
    readonly IAppLogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, IAppLogger<ExceptionMiddleware> logger)
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
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
                return;

            var statusCode = StatusCode(ex);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Title = "Erro inesperado na aplicação",
                Status = statusCode,
                Instance = context.Request.Path,
                Detail = "Ocorreu um erro inesperado.\n Tente novamente mais tarde."
            };

            _logger.LogError(ex, "Erro global capturado pelo middleware", new
            {
                Caminho = context.Request.Path,
                Metodo = context.Request.Method,
                Status = statusCode,
                Mensagem = ex.Message,
                Stack = ex.StackTrace
            });

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }

        static int StatusCode(Exception ex)
            => ex switch
            {
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                ArgumentException or ArgumentNullException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
    }
}