using System;
using System.Collections.Generic;

namespace OrderUp
{
	public class Customer
	{
		private readonly String name;
		OrderDetail order;

		public Customer(String n)
		{
			name = n;
			order = new OrderDetail();
		}

		public void BuyItem(Item it)
		{
			order.AddItem(it);
		}

		public void ShowList()
		{
			order.GetItems();
			order.PrintTotalCost();
		}

		public String GetName()
		{
			return name;
		}

	}

	public class Item
	{

		private readonly String name;
		private readonly double costPerItem;

		public Item(String n, double c)
		{
			name = n;
			costPerItem = c;
		}

		public virtual String GetName()
		{
			return name;
		}

		public virtual void ListName()
		{
			Console.WriteLine("--| " + name);
		}

		public virtual double GetCost()
		{
			return costPerItem;
		}

	}

	public class MultiItem : Item
	{

		private readonly int count = 0;

		public MultiItem(String n, int x, double c) : base(n, c)
		{
			count = x;
		}

		public override String GetName()
		{
			return count + "x " + base.GetName();
		}

		public override void ListName()
		{
			Console.WriteLine("--| " + GetName());
		}

		public override double GetCost()
		{
			return count * base.GetCost();
		}

	}

	public class Subscription : Item
	{

		private readonly int months;

		public Subscription(String n, int x, double c) : base(n, c)
		{
			months = x;
		}

		public override String GetName()
		{
			return months + " months of " + base.GetName();
		}

		public override void ListName()
		{
			Console.WriteLine("--| " + GetName());
		}

		public override double GetCost()
		{
			return months * base.GetCost();
		}

	}

	public class OrderDetail
	{

		List<Item> order = new List<Item>();

		public void PrintTotalCost()
		{
			double cost = 0;
			foreach (Item it in order)
			{
				cost += it.GetCost();
			}
			Console.WriteLine("Total Cost: ${0}", cost);
		}

		public void AddItem(Item it)
		{
			order.Add(it);
		}

		public void GetItems()
		{

			Console.WriteLine("Total Items\n--------------");
			foreach (Item it in order)
			{
				it.ListName();
			}
			Console.WriteLine();
		}

	}

	public class Program
	{
		public static void Main()
		{

			Customer c1 = new Customer("Carl");
			Customer c2 = new Customer("Sally");
			Customer c3 = new Customer("Jill");

			List<Customer> customers = new List<Customer> { c1, c2, c3 };

			c1.BuyItem(new MultiItem("Apple", 3, 0.59));
			c1.BuyItem(new Item("Personal Computer", 350.00));

			c2.BuyItem(new Item("Copper frying pan", 19.99));
			c2.BuyItem(new MultiItem("Lipstick", 5, 3.74));

			c3.BuyItem(new Item("Bicycle", 250.00));
			c3.BuyItem(new Item("Helmet", 29.99));
			c3.BuyItem(new Subscription("\"Cyclist Monthly\"", 3, 15.99));

			foreach (Customer cu in customers)
			{
				Console.WriteLine("Order for Customer {0}", cu.GetName());
				cu.ShowList();
				Console.WriteLine();
			}

			Console.ReadKey();

		}
	}
}