using Microsoft.AspNetCore.Mvc;
using HRApp.Web.Models.ViewModels;

namespace HRApp.Web.ViewComponents;

public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginationViewModel model)
    {
        return View(model);
    }
}
