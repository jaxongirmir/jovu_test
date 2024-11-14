using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[ApiController()]
public class PhonesController : PhonesControllerBase
{
    public PhonesController(IPhonesService service)
        : base(service) { }
}
