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
        public async Task Connect()
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                await conn.OpenAsync();
                Console.WriteLine("Open");
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "CREATE TABLE IF NOT EXISTS `users` (" +
                    "id INT(11) AUTO_INCREMENT PRIMARY KEY, " +
                    "login VARCHAR(50), " +
                    "password VARCHAR(50)) " +
                    "ENGINE = MYISAM";
                command.Connection = conn;
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("tst");
            } 
        }

    }
}
