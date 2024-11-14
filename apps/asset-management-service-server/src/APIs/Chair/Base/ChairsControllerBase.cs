using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ChairsControllerBase : ControllerBase
{
    protected readonly IChairsService _service;

    public ChairsControllerBase(IChairsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Chair
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Chair>> CreateChair(ChairCreateInput input)
    {
        var chair = await _service.CreateChair(input);

        return CreatedAtAction(nameof(Chair), new { id = chair.Id }, chair);
    }

    /// <summary>
    /// Delete one Chair
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteChair([FromRoute()] ChairWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteChair(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Chairs
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Chair>>> Chairs([FromQuery()] ChairFindManyArgs filter)
    {
        return Ok(await _service.Chairs(filter));
    }

    /// <summary>
    /// Meta data about Chair records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ChairsMeta([FromQuery()] ChairFindManyArgs filter)
    {
        return Ok(await _service.ChairsMeta(filter));
    }

    /// <summary>
    /// Get one Chair
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Chair>> Chair([FromRoute()] ChairWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Chair(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Chair
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateChair(
        [FromRoute()] ChairWhereUniqueInput uniqueId,
        [FromQuery()] ChairUpdateInput chairUpdateDto
    )
    {
        try
        {
            await _service.UpdateChair(uniqueId, chairUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
