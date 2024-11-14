using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class LaptopsControllerBase : ControllerBase
{
    protected readonly ILaptopsService _service;

    public LaptopsControllerBase(ILaptopsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Laptop
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Laptop>> CreateLaptop(LaptopCreateInput input)
    {
        var laptop = await _service.CreateLaptop(input);

        return CreatedAtAction(nameof(Laptop), new { id = laptop.Id }, laptop);
    }

    /// <summary>
    /// Delete one Laptop
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteLaptop([FromRoute()] LaptopWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteLaptop(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Laptops
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Laptop>>> Laptops([FromQuery()] LaptopFindManyArgs filter)
    {
        return Ok(await _service.Laptops(filter));
    }

    /// <summary>
    /// Meta data about Laptop records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> LaptopsMeta(
        [FromQuery()] LaptopFindManyArgs filter
    )
    {
        return Ok(await _service.LaptopsMeta(filter));
    }

    /// <summary>
    /// Get one Laptop
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Laptop>> Laptop([FromRoute()] LaptopWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Laptop(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Laptop
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateLaptop(
        [FromRoute()] LaptopWhereUniqueInput uniqueId,
        [FromQuery()] LaptopUpdateInput laptopUpdateDto
    )
    {
        try
        {
            await _service.UpdateLaptop(uniqueId, laptopUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
