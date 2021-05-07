using QL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
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
            List<Client> clients = new List<Client>();


            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from client");
            foreach(DataRow item in data.Rows)
            {
                Client client = new Client(item);
                clients.Add(client);
            }

            return clients;
        }

        public Client GetClientByPhone(string phone)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("usp_GetClientByPhone @phone ", new object[] { phone});
            if (data.Rows.Count > 0)
            {
                Client client = new Client(data.Rows[0]);
                return client;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Không tìm thấy khách hàng trong CSDL");
            }
            return new Client();
        }

        public void AddNewClient(string phone, string address, string comment)
        {
            try
            {
                DataProvider.Instance.ExecuteQuery("usp_AddNewClient @phone , @address , @comment", new object[] { phone, address, comment });
                System.Windows.Forms.MessageBox.Show("Thêm mới thành công");
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("UNIQUE"))
                {
                    System.Windows.Forms.MessageBox.Show("Khách hàng đã có trong hệ thống, không thể thêm mới.");

                }

            }

        }

        public int GetClientIdByPhone(string phone)
        {
            object id = DataProvider.Instance.ExecuteScalar("usp_GetClientIdByPhone @phone", new object[] { phone });
            if (id != null)
            {
                return Convert.ToInt32(id);
            }
            else
            {
                return -1;
            }
        }

    }
}
