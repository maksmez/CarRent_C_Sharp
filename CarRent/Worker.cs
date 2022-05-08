using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRent
{
    public class Worker : Person
    {

        public int Position { get; set; }
        public List<string> Positionn = new List<string>() { "Администратор", "Механик", "Менеджер" };

        public Worker()
        {

        }
        public bool Add(string name, string surname, string patronymic, DateTime date, string phone, string passport, string inn, int position, DateTime start, bool status)
        {
            using (Context context = new Context())
            {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Date = date;
                Phone = phone;
                Passport = passport;
                INN = inn;
                Position = position;
                DateStartExp = start;
                DelST = status;
                context.Workers.Add(this);
                context.SaveChanges();
                return true;
            }
        }
        public bool Del(int id)
        {
            using (Context cont = new Context())
            {
                Worker worker = cont.Workers.Find(id);
                worker.DelST = true;
                cont.SaveChanges();
                return true;
            }
        }
        public bool Update(int id,string name, string surname, string patronymic, DateTime date, string phone, string passport, string inn, int position, DateTime start)
        {
            using (Context cont = new Context())
            {
                Worker worker = cont.Workers.Find(id);
                worker.Name = name;
                worker.Surname = surname;
                worker.Patronymic = patronymic;
                worker.Date = date;
                worker.Phone = phone;
                worker.Passport = passport;
                worker.INN = inn;
                worker.DateStartExp = start;
                worker.Position = position;
                cont.SaveChanges();
                return true;
            }
        }
        public List<Contract> CustomerList()
        {
            using (Context cont = new Context())
            {
                
                Contract contract = new Contract();
                List<Contract> contlist = new List<Contract>();
                foreach (var s in cont.Contracts.Where(x => x.WorkerID == this.ID).ToList())
                {
                   
                    contlist.Add(s);

                }

                return contlist;


            }

        }

    }
}
