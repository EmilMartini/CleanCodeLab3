using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Enumerations
{
    public enum EnumIngredient
    {

        [Description("Skinka")]
        Ham = 1,
        [Description("Ananas")]
        Pineapple = 2,
        [Description("Champinjoner")]
        Mushrooms = 3,
        [Description("Lök")]
        Onion = 4,
        [Description("Kebabsås")]
        KebabSauce = 5,
        [Description("Räkor")]
        Shrimps = 6,
        [Description("Musslor")]
        Clams = 7,
        [Description("Kronärtskocka")]
        Artichoke = 8,
        [Description("Kebab")]
        Kebab = 9,
        [Description("Koriander")]
        Coriander = 10,
        [Description("Feferoni")]
        Pepperoni = 11,
        [Description("Isbergssallad")]
        Salad = 12,
        [Description("Tomat")]
        Tomato = 13,
        [Description("Ost")]
        Cheese = 14,
        [Description("Tomatsås")]
        Tomatosauce = 15
    }
}
