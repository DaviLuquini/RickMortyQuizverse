using Dapper;
using QuizverseBack.Models;
using System.Data;

namespace QuizVerse.Platform.Infrastructure.Database
{
    public class UserRepository(DbConnection dbConnection)
    {
        private readonly DbConnection dbConnection = dbConnection;

        public void AddUser(User user)
        {
            EnsureTableExists(dbConnection.Connection);

            string query = @"INSERT INTO public.""Users""(name, password)
                         VALUES (@name, @password);";

            dbConnection.Connection.Execute(sql: query, param: user);
        }

        public List<User> GetUsers()
        {
            EnsureTableExists(dbConnection.Connection);

            string query = @"SELECT * FROM public.""Users"";";

            var users = dbConnection.Connection.Query<User>(sql: query);

            return users.ToList();
        }

        public int GetUserPoints(string username)
        {
            EnsureTableExists(dbConnection.Connection);

            string query = @"SELECT userpoints
                     FROM public.""Users""
                     WHERE name = @Username;";

            var userPoints = dbConnection.Connection.QueryFirstOrDefault<int>(sql: query,param: new { Username = username });

            return userPoints;
        }

        public IEnumerable<User> GetAllUserPoints()
        {
            EnsureTableExists(dbConnection.Connection);

            string query = @"SELECT name, userpoints
                     FROM public.""Users""";

            var userPointsList = dbConnection.Connection.Query<User>(query);

            return userPointsList;
        }

        public int GetUserImage(string username)
        {
            EnsureTableExists(dbConnection.Connection);

            string query = @"SELECT userimage
                     FROM public.""Users""
                     WHERE name = @Username;";

            int userImage = dbConnection.Connection.QueryFirstOrDefault<int>(sql: query, param: new { Username = username });

            return userImage;
        }

        public void UpdateUserPoints(string username, int newUserPoints)
        {
            EnsureTableExists(dbConnection.Connection);

            string queryUpdate = @"UPDATE public.""Users""
                           SET userpoints = @newUserPoints
                           WHERE name = @Username;";


            dbConnection.Connection.Execute(
                sql: queryUpdate,
                param: new { newUserPoints, Username = username });
        }

        public void UpdateUserImage(string username, int newUserImage)
        {
            EnsureTableExists(dbConnection.Connection);

            string queryUpdate = @"UPDATE public.""Users""
                           SET userimage = @newUserImage
                           WHERE name = @Username;";

            dbConnection.Connection.Execute(
                sql: queryUpdate,
                param: new { newUserImage, Username = username });
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
                    userpoints INT DEFAULT 0,
                    userimage INT DEFAULT 0
                );";
                connection.Execute(createTableQuery);
            }
        }
    }
}

