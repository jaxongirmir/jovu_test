using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PeopleControllerBase : ControllerBase
{
    protected readonly IPeopleService _service;

    public PeopleControllerBase(IPeopleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Person
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Person>> CreatePerson(PersonCreateInput input)
    {
        var person = await _service.CreatePerson(input);

        return CreatedAtAction(nameof(Person), new { id = person.Id }, person);
    }

    /// <summary>
    /// Delete one Person
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeletePerson([FromRoute()] PersonWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePerson(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many People
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Person>>> People([FromQuery()] PersonFindManyArgs filter)
    {
        return Ok(await _service.People(filter));
    }

    /// <summary>
    /// Meta data about Person records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PeopleMeta([FromQuery()] PersonFindManyArgs filter)
    {
        return Ok(await _service.PeopleMeta(filter));
    }

    /// <summary>
    /// Get one Person
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Person>> Person([FromRoute()] PersonWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Person(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Person
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdatePerson(
        [FromRoute()] PersonWhereUniqueInput uniqueId,
        [FromQuery()] PersonUpdateInput personUpdateDto
    )
    {
        try
        {
            await _service.UpdatePerson(uniqueId, personUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
