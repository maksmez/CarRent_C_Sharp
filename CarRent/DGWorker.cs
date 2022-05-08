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
    public partial class DGWorker : Form
    {
        WorkerActions workeract = new WorkerActions();
        Worker worker = new Worker();
        CarActions carra = new CarActions();
        Car car = new Car();
        RateActions rateact = new RateActions();
        Rate rate = new Rate();
        
        ClientActions clientact = new ClientActions();
        
        public DGWorker()
        {
            InitializeComponent();
            ShowWorker();
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage1);
            bantextbox();
            button8.Visible = false;
            button7.Visible = false;
            comboBox2.DataSource = worker.Positionn;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
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
            
            foreach (ComboBox comboBox in tabPage2.Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
            {
                comboBox.Enabled = false;
            }


        }
        public void ClearText()
        {

            foreach (TextBox text_box in tabPage2.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                if (text_box != null)
                    text_box.Text = "";
            }
            foreach (ComboBox combo_box in tabPage2.Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
            {
                if (combo_box != null)
                    combo_box.Text = "";
            }
            


        }
        public void razbantextbox()
        {
            foreach (TextBox text_box in tabPage2.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                text_box.Enabled = true;
            }
           
            foreach (ComboBox comboBox in tabPage2.Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
            {
                comboBox.Enabled = true;
            }
            textBox6.Enabled = false;

        }
        public void ShowWorker()//обновление датагрида
        {
            dataGridView1.Rows.Clear();


            foreach (Worker p in workeract.ShowAll())
            {
                dataGridView1.Rows.Add(p.ID, p.Name, p.Surname, p.Patronymic, p.Date.ToString("d"), p.Phone, p.Positionn.ElementAt(p.Position));
            }

        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)//перехват переключения вкладки
        {
            string name = tabControl1.SelectedTab.Name;
            if (name == "All")
            {
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage1);
            }

        }//перехват выбора вкладки
        private void Button6_Click(object sender, EventArgs e)
        {
            ShowWorker();
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//нажатие на ячейку
        {
            

            if (e.ColumnIndex == 0) //индекс колонки, в которой находится кнопка.
            {
                tabControl1.TabPages.Add(tabPage2); //добавление вкладки
                tabControl1.TabPages.Add(tabPage1); //добавление вкладки
                int workerid = Convert.ToInt32((dataGridView1[e.ColumnIndex, e.RowIndex].Value));//присваиваивается значение нажатой кнопки
                worker = workeract.Find(workerid);

                textBox6.Text = Convert.ToString(worker.ID);
                textBox1.Text = worker.Surname;
                textBox2.Text = worker.Name;
                textBox3.Text = worker.Patronymic;
                dateTimePicker1.Value = worker.Date;
                textBox4.Text = worker.Phone;
                dateTimePicker2.Value = worker.DateStartExp;
                textBox12.Text = worker.Passport;
                textBox11.Text = worker.INN;
                comboBox2.Text = worker.Positionn.ElementAt(worker.Position);
                //contractBindingSource.DataSource = worker.CustomerList();

                dataGridView2.Rows.Clear();

                foreach (Contract p in worker.CustomerList())
                {
                    dataGridView2.Rows.Add(p.ID, clientact.Find(p.ClientID).Surname + " " + clientact.Find(p.ClientID).Name, p.DateStartContract, p.DateEndContract);
                }



                tabControl1.SelectTab(tabPage2);//открывает вкладку
            }
        }

        private void Button4_Click(object sender, EventArgs e)//редактировать
        {
            tabControl1.TabPages.Remove(All);
            button8.Visible = true;
            button7.Visible = true;
            button4.Enabled = false;
            button3.Enabled = false;
            razbantextbox();
        }

        private void Button7_Click(object sender, EventArgs e)//ок
        {
            tabControl1.TabPages.Add(All);
            button8.Visible = false;
            button7.Visible = false;
            button4.Enabled = true;
            button3.Enabled = true;
            int id = int.Parse(textBox6.Text);
            string surname = textBox1.Text;
            string name = textBox2.Text;
            string patronymic = textBox3.Text;
            DateTime date = dateTimePicker1.Value;
            string phone = textBox4.Text;
            DateTime start = dateTimePicker2.Value;
            string passport = textBox12.Text;
            string inn = textBox11.Text;
            string pos1 = comboBox2.Text;
            int position = worker.Positionn.IndexOf(pos1);

            if (name == "" || surname == "" || patronymic == "" || phone == "" || passport == "" || inn == "" || pos1 == "")
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {

                if (worker.Update(id,name, surname, patronymic, date, phone, passport, inn, position, start))
                {
                    MessageBox.Show("Работник успешно обновлен!");
                }
            }
            ShowWorker();
            bantextbox();
            
            
        }

        private void Button1_Click(object sender, EventArgs e)//добавить
        {
            WorkerAdd newForm = new WorkerAdd();
            newForm.ShowDialog();
        }

        private void Button8_Click(object sender, EventArgs e)//отмена
        {
            textBox1.Text = worker.Surname;
            textBox2.Text = worker.Name;
            textBox3.Text = worker.Patronymic;
            dateTimePicker1.Value = worker.Date;
            textBox4.Text = worker.Phone;
            dateTimePicker2.Value = worker.DateStartExp;
            textBox12.Text = worker.Passport;
            textBox11.Text = worker.INN;
            comboBox2.Text = worker.Positionn.ElementAt(worker.Position);

            tabControl1.TabPages.Add(All);
            button8.Visible = false;
            button7.Visible = false;
            button4.Enabled = true;
            button3.Enabled = true;
            bantextbox();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Работник не выбран!");
            }
            else
            {
                int id = Convert.ToInt32(textBox6.Text);
                DialogResult result = MessageBox.Show("Вы уверены?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    if (worker.Del(id))
                    {
                        ShowWorker();
                        tabControl1.SelectTab(All);//открывает вкладку
                        MessageBox.Show("Работник удален!");

                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
