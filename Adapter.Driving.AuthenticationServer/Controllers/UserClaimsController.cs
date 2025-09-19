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
public class UserClaimsController(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext dbContext
) : ControllerBase
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var claims = await _dbContext.UserClaims.ToListAsync();
        return Ok(claims);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var claims = await _dbContext.UserClaims.FindAsync(id);
        return Ok(claims);
    }

    [HttpPost]
    public async Task<IActionResult> AddClaimToUser(
        [FromBody] AddClaimToUserRequestBody requestBody
    )
    {
        var user = await _userManager.FindByIdAsync(requestBody.UserId.ToString());
        if (user == null)
            return NotFound();

        var result = await _userManager.AddClaimAsync(
            user,
            new Claim(requestBody.ClaimType, requestBody.ClaimValue)
        );

        if (result.Succeeded)
            return Ok();

        return BadRequest(result.Errors);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UserClaimUpdateRequestBody requestBody
    )
    {
        var claim = await _dbContext.UserClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        claim.ClaimType = requestBody.ClaimType;
        claim.ClaimValue = requestBody.ClaimValue;

        _dbContext.UserClaims.Update(claim);
        await _dbContext.SaveChangesAsync();

        return Ok(claim);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var claim = await _dbContext.UserClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        _dbContext.UserClaims.Remove(claim);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}
