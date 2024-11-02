using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserImageController : ControllerBase
    {
        [HttpGet]
        public IActionResult UserImage([FromQuery] string userName)
        {
            var userId = GetUserId(userName);

            int userImage = GetUserImage(userId);

            return Ok(new { Image = userImage });
        }

        [HttpPut]
        public IActionResult UpdateUserImage([FromQuery] string userName, int newUserImage)
        {
            var userId = GetUserId(userName);

            UpdateUserImage(userId, newUserImage);

            return Ok();
        }

        private int GetUserId(string userName)
        {
            var repository = new UserRepository();
            var users = repository.GetUsers();

            foreach (var user in users)
            {
                if (userName == user.Name)
                {
                    return user.Id;
                }
            }

            throw new Exception("User not found");
        }

        private int GetUserImage(int userId)
        {
            var repository = new UserRepository();
            return repository.GetUserImage(userId);
        }

        private void UpdateUserImage(int userId, int newUserImage)
        {
            var repository = new UserRepository();
            repository.UpdateUserImage(userId, newUserImage);
        }
    }
}
