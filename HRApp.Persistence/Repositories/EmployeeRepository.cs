using HRApp.Application.Interfaces;
using HRApp.Application.Models;
using HRApp.Persistence.InMemory;

namespace HRApp.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly InMemoryDbContext _context;

    public EmployeeRepository(InMemoryDbContext context)
    {
        _context = context;
    }

    public void Add(Employee employee)
    {
        _context.Add(employee);
        _context.SaveChanges();
    }

    public void Delete(Employee employee)
    {
        _context.Remove(employee);
        _context.SaveChanges();
    }

    public List<Employee> GetAll()
    {
        return _context.Employees.ToList();
    }

    public Employee? GetById(int id)
    {
      return _context.Employees.SingleOrDefault(e => e.Id == id);
    }

    public void Update(Employee employee)
    {
        var existing = GetById(employee.Id);
        if (existing == null) return;
        existing.EmployeeID = employee.EmployeeID;
        existing.Name = employee.Name;
        existing.BirthPlace = employee.BirthPlace;
        existing.BirthDate = employee.BirthDate;
        existing.BasicSalary = employee.BasicSalary;
        existing.Gender = employee.Gender;
        existing.MaritalStatus = employee.MaritalStatus;
        _context.SaveChanges();
    }
}