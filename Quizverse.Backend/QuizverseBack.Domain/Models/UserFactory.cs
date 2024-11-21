
namespace QuizverseBack.Models
{
    public class UserFactory
    {
        public User CreateUser(int id, string name, string password)
        {
            return new User(id, name, password);
        }
    }
}
