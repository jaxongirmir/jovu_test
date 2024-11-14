using AssetManagementService.Infrastructure;

namespace AssetManagementService.APIs;

public class LaptopsService : LaptopsServiceBase
{
    public LaptopsService(AssetManagementServiceDbContext context)
        : base(context) { }
}
