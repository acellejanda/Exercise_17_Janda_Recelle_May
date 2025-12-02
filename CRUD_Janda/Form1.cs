using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Janda
{
    public partial class Form1 : Form
    {
        string connStr = "server=localhost;uid=root;pwd=12345678;database=schooldb;";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }
        private void LoadStudents()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM students", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvStudents.DataSource = dt;
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "INSERT INTO students (firstname, lastname, age, course) VALUES (@first, @last, @age, @course)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@first", txtFirst.Text);
                cmd.Parameters.AddWithValue("@last", txtLast.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text);

                cmd.ExecuteNonQuery();
            }

            LoadStudents();   
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dgvStudents.Rows[e.RowIndex].Cells["id"].Value.ToString();
                txtFirst.Text = dgvStudents.Rows[e.RowIndex].Cells["firstname"].Value.ToString();
                txtLast.Text = dgvStudents.Rows[e.RowIndex].Cells["lastname"].Value.ToString();
                txtAge.Text = dgvStudents.Rows[e.RowIndex].Cells["age"].Value.ToString();
                txtCourse.Text = dgvStudents.Rows[e.RowIndex].Cells["course"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "UPDATE students SET firstname=@first, lastname=@last, age=@age, course=@course WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@first", txtFirst.Text);
                cmd.Parameters.AddWithValue("@last", txtLast.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text);

                cmd.ExecuteNonQuery();
            }

            LoadStudents();   
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = "DELETE FROM students WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", txtID.Text);

                cmd.ExecuteNonQuery();
            }

            LoadStudents();   
        }
    }
}
