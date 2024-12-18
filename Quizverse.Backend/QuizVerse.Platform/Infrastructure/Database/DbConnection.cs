using Npgsql;

namespace QuizVerse.Platform.Infrastructure.Database
{
    public class DbConnection : IDisposable
    {
        public NpgsqlConnection Connection { get; set; }

        public DbConnection()
        {
            //LOCALHOST: 
            //Connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=quizverse_users;User Id=postgres;Password=postgres");
            //DOCKER: 
            //Connection = new NpgsqlConnection("Host=db;Port=5432;Database=quizverse_users;User Id=postgres;Password=postgres");
            //Northflank
            string host = Environment.GetEnvironmentVariable("HOST");
            string database = Environment.GetEnvironmentVariable("DATABASE");
            string username = Environment.GetEnvironmentVariable("USERNAME");
            string password = Environment.GetEnvironmentVariable("PASSWORD");

            string connectionString = $"Host={host};Port=5432;Database={database};Username={username};Password={password}";

            // Atribuir a variável à propriedade Connection
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
