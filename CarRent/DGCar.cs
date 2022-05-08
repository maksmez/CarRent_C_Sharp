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
    public partial class DGCar : Form
    {
        CarActions carra = new CarActions();
        Car car = new Car();
        RateActions rateact = new RateActions();
        Rate rate = new Rate();
        CarActions caract = new CarActions();
        ClientActions clientact = new ClientActions();
        WorkerActions woract = new WorkerActions();
        public DGCar()
        {
            InitializeComponent();
            ShowCar();
            //заполнение комбо из листов
            comboBox3.DataSource = car.Body;
            comboBox4.DataSource = car.Engine;
            comboBox7.DataSource = car.KPP;
            comboBox8.DataSource = car.Drive;
            comboBox6.DataSource = car.StatussDamage;
            comboBox5.DataSource = car.Free;
            //
            //запрет ввода с клавы
            foreach (ComboBox combo_box in tabPage2.Controls.OfType<ComboBox>())
            {
                combo_box.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            //CarCombobox();
            bantextbox();
            //comboBox1.Enabled = true;
            button3.Enabled = false;//активность кнопки удалить
            button4.Enabled = false;//активность кнопки редактировать
            button7.Visible = false;//видимость "ок"
            button8.Visible = false;//видимость "отмена"
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage1);
            //tabControl1.TabPages[1].Parent = null;


        }
        //public void CarCombobox()
        //{
        //    comboBox1.Items.Clear();
        //    comboBox1.BeginUpdate();//обновление комбобокса
        //    foreach (var i in carra.ShowAll())
        //    {
        //        string s = Convert.ToString(i.ID + "|" + i.BrandAndName);
        //        comboBox1.Items.Add(s);
        //    }

        //}
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
            foreach (NumericUpDown numeric in tabPage2.Controls.OfType<NumericUpDown>()) //То есть берём только 2 вкладку
            {
                if (numeric != null)
                    numeric.Text = "";
            }


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
            //comboBox1.Enabled = true;

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
        public void ShowCar()
        {
            dataGridView1.Rows.Clear();
            foreach (Car p in carra.ShowAll())
            {
                dataGridView1.Rows.Add(p.ID, p.BrandAndName, p.Body.ElementAt(p.BodyType), p.Engine.ElementAt(p.EngineType), p.KPP.ElementAt(p.Transsmision), p.Ccategory.ElementAt(p.Category), p.carrclass.Where(x => x.Key == p.CarClass).FirstOrDefault().Value, p.MaxPow, p.Free.ElementAt(p.Status), p.StatussDamage.ElementAt(p.Damage));

            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            CarAdd newForm = new CarAdd();
            newForm.ShowDialog();

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ShowCar();
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
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //comboBox1.Items.Clear();//очищает комбобокс
            if (e.ColumnIndex == 0) //индекс колонки, в которой находится кнопка.
            {
                int carf = Convert.ToInt32((dataGridView1[e.ColumnIndex, e.RowIndex].Value));//присваиваивается значение нажатой кнопки
                //comboBox1.BeginUpdate();//обновление комбобокса
                //foreach (var i in carra.ShowAll())
                //{
                //    string s = Convert.ToString(i.ID+"|"+ i.BrandAndName);
                //    comboBox1.Items.Add(s);
                //    if (Convert.ToInt32(carf) == i.ID)
                //    comboBox1.Text = s;
                //}
                //comboBox1.EndUpdate();//завершение обновления

                tabControl1.TabPages.Add(tabPage2); ;
                tabControl1.TabPages.Add(tabPage1); ;
                car = carra.Find(carf);
                textBox2.Text = car.BrandAndName;
                comboBox3.Text = car.Body.ElementAt(car.BodyType);
                comboBox4.Text = car.Engine.ElementAt(car.EngineType);
                numericUpDown1.Text = Convert.ToString(car.MaxPow);
                comboBox7.Text = car.KPP.ElementAt(car.Transsmision);
                numericUpDown2.Value = car.NumberOfSeats;
                comboBox9.Text = car.Ccategory.ElementAt(car.Category);
                comboBox8.Text = car.Drive.ElementAt(car.WheelDrive);
                textBox10.Text = car.Color;
                textBox9.Text = car.CarNumber;
                comboBox2.Text = car.carrclass.Where(x => x.Key == car.CarClass).FirstOrDefault().Value;
                comboBox6.Text = car.StatussDamage.ElementAt(car.Damage);
                comboBox5.Text = car.Free.ElementAt(car.Status);
                textBox16.Text = car.Comment;
                textBox1.Text = Convert.ToString(car.ID);
                button3.Enabled = true;//активность кнопки удалить
                button4.Enabled = true;//активность кнопки редактировать

                //contractBindingSource.DataSource = car.ListOfContracts();

                dataGridView2.Rows.Clear();

                foreach (Contract p in car.ListOfContracts())
                {
                    dataGridView2.Rows.Add(p.ID, clientact.Find(p.ClientID).Surname + " " + clientact.Find(p.ClientID).Name, woract.Find(p.WorkerID).Surname + " " + woract.Find(p.WorkerID).Name, p.DateStartContract, p.DateEndContract);
                }



                tabControl1.SelectTab(tabPage2);

            }
        }
        //private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    string car = Convert.ToString(comboBox1.SelectedItem);
            
        //    string[] b = car.Split('|');
        //    if (car == "")
        //    {
        //        MessageBox.Show("Автомобиль не выбран!");
        //    }
        //    else
        //    {
        //        foreach (var i in carra.ShowAll())
        //        {
        //            if (Convert.ToInt32(b[0]) == i.ID)
        //            {

        //                comboBox3.Text = i.Body.ElementAt(i.BodyType);
        //                comboBox4.Text = i.Engine.ElementAt(i.EngineType);
        //                numericUpDown1.Text = Convert.ToString(i.MaxPow);
        //                comboBox7.Text = i.KPP.ElementAt(i.Transsmision);
        //                numericUpDown2.Value = i.NumberOfSeats;
        //                comboBox9.Text = i.Ccategory.ElementAt(i.Category);
        //                comboBox8.Text = i.Drive.ElementAt(i.WheelDrive);
        //                textBox10.Text = i.Color;
        //                textBox9.Text = i.CarNumber;
        //                comboBox2.Text = i.carrclass.Where(x => x.Key == i.CarClass).FirstOrDefault().Value;
        //                comboBox6.Text = i.StatussDamage.ElementAt(i.Status);
        //                comboBox5.Text = i.Free.ElementAt(i.Status);
        //                textBox16.Text = i.Comment;
        //                textBox1.Text = Convert.ToString(i.ID);
        //                button3.Enabled = true;//активность кнопки удалить
        //                button4.Enabled = true;//активность кнопки редактировать

        //            }
        //        }
        //    }


        //}
        private void Button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Автомобиль не выбран!");
            }
            else
            {
                int id = Convert.ToInt32(textBox1.Text);
                DialogResult result = MessageBox.Show("Вы уверены?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    if (car.Del(id))
                    {
                        MessageBox.Show("Автомобиль удален!");
                        ClearText();
                        ShowCar();
                        //CarCombobox();
                        tabControl1.SelectTab(All);//открывает вкладку

                    }
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)//кнопка редактировать
        {
            razbantextbox();
            tabControl1.TabPages.Remove(All);
            textBox1.Enabled = false;
            button7.Visible = true;//видимость "ок"
            button8.Visible = true;//видимость "отмена"
            button3.Enabled = false;//видимость удалить
            button4.Enabled = false;
            comboBox5.Enabled = false;
           // comboBox1.Enabled = false;//видимость комбо
        }

        private void Button7_Click(object sender, EventArgs e)//кнопка ОК
        {
            tabControl1.TabPages.Add(All);
            button7.Visible = false;//видимость "ок"
            button8.Visible = false;//видимость "отмена"
            button3.Enabled = true;//видимость удалить
           // comboBox1.Enabled = true;//видимость комбо
            button4.Enabled = true;

            //string mm = comboBox1.Text;//"Введите марку и модель
            string body2 = comboBox3.Text;//Введите тип кузова (Седан,Внедорожник,Минивэн,Хэтчбек,Купе,Универсал,Кабриолет, Пикап)
            int body = 0;
            body = car.Body.IndexOf(body2);

            string category1 = comboBox9.Text;//Введите категорию (A,B,C,D)
            int category = 0;
            category = car.Ccategory.IndexOf(category1);

            string engine1 = comboBox4.Text;//Введите тип двигателя (Бензиновый,Дизельный)
            int engine = 0;
            engine = car.Engine.IndexOf(engine1);

            int pow = Convert.ToInt32(numericUpDown1.Value);//Введите мощность

            int transmission = 0;
            string transmission1 = comboBox7.Text;//Введите тип трансмиссии (Автоматическая, Механическая)
            transmission = car.KPP.IndexOf(transmission1);

            int num = Convert.ToInt32(numericUpDown2.Value); //Введите число мест

            string drive1 = comboBox8.Text;//Введите тип привода (Полный,Передний,Задний)
            int drive = car.Drive.IndexOf(drive1);

            string color = textBox10.Text;//Введите цвет

            string carnum = textBox9.Text;//Введите номер

            string comment = textBox16.Text;//Введите комментарий

            string classcar1 = comboBox2.Text;//Введите класс(Мелкий,Низший,Средний,Высокий,Представительский)
            double classcar = 0;
            classcar = car.carrclass.Where(x => x.Value == classcar1).FirstOrDefault().Key;

            string damage1 = comboBox6.Text;//На ходу или сломан
            int damage = car.StatussDamage.IndexOf(damage1);


            string free1 = comboBox5.Text;
            int free = car.Free.IndexOf(free1);

            int id = Convert.ToInt32(textBox1.Text);

            if (body2 == "" || category1 == "" || engine1 == "" || transmission1 == "" || drive1 == "" || color == "" || carnum == "" || classcar1 == "" || damage1 == "" || pow == 0 || num == 0)
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {

                if (car.Update(id, body, category, engine, pow, transmission, num, drive, color, carnum, comment, damage, classcar, free))
                {
                    MessageBox.Show("Данные обновлены!");
                    foreach (TextBox text_box in Controls.OfType<TextBox>()) //То есть берём только 2 вкладку
                    {
                        if (text_box != null)
                            text_box.Text = "";
                    }
                    foreach (ComboBox combo_box in Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
                    {
                        if (combo_box != null)
                            combo_box.Text = "";
                    }
                    foreach (NumericUpDown numeric in Controls.OfType<NumericUpDown>()) //То есть берём только 2 вкладку
                    {
                        if (numeric != null)
                            numeric.Text = "";
                    }
                }

                else
                { MessageBox.Show("Ошибка!"); }

                ShowCar();
                bantextbox();
            }
        }

        private void Button8_Click(object sender, EventArgs e)//кнопка ОТМЕНА
        {
            tabControl1.TabPages.Add(All);
            comboBox3.Text = car.Body.ElementAt(car.BodyType);
            comboBox4.Text = car.Engine.ElementAt(car.EngineType);
            numericUpDown1.Text = Convert.ToString(car.MaxPow);
            comboBox7.Text = car.KPP.ElementAt(car.Transsmision);
            numericUpDown2.Text = Convert.ToString(car.NumberOfSeats);
            comboBox9.Text = car.Ccategory.ElementAt(car.Category);
            comboBox8.Text = car.Drive.ElementAt(car.WheelDrive);
            textBox10.Text = car.Color;
            textBox9.Text = car.CarNumber;
            comboBox2.Text = car.carrclass.Where(x => x.Key == car.CarClass).FirstOrDefault().Value;
            comboBox6.Text = car.StatussDamage.ElementAt(car.Damage);
            comboBox5.Text = car.Free.ElementAt(car.Status);
            textBox16.Text = car.Comment;
            textBox1.Text = Convert.ToString(car.ID);
            
            button7.Visible = false;//видимость "ок"
            button8.Visible = false;//видимость "отмена"
            button3.Enabled = true;//видимость удалить
            //comboBox1.Enabled = true;//видимость комбо
            button4.Enabled = true;
            bantextbox();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
