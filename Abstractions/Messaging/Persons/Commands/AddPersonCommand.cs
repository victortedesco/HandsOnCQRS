using HandsOnCQRS.DTOs;
using HandsOnCQRS.Requests;
using HandsOnCQRS.Services;

namespace HandsOnCQRS.Abstractions.Messaging.Persons.Commands;

public record AddPersonCommand(AddPersonRequest Person) : ICommand<PersonDTO?>;

internal sealed class AddPersonCommandHandler(IPersonService personService) : ICommandHandler<AddPersonCommand, PersonDTO?>
{
    private readonly IPersonService _personService = personService;

    public async Task<PersonDTO?> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        var person = new PersonDTO(Guid.NewGuid(), request.Person.Name, request.Person.Age, request.Person.TaxId);

        return await _personService.AddAsync(person);
    }
}
