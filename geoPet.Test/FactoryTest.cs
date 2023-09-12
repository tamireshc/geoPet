using geoPet.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace geoPet.Test
{
    public class FactoryTest<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<GeoPetContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<GeoPetContext>(options =>
                {
                    options.UseInMemoryDatabase("db");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>())
                {
                    try
                    {

                        appContext.Database.EnsureDeleted();

                        appContext.Database.EnsureCreated();

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            });
        }
    }
}
