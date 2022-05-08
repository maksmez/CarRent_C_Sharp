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
    public partial class DGPayment : Form
    {
        Car car = new Car();
        Client client = new Client();
        Worker worker = new Worker();
        Contract contract = new Contract();
        Rate rate = new Rate();
        ContractActions contractact = new ContractActions();
        Payment payment = new Payment();
        PaymentActions payact = new PaymentActions();
        ClientActions clientact = new ClientActions();
        WorkerActions woract = new WorkerActions();
        CarActions caract = new CarActions();
        RateActions rateact = new RateActions();
        public DGPayment()
        {
            
            InitializeComponent();
            PayCombobox();
            BanText();
        }
        public void PayCombobox()
        {
            comboBox1.Items.Clear();
            comboBox1.BeginUpdate();//обновление комбобокса
            foreach (var i in contractact.ShowAll())
            {
                string s = Convert.ToString(i.ID);
                if (i.Balance == 0)
                {

                }
                else
                {
                    comboBox1.Items.Add(s);
                }
            }

        }

        public void BanText()
        {

            foreach (TextBox text_box in splitContainer1.Panel1.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                text_box.Enabled = false;
            }
            foreach (DateTimePicker dateTimePicker in splitContainer1.Panel1.Controls.OfType<DateTimePicker>()) //То есть берём только 2 вкладку
            {
                dateTimePicker.Enabled = false;
            }
            numericUpDown3.Enabled = false;
            dateTimePicker1.Enabled = false;

        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(comboBox1.Text);
            contract = contractact.Find(id);
            client = clientact.Find(contract.ClientID);
            worker = woract.Find(contract.WorkerID);
            car = caract.Find(contract.CarID);
            rate = rateact.Find(contract.RateID);
            
            textBox1.Text = client.Surname +" "+ client.Name;
            textBox2.Text = worker.Surname +" "+ worker.Name;
            textBox5.Text = rate.Name;
            textBox6.Text = car.BrandAndName;
            dateTimePicker2.Value = contract.DateStartContract;
            dateTimePicker3.Value = contract.DateEndContract;
            numericUpDown3.Value = Convert.ToDecimal(contract.Balance);
            if (contract.Getpay() != DateTime.MinValue)
            {
                dateTimePicker1.Value = contract.Getpay();

            }
            else
                dateTimePicker1.Value = DateTime.Now;
           
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Выберите договор!");
            }
            else
            {
                int id = int.Parse(comboBox1.Text);
                contract = contractact.Find(id);
                int contid = int.Parse(comboBox1.Text);
                DateTime datepay = DateTime.Now;
                double ammount = Convert.ToDouble(numericUpDown2.Value);
                if (ammount == 0)
                {
                    MessageBox.Show("Ошибка!");
                }
                else
                {
                    if (ammount > contract.Balance)
                    {

                        MessageBox.Show("Сумма оплаты превосходит долг!");

                    }
                    else
                    {
                        if (payment.Add(contid, datepay, ammount))
                        {
                            id = int.Parse(comboBox1.Text);
                            contract = contractact.Find(id);
                            MessageBox.Show("Оплата прошла успешно!");
                            numericUpDown3.Value = Convert.ToDecimal(contract.Balance);
                            numericUpDown2.Value = 0;

                        }
                    }
                }
            }
        }
    }
}
