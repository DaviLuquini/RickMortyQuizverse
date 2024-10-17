using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        //public List<User> Users { get; set; } = [];

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var users = GetUsers();

            foreach (var user in users)
            {
                if (request.Username == user.Name && request.Password == user.Password)
                {
                    // Gera o token JWT
                    var token = GenerateJwtToken(user);

                    // Armazena informações da sessão se necessário
                    HttpContext.Session.SetString("Name", user.Name);
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    return Ok(new { token, message = "Login successful" });
                }
                else if (request.Username == user.Name && request.Password != user.Password)
                {
                    return Unauthorized(new {
                        code = "WRONG_PASSWORD",
                        message = "Wrong Password."
                    });
                }
            }
            return Unauthorized(new {
                code = "USERNAME_NOT_FOUND",
                message = "Name not found."
            });
        }

        [HttpGet("check-session")]
        [Authorize]
        public IActionResult CheckSession()
        {
            var name = User.Identity.Name; // Nome do usuário extraído do token
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // ID do usuário

            if (string.IsNullOrEmpty(name) || userId == null)
            {
                return Unauthorized(new { message = "User not logged in." });
            }

            return Ok(new { message = $"Logged in as {name} (ID: {userId})" });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims:
                [
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                ],
                expires: DateTime.Now.AddMinutes(180),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private List<User> GetUsers()
        {
            var repository = new UserRepository();
            return repository.GetUsers();
        }
    }

    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
