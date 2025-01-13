using Project.Persistence;

namespace Project.Identity.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.RegisterPersistence();

            builder.Services.AddRazorPages(); 

            var app = builder.Build();

            app.ConfigurePersistence();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapRazorPages()
                .RequireAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            ThreadPool.SetMinThreads(50, 50);

            app.Run();
        }
    }
}
