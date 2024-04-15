using UssJuniorTest.Core;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Infrastructure.Repositories;
using UssJuniorTest.Infrastructure.Store;
using UssJuniorTest.Logic;

namespace UssJuniorTest;

public static class ServiceCollectionExtensions
{
    public static void RegisterAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IStore, InMemoryStore>();

        services.AddScoped<IRepository<Car>, CarRepository>();
        services.AddScoped<IRepository<Person>, PersonRepository>();
        services.AddScoped<IRepository<DriveLog>, DriveLogRepository>();
        services.AddScoped<IAllRepositories, AllRepositories>();
        services.AddTransient<IDriveLogManager, DriveLogManager>();
    }
}