using HRApp.Application.DTO;
using HRApp.Application.Interfaces;
using HRApp.Application.Models;
using Mapster;

namespace HRApp.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
      _repository = employeeRepository;
    }
    public List<EmployeeDTO> GetAll() => _repository.GetAll().Adapt<List<EmployeeDTO>>();

    public EmployeeDTO? GetById(int id) => _repository.GetById(id).Adapt<EmployeeDTO>();

    public void Add(EmployeeDTO emp)
    {
        _repository.Add(
          new Employee(
              id: emp.Id,
              employeeId: emp.EmployeeID,
              name: emp.Name,
              birthPlace: emp.BirthPlace,
              birthDate: emp.BirthDate,
              basicSalary: emp.BasicSalary,
              gender: emp.Gender,
              maritalStatus: emp.MaritalStatus
          )
        );
    }

    public void Update(EmployeeDTO emp)
    {
        var currentEmp = _repository.GetById(emp.Id);
        if (currentEmp != null)
            _repository.Update(
              new Employee(
                id: emp.Id,
                employeeId: emp.EmployeeID,
                name: emp.Name,
                birthPlace: emp.BirthPlace,
                birthDate: emp.BirthDate,
                basicSalary: emp.BasicSalary,
                gender: emp.Gender,
                maritalStatus: emp.MaritalStatus
              )
            );
    }

    public void Delete(int id)
    {
        var emp = _repository.GetById(id);
        if (emp != null)
            _repository.Delete(emp);
    }

    public List<EmployeeDTO> GetPaged(int pageNumber, int pageSize, out int totalItems)
    {
        var employees = _repository.GetAll();
        totalItems = employees.Count;
        return employees
            .Select(e => e.Adapt<EmployeeDTO>())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
    
    public List<EmployeeDTO> GetPaged(int pageNumber, int pageSize, string search, out int totalItems)
    {
        var employees = _repository.GetAll();
        var query = employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            query = query.Where(e =>
                e.EmployeeID.ToLower().Contains(search) ||
                e.Name.ToLower().Contains(search) ||
                e.BirthPlace.ToLower().Contains(search) ||
                e.Gender.ToLower().Contains(search) ||
                e.MaritalStatus.ToLower().Contains(search));
        }

        totalItems = query.Count();
        return query
            .Select(e => e.Adapt<EmployeeDTO>())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public string GenerateNextEmployeeId()
    {
        // Assumes EmployeeId format is like "EMP001"
        var lastEmployee = _repository.GetAll()
                                      .OrderByDescending(e => e.EmployeeID)
                                      .FirstOrDefault();

        int lastNumber = 0;

        if (lastEmployee != null && 
            !string.IsNullOrEmpty(lastEmployee.EmployeeID) &&
            lastEmployee.EmployeeID.Length >= 3 &&
            int.TryParse(lastEmployee.EmployeeID.Substring(3), out var parsed))
        {
            lastNumber = parsed;
        }

        int nextNumber = lastNumber + 1;
        return $"EMP{nextNumber:D4}"; // EMP001, EMP002, ...
    }
}
