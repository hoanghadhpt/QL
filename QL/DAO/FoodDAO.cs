using QL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return instance; }
            private set { instance = value; }
        }
        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> foods = new List<Food>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.food f WHERE f.idCategory = " + id);
            foreach(DataRow row in data.Rows)
            {
                Food food = new Food(row);
                foods.Add(food);
            }

            return foods;
        }

    }
}
