using AssetManagementService.Infrastructure;

namespace AssetManagementService.APIs;

public class PhonesService : PhonesServiceBase
{
    public PhonesService(AssetManagementServiceDbContext context)
        : base(context) { }
}
