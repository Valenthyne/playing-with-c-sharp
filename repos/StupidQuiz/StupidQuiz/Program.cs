using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StupidQuiz
{
	public class Program
	{
		static void swapPassedByValue(int a, int b)
		{
			int temp;
			temp = a;
			a = b;
			b = temp;
		}

		static void swapPassedByReference(ref int a, ref int b)
		{
			int temp;
			temp = a;
			a = b;
			b = temp;
		}

		static void swapPassedByValueResult(out int a, out int b)
		{
			a = 250;
			b = 500;

			Console.WriteLine("Value of a inside call: " + a);
			Console.WriteLine("Value of b inside call: " + b);

			int temp;
			temp = a;
			a = b;
			b = temp;
		}
		public static void Main()
		{
			int a = 7;
			int b = 3;

			Console.WriteLine("Using Pass-by-Value");
			Console.WriteLine("Value of a before call: " + a);
			Console.WriteLine("Value of b before call: " + b);

			swapPassedByValue(a, b);

			Console.WriteLine("Value of a after call: " + a);
			Console.WriteLine("Value of b after call: " + b);

			Console.WriteLine();
			Console.WriteLine("Using Pass-by-Reference");
			Console.WriteLine("Value of a before call: " + a);
			Console.WriteLine("Value of b before call: " + b);

			swapPassedByReference(ref a, ref b);

			Console.WriteLine("Value of a after call: " + a);
			Console.WriteLine("Value of b after call: " + b);

			Console.WriteLine();
			Console.WriteLine("Using Pass-by-Value-Result");
			Console.WriteLine("Value of a before call: " + a);
			Console.WriteLine("Value of b before call: " + b);
			swapPassedByValueResult(out a, out b);
			Console.WriteLine("Value of a after call: " + a);
			Console.WriteLine("Value of b after call: " + b);

			Console.ReadKey();
		}
	}
}