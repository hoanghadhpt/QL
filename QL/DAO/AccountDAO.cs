using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }


        /// <summary>
        /// Thong tin dang nhap, tra ve false neu khong tim thay tai khoan.
        /// </summary>
        /// <param name="username">string : Ten dang nhap</param>
        /// <param name="password">string : Mat Khau</param>
        /// <returns></returns>
        public bool Login(string username, string password)
        {
            string query = "usp_login @username , @password ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password});
            return result.Rows.Count > 0;
        }


    }
}
