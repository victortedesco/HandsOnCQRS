using HandsOnCQRS.DTOs;
using HandsOnCQRS.Models;

namespace HandsOnCQRS.Extensions;

public static class PersonDTOExtensions
{
    public static IEnumerable<PersonDTO> ToDTO(this IEnumerable<Person> persons)
    {
        return persons.Select(p => p.ToDTO());
    }

    public static PersonDTO ToDTO(this Person person)
    {
        return new PersonDTO(person.Id, person.Name, person.Age, person.TaxId);
    }
}
