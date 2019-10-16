using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterPuller
{
	class Program
	{

		public static List<String> charName = new List<String>();
		public static List<String> charRace = new List<String>();
		public static List<String> charClass = new List<String>();

		static void Main(string[] args)
		{

		
			Console.WriteLine("Hewwo!");
			Console.WriteLine("Enter your file location: ");

			String source = Console.ReadLine();
			String[] content = FileReader(source);

			ArrayReader(content);

			CharacterSplitter(content);

			ListReader(charName);
			ListReader(charRace);
			ListReader(charClass);

			Menu();

			Console.ReadKey();

		}

		static String[] FileReader(String source)
		{
			try
			{ 
				String[] chars = System.IO.File.ReadAllLines(source);
				Console.WriteLine(chars.Length + " characters loaded!");
				return chars;
			} 
			catch (Exception ex)
			{
				Console.WriteLine("File doesn't exist");
				Console.WriteLine(ex);
			}

			return new string[0];
		}

		static void FileWriter(String source, String[] content)
		{
			System.IO.File.WriteAllLines(source, content);
		}

		static void CharacterSplitter (String[] chars)
		{
			foreach (String str in chars)
			{
				String[] splitStr = str.Split(',');
				charName.Add(splitStr[0]);
				charRace.Add(splitStr[1]);
				charClass.Add(splitStr[2]);
			}
		}

		static void ArrayReader(String[] arr)
		{
			foreach (String str in arr)
			{
				Console.WriteLine(str);
			}
		}

		static void ListReader(List<String> list)
		{
			foreach (String str in list)
			{
				Console.WriteLine(str);
			}
		}

		static void countRace()
		{

		}

		static void countClass()
		{
			String[] classes = {"Warrior", "Monk", "Rogue", "Mage", "Warlock", "Demon Hunter", "Death Knight", "Priest", "Paladin", "Shaman", "Hunter", "Druid"};
			
			foreach (String str in classes)
			{
				if (charClass.Contains(str)) {

				}
			}
		}

		static void Menu()
		{
			bool active = true;

			while (active)
			{
				active = false;
			}

		}

	}

}
