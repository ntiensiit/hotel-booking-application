using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CustomersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
