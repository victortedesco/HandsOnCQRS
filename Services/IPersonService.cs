using HandsOnCQRS.DTOs;
using HandsOnCQRS.Extensions;
using HandsOnCQRS.Repositories;

namespace HandsOnCQRS.Services;

public interface IPersonService : IService<PersonDTO>
{
    Task<IEnumerable<PersonDTO>> GetByNameAsync(string name);
    Task<PersonDTO?> GetByTaxIdAsync(string taxId);
}

public class PersonService(IPersonRepository personRepository) : IPersonService
{
    private readonly IPersonRepository _personRepository = personRepository;

    public async Task<IEnumerable<PersonDTO>> GetAllAsync()
    {
        var persons = await _personRepository.GetAllAsync();

        return persons.ToDTO();
    }

    public async Task<PersonDTO?> GetByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        return person?.ToDTO();
    }

    public async Task<IEnumerable<PersonDTO>> GetByNameAsync(string name)
    {
        var persons = await _personRepository.GetByNameAsync(name);

        return persons.ToDTO();
    }

    public async Task<PersonDTO?> GetByTaxIdAsync(string taxId)
    {
        var person = await _personRepository.GetByTaxIdAsync(taxId);

        return person?.ToDTO();
    }

    public async Task<PersonDTO?> AddAsync(PersonDTO dto)
    {
        var person = await _personRepository.AddAsync(dto.ToModel());

        return person?.ToDTO();
    }

    public async Task<bool> UpdateAsync(Guid id, PersonDTO dto)
    {
        return await _personRepository.UpdateAsync(id, dto.ToModel());
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _personRepository.DeleteAsync(id);
    }
}