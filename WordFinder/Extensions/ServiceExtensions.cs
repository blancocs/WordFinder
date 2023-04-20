using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApiVersiongExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}
