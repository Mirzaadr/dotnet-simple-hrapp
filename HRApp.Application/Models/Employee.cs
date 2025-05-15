using System.ComponentModel.DataAnnotations;

namespace HRApp.Application.Models;

public class Employee
{
    public int Id { get; set; }

    public string EmployeeID { get; set; }

    public string Name { get; set; }

    public string BirthPlace { get; set; }

    public DateTime BirthDate { get; set; }

    public double BasicSalary { get; set; }

    public string Gender { get; set; }

    public string MaritalStatus { get; set; }

    public Employee(
        int id,
        string employeeId,
        string name,
        string birthPlace,
        DateTime birthDate,
        double basicSalary,
        string gender,
        string maritalStatus
    )
    {
        Id = id;
        EmployeeID = employeeId;
        Name = name;
        BirthPlace = birthPlace;
        BirthDate = birthDate;
        BasicSalary = basicSalary;
        Gender = gender;
        MaritalStatus = maritalStatus;
    }
}