using AssetManagementService.APIs;
using AssetManagementService.APIs.Common;
using AssetManagementService.APIs.Dtos;
using AssetManagementService.APIs.Errors;
using AssetManagementService.APIs.Extensions;
using AssetManagementService.Infrastructure;
using AssetManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementService.APIs;

public abstract class PeopleServiceBase : IPeopleService
{
    protected readonly AssetManagementServiceDbContext _context;

    public PeopleServiceBase(AssetManagementServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Person
    /// </summary>
    public async Task<Person> CreatePerson(PersonCreateInput createDto)
    {
        var person = new PersonDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            person.Id = createDto.Id;
        }

        _context.People.Add(person);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PersonDbModel>(person.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Person
    /// </summary>
    public async Task DeletePerson(PersonWhereUniqueInput uniqueId)
    {
        var person = await _context.People.FindAsync(uniqueId.Id);
        if (person == null)
        {
            throw new NotFoundException();
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many People
    /// </summary>
    public async Task<List<Person>> People(PersonFindManyArgs findManyArgs)
    {
        var people = await _context
            .People.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return people.ConvertAll(person => person.ToDto());
    }

    /// <summary>
    /// Meta data about Person records
    /// </summary>
    public async Task<MetadataDto> PeopleMeta(PersonFindManyArgs findManyArgs)
    {
        var count = await _context.People.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Person
    /// </summary>
    public async Task<Person> Person(PersonWhereUniqueInput uniqueId)
    {
        var people = await this.People(
            new PersonFindManyArgs { Where = new PersonWhereInput { Id = uniqueId.Id } }
        );
        var person = people.FirstOrDefault();
        if (person == null)
        {
            throw new NotFoundException();
        }

        return person;
    }

    /// <summary>
    /// Update one Person
    /// </summary>
    public async Task UpdatePerson(PersonWhereUniqueInput uniqueId, PersonUpdateInput updateDto)
    {
        var person = updateDto.ToModel(uniqueId);

        _context.Entry(person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.People.Any(e => e.Id == person.Id))
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
