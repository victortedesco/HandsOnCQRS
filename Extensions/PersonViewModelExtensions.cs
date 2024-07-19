using HandsOnCQRS.DTOs;
using HandsOnCQRS.ViewModel;

namespace HandsOnCQRS.Extensions;

public static class PersonViewModelExtensions
{
    public static IEnumerable<PersonViewModel> ToViewModel(this IEnumerable<PersonDTO> persons)
    {
        return persons.Select(p => p.ToViewModel());
    }

    public static PersonViewModel ToViewModel(this PersonDTO person)
    {
        return new PersonViewModel(person.Id, person.Name, person.Age, person.TaxId);
    }
}
