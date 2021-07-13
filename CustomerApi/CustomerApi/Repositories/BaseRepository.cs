using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CustomerApi.Repositories
{
    public class BaseRepository
    {
        private readonly string connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DbConnectionString");
        }

        public NpgsqlConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }
    }
}
