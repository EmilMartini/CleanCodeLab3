using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
    public class OrderMenuModel
    {
        public string ID { get; set; }
        public List<string> Food { get; set; }
        public List<string> Drinks { get; set; }
        public string OrderStatus { get; set; }
    }
}
