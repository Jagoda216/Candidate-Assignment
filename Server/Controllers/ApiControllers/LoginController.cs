using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HashModel _hashModel;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
            _hashModel = new HashModel();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var user = await _context.Customer.SingleOrDefaultAsync(c => c.Name == model.Name);

            if (user == null)
            {
                return Unauthorized();
            }

            var verifyHash = _hashModel.VerifyHash(model.Password, user.Password);

            if (verifyHash == false)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jQYN7PJXLZFzCLTNb1CMgSFwvQHH2SGb"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7202",
                audience: "InternalSystemAPI",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );
            

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}
