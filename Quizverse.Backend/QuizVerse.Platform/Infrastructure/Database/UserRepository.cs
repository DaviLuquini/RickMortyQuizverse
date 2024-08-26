using Dapper;
using QuizverseBack.Models;

namespace QuizVerse.Platform.Infrastructure.Database
{
    public class UserRepository
    {
        public bool AddUser(User user)
        {
            using var conn = new DbConnection();

            string query = @"INSERT INTO public.""Users""(
                            name, password)
	                        VALUES (@name, @password);";

            var result = conn.Connection.Execute(sql: query, param: user);

            return result == 1;
        }

        public List<User> GetUsers()
        {
            using var conn = new DbConnection();

            string query = @"SELECT * FROM public.""Users"";";

            var users = conn.Connection.Query<User>(sql: query);

            return users.ToList();
        }
    }
}
