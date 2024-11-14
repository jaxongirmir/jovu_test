using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CarsControllerBase : ControllerBase
{
    protected readonly ICarsService _service;

    public CarsControllerBase(ICarsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Car
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Car>> CreateCar(CarCreateInput input)
    {
        var car = await _service.CreateCar(input);

        return CreatedAtAction(nameof(Car), new { id = car.Id }, car);
    }

    /// <summary>
    /// Delete one Car
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCar([FromRoute()] CarWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCar(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Cars
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Car>>> Cars([FromQuery()] CarFindManyArgs filter)
    {
        return Ok(await _service.Cars(filter));
    }

    /// <summary>
    /// Meta data about Car records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CarsMeta([FromQuery()] CarFindManyArgs filter)
    {
        return Ok(await _service.CarsMeta(filter));
    }

    /// <summary>
    /// Get one Car
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Car>> Car([FromRoute()] CarWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Car(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Car
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCar(
        [FromRoute()] CarWhereUniqueInput uniqueId,
        [FromQuery()] CarUpdateInput carUpdateDto
    )
    {
        try
        {
            await _service.UpdateCar(uniqueId, carUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
