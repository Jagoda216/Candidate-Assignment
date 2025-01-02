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
    /// <summary>
    /// API controller responsible for authentication of user's via JWT token
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HashModel _hashModel;

        /// <summary>
        /// Initializes a new instance LoginCotroller
        /// </summary>
        /// <param name="context">Database context for accessing data</param>
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
            _hashModel = new HashModel();
        }
        /// <summary>
        /// Authenticate user's and gives JWT token if successful 
        /// </summary>
        /// <param name="model">Model contains user's name and password from Client </param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            //Retrives user from database
            var user = await _context.Customer.SingleOrDefaultAsync(c => c.Name == model.Name);

            if (user == null)
            {
                return Unauthorized();
            }
            //Verify user's hash in database with the password sent from client
            var verifyHash = _hashModel.VerifyHash(model.Password, user.Password);

            if (verifyHash == false)
            {
                return Unauthorized();
            }
            //Define user claim for JWT token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            //Create a SymmetricSecurityKey for signing token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jQYN7PJXLZFzCLTNb1CMgSFwvQHH2SGb"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Generate JWT token
            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "InternalSystemAPI",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );
            
            //Return token to user
            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
        }
    }
}
