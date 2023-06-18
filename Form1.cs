using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace DatabaseApp
{
    public partial class MainForm : Form
    {
        private string connectionString = "Server=localhost;Port=5432;Database=db;User Id=root;Password=root;";
        Dictionary<int, string> words = new Dictionary<int, string>()
        {
            { 1, "Любить — значит видеть человека таким, каким его задумал Бог." },
            { 2, "Неужели это горе всего одной души?!" },
            { 3, "Внутри его души живут две силы, и они всегда сражаются друг с другом." },
            { 4, "Иди своей дорогой, чтобы ты никогда не пришел в мое сердце." },
            { 5, "Величие человека в его свободе выбора." },
            { 6, "Все гении начинают с детства." },
            { 7, "Надежда — это единственное, что остается у людей после потери всего." },
            { 8, "Самое сложное в любви — найти в ней себя." },
            { 9, "Мы все умрем, но некоторые из нас будут жить вечно в сердцах других людей." },
            { 10, "Каждое новое утро — это возможность начать все с чистого листа." }
        };
        int INSERT_SIZE = 1000;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Check the connection
            if (IsServerConnected(connectionString))
            {
                connectionStatusLabel.Text = "Соединение есть";
            }
            else
            {
                connectionStatusLabel.Text = "Соединения нет";
            }
        }
        //
        // Проверка соединения, пишет внизу экрана есть оно или нет. Нужно, потому что сервер запущен в докере и если докер не пашет - сервер тоже не пашет
        //
        private bool IsServerConnected(string connectionString)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        //
        // Вставка сколько хочешь значений значечний
        //
        private TimeSpan InsertData(int amount)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Table_1 (Column_1, Column_2, Column_3) VALUES (@Column_1, @Column_2, @Column_3);", connection))
                {
                    Random random = new Random();
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    for (int i = 0; i < amount; i++)
                    {
                        command.Parameters.AddWithValue("@Column_1", i + 1);
                        command.Parameters.AddWithValue("@Column_2", random.Next(1, 4));
                        command.Parameters.AddWithValue("@Column_3", words[random.Next(1, 11)]);
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                    }

                    stopwatch.Stop();
                    return stopwatch.Elapsed;
                }
            }
        }
        //
        // создание b-tree индекса
        //
        private void CreateBtreeIndex()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand createIndexCommand = new NpgsqlCommand("CREATE INDEX btree_idx ON Table_1 USING btree (column_1)", connection))
                {
                    createIndexCommand.ExecuteNonQuery();
                }
            }
        }
        //
        // создание хэш-индекса
        //
        private void CreateHashIndex()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand createIndexCommand = new NpgsqlCommand("CREATE INDEX hash_idx ON Table_1 USING hash (column_1)", connection))
                {
                    createIndexCommand.ExecuteNonQuery();
                }
            }
        }
        //
        // выборка 1000 произвольных значений
        //
        private TimeSpan SimpleSelect()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE column_1 = @value;", connection))
                {
                    Random random = new Random(); //будет рандомно выбирать число для значения в столбце 2
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    for (int i = 0; i < 1000; i++)
                    {
                        command.Parameters.AddWithValue("@value", random.Next(0, 350001));

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Обработка текущей строки результата
                            }
                        }
                    }

                    stopwatch.Stop();
                    return stopwatch.Elapsed;
                }
            }
        }
        //
        // Выбрать в диапозоне
        //
        private TimeSpan SelectWithRange()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE column_1 BETWEEN @minValue AND @maxValue;", connection))
                {
                    Random random = new Random();
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    for (int i = 0; i < 1000; i++)
                    {
                        int temp = random.Next(1, 200001);
                        command.Parameters.AddWithValue("@minValue", temp);
                        command.Parameters.AddWithValue("@maxValue", temp + 100000);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Обработка текущей строки результата
                            }
                        }
                    }

                    stopwatch.Stop();
                    return stopwatch.Elapsed;
                }
            }
        }
        //
        // Вставка 350000 значений (где-то 5 минут)
        //
        private void insertMillionButton_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Table_1 (Column_1, Column_2, Column_3) VALUES (@Column_1, @Column_2, @Column_3);", connection))
                {
                    Random random = new Random();
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    int totalRecords = 350000;
                    int batchSize = 1000;
                    int progress = 0;

                    progressBar.Visible = true;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = totalRecords;
                    progressBar.Value = 0;

                    for (int i = 0; i < totalRecords; i += batchSize)
                    {
                        progressBar.Value = i;

                        for (int j = 0; j < batchSize; j++)
                        {
                            int recordIndex = i + j;
                            if (recordIndex >= totalRecords)
                                break;

                            command.Parameters.AddWithValue("@Column_1", recordIndex + 1);
                            command.Parameters.AddWithValue("@Column_2", random.Next(1, 4));
                            command.Parameters.AddWithValue("@Column_3", words[random.Next(1, 11)]);
                            command.ExecuteNonQuery();

                            command.Parameters.Clear();
                        }

                        progress = i + batchSize;
                    }

                    progressBar.Value = totalRecords;
                    progressBar.Visible = false;

                    stopwatch.Stop();
                    MessageBox.Show("Время на вставку 350000 записей: " + stopwatch.Elapsed);
                }
            }
        }
        //
        // Удаление данных и таблицы
        //
        private void removeAllButton_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Table_1;", connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} записей были удалены.");
                }
            }
        }
        //
        // Сравнение запросов
        //
        private void compareQueriesButton_Click(object sender, EventArgs e)
        {
            StringBuilder resultBuilder = new StringBuilder();

            // Вставка без индекса
            TimeSpan insertTimeWithout = InsertData(INSERT_SIZE);
            resultBuilder.AppendLine("Время вставки без индекса " + INSERT_SIZE + " записей: " + insertTimeWithout);

            // Простая выборка без индекса
            TimeSpan timeSimpleSelectWithout = SimpleSelect();
            resultBuilder.AppendLine("Время на SELECT без индекса " + timeSimpleSelectWithout);

            // Выборка в диапозоне без индекса
            TimeSpan timeRangeSelectWithout = SelectWithRange();
            resultBuilder.AppendLine("Время на SELECT в диапозоне без индекса " + timeRangeSelectWithout);

            // Создаем Б-дерево индекс
            CreateBtreeIndex();

            // Вставляем INSERT_SIZE записей
            TimeSpan insertTime = InsertData(INSERT_SIZE);
            resultBuilder.AppendLine("Время вставки с b-tree " + INSERT_SIZE + " записей: " + insertTime);

            // Делаем селект
            TimeSpan timeBtreeSelect = SimpleSelect();
            resultBuilder.AppendLine("Время на SELECT с B-tree индексом: " + timeBtreeSelect);

            // Делаем селект с диапазоном
            TimeSpan timeBtreeRange = SelectWithRange();
            resultBuilder.AppendLine("Время на SELECT в диапозоне с B-tree индексом: " + timeBtreeRange);

            // Дропаем индекс b-tree
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand dropIndexCommand = new NpgsqlCommand("DROP INDEX btree_idx;", connection))
                {
                    dropIndexCommand.ExecuteNonQuery();
                }
            }

            // Создаем хэш индекс
            CreateHashIndex();

            // Вставляем INSERT_SIZE записей
            TimeSpan insertTime2 = InsertData(INSERT_SIZE);
            resultBuilder.AppendLine("Время вставки с хэш-индексом " + INSERT_SIZE + " записей: " + insertTime2);

            // Делаем селект
            TimeSpan timeHashSelect = SimpleSelect();
            resultBuilder.AppendLine("Время на SELECT с хэш-индексом: " + timeHashSelect);

            // Делаем селект с диапазоном
            TimeSpan timeHashRange = SelectWithRange();
            resultBuilder.AppendLine("Время на SELECT в диапозоне с хэш-индексом: " + timeHashRange);

            // Дропаем индекс
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand dropIndexCommand = new NpgsqlCommand("DROP INDEX hash_idx;", connection))
                {
                    dropIndexCommand.ExecuteNonQuery();
                }
            }

            MessageBox.Show(resultBuilder.ToString());
        }
        //
        //Создание GIN индекса для поля 3
        //
        private void CreateGINIndex()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand createIndexCommand = new NpgsqlCommand("CREATE INDEX gin_idx ON Table_1 USING GIN (to_tsvector('russian', column_3))", connection))
                {
                    createIndexCommand.ExecuteNonQuery();
                }
            }
        }
        //
        // SELECT c GIN
        //
        private TimeSpan SelectWithGIN()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE to_tsvector(column_3) @@ to_tsquery('все:*');", connection)) // запрос выбирает все строки, где в третьем столбце есть слова, которые начинаюстя с "все"
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    for (int i = 0; i < 50; i++)
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Обработка текущей строки результата
                            }
                        }
                    }
                    stopwatch.Stop();
                    return stopwatch.Elapsed;
                }
            }
        }
        /// 
        /// SELECT без GIN
        ///
        private TimeSpan SelectWithoutGIN()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE column_3 @@ 'все:*';", connection)) // запрос выбирает все строки, где в третьем столбце есть слова, которые начинаюстя с "все"
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    for (int i = 0; i < 50; i++)
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Обработка текущей строки результата
                            }
                        }
                    }
                    stopwatch.Stop();
                    return stopwatch.Elapsed;
                }
            }
        }
        private void GINindex_Click(object sender, EventArgs e)
        {
            StringBuilder resultBuilder = new StringBuilder();

            // Выборка без GIN
            TimeSpan timeWithoutGIN = SelectWithoutGIN();
            resultBuilder.AppendLine("Время на SELECT без GIN индекса: " + timeWithoutGIN);

            // Создание GIN 
            CreateGINIndex();

            // Выборка с GIN 
            TimeSpan timeWithGIN = SelectWithGIN();
            resultBuilder.AppendLine("Время на SELECT с GIN индексом: " + timeWithGIN);

            // Дроп GIN 
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand dropIndexCommand = new NpgsqlCommand("DROP INDEX gin_idx;", connection))
                {
                    dropIndexCommand.ExecuteNonQuery();
                }
            }

            MessageBox.Show(resultBuilder.ToString());
        }
    }
}