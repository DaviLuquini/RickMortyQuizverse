using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        public List<User> Users { get; set; } = [];

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var repository = new UserRepository();
            var users = GetUsers();
            bool userExists = users.Any(user => user.Name == request.Username);

            if (userExists)
            {
                return Unauthorized(new { message = "Register failed! Name already used." });
            }

            if (request.Username.Length > 20)
            {
                return Unauthorized(new { message = "Register failed! UserName reached Max characters (20)" });
            }

            if (request.Password.Length > 30)
            {
                return Unauthorized(new { message = "Register failed! Password reached Max characters (30)" });
            }

            if (users.Count >= 1000)
            {
                return BadRequest(new { message = "Maximum number of users reached. Cannot add more users." });
            }

            var newUser = new User(0, request.Username, request.Password);
            repository.AddUser(newUser);

            return Ok(new { message = "Register successful!", user = newUser });
        }

        private List<User> GetUsers()
        {
            var repository = new UserRepository();
            return repository.GetUsers();
        }
    }

    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
