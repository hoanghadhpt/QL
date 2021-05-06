using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DTO
{
    public class BillInfo
    {
        private int iD;
        private int idBill;
        private int idFood;
        private int count;
        private int? discount;

        public BillInfo(int id, int idFood, int idBill, int count, int discount)
        {
            this.ID = id;
            this.IdFood = idFood;
            this.IdBill = idBill;
            this.Count = count;
            this.Discount = discount;

        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.IdFood = (int)row["idFood"];
            this.IdBill = (int)row["idBill"];
            this.Count = (int)row["count"];
            var discountTemp = row["discount"];
            if(discountTemp.ToString() != "0")
            {
                this.Discount = (int)discountTemp;
            }
            

        }

        public int ID { get => iD; set => iD = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }
        public int? Discount { get => discount; set => discount = value; }
    }
}
