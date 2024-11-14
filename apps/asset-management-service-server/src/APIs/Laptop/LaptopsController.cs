using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[ApiController()]
public class LaptopsController : LaptopsControllerBase
{
    public LaptopsController(ILaptopsService service)
        : base(service) { }
}
