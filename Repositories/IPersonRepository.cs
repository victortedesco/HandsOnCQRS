using HandsOnCQRS.Context;
using HandsOnCQRS.Models;
using Microsoft.EntityFrameworkCore;

namespace HandsOnCQRS.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    Task<IEnumerable<Person>> GetByNameAsync(string name);
    Task<Person?> GetByTaxIdAsync(string taxId);
}

public class PersonRepository(ApplicationDbContext dbContext) : IPersonRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly DbSet<Person> _persons = dbContext.Persons;

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _persons.ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _persons.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Person>> GetByNameAsync(string name)
    {
        return await _persons.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }

    public async Task<Person?> GetByTaxIdAsync(string taxId)
    {
        return await _persons.FirstOrDefaultAsync(p => p.TaxId == taxId);
    }

    public async Task<Person?> AddAsync(Person model)
    {
        _dbContext.Add(model);
        return await _dbContext.SaveChangesAsync().ContinueWith(_ => model);
    }

    public async Task<bool> UpdateAsync(Guid id, Person model)
    {
        var person = await _persons.FirstOrDefaultAsync(p => p.Id == id);

        if (person is null)
            return false;

        person.Name = model.Name;
        person.TaxId = model.TaxId;
        person.Age = model.Age;

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