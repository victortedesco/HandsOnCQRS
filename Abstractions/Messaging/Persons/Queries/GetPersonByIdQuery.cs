using HandsOnCQRS.DTOs;
using HandsOnCQRS.Services;

namespace HandsOnCQRS.Abstractions.Messaging.Persons.Queries;

public record GetPersonByIdQuery(Guid Id) : IQuery<PersonDTO?>;

internal sealed class GetPersonByIdQueryHandler(IPersonService personService) : IQueryHandler<GetPersonByIdQuery, PersonDTO?>
{
    private readonly IPersonService _personService = personService;

    public Task<PersonDTO?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        return _personService.GetByIdAsync(request.Id);
    }
}
