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
    public partial class RateAdd : Form
    {
        DGRate gRate = new DGRate();
        Rate rate = new Rate();
        public RateAdd()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string name = textBox2.Text;
            double cost = Convert.ToDouble(numericUpDown1.Value);
            int exp = Convert.ToInt32(numericUpDown2.Value);
            string description = textBox1.Text;
            int hour = 0;
            if(radioButton1.Checked==true)
            {
                hour = 0;

            }
            if (radioButton2.Checked == true)
            {
                hour = 1;

            }
            bool status = false;//DelST
            if (textBox2.Text == "" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {
                if (rate.Add(name, exp, cost, description, hour, status))
                {
                    MessageBox.Show("Тариф успешно добавлен!");
                    foreach (TextBox text_box in Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
                    {
                        if (text_box != null)
                            text_box.Text = "";
                    }

                    foreach (NumericUpDown numeric in Controls.OfType<NumericUpDown>()) //То есть берём только 2 вкладку
                    {
                        if (numeric != null)
                            numeric.Text = "";
                    }
                }
            }
            gRate.ShowRate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gRate.ShowRate();
            Close();
        }

        private void RateAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            gRate.ShowRate();
        }
    }
}
