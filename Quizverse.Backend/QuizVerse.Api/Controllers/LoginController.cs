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
                    HttpContext.Session.SetString("Name", user.Name);
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    return Ok(new { message = "Login successful" });
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
        public IActionResult CheckSession()
        {
            var name = HttpContext.Session.GetString("Name");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (string.IsNullOrEmpty(name) || userId == null)
            {
                return Unauthorized(new { message = "User not logged in." });
            }

            return Ok(new { message = $"Logged in as {name} (ID: {userId})" });
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
