using Microsoft.AspNetCore.Identity.Data;
using QuizVerse.Platform.Application;
using QuizVerse.Platform.Infrastructure.Database;

namespace QuizverseBack.Requests
{
    public class UserRegisterRequestDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
