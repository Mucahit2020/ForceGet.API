using ForceGetCase.Core.Models.User;
using ForceGetCase.Core.Services.Abstracts;
using ForceGetCase.Core.Services.Concrates;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ForceGetCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthSettings _authSettings;
        private readonly IAuthService _authService;


        public AuthController(IOptions<AuthSettings> authSettings,IAuthService authService )
        {
            _authSettings = authSettings.Value;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            if (userRequest == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await _authService.AuthenticateAsync(userRequest.Username, userRequest.Password);

            if (result == "User not found")
            {
                return Unauthorized("User not found");
            }

            if (result == "Invalid password")
            {
                return Unauthorized("Invalid password");
            }

            return Ok(new { Token = result });
        }


    }
}
