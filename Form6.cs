using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Такси_Престиж
{
    public partial class Form6 : Form
    {
        private int numUpDownVal = 0;
        public int NumUpDown
        {
            get
            {
                return numUpDownVal;
            }
            set
            {
                numUpDownVal = value;
            }
        }
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Drivers WHERE ID = " + numUpDownVal;
            SqlDataReader r = myCommand.ExecuteReader();
            
            if (r.HasRows)
            {
                while (r.Read())
                {
                    textBox6.Text = r.GetValue(0).ToString();
                    textBox1.Text = r.GetValue(1).ToString();
                    textBox2.Text = r.GetValue(2).ToString();

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
            string query = "UPDATE Drivers SET Name = '" + textBox1.Text + "', ";
            query += "Model = '" + textBox2.Text + "' WHERE ID = " + numUpDownVal;

            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = query;
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Информация успешно обновлена.");
        }
    }
}
