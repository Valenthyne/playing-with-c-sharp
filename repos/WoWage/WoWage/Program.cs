using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoWage
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to WoWage!");

            String[] chars = System.IO.File.ReadAllLines(@"C:\Users\Drachen\Desktop\test.txt");

            List<String> name = new List<String>();
            List<String> race = new List<String>();
            List<String> playerClass = new List<String>();


            for (int i = 0; i < chars.Length; i++)
            {
               String[] stuff = chars[i].Split(',');
                name.Add(stuff[0]);
                race.Add(stuff[1]);
                playerClass.Add(stuff[2]);
            }
			
            foreach (String s in chars) {
                Console.WriteLine(s);
            }

            foreach (String s in name) {
                Console.WriteLine(s);
            }

            foreach (String s in race)
            {
                Console.WriteLine(s);
            }

            foreach (String s in playerClass)
            {
                Console.WriteLine(s);
            }

            Console.ReadKey();





        }
    }
}
