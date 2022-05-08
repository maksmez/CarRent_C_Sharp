using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
   public class ContractActions
    {
        public ContractActions()
        {
        }
        
        
        public List<Contract> ShowAll()
        {
            List<Contract> listcontract = new List<Contract>();
            using (Context cont = new Context())
            {
            listcontract = cont.Contracts.Where(x => x.DelST == false).ToList();

            }
            return listcontract;
        }
        public Contract Find(int id)
        {

            Contract findcontract = new Contract();
            using (Context context = new Context())
            {
                findcontract = context.Contracts.Where(x => x.ID == id).FirstOrDefault<Contract>();


            }
            return findcontract;

        }

    }
}
