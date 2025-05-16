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

    public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
    {
        var employees = _employeeService.GetPaged(page, pageSize, search, out int totalItems);
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = totalItems;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        ViewBag.Search = search;

        return View(employees);
    }

    public IActionResult Create()
    {
        var newEmployeeId = _employeeService.GenerateNextEmployeeId();
        var model = new EmployeeDTO { EmployeeID = newEmployeeId };
        return View(model);
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
