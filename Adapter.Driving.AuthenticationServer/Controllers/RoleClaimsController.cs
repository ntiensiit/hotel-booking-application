using Adapter.Driving.AuthenticationServer.DTOs.Requests.Claim;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]")]
public class RoleClaimsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roleClaims = await _parameter.ApplicationDbContext.RoleClaims.ToListAsync();
        return Ok(roleClaims);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var claim = await _parameter.ApplicationDbContext.RoleClaims.FindAsync(id);
        if (claim == null)
            return NotFound();
        return Ok(claim);
    }

    [HttpGet("roles/{roleId:int}")]
    public async Task<IActionResult> GetByRoleId(int roleId)
    {
        var role = await _parameter.RoleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
            return NotFound();

        var claims = await _parameter.ApplicationDbContext.RoleClaims.Where(rc => rc.RoleId == roleId).ToListAsync();

        return Ok(claims);
    }

    [HttpPost]
    public async Task<IActionResult> AddClaimToRole(
        [FromBody] AddClaimToRoleRequestBody requestBody
    )
    {
        var role = await _parameter.RoleManager.FindByIdAsync(requestBody.RoleId.ToString());
        if (role == null)
            return NotFound();

        var result = await _parameter.RoleManager.AddClaimAsync(role, new Claim(requestBody.ClaimType, requestBody.ClaimValue));

        if (result.Succeeded)
            return Ok();

        return BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] RoleClaimUpdateRequestBody requestBody
    )
    {
        var claim = await _parameter.ApplicationDbContext.RoleClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        claim.ClaimType = requestBody.ClaimType;
        claim.ClaimValue = requestBody.ClaimValue;

        _parameter.ApplicationDbContext.RoleClaims.Update(claim);
        await _parameter.ApplicationDbContext.SaveChangesAsync();

        return Ok(claim);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var claim = await _parameter.ApplicationDbContext.RoleClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        _parameter.ApplicationDbContext.RoleClaims.Remove(claim);
        await _parameter.ApplicationDbContext.SaveChangesAsync();

        return NoContent();
    }
}
