using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace CarRent
{
    public class Contract
    {

        public int ID { get; set; }
        public int ClientID { get; set; }
        public int WorkerID { get; set; }
        public int RateID { get; set; }
        public int CarID { get; set; }
        public DateTime DateStartContract { get; set; }
        public DateTime DateEndContract { get; set; }

        public double Balance { get; set; }
        public bool SActive { get; set; }
        public bool SExtend { get; set; }
        public bool DelST { get; set; }



        public Contract()
        {

        }
        public bool Add(int idcl, int idwor, int idcar, int idrate,  DateTime start, DateTime end, bool sactive)
        {
            using (Context context = new Context())
            {
                context.Cars.Find(idcar).Status = 1;
                ClientID = idcl;
                WorkerID = idwor;
                RateID = idrate;
                CarID = idcar;
                DateStartContract = start;
                DateEndContract = end;
                SActive = sactive;
                //SExtend = sextend;
                Balance = FullCost(idcar, idrate, start, end);
                DelST = false;
                context.Contracts.Add(this);
                context.SaveChanges();
                return true;
            }
        }
        public bool Del(int id)
        {
            using (Context cont = new Context())
            {
                Contract contract = cont.Contracts.Find(id);
                Car car = cont.Cars.Find(contract.CarID);
                car.Status = 0;
                contract.DelST = true;
                cont.SaveChanges();
                return true;
            }
        }
        public bool Update(int id, int idcar, int idrate,  DateTime end,double balance, bool sactive, bool sextend)
        {
            using (Context cont = new Context())
            {
                Contract contract = cont.Contracts.Find(id);
                //contract.ClientID = idcl;
               // contract.WorkerID = idwor;
                contract.RateID = idrate;
                contract.CarID = idcar;
                //contract.DateStartContract = start;
                contract.DateEndContract = end;
                contract.SActive = sactive;
                contract.SExtend = sextend;
                contract.Balance = balance;
                //contract.DelST = delst;
                if(contract.SActive==false)
                {
                    cont.Cars.Find(idcar).Status = 0;
                }
                cont.SaveChanges();
                return true;
            }
        }
        public double FullCost(int idcar, int idrate, DateTime start, DateTime end)
        {
            using (Context cont = new Context())
            {
                if (cont.Rates.Find(idrate).Hour==0)//по часам
                {
                    double costlass = cont.Cars.Find(idcar).CarClass;
                    double cash = cont.Rates.Find(idrate).Cost;
                    TimeSpan time = end - start;
                    double hour = time.TotalHours;
                    return Math.Round(hour * costlass * cash);
                }
                if (cont.Rates.Find(idrate).Hour==1)//по дням
                {
                    double costlass = cont.Cars.Find(idcar).CarClass;
                    double cash = cont.Rates.Find(idrate).Cost;
                    TimeSpan time = end - start;
                    double hour = time.TotalDays;
                    return Math.Round(hour * costlass * cash);
                }
                return 0;
            }

        }
        public DateTime Getpay()
        {
            using (Context context = new Context())
            {
                var list = context.Payments.Where(x => x.ContID == this.ID).ToList();
                if (list.Count != 0)
                {
                    Payment pay = list.Last();
                    return pay.DateOfPayment;

                }
                return DateTime.MinValue;


            }
        }
        public List<Payment> HistoryPayment()
        {
            using (Context cont = new Context())
            {


                List<Payment> paylist = new List<Payment>();
                foreach (var s in cont.Payments.Where(x => x.ContID == this.ID).ToList())
                {

                    paylist.Add(s);

                }

                return paylist;


            }

        }
    }
}
