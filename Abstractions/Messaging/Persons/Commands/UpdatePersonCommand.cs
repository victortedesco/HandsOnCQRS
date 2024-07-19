using HandsOnCQRS.DTOs;
using HandsOnCQRS.Requests;
using HandsOnCQRS.Services;

namespace HandsOnCQRS.Abstractions.Messaging.Persons.Commands;

public record UpdatePersonCommand(Guid Id, UpdatePersonRequest Person) : ICommand<bool>;

internal sealed class UpdatedPersonCommandHandler(IPersonService personService) : ICommandHandler<UpdatePersonCommand, bool>
{
    private readonly IPersonService _personService = personService;

    public Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new PersonDTO(request.Id, request.Person.Name, request.Person.Age, request.Person.TaxId);
        return _personService.UpdateAsync(request.Id, person);
    }
}
