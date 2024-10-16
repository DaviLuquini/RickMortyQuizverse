using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPointsController : Controller
    {
        //Ao dar login o site da um GET no userpoints do usuario
        //Ao acertar um personagem o site da um PUT no userpoints do usuario
        [HttpGet]
        public IActionResult UserPoints([FromQuery] string userName)
        {

            var userId = GetUserId(userName);

            var userPoints = GetUserPoints(userId);

            return Ok(new { userPoints });
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
    }
}
