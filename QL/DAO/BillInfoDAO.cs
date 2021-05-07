using QL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { instance = value; }
        }

        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int idBill)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.billinfo b Where idBill = " + idBill);
            foreach(DataRow row in data.Rows)
            {
                BillInfo info = new BillInfo(row);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count, int clientID, int billID)
        {
            DataProvider.Instance.ExecuteNonQuery("usp_updateClientToBill @clientID , @billID",new object[] { clientID, billID});
            DataProvider.Instance.ExecuteNonQuery("usp_InsertBillInfo @idBill , @idFood , @count ", new object[] { idBill, idFood, count });

        }

    }
}
