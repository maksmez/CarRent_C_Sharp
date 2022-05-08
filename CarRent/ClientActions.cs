using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRent
{
    public class ClientActions
    {
        public ClientActions()
        {
        }
        
            public List<Client> ShowAll()
            {
                List<Client> listclient = new List<Client>();
                using (Context cont = new Context())
                {
                    listclient = cont.Clients.Where(x => x.DelST == false).ToList();

                }
                return listclient;
            }
        public Client Find(int id)
        {

            Client findclient = new Client();
            using (Context context = new Context())
            {
                findclient = context.Clients.Where(x => x.ID == id).FirstOrDefault<Client>();


            }
            return findclient;

        }

    }
}
