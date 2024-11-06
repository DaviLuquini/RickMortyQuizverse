using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Application;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserImageController(IUserAppService userAppService) : ControllerBase
    {
        private readonly IUserAppService userAppService = userAppService;

        [HttpGet]
        public IActionResult UserImage([FromQuery] string username)
        {
            int userImage = userAppService.GetUserImage(username);

            return Ok(new { Image = userImage });
        }

        [HttpPut]
        public IActionResult UpdateUserImage([FromQuery] string username, int newUserImage)
        {
            userAppService.UpdateUserImage(username, newUserImage);

            return Ok();
        }
    }
}
