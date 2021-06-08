using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Такси_Престиж
{
    public partial class Form1 : Form
    {
        public int type = 0;
        public string userName = ""; 
        public Form1()
        {
            InitializeComponent();
            
            String time = DateTime.Now.ToString("HH:mm:ss");
            label1.Text = time;
            button1.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            updateInfo();
        }
        private void insertSession(string name)
        {
            userName = name;
            string query = "INSERT INTO Sessions (Name, Date) VALUES ('" + name + "', SYSDATETIME())";
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = query;
            myCommand.ExecuteNonQuery();
            updateInfo();
        }
        private void loadOrders()
        {
            type = 0;
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Orders ORDER BY ID ASC";
            SqlDataReader r = myCommand.ExecuteReader();
            setData(r);
            r.Close();
            myConnection.Close();
        }
        private void loadStopOrders()
        {
            type = 1;
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Orders WHERE Status > 5 ORDER BY ID ASC";
            SqlDataReader r = myCommand.ExecuteReader();
            setData(r);
            r.Close();
            myConnection.Close();
        }
        private void loadDrivers()
        {
            type = 3;
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Drivers ORDER BY ID ASC";
            SqlDataReader r = myCommand.ExecuteReader();


            dataGridView3.Rows.Clear();
            if (r.HasRows)
            {
                int index = 0;
                while (r.Read())
                {
                    int id = int.Parse(r.GetValue(0).ToString());
                    string name = r.GetValue(1).ToString();
                    string phone = r.GetValue(2).ToString();
                    dataGridView3.Rows.Add();

                    dataGridView3["driverID", index].Value = id;
                    dataGridView3["NameDriver", index].Value = name;
                    dataGridView3["AboutCar", index].Value = phone;
                    index++;
                }
            }

            r.Close();
            myConnection.Close();
        }
        private void setData(SqlDataReader r)
        {
            dataGridView1.Rows.Clear();
            if (r.HasRows)
            {
                int index = 0;
                while (r.Read())
                {
                    int id = int.Parse(r.GetValue(0).ToString());
                    string name = r.GetValue(1).ToString();
                    string phone = r.GetValue(2).ToString();
                    string date = r.GetValue(3).ToString();
                    string addressin = r.GetValue(4).ToString();
                    string addressto = r.GetValue(5).ToString();
                    int status = int.Parse(r.GetValue(6).ToString());
                    int driver = int.Parse(r.GetValue(7).ToString());
                    dataGridView1.Rows.Add();

                    dataGridView1["ID", index].Value = id;
                    dataGridView1["Client", index].Value = name;
                    dataGridView1["Phone", index].Value = phone;
                    dataGridView1["Date", index].Value = date;
                    dataGridView1["AddressIn", index].Value = addressin;
                    dataGridView1["AddressTo", index].Value = addressto;
                    dataGridView1["Status", index].Value = getStatus(status);
                    dataGridView1["Driver", index].Value = getDriver(driver);
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = getColor(status);
                    index++;
                }
            }
        }
        private string getDriver(int d)
        {
            string driver = "не назначен";
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Drivers WHERE ID = " + d;
            SqlDataReader r = myCommand.ExecuteReader();
            
            if (r.HasRows)
            {
                while (r.Read())
                {
                    driver = r.GetValue(1).ToString();
                }
            }
            r.Close();
            return driver;
        }
        private void loadNowOrders()
        {
            type = 2;
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Orders WHERE Status < 6 ORDER BY ID ASC";
            SqlDataReader r = myCommand.ExecuteReader();
            setData(r);
            r.Close();
            myConnection.Close();
        }
        private System.Drawing.Color getColor(int s)
        {
            System.Drawing.Color temp = System.Drawing.Color.White;

            if (s == 1) { temp = System.Drawing.Color.Pink; }
            else if (s == 2) { temp = System.Drawing.Color.Yellow; }
            else if (s == 3) { temp = System.Drawing.Color.Cyan; }
            else if (s == 4) { temp = System.Drawing.Color.Orange; }
            else if (s == 5) { temp = System.Drawing.Color.Lime; }
            else if (s == 6) { temp = System.Drawing.Color.Red; }
            else if (s == 7) { temp = System.Drawing.Color.Green; }

            return temp;
        }
        private string getStatus(int s)
        {
            string temp = "В обработке";

            if (s == 1) { temp = "Принят"; }
            else if (s == 2) { temp = "Выехал"; }
            else if (s == 3) { temp = "Ждёт"; }
            else if (s == 4) { temp = "В пути"; }
            else if (s == 5) { temp = "В завершении"; }
            else if (s == 6) { temp = "Отмена"; }
            else if (s == 7) { temp = "Завершён"; }

            return temp;
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            String time = DateTime.Now.ToString("HH:mm:ss");
            label1.Text = time;
        }
        private void reloadButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reloadButtons();
            button1.Enabled = false;
            type = 0;
            updateInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reloadButtons();
            button2.Enabled = false;
            type = 1;
            updateInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reloadButtons();
            button3.Enabled = false;
            type = 2;
            updateInfo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reloadButtons();
            button4.Enabled = false;
            dataGridView1.Visible = false;
            dataGridView3.Visible = true;
            type = 3;
            updateInfo();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Form2 f2 = new Form2();
            f2.ShowDialog();
            while(f2.NumUpDown == null)
            {
                f2.ShowDialog();
            }
            label2.Text = "Диспетчер: " + f2.NumUpDown;
            label4.Text = "АВТОРИЗАЦИЯ ПРОШЛА УСПЕШНО.";
            label4.ForeColor = System.Drawing.Color.Green;
            insertSession(f2.NumUpDown);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            updateInfo();
        }
        private void putBut()
        {
            button7.Enabled = true;
            button8.Enabled = true;
        }
        private void stopBut()
        {
            button7.Enabled = false;
            button8.Enabled = false;
        }
        private void updateSessions()
        {
            string connectionPath = Properties.Resources.stringConnect;
            SqlConnection myConnection = new SqlConnection(connectionPath);
            myConnection.Open();
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SELECT * FROM Sessions WHERE Name = '" + userName + "' ORDER BY ID ASC";
            SqlDataReader r = myCommand.ExecuteReader();
            
            dataGridView2.Rows.Clear();
            if (r.HasRows)
            {
                int index = 0;
                while (r.Read())
                {
                    int id = int.Parse(r.GetValue(0).ToString());
                    string date = r.GetValue(2).ToString();
                    dataGridView2.Rows.Add();

                    dataGridView2["sID", index].Value = id;
                    dataGridView2["sDate", index].Value = date;
                    index++;
                }
            }

            r.Close();
            myConnection.Close();
        }
        private void updateInfo()
        {
            updateSessions();
            switch (type)
            {
                case 0:
                    {
                        putBut();
                        loadOrders();
                        dataGridView1.Visible = true;
                        dataGridView3.Visible = false;
                        break;
                    }
                case 1:
                    {
                        putBut();
                        loadStopOrders();
                        dataGridView1.Visible = true;
                        dataGridView3.Visible = false;
                        break;
                    }
                case 2:
                    {
                        putBut();
                        loadNowOrders();
                        dataGridView1.Visible = true;
                        dataGridView3.Visible = false;
                        break;
                    }
                case 3:
                    {
                        stopBut();
                        loadDrivers();
                        dataGridView1.Visible = false;
                        dataGridView3.Visible = true;
                        break;
                    }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if(type == 3)
            {
                int did = dataGridView3.CurrentRow.Index;
                if (did == -1) { MessageBox.Show("Для редактирования информации выделите строку в таблице, и нажмите кнопку Изменить"); return; }
                try { 
                    int driverid = int.Parse(dataGridView3[0, did].Value.ToString());

                    Form6 form = new Form6();
                    form.NumUpDown = driverid;
                    form.ShowDialog();
                    updateInfo();
                }
                catch
                {
                    MessageBox.Show("Для начала выделите нужную строку.");
                }
                return;
            }
            try
            {
                int id = dataGridView1.CurrentRow.Index;
                if (id == -1) { MessageBox.Show("Для редактирования информации выделите строку в таблице, и нажмите кнопку Изменить"); return; }

                int clientid = int.Parse(dataGridView1[0, id].Value.ToString());

                Form3 f = new Form3();
                f.NumUpDown = clientid;
                f.ShowDialog();
                updateInfo();
            }
            catch
            {
                MessageBox.Show("Для начала выделите нужную строку.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try {
                int id = dataGridView1.CurrentRow.Index;
                if (id == -1) { MessageBox.Show("Для редактирования статуса выделите строку в таблице, и нажмите кнопку Статус"); return; }
                int clientid = int.Parse(dataGridView1[0, id].Value.ToString());
                Form4 f = new Form4();
                f.NumUpDown = clientid;
                f.ShowDialog();
                updateInfo();
            }
            catch
            {
                MessageBox.Show("Для начала выделите нужную строку.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (type == 3)
            {
                Form7 form = new Form7();
                form.ShowDialog();
                updateInfo();
                return;
            }
            Form5 f = new Form5();
            f.ShowDialog();
            updateInfo();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try {
                int id = dataGridView1.CurrentRow.Index;
                if (id == -1) { MessageBox.Show("Для завершения заказа выделите строку в таблице, и нажмите кнопку Завершить"); return; }
                string query = "UPDATE Orders SET Status = '7' WHERE ID = " + int.Parse(dataGridView1[0, id].Value.ToString());

                string connectionPath = Properties.Resources.stringConnect;
                SqlConnection myConnection = new SqlConnection(connectionPath);
                myConnection.Open();
                SqlCommand myCommand = myConnection.CreateCommand();
                myCommand.CommandText = query;
                myCommand.ExecuteNonQuery();
                MessageBox.Show("Информация успешно обновлена.");
                updateInfo();
            }
            catch
            {
                MessageBox.Show("Для начала выделите нужную строку.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (type == 3)
            {
                try { 
                    int ids = dataGridView3.CurrentRow.Index;
                    if (ids == -1) { MessageBox.Show("Для удаления выделите строку в таблице, и нажмите кнопку Удалить"); return; }

                    string info = "Вы действительно хотите удалить строку из базы данных об этом водителе?";
                    DialogResult dialogResult = MessageBox.Show(info, "Предупреждение", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string connectionPath = Properties.Resources.stringConnect;
                        SqlConnection myConnection = new SqlConnection(connectionPath);
                        myConnection.Open();
                        SqlCommand myCommand = myConnection.CreateCommand();
                        myCommand.CommandText = "DELETE FROM Drivers WHERE ID = " + int.Parse(dataGridView3[0, ids].Value.ToString());
                        myCommand.ExecuteNonQuery();
                        MessageBox.Show("Водитель успешно удалён.");
                        updateInfo();
                        return;
                    }
                    return;
                }
                catch
                {
                    MessageBox.Show("Для начала выделите нужную строку.");
                }
            }
            try { 
                int id = dataGridView1.CurrentRow.Index;
                if (id == -1) { MessageBox.Show("Для удаления выделите строку в таблице, и нажмите кнопку Удалить"); return; }

                string text = "Вы действительно хотите удалить строку из базы данных об этом заказе?";
                DialogResult Result = MessageBox.Show(text, "Предупреждение", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    string connectionPath = Properties.Resources.stringConnect;
                    SqlConnection myConnection = new SqlConnection(connectionPath);
                    myConnection.Open();
                    SqlCommand myCommand = myConnection.CreateCommand();
                    myCommand.CommandText = "DELETE FROM Orders WHERE ID = " + int.Parse(dataGridView1[0, id].Value.ToString());
                    myCommand.ExecuteNonQuery();
                    MessageBox.Show("Заказ успешно удалён.");
                    updateInfo();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Для начала выделите нужную строку.");
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if(type == 3)
            {
                Bitmap bpm = new Bitmap(dataGridView3.Size.Width + 10, dataGridView3.Size.Height + 10);
                dataGridView3.DrawToBitmap(bpm, dataGridView3.Bounds);
                e.Graphics.DrawImage(bpm, 0, 0);
                bpm.Save("last_print.png", System.Drawing.Imaging.ImageFormat.Png);
                return;
            }
            Bitmap bmp = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Height + 10);
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
            bmp.Save("last_print.png", System.Drawing.Imaging.ImageFormat.Png);
        }   

        private void button11_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }
    }
}