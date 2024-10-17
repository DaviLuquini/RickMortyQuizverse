using Dapper;
using QuizverseBack.Models;
using System.Data;

namespace QuizVerse.Platform.Infrastructure.Database
{
    public class UserRepository
    {
        public bool AddUser(User user)
        {
            using var conn = new DbConnection();

            EnsureTableExists(conn.Connection);

            string query = @"INSERT INTO public.""Users""(name, password)
                         VALUES (@name, @password);";

            var result = conn.Connection.Execute(sql: query, param: user);

            return result == 1;
        }

        public List<User> GetUsers()
        {
            using var conn = new DbConnection();

            EnsureTableExists(conn.Connection);

            string query = @"SELECT * FROM public.""Users"";";

            var users = conn.Connection.Query<User>(sql: query);

            return users.ToList();
        }

        public int GetUserPoints(int userId)
        {
            using var conn = new DbConnection();

            EnsureTableExists(conn.Connection);

            string query = @"SELECT userpoints
                     FROM public.""Users""
                     WHERE id = @UserId;";

            var userPoints = conn.Connection.QueryFirstOrDefault<int>(sql: query,param: new { UserId = userId });

            return userPoints;
        }

        public void UpdateUserPoints(int userId , int newUserPoints)
        {
            using var conn = new DbConnection();

            EnsureTableExists(conn.Connection);

            string query = @"SELECT userpoints
                     FROM public.""Users""
                     WHERE id = @UserId;";

            var userPoints = conn.Connection.QueryFirstOrDefault<int>(sql: query, param: new { UserId = userId });

            string queryUpdate = @"UPDATE public.""Users""
                           SET userpoints = @newUserPoints
                           WHERE id = @UserId;";

            conn.Connection.Execute(
                sql: queryUpdate,
                param: new { newUserPoints = newUserPoints, UserId = userId });
        }

        private void EnsureTableExists(IDbConnection connection)
        {
            string checkTableQuery = @"SELECT EXISTS (
                                    SELECT FROM information_schema.tables 
                                    WHERE table_schema = 'public' 
                                    AND table_name = 'Users');";

            bool tableExists = connection.ExecuteScalar<bool>(checkTableQuery);

            if (!tableExists)
            {
                string createTableQuery = @"
                CREATE TABLE public.""Users"" (
                    id SERIAL PRIMARY KEY,
                    name VARCHAR(100) NOT NULL,
                    password VARCHAR(100) NOT NULL,
                    userPoints INT DEFAULT 0
                );";
                connection.Execute(createTableQuery);
            }
        }
    }
}

