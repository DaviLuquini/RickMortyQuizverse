using Microsoft.AspNetCore.Mvc;
using QuizVerse.Platform.Application;
using QuizVerse.Platform.Application.Exceptions;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;
using QuizverseBack.Requests;

namespace QuizVerse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController(IUserAppService userAppservice) : ControllerBase
    {
        private readonly IUserAppService userAppService = userAppservice;

        [HttpPost]
        public IActionResult Register([FromBody] UserRegisterRequestDto request)
        {
            try
            {
                var newUser = userAppService.AddUser(request);

                return Ok(new { message = "Register successful!", user = newUser });
            }

            catch (UserRegisterException ex)
            {
                return BadRequest(new { code = ex.Code, message = ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }

}
