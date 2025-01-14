using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Infrastructure.Services;
using Project.Persistence;

namespace Project.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IHostApplicationBuilder RegisterInfrastructure(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();

            return builder;
        }

        public static IApplicationBuilder ConfigureInfrastructure(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
