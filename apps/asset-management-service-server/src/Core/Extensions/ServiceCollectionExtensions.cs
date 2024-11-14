using AssetManagementService.APIs;

namespace AssetManagementService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICarsService, CarsService>();
        services.AddScoped<IChairsService, ChairsService>();
        services.AddScoped<ILaptopsService, LaptopsService>();
        services.AddScoped<IPeopleService, PeopleService>();
        services.AddScoped<IPhonesService, PhonesService>();
    }
}
