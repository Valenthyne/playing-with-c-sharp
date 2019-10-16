using System;

namespace PizzaApplication
{

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Received pizza orders!\n");

			// Initializes an array of IPizza objects
			IPizza[] pizzas = {
								new CheesePizza(),
								new PepperoniPizza(),
								new PineapplePizza(),
								new VeggiePizza(),
								new GreekPizza(),
								new ChicagoStylePizza()
							};

			// Iterates through the IPizza array, using polymorphism to call the appropriate Prepare methods
			foreach (IPizza p in pizzas)
			{
				p.Prepare();
			}

			Console.WriteLine("\nPizzas are ready!");

			Console.ReadKey();
		}
	}

}