using HandsOnCQRS.DTOs;
using HandsOnCQRS.Services;

namespace HandsOnCQRS.Abstractions.Messaging.Persons.Queries;

public record GetAllPersonsQuery() : IQuery<IEnumerable<PersonDTO>>;

internal sealed class GetAllPersonsQueryHandler(IPersonService personService) : IQueryHandler<GetAllPersonsQuery, IEnumerable<PersonDTO>>
{
    private readonly IPersonService _personService = personService;

    public Task<IEnumerable<PersonDTO>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        return _personService.GetAllAsync();
    }
}
