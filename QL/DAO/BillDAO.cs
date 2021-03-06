using QL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set { instance = value; }
        }
        private BillDAO() { }

        /// <summary>
        /// Thành công : Return Bill ID
        /// Không thành công : Return -1
        /// </summary>
        /// <param name="id">Table id (int)</param>
        /// <returns></returns>
        public int GetUncheckBillIdByTableId(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from bill where idTable = "+id+" and status = 0");
            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }

        public void InsertBill(int idTable, int idClient)
        {
            DataProvider.Instance.ExecuteNonQuery("usp_InsertBill @idTable , @idClient ", new object[] { idTable, idClient});

        }

        public int GetMaxBillID()
        {
            try
            { 
            return (int)DataProvider.Instance.ExecuteScalar("Select Max(id) from bill");
            }
            catch
            {
                return 1;
            }
        }

        public void CheckOut(int id)
        {
            string query = "Update bill set status = 1 where id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);

        }

    }
}
