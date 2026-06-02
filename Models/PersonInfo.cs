using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class PersonInfo
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public PersonInfo(string name, string phone, string address)
        {
            Name = name;
            Phone = phone;
            Address = address;
        }
    }
}
