using HandsOnCQRS.DTOs;
using HandsOnCQRS.Models;

namespace HandsOnCQRS.Extensions;

public static class PersonExtensions
{
    public static Person ToModel(this PersonDTO person)
    {
        return new Person(person.Id, person.Name, person.Age, person.TaxId);
    }
}
