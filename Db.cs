using MySql.Data.MySqlClient;

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
            string sql = "CREATE TABLE IF NOT EXISTS `articles` (" +
                    "id INT(11) AUTO_INCREMENT PRIMARY KEY, " +
                    "title VARCHAR(50), " +
                    "text VARCHAR(50), " +
                    "date Date, " +
                    "author VARCHAR(50)) " +
                    "ENGINE = MYISAM";
            await ExecuteQuery(sql);
        }
        public async Task InsertData(params string[] parameters)
        {
            if (parameters.Length != 4)
            {
                throw new ArgumentException("Parameters number should be 4!!!");
            }
            string sql = "INSERT INTO articles (title, text, date, author) VALUES (@title, @text, @date, @author)";
            IEnumerable<string> result = parameters.Union(new string[] { "@title", "@text", "@date", "@author" });
            await ExecuteQuery(sql, result.ToArray() as string[]);
        }

        public async Task GetData()
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                await conn.OpenAsync();
                Console.WriteLine("Open");
                MySqlCommand command = new MySqlCommand("SELECT * FROM articles ORDER BY id DESC LIMIT 1", conn);
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            object id = reader["id"];
                            object text = reader["text"];
                            object date = reader["date"];
                            object author = reader["author"];
                            object title = reader["title"];
                            Console.WriteLine($"id = {id}\ntext = {text}\ndate = {date}\nauthor = {author}\ntitle = {title}\n");
                        }
                    }
                }
            }
        }

        public async Task ExecuteQuery(string SQLCommand, params string[]? parametersAndArguments)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                await conn.OpenAsync();
                Console.WriteLine("Open");
                MySqlCommand command = new MySqlCommand(SQLCommand, conn);
                int count = 0;
                if (parametersAndArguments != null)
                    count = parametersAndArguments.Length;
                for (int i = 0; i < count / 2; i++)
                {
                    command.Parameters.Add(new MySqlParameter(parametersAndArguments[i + count / 2], parametersAndArguments[i]));
                }

                await command.ExecuteNonQueryAsync();
                Console.WriteLine("tst");
            }
        }

    }
}
