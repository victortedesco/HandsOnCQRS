using HandsOnCQRS.Context;
using HandsOnCQRS.DTOs;
using HandsOnCQRS.Extensions;
using HandsOnCQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace HandsOnCQRS.Services;

public interface IPersonService : IService<PersonDTO>
{
    Task<IEnumerable<PersonDTO>> GetByNameAsync(string name);
    Task<PersonDTO?> GetByTaxIdAsync(string taxId);
}

public class PersonService(ApplicationDbContext dbContext) : IPersonService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly DbSet<Person> _persons = dbContext.Persons;

    public async Task<IEnumerable<PersonDTO>> GetAllAsync()
    {
        var persons = await _persons.AsNoTracking().ToListAsync();

        return persons.ToDTO();
    }

    public async Task<PersonDTO?> GetByIdAsync(Guid id)
    {
        var person = await _persons.FirstOrDefaultAsync(p => p.Id == id);

        return person?.ToDTO();
    }

    public async Task<IEnumerable<PersonDTO>> GetByNameAsync(string name)
    {
        var persons = await _persons.AsNoTracking()
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return persons.ToDTO();
    }

    public async Task<PersonDTO?> GetByTaxIdAsync(string taxId)
    {
        var person = await _persons.FirstOrDefaultAsync(p => p.TaxId == taxId);

        return person?.ToDTO();
    }

    public async Task<PersonDTO?> AddAsync(PersonDTO dto)
    {
        _persons.Add(dto.ToModel());
        await _dbContext.SaveChangesAsync();

        return dto;
    }

    public async Task<bool> UpdateAsync(Guid id, PersonDTO dto)
    {
        var person = await _persons.FindAsync(id);

        if (person is null)
            return false;

        person.Name = dto.Name;
        person.Age = dto.Age;
        person.TaxId = dto.TaxId;

        _persons.Update(person);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var person = await _persons.FirstOrDefaultAsync(p => p.Id == id);

        if (person is null)
            return false;

        _persons.Remove(person);

        return await _dbContext.SaveChangesAsync() > 0;
    }
}