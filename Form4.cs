using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Такси_Престиж
{
    public partial class Form4 : Form
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
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Orders WHERE ID = " + numUpDownVal;
            SqlDataReader r = myCommand.ExecuteReader();

            if (r.HasRows)
            {
                while (r.Read())
                {
                    int select = int.Parse(r.GetValue(6).ToString());
                    comboBox1.SelectedIndex = select;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            int status = comboBox1.SelectedIndex;
            myCommand.CommandText = "UPDATE Orders SET Status = " + status + " WHERE ID = " + numUpDownVal;
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Информация успешно обновлена.");
        }
    }
}
