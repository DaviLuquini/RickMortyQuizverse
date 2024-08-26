using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        public List<User> Users { get; set; } = [];

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var users = GetUsers();

            foreach (var user in users)
            {
                if (request.Username == user.Name && request.Password == user.Password)
                {
                    return Ok(new { message = "Login successful"});
                }
            }
            return Unauthorized(new { message = "Login failed" });
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
