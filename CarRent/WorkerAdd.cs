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
    public partial class WorkerAdd : Form
    {
        Worker worker = new Worker();
        DGWorker dgworker = new DGWorker();
        public WorkerAdd()
        {
            InitializeComponent();
            comboBox2.DataSource = worker.Positionn;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(-6574);//сегодня-18лет

        }

        private void Button3_Click(object sender, EventArgs e)
        {
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

            bool status = false;
            if (name == "" || surname == "" || patronymic == "" || phone == "" || passport == "" || inn == "" || pos1 == "")
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {

                if (worker.Add(name, surname, patronymic, date, phone, passport, inn, position, start, status))
                {
                    MessageBox.Show("Работник успешно добавлен!");
                }
                foreach (TextBox text_box in Controls.OfType<TextBox>()) 
                {
                    if (text_box != null)
                        text_box.Text = "";
                }
                foreach (ComboBox combo_box in Controls.OfType<ComboBox>()) 
                {
                    if (combo_box != null)
                        combo_box.Text = "";
                }
                foreach (CheckBox checkBox in Controls.OfType<CheckBox>()) 
                {
                    checkBox.Checked = false;
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)//закрыть
        {
            Close();
        }
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)//подстановка даты для ВУ
        {
            DateTime vud = dateTimePicker1.Value;
            vud = vud.AddDays(5843);
            dateTimePicker2.MinDate = vud;
        }
    }
}
