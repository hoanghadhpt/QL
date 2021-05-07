using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DTO
{
    public class Menu
    {
        private string foodName;
        private int count;
        private float price;
        private float totalPrice;
        //private int discount;

        public Menu(string foodname, int count, float price, float totalPrice, int discount)
        {
            this.FoodName = foodname;
            this.Count = count;
            this.Price = price;
            this.TotalPrice = totalPrice;
            //this.Discount = discount;
        }

        public Menu(DataRow row)
        {
            this.FoodName = row["name"].ToString();
            this.Count = (int)row["count"];
            this.Price = Convert.ToSingle(row["price"]);
            //this.Discount = Convert.ToInt32(row["discount"]);
            this.TotalPrice = Convert.ToSingle(row["totalPrice"]);
        }


        public string FoodName { get => foodName; set => foodName = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        //public int Discount { get => discount; set => discount = value; }
    }
}
