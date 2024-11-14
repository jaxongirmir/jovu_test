using AssetManagementService.APIs.Dtos;
using AssetManagementService.Infrastructure.Models;

namespace AssetManagementService.APIs.Extensions;

public static class PeopleExtensions
{
    public static Person ToDto(this PersonDbModel model)
    {
        return new Person
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PersonDbModel ToModel(
        this PersonUpdateInput updateDto,
        PersonWhereUniqueInput uniqueId
    )
    {
        var person = new PersonDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            person.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            person.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return person;
    }
}
