using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace CarRent
{
    public class Rate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ReqExp { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public int Hour { get; set; }
        public bool DelST { get; set; }


        public List<string> HourStatus = new List<string>() { "Часовой", "Суточный" };

        public Rate()
        {


        }
        public bool Add(string name, int exp, double cost, string description, int hour,bool status)
        {
            using (Context context = new Context())
            {
                Name = name;
                ReqExp = exp;
                Cost = cost;
                Description = description;
                Hour = hour;
                DelST = status;

                context.Rates.Add(this);
                context.SaveChanges();
                return true;
            }
        }
        public bool Del(int id)
        {
            using (Context cont = new Context())
            {
                Rate rate = cont.Rates.Find(id);
                rate.DelST = true;
                cont.SaveChanges();
                return true;
            }
        }
        public bool Update(int id, string name, int exp, double cost, string description, int hour, bool status)
        {
            using (Context cont = new Context())
            {
                Rate rate = cont.Rates.Find(id);
                rate.Name = name;
                rate.ReqExp = exp;
                rate.Cost = cost;
                rate.Description = description;
                rate.Hour = hour;
                rate.DelST = status;
                cont.SaveChanges();
                return true;
            }
        }

        public List<Contract> GetContracts()
        {
            using (Context cont = new Context())
            {
                List<Contract> contlist = new List<Contract>();
                foreach (var s in cont.Contracts.Where(x => x.RateID == this.ID).ToList())
                {
                    contlist.Add(s);

                }
                return contlist;


            }

        }





    }
}
