using Adapter.Driven.EFCore.Contexts;
using Adapter.Driving.AuthenticationServer.DTOs.Requests.Claim;
using Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleClaimsController(
    RoleManager<ApplicationRole> roleManager,
    ApplicationDbContext dbContext
) : ControllerBase
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roleClaims = await _dbContext.RoleClaims.ToListAsync();
        return Ok(roleClaims);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var claim = await _dbContext.RoleClaims.FindAsync(id);
        if (claim == null)
            return NotFound();
        return Ok(claim);
    }

    [HttpGet("roles/{roleId:int}")]
    public async Task<IActionResult> GetByRoleId(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null)
            return NotFound();

        var claims = await _dbContext.RoleClaims.Where(rc => rc.RoleId == roleId).ToListAsync();

        return Ok(claims);
    }

    [HttpPost]
    public async Task<IActionResult> AddClaimToRole(
        [FromBody] AddClaimToRoleRequestBody requestBody
    )
    {
        var role = await _roleManager.FindByIdAsync(requestBody.RoleId.ToString());
        if (role == null)
            return NotFound();

        var result = await _roleManager.AddClaimAsync(
            role,
            new Claim(requestBody.ClaimType, requestBody.ClaimValue)
        );

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
        var claim = await _dbContext.RoleClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        claim.ClaimType = requestBody.ClaimType;
        claim.ClaimValue = requestBody.ClaimValue;

        _dbContext.RoleClaims.Update(claim);
        await _dbContext.SaveChangesAsync();

        return Ok(claim);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var claim = await _dbContext.RoleClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        _dbContext.RoleClaims.Remove(claim);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
