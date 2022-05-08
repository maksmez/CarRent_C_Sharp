using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class CarActions
    {
        public CarActions()
        {
        }
        public List<Car> ShowAll()
        {
            List<Car> listcar = new List<Car>();
            using (Context cont = new Context())
            {
                listcar = cont.Cars.Where(x => x.DelST == false).ToList();
                
            }
            return listcar;
        }
        public List<Car> ShowAll(int id)
        {
            List<Car> listcar = new List<Car>();
            using (Context cont = new Context())
            {
                foreach (var c in cont.clCategories.Where(x => x.IDClient == id).ToList())
                {
                    listcar.AddRange(cont.Cars.Where(x => x.Category == c.IDCategory).ToList());
                }
                listcar = listcar.Where(x => x.DelST == false).ToList();
                return listcar;
            }
        }
        public Car Find(int id)
        {

            Car findcar = new Car();
            using (Context context = new Context())
            {
                findcar = context.Cars.Where(x => x.ID == id).FirstOrDefault<Car>();


            }
            return findcar;
            
        }
    }

}
