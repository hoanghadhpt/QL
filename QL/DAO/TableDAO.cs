using QL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return instance; }
            private set { instance = value; }
        }
        private TableDAO() { }

        // Set table with and heigh
        public static int tableWith = 150;
        public static int tableHeigh = 150;

        public List<Table> GetListTables()
        {
            List<Table> tables = new List<Table>();

            DataTable dt = DataProvider.Instance.ExecuteQuery("usp_getlisttable");
            foreach(DataRow row in dt.Rows)
            {
                Table table = new Table(row);
                tables.Add(table);
            }
            return tables;
        }

    }
}
