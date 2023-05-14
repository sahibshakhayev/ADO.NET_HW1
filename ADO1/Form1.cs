using System.Data.SqlClient;
using System.Xml.Linq;

namespace ADO1
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source = SAHIB-LAPTOP; Integrated Security = SSPI; Initial Catalog = Library;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillListBox();
        }

        private void FillListBox()
        {
            // Очистить ListBox
            listBox1.Items.Clear();

            // Создать подключение к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Создать команду для выборки всех записей из таблицы Author
                SqlCommand command = new SqlCommand("SELECT * FROM Authors", connection);

                try
                {
                    // Открыть подключение
                    connection.Open();

                    // Создать объект для чтения данных из таблицы Author
                    SqlDataReader reader = command.ExecuteReader();

                    // Прочитать данные построчно и добавить их в ListBox
                    while (reader.Read())
                    {
                        // Сформировать строку из имени и страны автора
                        string author = reader["Id"] + ". " + reader["FirstName"].ToString() + " " + reader["LastName"].ToString();

                        // Добавить строку в ListBox
                        listBox1.Items.Add(author);
                    }

                    // Закрыть объект для чтения данных
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Отобразить сообщение об ошибке
                    label3.Text = "Ошибка при работе с базой данных: " + ex.Message;
                }
            }
        }

        // Метод для добавления новой записи в таблицу Author
        private void AddAuthor(string firstname, string lastname)
        {
            // Создать подключение к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Создать команду для вставки новой записи в таблицу Author
                SqlCommand command = new SqlCommand("INSERT INTO Authors (FirstName, LastName) VALUES (@FirstName, @LastName)", connection);

                // Добавить параметры к команде
                command.Parameters.AddWithValue("@FirstName", firstname);
                command.Parameters.AddWithValue("@LastName", lastname);

                try
                {
                    // Открыть подключение
                    connection.Open();

                    // Выполнить команду
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Отобразить сообщение об ошибке
                    label3.Text = "Ошибка при работе с базой данных: " + ex.Message;
                }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверить, что поля TextBox не пустые
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                label3.Text = "Пожалуйста, введите имя и страну автора";
                return;
            }

            // Добавить новую запись в таблицу Author
            AddAuthor(textBox1.Text, textBox2.Text);

            // Обновить ListBox новыми данными
            FillListBox();

            // Очистить поля TextBox
            textBox1.Clear();
            textBox2.Clear();

            // Отобразить сообщение об успешном добавлении
            label3.Text = "Автор успешно добавлен";
        }
    }

}