using HRApp.Application.DTO;

namespace HRApp.Application.Interfaces;

public interface IEmployeeService{
    public List<EmployeeDTO> GetAll();
    public List<EmployeeDTO> GetPaged(int page, int pageSize, out int totalItems);
    public List<EmployeeDTO> GetPaged(int page, int pageSize, string search, out int totalItems);
    public EmployeeDTO? GetById(int id);
    string GenerateNextEmployeeId();
    public void Add(EmployeeDTO employee);
    public void Update(EmployeeDTO employee);
    public void Delete(int id);
}