using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class RateActions
    {
        public RateActions()
        {
        }
        public List<Rate> ShowAll()
        {
            List<Rate> listrate = new List<Rate>();
            using (Context cont = new Context())
            {
                listrate = cont.Rates.Where(x => x.DelST == false).ToList();

                
            }
            return listrate;
        }
        public List<Rate> ShowAll(int id)
        {
            List<Rate> listrate = new List<Rate>();
            using (Context cont = new Context())
            {
                int cl = cont.Clients.Find(id).DateStartExp.Year;
                int stage = DateTime.Today.Year - cl;
                //int clstage = Convert.ToInt32(stage);
                listrate = cont.Rates.Where(x => x.ReqExp <= stage).ToList();
                listrate = listrate.Where(x => x.DelST == false).ToList();
                

            }
            return listrate;
        }
        public Rate Find(int id)
        {

            Rate findrate = new Rate();
            using (Context context = new Context())
            {
                findrate = context.Rates.Where(x => x.ID == id).FirstOrDefault<Rate>();


            }
            return findrate;

        }
    }
}
