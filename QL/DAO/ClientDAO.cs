using QL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class ClientDAO
    {

        private static ClientDAO instance;

        public static ClientDAO Instance
        {
            get { if (instance == null) instance = new ClientDAO(); return instance; }
            private set { instance = value; }
        }
        private ClientDAO() { }

        public List<Client> GetClients()
        {
            Client client = new Client();
            DataProvider data = new DataProvider();
            data.ExecuteNonQuery
        }
    }
}
