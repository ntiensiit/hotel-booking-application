using Adapter.Driving.AuthenticationServer.DTOs.Requests.Role;
using Domain.Identity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]")]
public class RolesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await Task.FromResult(Ok(_parameter.RoleManager.Roles));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var role = await _parameter.RoleManager.FindByIdAsync(id.ToString());
        if (role == null)
            return NotFound();
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleCreateRequestBody requestBody)
    {
        var role = new ApplicationRole { Name = requestBody.Name };
        await _parameter.RoleManager.CreateAsync(role);
        return Ok(role);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoleUpdateRequestBody requestBody)
    {
        var role = await _parameter.RoleManager.FindByIdAsync(id.ToString());
        if (role == null)
            return NotFound();
        role.Name = requestBody.Name;
        await _parameter.RoleManager.UpdateAsync(role);
        return Ok(role);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _parameter.RoleManager.FindByIdAsync(id.ToString());
        if (role == null)
            return NotFound();
        await _parameter.RoleManager.DeleteAsync(role);
        return Ok();
    }
}
