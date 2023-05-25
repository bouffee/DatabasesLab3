using System;
using System.Diagnostics;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                connectionStatusLabel.Text = "Connection successful!";
            }
            else
            {
                connectionStatusLabel.Text = "Connection failed!";
            }
        }


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
                    MessageBox.Show("Time taken to insert 1000 records: " + stopwatch.Elapsed);
                }
            }
        }

        private void selectRandomButton_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 LIMIT 1000;", connection))
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Do something with the retrieved data
                        }
                    }

                    stopwatch.Stop();
                    MessageBox.Show("Time taken to select 1000 random records: " + stopwatch.Elapsed);
                }
            }
        }

        private void selectRangeButton_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM Table_1 WHERE Column_1 BETWEEN 500000 AND 500999;", connection))
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Do something with the retrieved data
                        }
                    }

                    stopwatch.Stop();
                    MessageBox.Show("Time taken to select 1000 records within a range: " + stopwatch.Elapsed);
                }
            }
        }

        private void removeAllButton_Click(object sender, EventArgs e)
        {
            // Remove all records from the table
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Table_1;", connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} records removed from the table.");
                }
            }
        }

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
                    MessageBox.Show("Time taken to insert 1 million records: " + stopwatch.Elapsed);
                }
            }
        }

    }
}
