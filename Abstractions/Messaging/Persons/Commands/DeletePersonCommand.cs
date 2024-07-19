using HandsOnCQRS.Services;

namespace HandsOnCQRS.Abstractions.Messaging.Persons.Commands;

public record DeletePersonCommand(Guid Id) : ICommand<bool>;

internal sealed class DeletePersonCommandHandler(IPersonService personService) : ICommandHandler<DeletePersonCommand, bool>
{
    private readonly IPersonService _personService = personService;

    public Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        return _personService.DeleteAsync(request.Id);
    }
}
