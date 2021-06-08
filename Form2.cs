using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Такси_Престиж
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private string numUpDownVal = null;
        public string NumUpDown
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
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionPath = Properties.Resources.stringConnect;
            string sqlExpression = "SELECT * FROM admins WHERE Pass = '" + textBox1.Text + "'";
            using (SqlConnection connection = new SqlConnection(connectionPath))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader count = command.ExecuteReader();
                if(!count.HasRows)
                {
                    MessageBox.Show("Введён неверный пароль!");
                    return;
                }
                while(count.Read())
                {
                    NumUpDown = count.GetValue(2).ToString();
                    this.Hide();
                }
                // = 1;
            }



            
            //this.Hide();
        }
    }
}
