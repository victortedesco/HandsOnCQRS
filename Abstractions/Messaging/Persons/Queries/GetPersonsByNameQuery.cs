using HandsOnCQRS.DTOs;
using HandsOnCQRS.Services;

namespace HandsOnCQRS.Abstractions.Messaging.Persons.Queries;

public record GetPersonsByNameQuery(string Name) : IQuery<IEnumerable<PersonDTO>>;

internal sealed class GetPersonsByNameQueryHandler(IPersonService personService) : IQueryHandler<GetPersonsByNameQuery, IEnumerable<PersonDTO>>
{
    private readonly IPersonService _personService = personService;

    public Task<IEnumerable<PersonDTO>> Handle(GetPersonsByNameQuery request, CancellationToken cancellationToken)
    {
        return _personService.GetByNameAsync(request.Name);
    }
}
