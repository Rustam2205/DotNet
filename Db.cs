using MySql.Data.MySqlClient;
using System;

namespace ITProgerDB
{
    class Db
    {
       
        private const string HOST = "localhost";
        private const string PORT = "3306"; //порт MySQL
        private const string DATABASE = "itproger";
        private const string USER = "root";
        private const string PASS = "root";
        private string connect;
        public Db()
        {
            connect = $"Server={HOST};Password={PASS};Database={DATABASE};User={USER};Port={PORT};";
        }
        public async Task Create()
        {
            string sql = "CREATE TABLE IF NOT EXISTS `users` (" +
                    "id INT(11) AUTO_INCREMENT PRIMARY KEY, " +
                    "login VARCHAR(50), " +
                    "password VARCHAR(50)) " +
                    "ENGINE = MYISAM";
            await ExecuteQuery(sql);
        }
        public async Task InsertData(string? title, string? text, string? date, string? author)
        {
            string sql = "INSERT INTO articles (title, text, date, author) VALUES (@title, @text, @date, @author)";
            await ExecuteQuery(sql);
        }
        public async Task ExecuteQuery(string SQLCommand)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                await conn.OpenAsync();
                Console.WriteLine("Open");
                MySqlCommand command = new MySqlCommand(SQLCommand, conn);
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("tst");
            }
        }

    }
}
