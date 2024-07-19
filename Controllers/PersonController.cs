using HandsOnCQRS.Abstractions.Messaging.Persons.Commands;
using HandsOnCQRS.Abstractions.Messaging.Persons.Queries;
using HandsOnCQRS.Extensions;
using HandsOnCQRS.Requests;
using HandsOnCQRS.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HandsOnCQRS.Controllers;

[ApiController]
[Route("person")]
public class PersonController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllPersonsQuery();
        var result = await _sender.Send(command);

        if (!result.Any())
            return NoContent();

        return Ok(result.ToViewModel());
    }

    [HttpGet("id/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var command = new GetPersonByIdQuery(id);
        var result = await _sender.Send(command);

        if (result is null)
            return NotFound();

        return Ok(result.ToViewModel());
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByName(string name)
    {
        var command = new GetPersonsByNameQuery(name);
        var result = await _sender.Send(command);

        if (!result.Any())
            return NoContent();

        return Ok(result.ToViewModel());
    }

    [HttpGet("taxid/{taxId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByTaxId(string taxId)
    {
        var command = new GetPersonByTaxIdQuery(taxId);
        var result = await _sender.Send(command);

        if (result is null)
            return NotFound();

        return Ok(result.ToViewModel());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PersonViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(AddPersonRequest request)
    {
        var command = new AddPersonCommand(request);
        var person = await _sender.Send(command);

        if (person is null)
            return BadRequest();

        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person.ToViewModel());
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, UpdatePersonRequest request)
    {
        var command = new UpdatePersonCommand(id, request);
        var isUpdated = await _sender.Send(command);

        return isUpdated ? Ok() : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeletePersonCommand(id);
        var isDeleted = await _sender.Send(command);

        return isDeleted ? Ok() : NotFound();
    }
}
