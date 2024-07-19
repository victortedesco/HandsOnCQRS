using HandsOnCQRS.DTOs;
using HandsOnCQRS.Services;

namespace HandsOnCQRS.Abstractions.Messaging.Persons.Queries;

public record GetPersonByTaxIdQuery(string TaxId) : IQuery<PersonDTO?>;

internal sealed class GetPersonByTaxIdQueryHandler(IPersonService personService) : IQueryHandler<GetPersonByTaxIdQuery, PersonDTO?>
{
    private readonly IPersonService _personService = personService;

    public Task<PersonDTO?> Handle(GetPersonByTaxIdQuery request, CancellationToken cancellationToken)
    {
        return _personService.GetByTaxIdAsync(request.TaxId);
    }
}
