using QL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get { if (instance == null) instance = new CategoryDAO(); return instance; }
            private set { instance = value; }
        }
        private CategoryDAO() { }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from FoodCategory");
            foreach(DataRow row in data.Rows)
            {
                Category category = new Category(row);
                categories.Add(category);
            }

            return categories;
        }

    }
}
