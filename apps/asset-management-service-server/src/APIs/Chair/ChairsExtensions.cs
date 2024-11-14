using AssetManagementService.APIs.Dtos;
using AssetManagementService.Infrastructure.Models;

namespace AssetManagementService.APIs.Extensions;

public static class ChairsExtensions
{
    public static Chair ToDto(this ChairDbModel model)
    {
        return new Chair
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ChairDbModel ToModel(
        this ChairUpdateInput updateDto,
        ChairWhereUniqueInput uniqueId
    )
    {
        var chair = new ChairDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            chair.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            chair.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return chair;
    }
}
