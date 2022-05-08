using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRent
{
    public partial class DGClient : Form
    {
        ClientActions clientact = new ClientActions();
        Client client = new Client();
        WorkerActions workeract = new WorkerActions();
        Worker worker = new Worker();
        CarActions carra = new CarActions();
        Car car = new Car();
        RateActions rateact = new RateActions();
        Rate rate = new Rate();
        public DGClient()
        {
            InitializeComponent();
            ShowClient();
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage1);
            bantextbox();
            button8.Visible = false;
            button9.Visible = false;
            
        }
        public void bantextbox()
        {

            foreach (TextBox text_box in tabPage2.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                text_box.Enabled = false;
            }
            foreach (DateTimePicker dateTime in tabPage2.Controls.OfType<DateTimePicker>()) //То есть берём только 2 вкладку
            {
                dateTime.Enabled = false;
            }
            foreach (CheckBox checkBox in tabPage2.Controls.OfType<CheckBox>()) //То есть берём только 2 вкладку
            {
                checkBox.Enabled = false;
            }
            

        }
        public void razbantextbox()
        {
            foreach (TextBox text_box in tabPage2.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                text_box.Enabled = true;
            }
            
            foreach (CheckBox checkBox in tabPage2.Controls.OfType<CheckBox>()) //То есть берём только 2 вкладку
            {
                checkBox.Enabled = true;
            }
            textBox3.Enabled = false;

        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = tabControl1.SelectedTab.Name;
            if (name == "All")
            {
              tabControl1.TabPages.Remove(tabPage2);
              tabControl1.TabPages.Remove(tabPage1);
            }

        }//перехват выбора вкладки

        public void ShowClient()//обновление датагрида
        {
            dataGridView1.Rows.Clear();

            //dataGridView1.DataSource = rateact.ShowAll();
            foreach (Client p in clientact.ShowAll())
            {
                dataGridView1.Rows.Add(p.ID, p.Name, p.Surname,p.Patronymic,p.Date.ToString("d"),p.Phone);
            }

        }

        private void Button6_Click(object sender, EventArgs e)//кнопка обновить
        {
            ShowClient();
        }

        private void Button5_Click(object sender, EventArgs e)//кнопка ДОБАВИТЬ
        {
            ClientAdd newForm = new ClientAdd();
            newForm.ShowDialog();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//нажатие на ячейку
        {
            foreach (CheckBox checkBox in tabPage2.Controls.OfType<CheckBox>()) //То есть берём только 2 вкладку
            {
                checkBox.Checked = false;
            }

            if (e.ColumnIndex == 0) //индекс колонки, в которой находится кнопка.
            {
                tabControl1.TabPages.Add(tabPage2); //добавление вкладки
                tabControl1.TabPages.Add(tabPage1); //добавление вкладки
                int clientid = Convert.ToInt32((dataGridView1[e.ColumnIndex, e.RowIndex].Value));//присваиваивается значение нажатой кнопки
                client = clientact.Find(clientid);

                textBox3.Text = Convert.ToString(client.ID);
                textBox18.Text = client.Surname;
                textBox17.Text = client.Name;
                textBox16.Text = client.Patronymic;
                textBox15.Text = client.Phone;
                textBox6.Text = client.Passport;
                textBox1.Text = client.INN;
                textBox14.Text = client.NumVU;
                textBox2.Text = client.ClientComment;
                dateTimePicker1.Value = client.DateStartExp;
                dateTimePicker2.Value = client.Date;


                var catt = client.Clcategory();

                foreach(var c in catt)
                {
                    if(c.ID == 0)
                    {
                        checkBox1.Checked = true;
                        checkBox1.CheckState = CheckState.Indeterminate;
                    }
                    if (c.ID == 1)
                    {
                        checkBox2.Checked = true;
                        checkBox2.CheckState = CheckState.Indeterminate;

                    }
                    if (c.ID == 2)
                    {
                        checkBox3.Checked = true;
                        checkBox3.CheckState = CheckState.Indeterminate;
                    }
                    if (c.ID == 3)
                    {
                        checkBox4.Checked = true;
                        checkBox4.CheckState = CheckState.Indeterminate;
                    }
                }


                // contractBindingSource.DataSource = client.RentalHistory();
                dataGridView2.Rows.Clear();

                foreach (Contract p in client.RentalHistory())
                {
                    dataGridView2.Rows.Add(p.ID, workeract.Find(p.WorkerID).Surname + " " + workeract.Find(p.WorkerID).Name, carra.Find(p.CarID).BrandAndName, rateact.Find(p.RateID).Name,  p.DateStartContract, p.DateEndContract);
                }

                tabControl1.SelectTab(tabPage2);//открывает вкладку
            }
        }

        private void Button3_Click(object sender, EventArgs e)//удаление
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Клиент не выбран!");
            }
            else
            {
                int id = Convert.ToInt32(textBox3.Text);
                DialogResult result = MessageBox.Show("Вы уверены?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    if (client.Del(id))
                    {
                        ShowClient();
                        tabControl1.SelectTab(All);//открывает вкладку
                        MessageBox.Show("Клиент удален!");

                    }
                }
            }

        }


        
        public void Button4_Click(object sender, EventArgs e)//редактировать
        {
            tabControl1.TabPages.Remove(All);
            razbantextbox();
            button8.Visible = true;
            button9.Visible = true;
            button3.Enabled = false;
            button4.Enabled = false;
            
            
            if (checkBox1.Checked)
            {
                checkBox1.Enabled = false;

            }
            if (checkBox2.Checked)
            {
                checkBox2.Enabled = false;
            }
            if (checkBox3.Checked)
            {
                checkBox3.Enabled = false;
            }
            if (checkBox4.Checked)
            {
                checkBox4.Enabled = false;
            }
            return;
        }

        private void Button8_Click(object sender, EventArgs e)//отмена
        {
            textBox18.Text = client.Surname;
            textBox17.Text = client.Name;
            textBox16.Text = client.Patronymic;
            textBox15.Text = client.Phone;
            textBox6.Text = client.Passport;
            textBox1.Text = client.INN;
            textBox14.Text = client.NumVU;
            textBox2.Text = client.ClientComment;
            dateTimePicker1.Value = client.DateStartExp;
            dateTimePicker2.Value = client.Date;

            foreach (CheckBox checkBox in tabPage2.Controls.OfType<CheckBox>()) //То есть берём только 2 вкладку
            {
                checkBox.Checked = false;
            }
            var catt = client.Clcategory();

            foreach (var c in catt)
            {
                if (c.ID == 0)
                {
                    checkBox1.Checked = true;
                    checkBox1.CheckState = CheckState.Indeterminate;
                }
                if (c.ID == 1)
                {
                    checkBox2.Checked = true;
                    checkBox2.CheckState = CheckState.Indeterminate;

                }
                if (c.ID == 2)
                {
                    checkBox3.Checked = true;
                    checkBox3.CheckState = CheckState.Indeterminate;
                }
                if (c.ID == 3)
                {
                    checkBox4.Checked = true;
                    checkBox4.CheckState = CheckState.Indeterminate;
                }
            }
            tabControl1.TabPages.Add(All);
            bantextbox();
            button8.Visible = false;
            button9.Visible = false;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void Button9_Click(object sender, EventArgs e)//ок
        {
            tabControl1.TabPages.Add(All);
            button8.Visible = false;
            button9.Visible = false;
            button3.Enabled = true;
            button4.Enabled = true;

            int id = int.Parse(textBox3.Text);
            string surname = textBox18.Text;
            string name = textBox17.Text;
            string patronymic = textBox16.Text;
            DateTime date = dateTimePicker2.Value;
            string phone = textBox15.Text;
            string passport = textBox6.Text;
            string inn = textBox1.Text;
            string vu = textBox14.Text;
            DateTime start = dateTimePicker1.Value;
            string comm = textBox2.Text;

            

            if (name == "" || surname == "" || patronymic == "" || phone == "" || passport == "" || inn == "" || vu == "")
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {

                if (client.Update(id, name, surname, patronymic, date, phone, passport, inn, vu, start, comm))
                {

                    MessageBox.Show("Клиент успешно обновлен!");
                    if (checkBox1.CheckState != CheckState.Indeterminate && checkBox1.Checked)
                    {
                        client.Clcategory("A", id);

                    }
                    if (checkBox2.CheckState != CheckState.Indeterminate && checkBox2.Checked)
                    {
                        client.Clcategory("B", id);

                    }
                    if (checkBox3.CheckState != CheckState.Indeterminate && checkBox3.Checked)
                    {
                        client.Clcategory("C", id);

                    }
                    if (checkBox4.CheckState != CheckState.Indeterminate && checkBox4.Checked)
                    {
                        client.Clcategory("D", id);

                    }
                    ShowClient();
                    bantextbox();
                    
                }


            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
