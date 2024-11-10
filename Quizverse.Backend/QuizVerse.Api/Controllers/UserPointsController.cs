using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Application;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPointsController(IUserAppService userAppService) : ControllerBase
    {
        private readonly IUserAppService userAppService = userAppService;

        [HttpGet]
        public IActionResult UserPoints([FromQuery] string username)
        {
            var userPoints = userAppService.GetUserPoints(username);

            return Ok(new { Points = userPoints });
        }

        [HttpGet("all")]
        public IActionResult AllUserPoints()
        {
            var userPointsList = userAppService.GetAllUserPoints();

            return Ok(new { userPointsList });
        }

        [HttpPut]
        public IActionResult UpdateUserPoints([FromQuery] string username, int newUserPoints)
        {
            userAppService.UpdateUserPoints(username, newUserPoints);

            return Ok();
        }
    }
}
