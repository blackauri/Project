using Project.Application;
using Project.Infrastructure;
using Project.Persistence;

namespace Project.Identity.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder
                .RegisterApplication()
                .RegisterPersistence()
                .RegisterInfrastructure()
                .RegisterApiClient();

            var app = builder.Build();

            app.ConfigureApplication();
            app.ConfigurePersistence();
            app.ConfigureInfrastructure();
            app.ConfigureApiClient();

            ThreadPool.SetMinThreads(50, 50);

            app.Run();
        }
    }
}
