using QL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            private set { instance = value; }
        }
        private MenuDAO() { }


        public List<Menu> GetListMenuByTable(int idTable, int discount)
        {
            List<Menu> menus = new List<Menu>();
            string query = "SELECT f.name, bi.count, f.price, f.price * bi.count AS [totalPrice] FROM dbo.bill as b, dbo.food as f, dbo.billinfo AS bi WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = " + idTable;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                menus.Add(menu);
            }
            return menus;
        }

        public Client GetClientInMenuByTableID(int idTable)
        {
            
            DataTable queryIDClient = DataProvider.Instance.ExecuteQuery("SELECT b.idClient FROM dbo.bill b WHERE idTable = " + idTable);
            if (queryIDClient.Rows.Count > 0 && queryIDClient.Rows[0][0].ToString() != "")
            {
                int id = Convert.ToInt32(queryIDClient.Rows[0][0].ToString());
                DataTable queryClient = DataProvider.Instance.ExecuteQuery("SELECT * FROM Client WHERE id = " + id);
                Client client = new Client(queryClient.Rows[0]);
                return client;
            }
            else
            { return new Client(); }
            
        }

        
    }
}
