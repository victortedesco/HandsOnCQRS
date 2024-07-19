using HandsOnCQRS.Abstractions;

namespace HandsOnCQRS.DTOs;

public record PersonDTO(Guid Id, string Name, int Age, string TaxId) : IDTO;
