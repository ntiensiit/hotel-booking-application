using Microsoft.AspNetCore.Mvc;
using Port.Driving.Shared.Services;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, IConfiguration configuration, IAuthService authService)
    {
        _logger = logger;
        _configuration = configuration;
        _authService = authService;
    }
}