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
    public partial class CarAdd : Form
    {
        DGCar show = new DGCar();
        Car car = new Car();

        public CarAdd()
        {
            InitializeComponent();
            //заполнение комбо из листов
            comboBox3.DataSource = car.Body;
            comboBox4.DataSource = car.Engine;
            comboBox7.DataSource = car.KPP;
            comboBox8.DataSource = car.Drive;
            comboBox6.DataSource = car.StatussDamage;
            //
            //запрет ввода с клавы
            foreach (ComboBox combo_box in Controls.OfType<ComboBox>()) //То есть берём только 2 вкладку
            {
                combo_box.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            //
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
            show.ShowCar();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string mm = textBox14.Text;//"Введите марку и модель
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

            int free = 0; 
            bool status = false;//столбец DELST

            if (mm == "" || body2 == "" || category1 == "" || engine1 == "" || transmission1 == "" || drive1 == "" || color == "" || carnum == "" || classcar1 == "" || damage1 == "" || pow == 0 || num == 0)
            {
                MessageBox.Show("Не все поля заполнены!");

            }
            else
            {

                if (car.Add(mm, body, category, engine, pow, transmission, num, drive, color, carnum, comment, damage, classcar, free, status))
                {
                     
                    MessageBox.Show("Автомобиль успешно добавлен!");
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

            }
        }

        
    }
}
