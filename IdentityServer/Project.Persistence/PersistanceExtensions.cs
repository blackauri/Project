using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Domain.IdentityEntities;
using System.Reflection;

namespace Project.Persistence
{
    public static class PersistanceExtensions
    {
        public static IHostApplicationBuilder RegisterPersistence(this IHostApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<ProjectIdentityDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    });

                    if (!builder.Environment.IsProduction())
                    {
                        options.LogTo(msg => System.Diagnostics.Debug.WriteLine(msg));
                        options.EnableDetailedErrors(true);
                        options.EnableSensitiveDataLogging(true);
                    }
                }, ServiceLifetime.Scoped);

            builder.Services.AddIdentity<ProjectUser, IdentityRole>()
                .AddEntityFrameworkStores<ProjectIdentityDbContext>()
                .AddDefaultTokenProviders();

            return builder;
        }

        public static IApplicationBuilder ConfigurePersistence(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<ProjectIdentityDbContext>();

            db.Database.Migrate();

            db.Seed().Wait();

            return app;
        }
    }
}
