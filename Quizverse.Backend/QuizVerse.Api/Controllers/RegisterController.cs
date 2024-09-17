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

            if (!userExists)
            {
                var newUser = new User(0, request.Username, request.Password);
                repository.AddUser(newUser);

                return Ok(new { message = "Register successful!", user = newUser });
            }
            else
            {
                return Unauthorized(new { message = "Register failed! Name already used." });
            }
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
