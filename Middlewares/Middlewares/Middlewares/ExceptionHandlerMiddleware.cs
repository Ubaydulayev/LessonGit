namespace Middlewares.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new{error=e.Message});

            var client = new HttpClient();
            var result = await client.GetAsync($"https://api.telegram.org/bot5416132893:AAHC0lAheGsgeOjXOxglvn6YpsshMqWOcYo/sendmessage?chat_id=-841363105&text={e.Message}");
        }
    }
}