

using Npgsql;

namespace QuizVerse.Platform.Infrastructure.Database
{
    public class DbConnection : IDisposable
    {
        public NpgsqlConnection Connection { get; set; }

        public DbConnection()
        {
            //localhost: Connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=quizverse_users;User Id=postgres;Password=postgres");
            Connection = new NpgsqlConnection("Server=db;Port=5432;Database=quizverse_users;User Id=postgres;Password=postgres");
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
