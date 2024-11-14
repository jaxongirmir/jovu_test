using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[ApiController()]
public class ChairsController : ChairsControllerBase
{
    public ChairsController(IChairsService service)
        : base(service) { }
}
