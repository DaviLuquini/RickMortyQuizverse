using QuizverseBack.Models;
using QuizverseBack.Requests;
using System.Collections.Generic;

namespace QuizVerse.Platform.Application
{
    public interface IUserAppService
    {
        User AddUser(UserRegisterRequestDto registerRequest);
        List<User> GetUsers();
        int GetUserPoints(string username);
        IEnumerable<User> GetAllUserPoints();
        int GetUserImage(string username);
        void UpdateUserPoints(string username, int newUserPoints);
        void UpdateUserImage(string username, int newUserImage);
    }
}
