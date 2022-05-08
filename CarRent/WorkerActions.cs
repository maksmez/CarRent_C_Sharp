using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class WorkerActions
    {
        public WorkerActions()
        {
        }
        public List<Worker> ShowAll()
        {
            List<Worker> workerlist = new List<Worker>();
            using (Context cont = new Context())
            {
                workerlist = cont.Workers.Where(x => x.DelST == false).ToList();

            }
            return workerlist;
        }
        public Worker Find(int id)
        {

            Worker findworker = new Worker();
            using (Context context = new Context())
            {
                findworker = context.Workers.Where(x => x.ID == id).FirstOrDefault<Worker>();


            }
            return findworker;

        }
    }
}
