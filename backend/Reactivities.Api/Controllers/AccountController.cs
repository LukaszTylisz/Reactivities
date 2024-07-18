using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reactivities.Api.DTOs;
using Reactivities.Domain;

namespace Reactivities.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(UserManager<AppUser> userManager) : ControllerBase
{

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return Unauthorized();

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Image = null,
                Token = "this will be a token",
                UserName = user.DisplayName
            };
        }

        return Unauthorized();
    }
}
