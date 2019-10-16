using System;

namespace PizzaApplication
{

	// Establishes a common interface to be used by all pizza flavors
	public interface IPizza
	{
		void Prepare();
	}

	public class CheesePizza : IPizza
	{
		public void Prepare()
		{
			Console.WriteLine("Preparing ingredients for a cheese pizza...");
		}
	}

	public class PepperoniPizza : IPizza
	{
		public void Prepare()
		{
			Console.WriteLine("Preparing ingredients for a pepperoni pizza...");
		}
	}

	public class PineapplePizza : IPizza
	{
		public void Prepare()
		{
			Console.WriteLine("Preparing ingredients for a pineapple pizza...");
		}
	}

	public class VeggiePizza : IPizza
	{
		public void Prepare()
		{
			Console.WriteLine("Preparing ingredients for a veggie pizza...");
		}
	}

	public class GreekPizza : IPizza
	{
		public void Prepare()
		{
			Console.WriteLine("Preparing ingredients for a Greek pizza...");
		}
	}

	public class ChicagoStylePizza : IPizza
	{
		public void Prepare()
		{
			Console.WriteLine("Preparing ingredients for a Chicago style pizza...");
		}
	}

}