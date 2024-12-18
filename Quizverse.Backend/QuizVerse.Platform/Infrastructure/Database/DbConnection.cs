

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
            Connection = new NpgsqlConnection("Host=primary.rickmortyquizversedb--2fzvm2y2h546.addon.code.run;Port=5432;Database=_2f532a2137a3;Username=_1492df07e1525389;Password=_69c9cd9e4026767900366c464b03f6");
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
