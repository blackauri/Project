using Project.Domain.IdentityEntities;
using Project.Infrastructure;

namespace Project.Identity.Client
{
    public static class ApiExtensions
    {
        public static IHostApplicationBuilder RegisterApiClient(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(ProjectIdentityItems.IdentityResources)
                .AddInMemoryApiScopes(ProjectIdentityItems.ApiScopes)
                .AddAspNetIdentity<ProjectUser>()
                .AddDeveloperSigningCredential();

            return builder;
        }
        public static IApplicationBuilder ConfigureApiClient(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();
            app.MapRazorPages()
                .RequireAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            return app;
        }
    }
}
