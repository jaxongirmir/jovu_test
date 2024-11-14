using AssetManagementService.Infrastructure;

namespace AssetManagementService.APIs;

public class PeopleService : PeopleServiceBase
{
    public PeopleService(AssetManagementServiceDbContext context)
        : base(context) { }
}
