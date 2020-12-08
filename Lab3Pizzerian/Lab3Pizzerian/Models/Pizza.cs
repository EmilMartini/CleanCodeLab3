using Lab3Pizzerian.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Pizzerian.Models
{
	public class Pizza
	{
		public string Name { get; set; }
		public List<EnumIngredient> Standard { get; set; }
		public int StandardPrice { get; set; }
		public List<EnumIngredient> Extras { get; set; }
		// kan vi använda PrototypeDesignpattern när vi skapar pizzor? 
		// Där vår PizzaMeny är mallen för hur pizzan skall se ut i grunden
	}


	//Försök till ProtoType, tror det är såhär man gör.
	public class Pizza2
	{
		public string Name { get; set; }
		public List<EnumIngredient> Standard { get; set; }
		public int StandardPrice { get; set; }
		public List<EnumIngredient> Extras { get; set; }
	}
	public abstract class StandardPizzaPrototype : Pizza2
	{
		public abstract StandardPizzaPrototype Clone();
	}

	public class Margarita : StandardPizzaPrototype
	{
		public Margarita()
		{
			Name = "Margarita";
			Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås};
			StandardPrice = 85;
			Extras = new List<EnumIngredient>();
		}
		public override StandardPizzaPrototype Clone()
		{
			return (StandardPizzaPrototype)this.MemberwiseClone(); // Clones the concrete class.
		}
	}

	public class Hawaii : StandardPizzaPrototype
	{
		public Hawaii()
		{
			Name = "Hawaii";
			Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås,
						  EnumIngredient.Skinka,
						  EnumIngredient.Ananas };
			StandardPrice = 95;
			Extras = new List<EnumIngredient>();
		}
		public override StandardPizzaPrototype Clone()
		{
			return (StandardPizzaPrototype)this.MemberwiseClone(); // Clones the concrete class.
		}
	};
	public class Kebabpizza : StandardPizzaPrototype
	{
		public Kebabpizza()
		{
			Name = "Kebabpizza";
			Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås,
						  EnumIngredient.Kebab,
						  EnumIngredient.Champinjoner,
						  EnumIngredient.Lök,
						  EnumIngredient.Feferoni,
						  EnumIngredient.Isbergssallad,
						  EnumIngredient.Tomat,
						  EnumIngredient.Kebabsås };
			StandardPrice = 105;
			Extras = new List<EnumIngredient>();
		}
		public override StandardPizzaPrototype Clone()
		{
			return (StandardPizzaPrototype)this.MemberwiseClone(); // Clones the concrete class.
		}
	};
	public class QuatroStagioni : StandardPizzaPrototype
	{
		public QuatroStagioni()
		{
			Name = "Quatro Stagioni";
			Standard = new List<EnumIngredient>{
						  EnumIngredient.Ost,
						  EnumIngredient.Tomatsås,
						  EnumIngredient.Skinka,
						  EnumIngredient.Räkor,
						  EnumIngredient.Musslor,
						  EnumIngredient.Champinjoner,
						  EnumIngredient.Kronärtskocka };
			StandardPrice = 115;
			Extras = new List<EnumIngredient>();
		}
		public override StandardPizzaPrototype Clone()
		{
			return (StandardPizzaPrototype)this.MemberwiseClone(); // Clones the concrete class.
		}
	};
}
