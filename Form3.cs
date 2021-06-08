using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Такси_Престиж
{
    public partial class Form3 : Form
    {
        public int[] drivers = new int[200];
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
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Orders WHERE ID = " + numUpDownVal;
            SqlDataReader r = myCommand.ExecuteReader();

            int driver = 0;
            if(r.HasRows)
            {
                while(r.Read())
                {
                    textBox1.Text = r.GetValue(1).ToString();
                    textBox2.Text = r.GetValue(2).ToString();
                    textBox3.Text = r.GetValue(3).ToString();
                    textBox4.Text = r.GetValue(4).ToString();
                    textBox5.Text = r.GetValue(5).ToString();
                    textBox6.Text = r.GetValue(0).ToString();
                    driver = int.Parse(r.GetValue(7).ToString());

                }
            }
            r.Close();
            
            myCommand.CommandText = "SELECT * FROM Drivers";
            r = myCommand.ExecuteReader();

            int i = 0;
            comboBox1.Items.Clear();
            if (r.HasRows)
            {
                while (r.Read())
                {
                    drivers[i] = int.Parse(r.GetValue(0).ToString());
                    comboBox1.Items.Add(r.GetValue(1).ToString() + " (" + r.GetValue(2).ToString() + ")");
                    if (driver == int.Parse(r.GetValue(0).ToString())) { comboBox1.SelectedIndex = comboBox1.Items.Count - 1; }
                    i++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Orders SET Name = '" + textBox1.Text + "', ";
            query += "Phone = '" + textBox2.Text + "', ";
            query += "Date = '" + textBox3.Text + "', ";
            query += "AddressIn = '" + textBox4.Text + "', ";
            query += "AddressTo = '" + textBox5.Text + "', ";
            query += "Driver = '" + drivers[comboBox1.SelectedIndex] + "' WHERE ID = " + numUpDownVal;

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
