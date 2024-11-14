using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;

namespace AssetManagementService.APIs;

public interface IChairsService
{
    /// <summary>
    /// Create one Chair
    /// </summary>
    public Task<Chair> CreateChair(ChairCreateInput chair);

    /// <summary>
    /// Delete one Chair
    /// </summary>
    public Task DeleteChair(ChairWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Chairs
    /// </summary>
    public Task<List<Chair>> Chairs(ChairFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Chair records
    /// </summary>
    public Task<MetadataDto> ChairsMeta(ChairFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Chair
    /// </summary>
    public Task<Chair> Chair(ChairWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Chair
    /// </summary>
    public Task UpdateChair(ChairWhereUniqueInput uniqueId, ChairUpdateInput updateDto);
}
