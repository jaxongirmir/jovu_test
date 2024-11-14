using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using AssetManagementService.APIs.Extensions;
using AssetManagementService.Infrastructure;
using AssetManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementService.APIs;

public abstract class PhonesServiceBase : IPhonesService
{
    protected readonly AssetManagementServiceDbContext _context;

    public PhonesServiceBase(AssetManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Phone
    /// </summary>
    public async Task<Phone> CreatePhone(PhoneCreateInput createDto)
    {
        var phone = new PhoneDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            phone.Id = createDto.Id;
        }

        _context.Phones.Add(phone);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PhoneDbModel>(phone.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Phone
    /// </summary>
    public async Task DeletePhone(PhoneWhereUniqueInput uniqueId)
    {
        var phone = await _context.Phones.FindAsync(uniqueId.Id);
        if (phone == null)
        {
            throw new NotFoundException();
        }

        _context.Phones.Remove(phone);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Phones
    /// </summary>
    public async Task<List<Phone>> Phones(PhoneFindManyArgs findManyArgs)
    {
        var phones = await _context
            .Phones.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return phones.ConvertAll(phone => phone.ToDto());
    }

    /// <summary>
    /// Meta data about Phone records
    /// </summary>
    public async Task<MetadataDto> PhonesMeta(PhoneFindManyArgs findManyArgs)
    {
        var count = await _context.Phones.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Phone
    /// </summary>
    public async Task<Phone> Phone(PhoneWhereUniqueInput uniqueId)
    {
        var phones = await this.Phones(
            new PhoneFindManyArgs { Where = new PhoneWhereInput { Id = uniqueId.Id } }
        );
        var phone = phones.FirstOrDefault();
        if (phone == null)
        {
            throw new NotFoundException();
        }

        return phone;
    }

    /// <summary>
    /// Update one Phone
    /// </summary>
    public async Task UpdatePhone(PhoneWhereUniqueInput uniqueId, PhoneUpdateInput updateDto)
    {
        var phone = updateDto.ToModel(uniqueId);

        _context.Entry(phone).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Phones.Any(e => e.Id == phone.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
