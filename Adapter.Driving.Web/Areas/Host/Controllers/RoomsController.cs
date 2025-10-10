using Adapter.Driving.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.Web.Areas.Host.Controllers;

[Area("Host")]
public class RoomsController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
