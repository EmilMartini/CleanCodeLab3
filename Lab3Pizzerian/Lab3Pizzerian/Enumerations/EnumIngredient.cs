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
        Skinka = 1,
        [Description("Ananas")]
        Ananas = 2,
        [Description("Champinjoner")]
        Champinjoner = 3,
        [Description("Lök")]
        Lök = 4,
        [Description("Kebabsås")]
        Kebabsås = 5,
        [Description("Räkor")]
        Räkor = 6,
        [Description("Musslor")]
        Musslor = 7,
        [Description("Kronärtskocka")]
        Kronärtskocka = 8,
        [Description("Kebab")]
        Kebab = 9,
        [Description("Koriander")]
        Koriander = 10,

        //Free
        [Description("Feferoni")]
        Feferoni = 11,
        [Description("Isbergssallad")]
        Isbergssallad = 12,
        [Description("Tomat")]
        Tomat = 13,
        [Description("Ost")]
        Ost = 14,
        [Description("Tomatsås")]
        Tomatsås = 15
    }
}
