using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebAPI.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureSwagger(this WebApplication webApp)
    {
        webApp.UseSwagger();

        webApp.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Reminders API");
            options.DisplayRequestDuration();
            options.EnableDeepLinking();
            options.EnableFilter();
            options.EnableValidator();
            options.EnableTryItOutByDefault();
            options.DocExpansion(DocExpansion.None);
        });

        webApp
            .MapGet("/", () => Results.Redirect("/swagger/index.html"))
            .WithTags(string.Empty);
    }
}