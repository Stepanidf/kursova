using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Такси_Престиж
{
    public partial class Form5 : Form
    {
        public int[] drivers = new int[200];
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Drivers";
            SqlDataReader r = myCommand.ExecuteReader();

            int i = 0;
            if (r.HasRows)
            {
                while (r.Read())
                {
                    drivers[i] = int.Parse(r.GetValue(0).ToString());
                    comboBox1.Items.Add(r.GetValue(1).ToString() + " (" + r.GetValue(2).ToString() + ")");
                    i++;

                }
            }
            r.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Orders (Name, Phone, Date, AddressIn, AddressTo, Driver, Status) VALUES";
            query += " ('" + textBox1.Text + "', ";
            query += "'" + textBox2.Text + "', ";
            query += "SYSDATETIME(), ";
            query += "'" + textBox4.Text + "', ";
            query += "'" + textBox5.Text + "', ";
            query += "'" + drivers[comboBox1.SelectedIndex] + "', 0)";

            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = query;
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Информация успешно сохранена.");
        }
    }
}
