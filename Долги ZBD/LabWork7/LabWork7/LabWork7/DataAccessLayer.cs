using Microsoft.Data.SqlClient;

namespace LabWork7
{
    public static class DataAccessLayer
    {
        private static string Server { get; set; } = "mssql";
        private static string Database { get; set; } = "ispp3103";
        private static string Login { get; set; } = "ispp3103";
        private static string Password { get; set; } = "3103";


        public static string ConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = Server,
                    InitialCatalog = Database,
                    UserID = Login,
                    Password = Password
                };
                return builder.ConnectionString;
            }
        }

        public static void UpdateConnectionSettings(string server, string database, string login, string password)
        {
            Server = server;
            Database = database;
            Login = login;
            Password = password;
        }

        public static bool ConnectDatabase()
        {
            using SqlConnection connection = new(ConnectionString);

            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task ChangeSessionPriceAsync(decimal newPrice, int sessionId)
        {
            await using SqlConnection connection = new(ConnectionString);

            try
            {
                await connection.OpenAsync();

                string query = "UPDATE Session SET Price = @newPrice WHERE SessionId = @sessionId";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@newPrice", newPrice);
                command.Parameters.AddWithValue("@sessionId", sessionId);

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
