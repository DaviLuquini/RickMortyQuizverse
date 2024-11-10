using QuizVerse.Platform.Application.Exceptions;
using QuizVerse.Platform.Infrastructure.Database;
using QuizverseBack.Models;
using QuizverseBack.Requests;

namespace QuizVerse.Platform.Application
{
    public class UserAppService(UserRepository userRepository, UserFactory userFactory) : IUserAppService
    {
        private readonly UserRepository userRepository = userRepository;

        private readonly UserFactory userFactory = userFactory;

        public User AddUser(UserRegisterRequestDto registerRequest)
        {
            EnsureRegisterIsValid(registerRequest.Username, registerRequest.Password);

            var lowerUsername = registerRequest.Username.ToLower();

            var newUser = userFactory.CreateUser(0, lowerUsername, registerRequest.Password);

            userRepository.AddUser(newUser);

            return newUser;
        }

        public List<User> GetUsers()
        {
            return userRepository.GetUsers();
        }

        public int GetUserPoints(string username)
        {
            EnsureNameExists(username);

            var userpoints = userRepository.GetUserPoints(username);
            return userpoints;
        }

        public IEnumerable<User> GetAllUserPoints()
        {
            return userRepository.GetAllUserPoints();
        }

        public void UpdateUserPoints(string username, int newUserPoints)
        {
            if(newUserPoints <= 0)
            {
                throw new Exception("INVALID_NEW_USERPOINTS");
            }

            EnsureNameExists(username);

            userRepository.UpdateUserPoints(username, newUserPoints);
        }

        public int GetUserImage(string username)
        {
            EnsureNameExists(username);

            return userRepository.GetUserImage(username);
        }

        public void UpdateUserImage(string username, int newUserImage)
        {
            if(newUserImage <= 0)
            {
                throw new Exception("INVALID_NEW_USERIMAGE");
            }

            EnsureNameExists(username);

            userRepository.UpdateUserImage(username, newUserImage);
        }

        public void EnsureNameExists(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(username);
            }

            var users = GetUsers();
            if (!users.Any(user => user.Name == username))
            {
                throw new Exception("NAME_NOT_FOUND");
            }
        }
        public void EnsureRegisterIsValid(string username, string password)
        {
            var users = GetUsers();
            username = username.ToLower();

            if (users.Any(user => user.Name == username))
            {
                throw new UserRegisterException("NAME_ALREADY_USED", "The username is already taken.");
            }

            if (username.Length > 20)
            {
                throw new UserRegisterException("USERNAME_TOO_LONG", "Username is too long. Maximum length is 20 characters.");
            }

            if (password.Length > 30)
            {
                throw new UserRegisterException("PASSWORD_TOO_LONG", "Password is too long. Maximum length is 30 characters.");
            }

            if (users.Count >= 1000)
            {
                throw new UserRegisterException("USERS_LIMIT_REACHED", "The user limit has been reached.");
            }
        }
    }
}
