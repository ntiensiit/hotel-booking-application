using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.Web.Controllers;

public class AccountsController : BaseController
{
    public IActionResult Login()
    {
        return View();
    }
}
