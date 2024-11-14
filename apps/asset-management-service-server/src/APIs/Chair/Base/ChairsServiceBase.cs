using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using AssetManagementService.APIs.Extensions;
using AssetManagementService.Infrastructure;
using AssetManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementService.APIs;

public abstract class ChairsServiceBase : IChairsService
{
    protected readonly AssetManagementServiceDbContext _context;

    public ChairsServiceBase(AssetManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Chair
    /// </summary>
    public async Task<Chair> CreateChair(ChairCreateInput createDto)
    {
        var chair = new ChairDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            chair.Id = createDto.Id;
        }

        _context.Chairs.Add(chair);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ChairDbModel>(chair.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Chair
    /// </summary>
    public async Task DeleteChair(ChairWhereUniqueInput uniqueId)
    {
        var chair = await _context.Chairs.FindAsync(uniqueId.Id);
        if (chair == null)
        {
            throw new NotFoundException();
        }

        _context.Chairs.Remove(chair);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Chairs
    /// </summary>
    public async Task<List<Chair>> Chairs(ChairFindManyArgs findManyArgs)
    {
        var chairs = await _context
            .Chairs.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return chairs.ConvertAll(chair => chair.ToDto());
    }

    /// <summary>
    /// Meta data about Chair records
    /// </summary>
    public async Task<MetadataDto> ChairsMeta(ChairFindManyArgs findManyArgs)
    {
        var count = await _context.Chairs.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Chair
    /// </summary>
    public async Task<Chair> Chair(ChairWhereUniqueInput uniqueId)
    {
        var chairs = await this.Chairs(
            new ChairFindManyArgs { Where = new ChairWhereInput { Id = uniqueId.Id } }
        );
        var chair = chairs.FirstOrDefault();
        if (chair == null)
        {
            throw new NotFoundException();
        }

        return chair;
    }

    /// <summary>
    /// Update one Chair
    /// </summary>
    public async Task UpdateChair(ChairWhereUniqueInput uniqueId, ChairUpdateInput updateDto)
    {
        var chair = updateDto.ToModel(uniqueId);

        _context.Entry(chair).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Chairs.Any(e => e.Id == chair.Id))
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
