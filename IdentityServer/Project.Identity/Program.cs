using Project.Persistence;

namespace Project.Identity.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder
                .RegisterPersistence()
                .RegisterApiClient();

            var app = builder.Build();

            app.ConfigurePersistence();
            app.ConfigureApiClient();

            ThreadPool.SetMinThreads(50, 50);

            app.Run();
        }
    }
}
