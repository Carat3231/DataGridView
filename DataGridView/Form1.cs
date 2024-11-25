using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DataGridView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData() {

            string connectString = "Data Source  = CARAT; Initial Catalog = DataGridView; Integrated Security = true;"; 
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();

            string query = "SELECT * FROM Group_IS211 ORDER BY id";

            SqlCommand command = new SqlCommand(query, myConnection);
            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]> ();

            while (reader.Read()) {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader["id"].ToString();
                data[data.Count - 1][1] = reader["name_students"].ToString();
                data[data.Count - 1][2] = reader["secondName_students"].ToString();

            }
            reader.Close();
            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox2.Text;

            string connectionString = "Data Source  = CARAT; Initial Catalog = DataGridView; Integrated Security = true;";
            using (SqlConnection myConnection = new SqlConnection(connectionString)) { 
                myConnection.Open();
                string query = "INSERT INTO Group_IS211 (name_students, secondName_students) VALUES (@Fname, @Sname); SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, myConnection)) {
                    command.Parameters.AddWithValue("@Fname", firstName);
                    command.Parameters.AddWithValue("@Sname", lastName);

                    int newId = Convert.ToInt32(command.ExecuteScalar());

                    dataGridView1.Rows.Add(new object[] { newId, firstName, lastName });
                }
                textBox1.Clear();
                textBox2.Clear();
            
            }
        }
    }
}
