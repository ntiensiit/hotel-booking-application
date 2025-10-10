using Adapter.Driving.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class HostsController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
