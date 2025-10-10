using Adapter.Driving.AuthenticationServer.DTOs.Requests.Claim;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Adapter.Driving.AuthenticationServer.Controllers;

[Route("api/[controller]")]
public class UserClaimsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var claims = await _parameter.ApplicationDbContext.UserClaims.ToListAsync();
        return Ok(claims);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var claims = await _parameter.ApplicationDbContext.UserClaims.FindAsync(id);
        return Ok(claims);
    }

    [HttpPost]
    public async Task<IActionResult> AddClaimToUser(
        [FromBody] AddClaimToUserRequestBody requestBody
    )
    {
        var user = await _parameter.UserManager.FindByIdAsync(requestBody.UserId.ToString());
        if (user == null)
            return NotFound();

        var result = await _parameter.UserManager.AddClaimAsync(user, new Claim(requestBody.ClaimType, requestBody.ClaimValue));

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
        var claim = await _parameter.ApplicationDbContext.UserClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        claim.ClaimType = requestBody.ClaimType;
        claim.ClaimValue = requestBody.ClaimValue;

        _parameter.ApplicationDbContext.UserClaims.Update(claim);
        await _parameter.ApplicationDbContext.SaveChangesAsync();

        return Ok(claim);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var claim = await _parameter.ApplicationDbContext.UserClaims.FindAsync(id);
        if (claim == null)
            return NotFound();

        _parameter.ApplicationDbContext.UserClaims.Remove(claim);
        await _parameter.ApplicationDbContext.SaveChangesAsync();

        return Ok();
    }
}
