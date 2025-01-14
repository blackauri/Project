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

            builder.Services
                .AddIdentity<ProjectUser, IdentityRole>(setup =>
                {
                    setup.Password.RequireNonAlphanumeric = false;
                    setup.Password.RequireLowercase = false;
                    setup.Password.RequireUppercase = false;
                    setup.Password.RequireDigit = false;
                    setup.Password.RequiredLength = 1;
                    setup.Password.RequiredUniqueChars = 0;

                })
                .AddEntityFrameworkStores<ProjectIdentityDbContext>()
                .AddDefaultTokenProviders();

            return builder;
        }

        public static IApplicationBuilder ConfigurePersistence(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var db = services.GetRequiredService<ProjectIdentityDbContext>();
            var initializer = services.GetRequiredService<IDbInitializer>();

            db.Database.Migrate();

            initializer.Start().Wait();

            return app;
        }
    }
}
