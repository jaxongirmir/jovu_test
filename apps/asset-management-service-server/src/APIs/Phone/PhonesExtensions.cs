using AssetManagementService.APIs.Dtos;
using AssetManagementService.Infrastructure.Models;

namespace AssetManagementService.APIs.Extensions;

public static class PhonesExtensions
{
    public static Phone ToDto(this PhoneDbModel model)
    {
        return new Phone
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PhoneDbModel ToModel(
        this PhoneUpdateInput updateDto,
        PhoneWhereUniqueInput uniqueId
    )
    {
        var phone = new PhoneDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            phone.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            phone.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return phone;
    }
}
