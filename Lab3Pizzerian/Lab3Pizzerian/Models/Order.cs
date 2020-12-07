using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian
{
    public class Order
    {
        public Guid ID { get; set; }
        public List<Pizza> Food { get; set; }
        public Dictionary<Drink, int> Drinks { get; set; }
    }
}
