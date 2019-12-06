using System;
using System.Collections.Generic;

class Program
{
	static void Main(string[] args)
	{

		// int[] testRefStr = {7, 0, 1, 2, 0, 3, 0, 4, 2, 3, 0, 3, 2, 1, 2, 0, 1, 7, 0, 1};
		// Console.WriteLine("Page Faults w/ Optimal Page: " + OptimalPage(testRefStr, 3));
		//	Result should be: 9 page faults
		// Console.WriteLine("Page Faults w/ LRU: " + LeastRecentlyUsed(testRefStr, 3));
		//	Result should be: 12 page faults

		// Generating five random page-reference strings
		int[] arr1 = new int[20];
		int[] arr2 = new int[20];
		int[] arr3 = new int[20];
		int[] arr4 = new int[20];
		int[] arr5 = new int[20];

		// Initializing frame counter
		int frames = 1;

		// Generating two-dimensional array to store all previous arrays
		int[][] arrays = new int[][] {arr1, arr2, arr3, arr4, arr5};

		// Initializing each array with values ranging from 0 to 20
		for (int i = 0; i < 5; i++)
		{
			InitArray(arrays[i], 20, 0, 21);
		}

		// Generating test cases for frame counts between 1 and 7
		for (; frames <= 7; frames++)
		{
			int pageFaultOP = 0;
			int pageFaultLRU = 0;

			// Run the two page replacement algorithms on each of the arrays
			for (int i = 0; i < 5; i++) 
			{ 
				pageFaultOP += OptimalPage(arrays[i], frames);
				pageFaultLRU += LeastRecentlyUsed(arrays[i], frames);
			}

			Console.WriteLine("Average Page Faults for " + frames + " frames: ");
			Console.WriteLine("Using Optimal Page: " + pageFaultOP / 5);
			Console.WriteLine("Using LRU: " + pageFaultLRU / 5);
			Console.WriteLine("");
		}

		// Reset frame counter
		frames = 1;

		// Generate single reference string length 500,000, each value ranging from 0 to 20
		int[] arr = new int[500000];
		InitArray(arr, 500000, 0, 21);

		// Generating test cases for frame counts between 1 and 10
		for (; frames <= 10; frames++)
		{
			// Establish stopwatch variable to record time spent in first algorithm
			var w1 = System.Diagnostics.Stopwatch.StartNew();
			OptimalPage(arr, frames);
			w1.Stop();

			// Store the elapsed milliseconds for first algorithm
			long runtimeOP = w1.ElapsedMilliseconds;

			// Establish stopwatch variable to record time spent in second algorithm
			var w2 = System.Diagnostics.Stopwatch.StartNew();
			LeastRecentlyUsed(arr, frames);
			w2.Stop();

			// Store the elapsed milliseconds for second algorithm
			long runtimeLRU = w2.ElapsedMilliseconds;

			Console.WriteLine("Running time for " + frames + " frames: ");
			Console.WriteLine("Using Optimal Page: " + runtimeOP + " milliseconds");
			Console.WriteLine("Using LRU: " + runtimeLRU + " milliseconds");
			Console.WriteLine("");
		}

	}

	// Initialize array based on random values
	static void InitArray(int[] array, int values, int l, int r)
	{
		Random random = new Random();

		for (int i = 0; i < values; i++)
		{
			array[i] = random.Next(l, r);
		}
	}

	// Method containing the optimal page replacement algorithm
	static int OptimalPage(int[] refStr, int frames)
	{
		int totalPageFault = 0;

		int[] mem = new int[frames];

		for (int i = 0; i < mem.Length; i++)
		{
			mem[i] = -1;
		}

		// Cycle through all values in the reference string
		for (int i = 0; i < refStr.Length; i++)
		{
			Boolean alreadyPresent = false;

			// Value to be inserted in the memory
			int insert = refStr[i];

			// Determine if value to be inserted is already present
			for (int a = 0; a < mem.Length; a++)
			{
				if (mem[a] == insert)
				{
					alreadyPresent = true;
					break;
				}
			}

			// Value to be inserted not already in memory block
			if (!alreadyPresent)
			{
				totalPageFault++;

				int max = -1;
				int index = 0;

				// Cycle through each value in the memory block
				for (int j = 0; j < mem.Length; j++)
				{
					int cur = mem[j];
					int steps = 0;

					// Cycle through each value from here on in the reference string
					for (int k = i; k < refStr.Length; k++)
					{
						if (cur == refStr[k])
							break;

						steps++;
					}

					if (steps > max)
					{
						max = steps;
						index = j;
					}
				}

				mem[index] = insert;
			}

		}

		return totalPageFault;
	}

	// Method containing the least recently used page replacement algorithm
	static int LeastRecentlyUsed(int[] refStr, int frames)
	{
		int totalPageFault = 0;

		int[] mem = new int[frames];
		int[,] lookup = new int[21, 2];

		for (int i = 0; i < mem.Length; i++)
		{
			mem[i] = -1;
		}

		List<int> values = new List<int>();

		for (int i = 0; i < refStr.Length; i++)
		{
			Boolean alreadyPresent = false;

			// Value to be inserted in the memory
			int insert = refStr[i];

			// Determine if value to be inserted is already present
			for (int a = 0; a < mem.Length; a++)
			{
				if (mem[a] == insert)
				{
					alreadyPresent = true;
					lookup[insert, 1] = 0;
					break;
				}
			}

			if (!alreadyPresent)
			{
				totalPageFault++;

				int index = IsEmptySpace(mem);

				// Case #1: Empty space present in memory block
				if (index != -1)
				{
					mem[index] = insert;
					values.Add(insert);
					lookup[insert, 1] = 0;
				} 

				// Case #2: Value in memory block must be replaced
				else
				{
					int max = -1;
					int min = refStr.Length + 2;
					int indy = 0;

					// Cycle through values in memory block
					for (int j = 0; j < mem.Length; j++)
					{
						// Determine how many times value has been used
						int used = lookup[mem[j], 0];

						// Determine how many steps it's been since value last used
						int steps = lookup[mem[j], 1];

						if (steps > max)
						{
							min = used;
							max = steps;
							indy = j;
						}

					}

					values.Remove(mem[indy]);
					mem[indy] = insert;
					values.Add(insert);
					lookup[insert, 0]++;
					lookup[insert, 1] = 0;
				}

			}

			foreach (int key in values)
			{
				lookup[key, 1]++;
			}
		}

		return totalPageFault;
	}

	// Method used to see if there exists empty space in the memory block
	static int IsEmptySpace(int[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == -1)
			{
				return i;
			}
		}

		return -1;
	}
}
