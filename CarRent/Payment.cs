using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRent
{
    public class Payment
    {
        public int ID { get; set; }
        public int ContID { get; set; }
        public DateTime DateOfPayment { get; set; }
        public double AmmountPaid { get; set; }


        public Payment()
        {


        }
        public bool Add(int contid, DateTime datepay, double ammount)
        {
            using (Context context = new Context())
            {
                ContID = contid;
                DateOfPayment = datepay;
                AmmountPaid = ammount;
               double a= context.Contracts.Find(contid).Balance-ammount;
                context.Contracts.Find(contid).Balance = a;
                context.Payments.Add(this);
                context.SaveChanges();
                return true;
            }
        }
        

    }
}
