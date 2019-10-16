using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayingAround
{
	class Program
	{
		static void Main(string[] args)
		{
			Foo(1, 2, 3, 4, 5);
			Bar("hello", "its", "me");

			// Keep the damn console open until I'm ready
			Console.ReadKey();
		}

		static void Foo(params int[] args)
		{
			foreach (int i in args)
			{
				Console.WriteLine(i + " ");
			}
		}

		static void Bar<T>(params T[] t)
		{
			foreach (T u in t)
			{
				Console.WriteLine(u);
			}
		}

	}
}
