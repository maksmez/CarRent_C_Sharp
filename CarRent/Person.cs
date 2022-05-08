using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace CarRent
{
    public class Person
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Date { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }
        public string INN { get; set; }
        public DateTime DateStartExp { get; set; }
        
        public bool DelST { get; set; }

        public Person()
        {

        }

        public bool Add()
        {
            using (Context context = new Context())
            {

                context.People.Add(this);
                context.SaveChanges();
                return true;
            }
        }
        
    }
}
