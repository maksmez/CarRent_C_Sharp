using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRent
{
    public class Car
    {
        public int ID { get; set; }
        public string BrandAndName { get; set; }
        public int BodyType { get; set; }
        public int EngineType { get; set; }
        public int MaxPow { get; set; }
        public int Transsmision { get; set; }
        public int NumberOfSeats { get; set; }
        public int Category { get; set; }
        public int WheelDrive { get; set; }
        public string Color { get; set; }
        public string CarNumber { get; set; }

        public string Comment { get; set; }
        public int Damage { get; set; }
        public double CarClass { get; set; }
        public int Status { get; set; }
        public bool DelST { get; set; }

        public List<string> Body = new List<string>() { "Седан", "Внедорожник", "Минивэн", "Хэтчбек", "Купе", "Универсал", "Кабриолет", "Пикап" };
        public List<string> Drive = new List<string>() { "Передний", "Задний", "Полный" };
        public List<string> StatussDamage = new List<string>() { "На ходу", "Сломан" };
        public List<string> Free = new List<string>() { "Свободен", "Занят" };
        public List<string> Engine = new List<string>() { "Бензиновый", "Дизельный" };
        public List<string> KPP = new List<string>() { "Автоматическая", "Механическая" };
        public List<string> Ccategory = new List<string>() { "A", "B", "C", "D" };
        public Dictionary<double, string> carrclass = new Dictionary<double, string>
        {
            {1.1, "Мелкий"},
            {1.2, "Низший"},
            {1.3, "Средний"},
            {1.4, "Высокий"},
            {1.5, "Представительский"},

        };
        public Car()
        {

        }


        public bool Add(string mm, int body, int category, int engine, int pow, int transmission, int num, int drive, string color, string carnum, string comment, int damage, double classcar, int free, bool status)
        {
            using (Context context = new Context())
            {
                BrandAndName = mm;
                BodyType = body;
                EngineType = engine;
                MaxPow = pow;
                Transsmision = transmission;
                NumberOfSeats = num;
                WheelDrive = drive;
                Color = color;
                CarNumber = carnum;
                Comment = comment;
                Damage = damage;
                CarClass = classcar;
                Status = free;
                DelST = status;
                Category = category;
                context.Cars.Add(this);
                context.SaveChanges();
                return true;
            }
        }
        public bool Del(int id)
        {
            using (Context cont = new Context())
            {
                Car car = cont.Cars.Find(id);
                car.DelST = true;
                cont.SaveChanges();
                return true;
            }
        }
        public bool Update(int id,int body, int category, int engine, int pow, int transmission, int num, int drive, string color, string carnum, string comment, int damage, double classcar, int free)
        {
            using (Context cont = new Context())
            {
                Car car = cont.Cars.Find(id);
                //car.BrandAndName = mm;
                car.BodyType = body;
                car.EngineType = engine;
                car.MaxPow = pow;
                car.Transsmision = transmission;
                car.NumberOfSeats = num;
                car.WheelDrive = drive;
                car.Color = color;
                car.CarNumber = carnum;
                car.Comment = comment;
                car.Damage = damage;
                car.CarClass = classcar;
                car.Status = free;
                car.Category = category;
                cont.SaveChanges();
                return true;
            }
        }
        public List<Contract> ListOfContracts()
        {
            using (Context cont = new Context())
            {

                Contract contract = new Contract();
                List<Contract> contlist = new List<Contract>();
                foreach (var s in cont.Contracts.Where(x => x.CarID == this.ID).ToList())
                {

                    contlist.Add(s);

                }

                return contlist;


            }

        }
    }
}
