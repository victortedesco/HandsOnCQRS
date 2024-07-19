namespace HandsOnCQRS.Requests;

public record AddPersonRequest(string Name, int Age, string TaxId);