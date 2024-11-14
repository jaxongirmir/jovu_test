using AssetManagementService.APIs.Dtos;
using AssetManagementService.Infrastructure.Models;

namespace AssetManagementService.APIs.Extensions;

public static class CarsExtensions
{
    public static Car ToDto(this CarDbModel model)
    {
        return new Car
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CarDbModel ToModel(this CarUpdateInput updateDto, CarWhereUniqueInput uniqueId)
    {
        var car = new CarDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            car.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            car.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return car;
    }
}
