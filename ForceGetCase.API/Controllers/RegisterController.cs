using ForceGetCase.Core.Dtos;
using ForceGetCase.DataAccess.Context;
using ForceGetCase.DataAccess.Entity;
using ForceGetCase.Shared.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ForceGetCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ForceGetDbContext _context;

        public RegisterController(ForceGetDbContext context)
        {
            _context = context; 
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            string hashedPassword = PasswordHelper.HashPassword(userDto.Password);

            var user = new User
            {
                Username = userDto.Username,
                PasswordHash = hashedPassword 
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

    }


}

