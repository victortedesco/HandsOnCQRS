using HandsOnCQRS.Abstractions;

namespace HandsOnCQRS.Models
{
    public class Person(Guid id, string name, int age, string taxId) : IEntity
    {
        public Guid Id { get; private set; } = id;
        public string Name { get; set; } = name;
        public int Age { get; set; } = age;
        public string TaxId { get; set; } = taxId;
    }
}
