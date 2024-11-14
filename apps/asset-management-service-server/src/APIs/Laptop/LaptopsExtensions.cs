using AssetManagementService.APIs.Dtos;
using AssetManagementService.Infrastructure.Models;

namespace AssetManagementService.APIs.Extensions;

public static class LaptopsExtensions
{
    public static Laptop ToDto(this LaptopDbModel model)
    {
        return new Laptop
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static LaptopDbModel ToModel(
        this LaptopUpdateInput updateDto,
        LaptopWhereUniqueInput uniqueId
    )
    {
        var laptop = new LaptopDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            laptop.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            laptop.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return laptop;
    }
}
