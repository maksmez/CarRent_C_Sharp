using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class PaymentActions
    {
        public PaymentActions()
        {
        }
        public void ShowAll()
        {
            List<Payment> listpay = new List<Payment>();
            using (Context cont = new Context())
            {
               listpay = cont.Payments.ToList();
                //listpay = cont.Payments.Where(x => x.ContID == 14).ToList();

                foreach (Payment p in listpay)
                {
                    Console.WriteLine(p.ID + "\t" + p.DateOfPayment + "\t" +  p.AmmountPaid + "\t" + p.ContID + "\t");
                }
            }

        }
    }
}
