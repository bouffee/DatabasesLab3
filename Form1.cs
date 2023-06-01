using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace DatabaseApp
{
    public partial class MainForm : Form
    {
        private string connectionString = "Server=localhost;Port=5432;Database=db;User Id=root;Password=root;";

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
        // выборка с b-tree индексом
        //
        private void SelectWithBtreeIndex()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE column_1 = @value;", connection))
                {
                    command.Parameters.AddWithValue("@value", 500);
                    command.CommandTimeout = 0;

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Do something with the retrieved data
                        }
                    }

                    stopwatch.Stop();
            
                }
            }
        }
        //
        // выборка с хэш-индексом
        //
        private void SelectWithHashIndex()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE column_1 = @value;", connection))
                {
                    command.Parameters.AddWithValue("@value", 500);
                    command.CommandTimeout = 0;

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Do something with the retrieved data
                        }
                    }

                    stopwatch.Stop();
                }
            }
        }
        //
        // Выбрать в диапозоне с b-tree индексом
        //
        private void SelectWithRangeBtreeIndex()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE column_1 BETWEEN @minValue AND @maxValue;", connection))
                {
                    command.Parameters.AddWithValue("@minValue", 100);
                    command.Parameters.AddWithValue("@maxValue", 200);
                    command.CommandTimeout = 0;

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Do something with the retrieved data
                        }
                    }

                    stopwatch.Stop();
                }
            }
        }
        //
        // Выбрать в диапазоне с хэш-индексом
        //
        private void SelectWithRangeHashIndex()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE column_1 BETWEEN @minValue AND @maxValue;", connection))
                {
                    command.Parameters.AddWithValue("@minValue", 100);
                    command.Parameters.AddWithValue("@maxValue", 200);
                    command.CommandTimeout = 0;

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Do something with the retrieved data
                        }
                    }

                    stopwatch.Stop();
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
                            command.Parameters.AddWithValue("@Column_3", "Some random text");
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

            // Создаем Б-дерево индекс
            CreateBtreeIndex();

            // Делаем селект
            Stopwatch stopwatch = Stopwatch.StartNew();
            SelectWithBtreeIndex();
            stopwatch.Stop();
            resultBuilder.AppendLine("Время на SELECT с B-tree индексом: " + stopwatch.Elapsed);

            // Делаем селект с диапазоном
            stopwatch.Restart();
            SelectWithRangeBtreeIndex();
            stopwatch.Stop();
            resultBuilder.AppendLine("Время на SELECT с B-tree индексом и диапазоном: " + stopwatch.Elapsed);

            // Дропаем индекс Б-дерева
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

            // Делаем селект
            stopwatch.Restart();
            SelectWithHashIndex();
            stopwatch.Stop();
            resultBuilder.AppendLine("Время на SELECT с хэш-индексом: " + stopwatch.Elapsed);

            // Делаем селект с диапазоном
            stopwatch.Restart();
            SelectWithRangeHashIndex();
            stopwatch.Stop();
            resultBuilder.AppendLine("Время на SELECT с хэш-индексом и диапазоном: " + stopwatch.Elapsed);

            // Дропаем хэш индекс
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
        // Вставка 1000 значечний
        //
        private void insertButton_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("INSERT INTO Table_1 (Column_1, Column_2, Column_3) VALUES (@Column_1, @Column_2, @Column_3);", connection))
                {
                    Random random = new Random();
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    for (int i = 0; i < 1000; i++)
                    {
                        command.Parameters.AddWithValue("@Column_1", i + 1);
                        command.Parameters.AddWithValue("@Column_2", random.Next(1, 4));
                        command.Parameters.AddWithValue("@Column_3", "Some random text");
                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                    }

                    stopwatch.Stop();
                    MessageBox.Show("Время на вставку 1000 записей: " + stopwatch.Elapsed);
                }
            }
        }
    }
}