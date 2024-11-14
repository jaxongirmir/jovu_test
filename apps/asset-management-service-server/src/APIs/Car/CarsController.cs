using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[ApiController()]
public class CarsController : CarsControllerBase
{
    public CarsController(ICarsService service)
        : base(service) { }
}
