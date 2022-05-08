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

    public partial class DGContract : Form
    {
        Car car = new Car();
        Client client = new Client();
        Worker worker = new Worker();
        Contract contract = new Contract();
        Rate rate = new Rate();
        ContractActions contractact = new ContractActions();
        Payment payment = new Payment();



        public DGContract()
        {
            InitializeComponent();
            ShowContract();
            tabControl1.TabPages.Remove(tabPage2);
            button2.Visible = false;
            button4.Visible = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy hh:mm";
            
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy hh:mm";
            
        }
        public void ShowContract()//обновление датагрида
        {
            dataGridView1.Rows.Clear();
            using (Context cont = new Context())
            {

                
                foreach (Contract p in contractact.ShowAll())
                {
                    Client client = cont.Clients.Where(x=>x.ID == p.ClientID).SingleOrDefault();
                    Car car = cont.Cars.Where(x => x.ID == p.CarID).SingleOrDefault();
                    Rate rate = cont.Rates.Where(x => x.ID == p.RateID).SingleOrDefault();
                    string active;
                    if(p.SActive==true)
                    {
                        active = "Активен";
                    }
                    else
                    {
                        active = "Расторгнут";
                    }
                    dataGridView1.Rows.Add(p.ID, client.Surname+" "+client.Name, car.BrandAndName, rate.Name, p.Balance, active);
                }

            }
        }
        public void Bantext()
        {

            foreach (TabPage tab in tabControl2.Controls.OfType<TabPage>())
            {
                foreach (TextBox text_box in tab.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
                {
                    text_box.Enabled = false;
                }
                foreach (ComboBox combo_box in tab.Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
                {
                    combo_box.Enabled = false;
                }
                foreach (NumericUpDown numeric in tab.Controls.OfType<NumericUpDown>()) //То есть берём только 2 вкладку
                {
                    numeric.Enabled = false;
                }
                foreach (RadioButton radio in tab.Controls.OfType<RadioButton>()) //То есть берём только 2 вкладку
                {
                    radio.Enabled = false;
                }
                foreach (CheckBox checkBox in tab.Controls.OfType<CheckBox>()) //То есть берём только 2 вкладку
                {
                    checkBox.Enabled = false;
                }
                foreach (DateTimePicker date in tab.Controls.OfType<DateTimePicker>()) //То есть берём только 2 вкладку
                {
                    date.Enabled = false;
                }
            }

        }
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)//перехват переключения вкладки
        {
            string name = tabControl1.SelectedTab.Name;
            if (name == "All")
            {
                tabControl1.TabPages.Remove(tabPage2);
               
            }

        }//перехват выбора вкладки
        private void Button7_Click(object sender, EventArgs e)
        {
            ShowContract();
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (CheckBox checkBox in tabPage2.Controls.OfType<CheckBox>()) //То есть берём только 2 вкладку
            {
                checkBox.Checked = false;
            }
            checkBox1.Checked = false;
            if (e.ColumnIndex == 0) //индекс колонки, в которой находится кнопка.
            {
                dateTimePicker2.MinDate = new DateTime(1900, 6, 20);
                tabControl1.TabPages.Add(tabPage2);//добавление вкладки
                int contid = Convert.ToInt32((dataGridView1[e.ColumnIndex, e.RowIndex].Value));//присваиваивается значение нажатой кнопки
                contract = contractact.Find(contid);
                client = new ClientActions().Find(contract.ClientID);
                rate = new RateActions().Find(contract.RateID);
                car = new CarActions().Find(contract.CarID);
                worker = new WorkerActions().Find(contract.WorkerID);

                textBox1.Text = Convert.ToString(contract.ID);
                dateTimePicker1.Value = contract.DateStartContract;
                dateTimePicker2.Value = contract.DateEndContract;
                numericUpDown1.Text = Convert.ToString(contract.Balance);

                if (contract.SActive == true)
                {
                    radioButton1.Checked = true;

                }
                if (contract.SActive == false)
                {
                    radioButton2.Checked = true;
                }
                if(contract.SExtend==true)
                {
                    checkBox1.Checked = true;
                }
                ///////////////////////////////////////////
                ///клиент
                textBox3.Text = Convert.ToString(client.ID);
                textBox18.Text = client.Surname;
                textBox17.Text = client.Name;
                textBox16.Text = client.Patronymic;
                textBox15.Text = client.Phone;
                textBox6.Text = client.Passport;
                textBox4.Text = client.INN;
                textBox14.Text = client.NumVU;
                textBox2.Text = client.ClientComment;
                dateTimePicker3.Value = client.DateStartExp;
                dateTimePicker4.Value = client.Date;
                var catt = client.Clcategory();

                foreach (var c in catt)
                {
                    if (c.ID == 0)
                    {
                        checkBox5.Checked = true;
                        checkBox5.CheckState = CheckState.Indeterminate;
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



                ////////////////////////////////////////////
                //Тариф
                if (rate.Hour == 1)
                {
                    radioButton4.Checked = true;
                }
                if (rate.Hour == 0)
                {
                    radioButton3.Checked = true;
                }

                textBox24.Text = rate.Description;
                numericUpDown4.Value = Convert.ToInt32(rate.ReqExp);
                numericUpDown5.Value = Convert.ToDecimal(rate.Cost);
                textBox22.Text = rate.Name;
                textBox23.Text = Convert.ToString(rate.ID);
                
                if (rate.Hour == 0)
                    label47.Text = "Стоимость в час";
                else
                    label47.Text = "Стоимость за сутки";
                ///////////////////////////////////////////////////////
                ///машина
                textBox25.Text = car.BrandAndName;
                textBox13.Text = Convert.ToString(car.ID);
                comboBox3.Text = car.Body.ElementAt(car.BodyType);
                comboBox4.Text = car.Engine.ElementAt(car.EngineType);
                numericUpDown2.Text = Convert.ToString(car.MaxPow);
                comboBox8.Text = car.KPP.ElementAt(car.Transsmision);
                numericUpDown3.Value = car.NumberOfSeats;
                comboBox10.Text = car.Ccategory.ElementAt(car.Category);
                comboBox9.Text = car.Drive.ElementAt(car.WheelDrive);
                textBox21.Text = car.Color;
                textBox20.Text = car.CarNumber;
                comboBox6.Text = car.carrclass.Where(x => x.Key == car.CarClass).FirstOrDefault().Value;
                comboBox7.Text = car.StatussDamage.ElementAt(car.Damage);
                comboBox5.Text = car.Free.ElementAt(car.Status);
                textBox19.Text = car.Comment;
                ///////////////////////////////////////////////////////////
                ///работник
                textBox5.Text = Convert.ToString(worker.ID);
                textBox10.Text = worker.Surname;
                textBox9.Text = worker.Name;
                textBox8.Text = worker.Patronymic;
                dateTimePicker6.Value = worker.Date;
                textBox7.Text = worker.Phone;
                comboBox2.Text = worker.Positionn.ElementAt(worker.Position);
                dateTimePicker5.Value = worker.DateStartExp;
                textBox12.Text = worker.Passport;
                textBox11.Text = worker.INN;

                Bantext();
                dateTimePicker2.MinDate = contract.DateEndContract;
                tabControl1.SelectTab(tabPage2);//открывает вкладку
                if (radioButton2.Checked == true)
                {
                    Bantext();
                    button1.Visible = false;

                }
                else
                {

                    button1.Visible = true;
                }
                paymentBindingSource.DataSource = contract.HistoryPayment();           
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            ContractAdd newForm = new ContractAdd();
            newForm.ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            DialogResult result = MessageBox.Show("Вы уверены?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                if (contract.Del(id))
                {
                    MessageBox.Show("Договор удален!");
                    ShowContract();
                    tabControl1.SelectTab(All);//открывает вкладку
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)//редактировать
        {
            tabControl1.TabPages.Remove(All);
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            button3.Enabled = false;
            button1.Enabled = false;
            if(radioButton1.Checked==true)
            {
            //dateTimePicker2.Enabled = true;
            checkBox1.Enabled = true;
            button2.Visible = true;
            button4.Visible = true;

            }
            if (checkBox1.Checked == true)
            {

                dateTimePicker2.Enabled = true;

            }

        }

        private void Button2_Click(object sender, EventArgs e)//ок
        {
            tabControl1.TabPages.Add(All);

            int id = int.Parse(textBox1.Text);
            bool status = true;
            bool sextend = false;
            button1.Enabled = true;
            if (radioButton1.Checked==true)
            {
                status = true;
                button2.Visible = false;
                button4.Visible = false;
                button3.Enabled = true;
                Bantext();
            }
            if (radioButton2.Checked == true)
            {
                if (contract.Balance == 0)
                {
                    status = false;
                    button2.Visible = false;
                    button4.Visible = false;
                    button3.Enabled = true;
                    Bantext();
                    contract = contractact.Find(int.Parse(textBox1.Text));
                    car = new CarActions().Find(contract.CarID);
                    car.Status = 0;
                }
                else
                {
                    MessageBox.Show("Долг не оплачен!");
                }
            }
            if(checkBox1.Checked==true)
            {
                
              sextend = true;
            }
            int idcar = int.Parse(textBox13.Text);
            int idrate = int.Parse(textBox23.Text);
           // DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;
            double balance = Convert.ToDouble(numericUpDown1.Value);
            contract.Update(id,idcar,idrate,end,balance,status,sextend);
        }

        private void Button4_Click(object sender, EventArgs e)//отмена
        {
            tabControl1.TabPages.Add(All);
            button1.Enabled = true;
            button2.Visible = false;
            button4.Visible = false;
            button3.Enabled = true;
            dateTimePicker2.Value = contract.DateEndContract;
            numericUpDown1.Value = Convert.ToDecimal(contract.Balance);
            if(contract.SActive==true)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            if(contract.SExtend==true)
            {

                checkBox1.Checked = true;
            }
            else
            {

                checkBox1.Checked = false;
            }

            Bantext();
        }


        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
            if (textBox13.Text == "")
            {
                

            }
            else
            {
                double b = contract.Balance;
                int idcar = int.Parse(textBox13.Text);
                int idrate = int.Parse(textBox23.Text);
                DateTime o = contract.DateEndContract;
                DateTime start = o;
                DateTime end = dateTimePicker2.Value;
                double balance1 = contract.FullCost(idcar, idrate, start, end);
                double balance = b + balance1;
                numericUpDown1.Value = Convert.ToDecimal(balance);

            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {

                dateTimePicker2.Enabled = true;

            }
            else
            {
                dateTimePicker2.Enabled = false;

            }
        }
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            radioButton2.Enabled = true;
        }
        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Bantext();
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
