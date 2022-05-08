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
    public partial class ClientAdd : Form
    {
        DGClient show = new DGClient();
        Client client = new Client();
        ClientActions clientact = new ClientActions();
        
        public ClientAdd()
        {
            InitializeComponent();
            dateTimePicker2.MaxDate = DateTime.Today.AddDays(-6574);//сегодня-18лет
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {

            string surname = textBox18.Text;
            string name = textBox17.Text;
            string patronymic = textBox16.Text;
            DateTime date = dateTimePicker2.Value.Date;
            string phone = textBox15.Text;
            string passport = textBox6.Text;
            string inn = textBox1.Text;
            string vu = textBox14.Text;
            DateTime start = dateTimePicker1.Value.Date;
            string comm = textBox2.Text;
            bool status = false;
            int id = 0;


            if (name == "" || surname == "" || patronymic == "" || phone == "" || passport == "" || inn == "" || vu == ""|| !checkBox1.Checked & !checkBox2.Checked & !checkBox3.Checked & !checkBox4.Checked)
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {

                if(client.Add( name,  surname,  patronymic,  date,  phone,  passport,  inn,  vu,  start,  comm,  status, out id))
                {
                    MessageBox.Show("Клиент успешно добавлен!");

                    if (checkBox1.Checked)
                    {
                        client.Clcategory("A", id);

                    }
                    if (checkBox2.Checked)
                    {
                        client.Clcategory("B", id);

                    }
                    if (checkBox3.Checked)
                    {
                        client.Clcategory("C", id);

                    }
                    if (checkBox4.Checked)
                    {
                        client.Clcategory("D", id);

                    }
                    

                    foreach (TextBox text_box in Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
                    {
                        if (text_box != null)
                            text_box.Text = "";
                    }
                    foreach (CheckBox checkBox in Controls.OfType<CheckBox>()) //То есть берём только 2 вкладку
                    {
                        checkBox.Checked = false;
                    }
                    

                }


            }
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)//подстановка даты для ВУ
        {
            DateTime vud = dateTimePicker2.Value;
            vud = vud.AddDays(6574);
            dateTimePicker1.MinDate = vud;
        }
    }
}
