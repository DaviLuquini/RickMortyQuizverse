
namespace QuizVerse.Platform.Application.Requests
{
    public class UserLoginRequestDto()
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
