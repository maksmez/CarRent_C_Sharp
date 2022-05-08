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
    public partial class DGRate : Form
    {
        RateActions rateact = new RateActions();
        Rate rate = new Rate();
        CarActions caract = new CarActions();
        ClientActions clientact = new ClientActions();


        public DGRate()
        {
            InitializeComponent();
            ShowRate();
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage1);
            bantextbox();
            button4.Visible = false;
            button2.Visible = false;


        }
        public void bantextbox()
        {

            foreach (TextBox text_box in tabPage2.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                text_box.Enabled = false;
            }
            foreach (ComboBox combo_box in tabPage2.Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
            {
                combo_box.Enabled = false;
            }
            foreach (NumericUpDown numeric in tabPage2.Controls.OfType<NumericUpDown>()) //То есть берём только 2 вкладку
            {
                numeric.Enabled = false;
            }
            foreach (RadioButton radio in tabPage2.Controls.OfType<RadioButton>()) //То есть берём только 2 вкладку
            {
                radio.Enabled = false;
            }

        }
        public void cleartext()
        {
            foreach (TextBox text_box in tabPage2.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                if (text_box != null)
                    text_box.Text = "";
            }
            
            foreach (NumericUpDown numeric in tabPage2.Controls.OfType<NumericUpDown>()) //То есть берём только 2 вкладку
            {
                if (numeric != null)
                    numeric.Text = "";
            }

            foreach (RadioButton radio in tabPage2.Controls.OfType<RadioButton>()) //То есть берём только 2 вкладку
            {
                radio.Checked = false;
            }
        }
        public void razbantextbox()
        {
            foreach (TextBox text_box in tabPage2.Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
            {
                text_box.Enabled = true;
            }
            foreach (ComboBox combo_box in tabPage2.Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
            {
                combo_box.Enabled = true;
            }
            foreach (NumericUpDown numeric in tabPage2.Controls.OfType<NumericUpDown>()) //То есть берём только 2 вкладку
            {
                numeric.Enabled = true;
            }


        }

        public void ShowRate()
        {
            dataGridView1.Rows.Clear();
            
            //dataGridView1.DataSource = rateact.ShowAll();
            foreach (Rate p in rateact.ShowAll())
            {
                dataGridView1.Rows.Add(p.ID, p.Name, p.ReqExp, p.Cost, p.HourStatus.ElementAt(p.Hour));
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RateAdd newForm = new RateAdd();
            newForm.ShowDialog();
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.ColumnIndex == 0) //индекс колонки, в которой находится кнопка.
            {
                tabControl1.TabPages.Add(tabPage2);//добавление вкладки
                tabControl1.TabPages.Add(tabPage1);//добавление вкладки
                int ratef = Convert.ToInt32((dataGridView1[e.ColumnIndex, e.RowIndex].Value));//присваиваивается значение нажатой кнопки
                rate = rateact.Find(ratef);
                
                if (rate.Hour == 1)
                {
                    radioButton1.Checked = true;
                }
                if (rate.Hour == 0)
                {
                    radioButton2.Checked = true;
                }

                textBox1.Text = rate.Description;
                numericUpDown2.Value = Convert.ToInt32(rate.ReqExp);
                numericUpDown1.Value = Convert.ToDecimal(rate.Cost);
                textBox3.Text = rate.Name;
                textBox2.Text = Convert.ToString(rate.ID);
                button3.Enabled = true;//активность кнопки удалить
                button4.Enabled = true;//активность кнопки редактировать
                if (rate.Hour == 0)
                    label1.Text = "Стоимость в час";
                else
                    label1.Text = "Стоимость за сутки";

                tabControl1.SelectTab(tabPage2);//открывает вкладку
                
                dataGridView2.Rows.Clear();
                
                foreach (Contract p in rate.GetContracts())
                {
                    dataGridView2.Rows.Add(p.ID, caract.Find(p.CarID).BrandAndName, clientact.Find(p.ClientID).Surname+" "+ clientact.Find(p.ClientID).Name, p.DateStartContract,p.DateEndContract);
                }
                
            }
        }



        private void Button1_Click(object sender, EventArgs e)//редактировать
        {
            tabControl1.TabPages.Remove(All);
            razbantextbox();
            button2.Visible = true;
            button4.Visible = true;
            button1.Enabled = false;
            button3.Enabled = false;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            textBox2.Enabled = false;
        }

        private void Button2_Click(object sender, EventArgs e)//ок
        {
            tabControl1.TabPages.Add(All);
            button3.Enabled = true;
            button1.Enabled = true;
            button2.Visible = false;
            button4.Visible = false;
            bantextbox();
            
            int id = int.Parse(textBox2.Text);
            string name = textBox3.Text;
            double cost = Convert.ToDouble(numericUpDown1.Value);
            int exp = Convert.ToInt32(numericUpDown2.Value);
            string description = textBox1.Text;
            int hour = 0;
            if (radioButton2.Checked == true)
            {
                hour = 0;

            }
            if (radioButton1.Checked == true)
            {
                hour = 1;

            }
            bool status = false;//DelST
            if (textBox3.Text == "" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {
                if (rate.Update(id, name, exp, cost, description, hour, status))
                {
                    MessageBox.Show("Тариф успешно обновлен!");
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
                ShowRate();
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

        }

        private void Button4_Click(object sender, EventArgs e)//отмена
        {
            if (rate.Hour == 1)
            {
                radioButton1.Checked = true;
            }
            if (rate.Hour == 0)
            {
                radioButton2.Checked = true;
            }

            textBox1.Text = rate.Description;
            numericUpDown2.Text = Convert.ToString(rate.ReqExp);/////////////////при пустом поле не возврещается значение
            numericUpDown1.Text = Convert.ToString(rate.Cost);/////////////////при пустом поле не возврещается значение
            textBox3.Text = rate.Name;
            textBox2.Text = Convert.ToString(rate.ID);
            button3.Enabled = true;//активность кнопки удалить
            button4.Enabled = true;//активность кнопки редактировать
            if (rate.Hour == 0)
                label1.Text = "Стоимость в час";
            else
                label1.Text = "Стоимость за сутки";
            tabControl1.TabPages.Add(All);
            button3.Enabled = true;
            button1.Enabled = true;
            button2.Visible = false;
            button4.Visible = false;
            bantextbox();
        }

        private void Button3_Click(object sender, EventArgs e)//кнопка удалить
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Тариф не выбран!");
            }
            else
            {
                int id = Convert.ToInt32(textBox2.Text);
                DialogResult result = MessageBox.Show("Вы уверены?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    if (rate.Del(id))
                    {
                        MessageBox.Show("Тариф удален!");
                        ShowRate();
                        tabControl1.SelectTab(All);//открывает вкладку
                        cleartext();
                    }
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            ShowRate();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
