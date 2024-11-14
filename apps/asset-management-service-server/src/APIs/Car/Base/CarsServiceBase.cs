using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using AssetManagementService.APIs.Extensions;
using AssetManagementService.Infrastructure;
using AssetManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementService.APIs;

public abstract class CarsServiceBase : ICarsService
{
    protected readonly AssetManagementServiceDbContext _context;

    public CarsServiceBase(AssetManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Car
    /// </summary>
    public async Task<Car> CreateCar(CarCreateInput createDto)
    {
        var car = new CarDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            car.Id = createDto.Id;
        }

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CarDbModel>(car.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Car
    /// </summary>
    public async Task DeleteCar(CarWhereUniqueInput uniqueId)
    {
        var car = await _context.Cars.FindAsync(uniqueId.Id);
        if (car == null)
        {
            throw new NotFoundException();
        }

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Cars
    /// </summary>
    public async Task<List<Car>> Cars(CarFindManyArgs findManyArgs)
    {
        var cars = await _context
            .Cars.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return cars.ConvertAll(car => car.ToDto());
    }

    /// <summary>
    /// Meta data about Car records
    /// </summary>
    public async Task<MetadataDto> CarsMeta(CarFindManyArgs findManyArgs)
    {
        var count = await _context.Cars.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Car
    /// </summary>
    public async Task<Car> Car(CarWhereUniqueInput uniqueId)
    {
        var cars = await this.Cars(
            new CarFindManyArgs { Where = new CarWhereInput { Id = uniqueId.Id } }
        );
        var car = cars.FirstOrDefault();
        if (car == null)
        {
            throw new NotFoundException();
        }

        return car;
    }

    /// <summary>
    /// Update one Car
    /// </summary>
    public async Task UpdateCar(CarWhereUniqueInput uniqueId, CarUpdateInput updateDto)
    {
        var car = updateDto.ToModel(uniqueId);

        _context.Entry(car).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Cars.Any(e => e.Id == car.Id))
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
