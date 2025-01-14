using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Project.Application
{
    public static class ApplicationExtensions
    {
        public static IHostApplicationBuilder RegisterApplication(this IHostApplicationBuilder builder)
        {
            return builder;
        }

        public static IApplicationBuilder ConfigureApplication(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
