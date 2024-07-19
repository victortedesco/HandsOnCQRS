namespace HandsOnCQRS.Requests;

public record UpdatePersonRequest(string Name, int Age, string TaxId);