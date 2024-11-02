using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPointsController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public UserPointsController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult UserPoints([FromQuery] string userName)
        {
            var userId = GetUserId(userName);

            var userPoints = GetUserPoints(userId);

            return Ok(new { Points = userPoints });
        }

        [HttpGet("all")]
        public IActionResult AllUserPoints()
        {
            var userPointsList = GetAllUserPoints();

            return Ok(new {userPointsList});
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
            var users = userRepository.GetUsers();

            foreach (var user in users)
            {
                if (userName == user.Name)
                {
                    return user.Id;
                }
            }

            throw new Exception("User not found");
        }

        private int GetUserPoints(int userId)
        {
            return userRepository.GetUserPoints(userId);
        }

        private IEnumerable<User> GetAllUserPoints()
        {
            return userRepository.GetAllUserPoints();
        }

        private void UpdateUserPoints(int userId, int newUserPoints)
        {
            userRepository.UpdateUserPoints(userId, newUserPoints);
        }
    }
}
