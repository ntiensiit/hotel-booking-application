using Adapter.Driving.AuthenticationServer.DTOs.Requests.Role;
using Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RolesController(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await Task.FromResult(Ok(_roleManager.Roles));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role == null) return NotFound();
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleCreateRequestBody requestBody)
    {
        var role = new ApplicationRole { Name = requestBody.Name };
        await _roleManager.CreateAsync(role);
        return Ok(role);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoleUpdateRequestBody requestBody)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        role.Name = requestBody.Name;
        await _roleManager.UpdateAsync(role);
        return Ok(role);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        await _roleManager.DeleteAsync(role);
        return Ok();
    }
}