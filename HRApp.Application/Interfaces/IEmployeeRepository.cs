using HRApp.Application.Models;

namespace HRApp.Application.Interfaces;

public interface IEmployeeRepository
{
  List<Employee> GetAll();
  Employee? GetById(int id);
  void Add(Employee employee);
  void Update(Employee employee);
  void Delete(Employee employee);
}