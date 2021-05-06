using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.DTO
{
    public class Client
    {
        private string phone;
        private string address;
        private string comment;

        public Client()
        {
            Phone = "";
            Address = "";
            Comment = "";
        }

        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Comment { get => comment; set => comment = value; }

        public Client(DataRow row)
        {
            this.Phone = row["phone"].ToString();
            this.Address = row["address"].ToString();
            this.Comment = row["comment"].ToString();
        }
    }
}
