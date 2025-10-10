using Adapter.Driving.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.Web.Areas.Customer.Controllers;

[Area("Customer")]
public class HotelsController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
