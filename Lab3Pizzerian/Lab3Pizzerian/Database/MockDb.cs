using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian
{
    public class MockDb
    {
        private static MockDb instance = null;

        public Dictionary<Ingredient, int> Ingredients { get; set; }
        public Dictionary<Drink, int> Drinks { get; set; }

        public static MockDb GetDbInstance()
        {
            if(instance == null)
            {
                instance = new MockDb();
            }
            return instance;
        }
    }
}
