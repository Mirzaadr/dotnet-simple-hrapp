using HRApp.Application.Models;
using Bogus;
namespace HRApp.Persistence.InMemory;

public class InMemoryDbContext
{
    private readonly List<Employee> _employees = new();
    public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

    private static int _idCounter = 1;

    public void Seed(int count = 200)
    {
        if (_employees.Any()) return; // Skip if already populated

        var faker = new Faker<Employee>()
        .CustomInstantiator(f =>
            new Employee(
                0,
                employeeId: $"EMP{f.IndexFaker + 1:D4}",
                name: f.Name.FullName(),
                birthPlace: f.Address.City(),
                birthDate: f.Date.Between(new DateTime(1970, 1, 1), new DateTime(2003, 1, 1)),
                basicSalary: f.Random.Double(3000000, 10000000),
                gender: f.PickRandom(new[] { "Male", "Female" }),
                maritalStatus: f.PickRandom(new[] { "Single", "Married", "Divorced" })
            )
        );
        var fakeEmployees = faker.Generate(count);

        foreach (var emp in fakeEmployees)
        {
            emp.Id = _idCounter++;
            _employees.Add(emp);
        }
    }

    public void Add<TEntity>(TEntity entity) where TEntity : class
    {
        if (typeof(TEntity) == typeof(Employee) && entity is not null)
        {
            (entity as Employee)!.Id = _idCounter++;
            _employees.Add((entity as Employee)!);
        }
    }
    
    public void Remove<TEntity>(TEntity entity) where TEntity : class
    {
        if (typeof(TEntity) == typeof(Employee) && entity is not null)
        {
            _employees.Remove((entity as Employee)!);
        }
    }

    public void SaveChanges()
    {}

}