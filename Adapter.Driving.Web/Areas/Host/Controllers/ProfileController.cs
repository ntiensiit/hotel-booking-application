using Adapter.Driven.EFCore.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.Web.Areas.Host.Controllers;

[Area("Host")]
public class ProfileController(ISession session, ApplicationDbContext dbContext) : Controller
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly ISession _session = session;

    public IActionResult Index()
    {
        return View();
    }
}
