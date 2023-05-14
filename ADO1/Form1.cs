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
            // �������� ListBox
            listBox1.Items.Clear();

            // ������� ����������� � ���� ������
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // ������� ������� ��� ������� ���� ������� �� ������� Author
                SqlCommand command = new SqlCommand("SELECT * FROM Authors", connection);

                try
                {
                    // ������� �����������
                    connection.Open();

                    // ������� ������ ��� ������ ������ �� ������� Author
                    SqlDataReader reader = command.ExecuteReader();

                    // ��������� ������ ��������� � �������� �� � ListBox
                    while (reader.Read())
                    {
                        // ������������ ������ �� ����� � ������ ������
                        string author = reader["Id"] + ". " + reader["FirstName"].ToString() + " " + reader["LastName"].ToString();

                        // �������� ������ � ListBox
                        listBox1.Items.Add(author);
                    }

                    // ������� ������ ��� ������ ������
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // ���������� ��������� �� ������
                    label3.Text = "������ ��� ������ � ����� ������: " + ex.Message;
                }
            }
        }

        // ����� ��� ���������� ����� ������ � ������� Author
        private void AddAuthor(string firstname, string lastname)
        {
            // ������� ����������� � ���� ������
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // ������� ������� ��� ������� ����� ������ � ������� Author
                SqlCommand command = new SqlCommand("INSERT INTO Authors (FirstName, LastName) VALUES (@FirstName, @LastName)", connection);

                // �������� ��������� � �������
                command.Parameters.AddWithValue("@FirstName", firstname);
                command.Parameters.AddWithValue("@LastName", lastname);

                try
                {
                    // ������� �����������
                    connection.Open();

                    // ��������� �������
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // ���������� ��������� �� ������
                    label3.Text = "������ ��� ������ � ����� ������: " + ex.Message;
                }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ���������, ��� ���� TextBox �� ������
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                label3.Text = "����������, ������� ��� � ������ ������";
                return;
            }

            // �������� ����� ������ � ������� Author
            AddAuthor(textBox1.Text, textBox2.Text);

            // �������� ListBox ������ �������
            FillListBox();

            // �������� ���� TextBox
            textBox1.Clear();
            textBox2.Clear();

            // ���������� ��������� �� �������� ����������
            label3.Text = "����� ������� ��������";
        }
    }

}