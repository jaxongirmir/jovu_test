using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using AssetManagementService.APIs.Extensions;
using AssetManagementService.Infrastructure;
using AssetManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementService.APIs;

public abstract class LaptopsServiceBase : ILaptopsService
{
    protected readonly AssetManagementServiceDbContext _context;

    public LaptopsServiceBase(AssetManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Laptop
    /// </summary>
    public async Task<Laptop> CreateLaptop(LaptopCreateInput createDto)
    {
        var laptop = new LaptopDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            laptop.Id = createDto.Id;
        }

        _context.Laptops.Add(laptop);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<LaptopDbModel>(laptop.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Laptop
    /// </summary>
    public async Task DeleteLaptop(LaptopWhereUniqueInput uniqueId)
    {
        var laptop = await _context.Laptops.FindAsync(uniqueId.Id);
        if (laptop == null)
        {
            throw new NotFoundException();
        }

        _context.Laptops.Remove(laptop);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Laptops
    /// </summary>
    public async Task<List<Laptop>> Laptops(LaptopFindManyArgs findManyArgs)
    {
        var laptops = await _context
            .Laptops.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return laptops.ConvertAll(laptop => laptop.ToDto());
    }

    /// <summary>
    /// Meta data about Laptop records
    /// </summary>
    public async Task<MetadataDto> LaptopsMeta(LaptopFindManyArgs findManyArgs)
    {
        var count = await _context.Laptops.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Laptop
    /// </summary>
    public async Task<Laptop> Laptop(LaptopWhereUniqueInput uniqueId)
    {
        var laptops = await this.Laptops(
            new LaptopFindManyArgs { Where = new LaptopWhereInput { Id = uniqueId.Id } }
        );
        var laptop = laptops.FirstOrDefault();
        if (laptop == null)
        {
            throw new NotFoundException();
        }

        return laptop;
    }

    /// <summary>
    /// Update one Laptop
    /// </summary>
    public async Task UpdateLaptop(LaptopWhereUniqueInput uniqueId, LaptopUpdateInput updateDto)
    {
        var laptop = updateDto.ToModel(uniqueId);

        _context.Entry(laptop).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Laptops.Any(e => e.Id == laptop.Id))
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
