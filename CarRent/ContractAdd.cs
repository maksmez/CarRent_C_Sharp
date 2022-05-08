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
    public partial class ContractAdd : Form
    {
        Client client = new Client();
        Worker worker = new Worker();
        Car car = new Car();
        Rate rate = new Rate();
        Contract contract = new Contract();
        RateActions rateact = new RateActions();
        WorkerActions workact = new WorkerActions();
        CarActions caract = new CarActions();
        ClientActions clientact = new ClientActions();
        public ContractAdd()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker1.Enabled = false;
            
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";

            dateTimePicker1.MinDate = DateTime.Today;

            TimeSpan time = TimeSpan.FromHours(1);

            dateTimePicker2.MinDate = DateTime.Now+time;
            numericUpDown1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            maskedTextBox1.Enabled = false;
            maskedTextBox2.Enabled = false;
            maskedTextBox3.Enabled = false;
            maskedTextBox4.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)//клиент
        {
            button2.Enabled = false;
            button6.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DataGridViewButtonColumn dgvID = new DataGridViewButtonColumn();
            dgvID.Name = "ID";
            DataGridViewTextBoxColumn surname = new DataGridViewTextBoxColumn();
            surname.Name = "Фамилия";
            DataGridViewTextBoxColumn patronymic = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.Name = "Имя";
            patronymic.Name = "Отчество";
            DataGridViewTextBoxColumn date = new DataGridViewTextBoxColumn();
            date.Name = "Дата рождения";
            DataGridViewTextBoxColumn phone = new DataGridViewTextBoxColumn();
            phone.Name = "Телефон";
            DataGridViewTextBoxColumn pos = new DataGridViewTextBoxColumn();
            pos.Name = "Паспорт";


            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dgvID, surname, name, patronymic, date , phone, pos });

            foreach (Client p in clientact.ShowAll())
            {
                dataGridView1.Rows.Add(p.ID, p.Surname, p.Name, p.Patronymic, p.Date.ToString("d"), p.Phone , p.Passport);
            }

        }

        private void Button2_Click(object sender, EventArgs e)//механик
        {
            button1.Enabled = false;
            button6.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            DataGridViewButtonColumn dgvID = new DataGridViewButtonColumn();
            dgvID.Name = "ID";
            DataGridViewTextBoxColumn surname = new DataGridViewTextBoxColumn();
            surname.Name = "Фамилия";
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.Name = "Имя";
            DataGridViewTextBoxColumn patronymic = new DataGridViewTextBoxColumn();
            patronymic.Name = "Отчество";
            DataGridViewTextBoxColumn date = new DataGridViewTextBoxColumn();
            date.Name = "Дата рождения";
            DataGridViewTextBoxColumn phone = new DataGridViewTextBoxColumn();
            phone.Name = "Телефон";
            DataGridViewTextBoxColumn pos = new DataGridViewTextBoxColumn();
            pos.Name = "Позиция";


            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dgvID, surname, name, patronymic, date, phone, pos });


            foreach (Worker p in workact.ShowAll())
            {
                if(p.Position==1)
                dataGridView1.Rows.Add(p.ID, p.Name, p.Surname, p.Patronymic, p.Date.ToString("d"), p.Phone, p.Positionn.ElementAt(p.Position));
            }
        }
        private void Button3_Click(object sender, EventArgs e)//авто
        {
            if(maskedTextBox1.Text == "")
            {
                MessageBox.Show("Выберите клиента!");
            }
            else
            {

                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                button1.Enabled = false;
                button6.Enabled = false;
                button2.Enabled = false;
                button4.Enabled = false;
                DataGridViewButtonColumn dgvID = new DataGridViewButtonColumn();
                dgvID.Name = "ID";
                DataGridViewTextBoxColumn brand = new DataGridViewTextBoxColumn();
                brand.Name = "Марка";
                DataGridViewTextBoxColumn body = new DataGridViewTextBoxColumn();
                body.Name = "Кузов";
                DataGridViewTextBoxColumn pow = new DataGridViewTextBoxColumn();
                pow.Name = "Мощность";
                DataGridViewTextBoxColumn kpp = new DataGridViewTextBoxColumn();
                kpp.Name = "Трансмиссия";
                DataGridViewTextBoxColumn cat = new DataGridViewTextBoxColumn();
                cat.Name = "Категория";
                DataGridViewTextBoxColumn classs = new DataGridViewTextBoxColumn();
                classs.Name = "Класс автомобиля";

                dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dgvID, brand, body, pow, kpp, cat,classs });


                foreach (Car p in caract.ShowAll(Convert.ToInt32(maskedTextBox1.Text))) //выводит в зависимости от клиента
                {
                    if(p.Status==0&&p.Damage==0)
                    dataGridView1.Rows.Add(p.ID, p.BrandAndName, p.Body.ElementAt(p.BodyType), p.MaxPow, p.KPP.ElementAt(p.Transsmision), p.Ccategory.ElementAt(p.Category),p.carrclass.Where(x => x.Key == p.CarClass).FirstOrDefault().Value);
                }
            }

        }

        private void Button4_Click(object sender, EventArgs e)//тариф

        {
            if (maskedTextBox1.Text == "")
            {
                MessageBox.Show("Выберите клиента!");
            }
            else
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                button1.Enabled = false;
                button6.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                DataGridViewButtonColumn dgvID = new DataGridViewButtonColumn();
                dgvID.Name = "ID";
                DataGridViewTextBoxColumn brand = new DataGridViewTextBoxColumn();
                brand.Name = "Название";
                DataGridViewTextBoxColumn body = new DataGridViewTextBoxColumn();
                body.Name = "Стоимость";
                DataGridViewTextBoxColumn pow = new DataGridViewTextBoxColumn();
                pow.Name = "Стаж";
                DataGridViewTextBoxColumn kpp = new DataGridViewTextBoxColumn();
                kpp.Name = "Часовой";
                dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dgvID, brand, body, pow, kpp });


                foreach (Rate p in rateact.ShowAll(int.Parse(maskedTextBox1.Text)))
                {
                    dataGridView1.Rows.Add(p.ID, p.Name, p.Cost, p.ReqExp, p.HourStatus.ElementAt(p.Hour));
                }
            }

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0) //индекс колонки, в которой находится кнопка.
            {
                int id = Convert.ToInt32((dataGridView1[e.ColumnIndex, e.RowIndex].Value));//присваиваивается значение нажатой кнопки
                
                if (button1.Enabled == true)
                {
                    client = clientact.Find(id);
                    textBox1.Text = client.Name +" "+ client.Surname;
                    maskedTextBox1.Text = Convert.ToString(id);
                }
                if(button2.Enabled==true)
                {
                    worker = workact.Find(id);
                    textBox2.Text = worker.Name + " " + worker.Surname;
                    maskedTextBox2.Text = Convert.ToString(id);
                }
                if (button3.Enabled == true)
                {
                    car = caract.Find(id);
                    textBox3.Text = car.BrandAndName + "|" + car.carrclass.Where(x => x.Key == car.CarClass).FirstOrDefault().Value;
                    maskedTextBox3.Text = Convert.ToString(id);
                    
                }
                if (button4.Enabled == true)
                {
                    rate = rateact.Find(id);
                    textBox4.Text = rate.Name + "|" + rate.HourStatus.ElementAt(rate.Hour);
                    maskedTextBox4.Text = Convert.ToString(id);
                    
                }
               
                
                DateTime start = dateTimePicker1.Value;
                DateTime end = dateTimePicker2.Value;
                
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button6.Enabled = true;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (maskedTextBox4.Text=="" || maskedTextBox3.Text=="")
            { MessageBox.Show("Не все поля заполнены!"); }
            else
            {
                int idrate = int.Parse(maskedTextBox4.Text);
                int idcar = int.Parse(maskedTextBox3.Text);
                DateTime start = dateTimePicker1.Value;
                DateTime end = dateTimePicker2.Value;
                double ee = contract.FullCost(idcar, idrate, start, end);
                numericUpDown1.Value = Convert.ToDecimal(contract.FullCost(idcar, idrate, start, end));
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            
            if (maskedTextBox1.Text=="" || maskedTextBox2.Text == "" || maskedTextBox3.Text == "" || maskedTextBox4.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {
                if (dateTimePicker1.Value == dateTimePicker2.Value)
                {

                    MessageBox.Show("Проверьте дату!");

                }
                else
                {
                    int idcl = int.Parse(maskedTextBox1.Text);
                    int idwor = int.Parse(maskedTextBox2.Text);
                    int carid = int.Parse(maskedTextBox3.Text);
                    int idrent = int.Parse(maskedTextBox4.Text);
                    DateTime start = dateTimePicker1.Value;
                    DateTime end = dateTimePicker2.Value;
                    bool active = true;
                    if (contract.Add(idcl, idwor, carid, idrent, start, end, active))
                    {

                        MessageBox.Show("Договор добавлен!");
                        Close();
                    }
                }
            }
        }
    }
}
