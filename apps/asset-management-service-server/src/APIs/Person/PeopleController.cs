using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[ApiController()]
public class PeopleController : PeopleControllerBase
{
    public PeopleController(IPeopleService service)
        : base(service) { }
}
