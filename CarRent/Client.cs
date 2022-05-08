using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRent
{
    public class Client : Person
    {

        public string NumVU { get; set; }
        public string ClientComment { get; set; }
        
        


    public Client()
        {

        }
        public bool Add(string name, string surname, string patronymic, DateTime date, string phone, string passport, string inn, string vu, DateTime start, string comm, bool status, out int id)
        {
            using (Context context = new Context())
            {
                id = 0;
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Date = date;
                Phone = phone;
                Passport = passport;
                INN = inn;
                DateStartExp = start;
                NumVU = vu;
                ClientComment = comm;
                DelST = status;
                context.Clients.Add(this);
                context.SaveChanges();
                id = this.ID;
                return true;
            }
        }
        public bool Del(int id)
        {
            using (Context cont = new Context())
            {
                Client client = cont.Clients.Find(id);
                client.DelST = true;
                cont.SaveChanges();
                return true;
            }
        }
        public bool Update(int id, string name, string surname, string patronymic, DateTime date, string phone, string passport, string inn, string vu, DateTime start, string comm)
        {
            using (Context cont = new Context())
            {
                Client client = cont.Clients.Find(id);
                client.Name = name;
                client.Surname = surname;
                client.Patronymic = patronymic;
                client.Date = date;
                client.Phone = phone;
                client.Passport = passport;
                client.INN = inn;
                client.DateStartExp = start;
                client.NumVU = vu;
                client.ClientComment = comm;
                cont.SaveChanges();
                return true;
            }
        }
        public void Clcategory(string category, int clid)//добавление
        {
            using (Context context = new Context())
            {
                int catid=0;
                foreach (var p in context.Categories.Where(x => x.NameCat == category).ToList())
                {
                    catid = p.ID;
                    context.clCategories.Add(new ClCategory { IDClient = clid, IDCategory = catid });
                    context.SaveChanges();
                }

            }

        }
        public List<Category> Clcategory()//считывание
        {
            List<Category> category = new List<Category>();

            using (Context context = new Context())
            {
                foreach( var  s in context.clCategories.Where(x => x.IDClient == this.ID).ToList())
                {
                    category.AddRange(context.Categories.Where(x => x.ID == s.IDCategory).ToList());

                }
                return category;

            }

        }
        public List<Contract> RentalHistory()
        {
            using (Context cont = new Context())
            {
                Car car = new Car();
                Contract contract = new Contract();
                List<Contract> contlist = new List<Contract>();
                foreach (var s in cont.Contracts.Where(x => x.ClientID == this.ID).ToList())
                {
                    car = cont.Cars.Find(s.CarID);
                    
                    contlist.Add(s);

                }
               
                return contlist;


            }

        }

    }
}
