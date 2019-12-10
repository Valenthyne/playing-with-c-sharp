using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
  static void Main(string[] args)
  {
    int frames = 1;

    // Generate single reference string length 500,000, each value ranging from 0 to 20
    int[] arr = new int[500000];
    InitArray(arr, 500000, 0, 21);

    // Generating test cases for frame counts between 1 and 10
    for (; frames <= 10; frames++)
    {
      // Establish stopwatch variable to record time spent in first algorithm
      var w1 = System.Diagnostics.Stopwatch.StartNew();
      int pf1 = LeastRecentlyUsedCounter(arr, frames);
      w1.Stop();

      // Store the elapsed milliseconds for first algorithm
      long runtimeLRUCounter = w1.ElapsedMilliseconds;

      // Establish stopwatch variable to record time spent in second algorithm
      var w2 = System.Diagnostics.Stopwatch.StartNew();
      int pf2 = LeastRecentlyUsedStack(arr, frames);
      w2.Stop();

      // Store the elapsed milliseconds for second algorithm
      long runtimeLRUStack = w2.ElapsedMilliseconds;

      Console.WriteLine("Running time for " + frames + " frames: ");
      Console.WriteLine("Using Counter implementation: " + runtimeLRUCounter + " milliseconds");
      Console.WriteLine("Using Counter implementation: " + pf1 + " page faults\n");
      Console.WriteLine("Using Stack implementation: " + runtimeLRUStack + " milliseconds");
      Console.WriteLine("Using Stack implementation: " + pf2 + " page faults\n");
      Console.WriteLine("");
    }

  }

  static void InitArray(int[] array, int values, int l, int r)
  {
    Random random = new Random();

    for (int i = 0; i < values; i++)
    {
      array[i] = random.Next(l, r);
    }
  }

  static int LeastRecentlyUsedCounter(int[] refStr, int frames)
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

  static int LeastRecentlyUsedStack(int[] refStr, int frames)
  {
    int totalPageFault = 0;

    LinkedList<int> vals = new LinkedList<int>();

    int[] mem = new int[frames];

    for (int i = 0; i < mem.Length; i++)
    {
      mem[i] = -1;
    }

    for (int i = 0; i < refStr.Length; i++)
    {

      Boolean alreadyPresent = false;

      int insert = refStr[i];

      // If the referenced insert value is not already in the list
      if (!vals.Contains(insert))
      {
        // Add it to the top of the list
        vals.AddFirst(insert);
      } else
      // Otherwise, the value needs to be plucked out and placed on top
      {
        vals.Remove(insert);
        vals.AddFirst(insert);
      }


      for (int a = 0; a < mem.Length; a++)
      {
        if (mem[a] == insert)
        {
          alreadyPresent = true;
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
        }

        else { 

          int toBeRemoved = vals.Last.Value;
          vals.RemoveLast();

          for (int m = 0; m < mem.Length; m++)
          {
            if (mem[m] == toBeRemoved)
            {
              mem[m] = insert;
              break;
            }
          }
        }
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
