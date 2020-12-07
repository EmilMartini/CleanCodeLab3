using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian
{
    public class MockDb
    {
        private static MockDb instance = null;

        public Dictionary<EnumIngredient, int> Ingredients { get; }
        public Dictionary<EnumDrink, int> Drinks { get; }

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
