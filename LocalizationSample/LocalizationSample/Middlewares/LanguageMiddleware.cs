using System.Globalization;

namespace LocalizationSample.Middlewares;

public class LanguageMiddleware
{
    private readonly RequestDelegate _next;

    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        var culture = "uz";

        if(httpContext.Request.Query.ContainsKey("culture"))
        {
            culture = httpContext.Request.Query["culture"];
        }

        SetCulture(culture);

        return _next(httpContext);
    }

    private void SetCulture(string culture)
    {
        if (culture == "uz")
        {
            CultureInfo.CurrentUICulture = new CultureInfo("Uz");
        }
        else if (culture == "en")
        {
            CultureInfo.CurrentUICulture = new CultureInfo("En");
        }
        else if (culture == "ru")
        {
            CultureInfo.CurrentUICulture = new CultureInfo("Ru");
        }
    }
}

public static class LanguageMiddlewareExtensions
{
    public static IApplicationBuilder UseLanguageMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LanguageMiddleware>();
    }
}