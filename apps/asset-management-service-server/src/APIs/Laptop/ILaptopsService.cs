using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;

namespace AssetManagementService.APIs;

public interface ILaptopsService
{
    /// <summary>
    /// Create one Laptop
    /// </summary>
    public Task<Laptop> CreateLaptop(LaptopCreateInput laptop);

    /// <summary>
    /// Delete one Laptop
    /// </summary>
    public Task DeleteLaptop(LaptopWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Laptops
    /// </summary>
    public Task<List<Laptop>> Laptops(LaptopFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Laptop records
    /// </summary>
    public Task<MetadataDto> LaptopsMeta(LaptopFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Laptop
    /// </summary>
    public Task<Laptop> Laptop(LaptopWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Laptop
    /// </summary>
    public Task UpdateLaptop(LaptopWhereUniqueInput uniqueId, LaptopUpdateInput updateDto);
}
