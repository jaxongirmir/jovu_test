using AssetManagementService.Infrastructure;

namespace AssetManagementService.APIs;

public class CarsService : CarsServiceBase
{
    public CarsService(AssetManagementServiceDbContext context)
        : base(context) { }
}
