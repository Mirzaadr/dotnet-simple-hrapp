using Microsoft.AspNetCore.Mvc;
using HRApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using HRApp.Application.DTO;

namespace HRApp.Web.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult LoadEmployees()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var start = Request.Form["start"].FirstOrDefault();
        var length = Request.Form["length"].FirstOrDefault();
        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"]);
        var sortDirection = Request.Form["order[0][dir]"];

        int pageSize = length != null ? Convert.ToInt32(length) : 10;
        int skip = start != null ? Convert.ToInt32(start) : 0;

        var data = _employeeService.GetAll();

        // Search
        if (!string.IsNullOrWhiteSpace(searchValue))
        {
            searchValue = searchValue.ToLower();
            data = data.Where(e =>
                e.EmployeeID.ToLower().Contains(searchValue) ||
                e.Name.ToLower().Contains(searchValue) ||
                e.BirthPlace.ToLower().Contains(searchValue) ||
                e.Gender.ToLower().Contains(searchValue) ||
                e.MaritalStatus.ToLower().Contains(searchValue)).ToList();
        }

        // Sorting
        var sortColumn = Request.Form[$"columns[{sortColumnIndex}][data]"];
        var prop = typeof(EmployeeDTO).GetProperty(sortColumn!, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        if (prop != null)
        {
            data = sortDirection == "asc"
                ? data.OrderBy(e => prop.GetValue(e)).ToList()
                : data.OrderByDescending(e => prop.GetValue(e)).ToList();
        }

        // Paging
        var pagedData = data.Skip(skip).Take(pageSize).ToList();

        return Json(new
        {
            draw = draw,
            recordsTotal = _employeeService.GetAll().Count,
            recordsFiltered = data.Count,
            data = pagedData
        });
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([Bind("EmployeeID,Name,BirthPlace,BirthDate,BasicSalary,Gender,MaritalStatus")] EmployeeDTO employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        _employeeService.Add(employee);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var emp = _employeeService.GetById(id);
        if (emp == null) return NotFound();
        return View(emp);
    }

    [HttpPost]
    public IActionResult Edit(EmployeeDTO employee)
    {
        if (!ModelState.IsValid)
            return View(employee);

        _employeeService.Update(employee);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var emp = _employeeService.GetById(id);
        if (emp == null) return NotFound();
        return View(emp);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _employeeService.Delete(id);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var emp = _employeeService.GetById(id);
        if (emp == null) return NotFound();
        return View(emp);
    }
}
