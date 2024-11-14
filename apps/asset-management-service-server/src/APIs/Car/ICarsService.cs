using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;

namespace AssetManagementService.APIs;

public interface ICarsService
{
    /// <summary>
    /// Create one Car
    /// </summary>
    public Task<Car> CreateCar(CarCreateInput car);

    /// <summary>
    /// Delete one Car
    /// </summary>
    public Task DeleteCar(CarWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Cars
    /// </summary>
    public Task<List<Car>> Cars(CarFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Car records
    /// </summary>
    public Task<MetadataDto> CarsMeta(CarFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Car
    /// </summary>
    public Task<Car> Car(CarWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Car
    /// </summary>
    public Task UpdateCar(CarWhereUniqueInput uniqueId, CarUpdateInput updateDto);
}
