using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPointsController : Controller
    {
        [HttpGet]
        public IActionResult UserPoints([FromQuery] string userName)
        {
            var userId = GetUserId(userName);

            var userPoints = GetUserPoints(userId);

            return Ok(new { Points = userPoints });
        }

        [HttpPut]
        public IActionResult UpdateUserPoints([FromQuery] string userName, int newUserPoints)
        {
            var userId = GetUserId(userName);

           UpdateUserPoints(userId, newUserPoints);

            return Ok();
        }

        private int GetUserId(string userName)
        {
            var repository = new UserRepository();
            var users = repository.GetUsers();

            foreach (var user in users)
            {
                Console.WriteLine(userName);
                Console.WriteLine($"User ID: {user.Id}, User Name: {user.Name}");
                if (userName == user.Name)
                {
                    return user.Id;
                }
            }

            throw new Exception("User not found");
        }

        private int GetUserPoints(int userId)
        {
            var repository = new UserRepository();
            return repository.GetUserPoints(userId);
        }

        private void UpdateUserPoints(int userId, int newUserPoints)
        {
            var repository = new UserRepository();
            repository.UpdateUserPoints(userId, newUserPoints);
        }
    }
}
