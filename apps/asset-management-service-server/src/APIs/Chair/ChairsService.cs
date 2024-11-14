using AssetManagementService.Infrastructure;

namespace AssetManagementService.APIs;

public class ChairsService : ChairsServiceBase
{
    public ChairsService(AssetManagementServiceDbContext context)
        : base(context) { }
}
