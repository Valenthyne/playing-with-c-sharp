// est. 11/11/2018 by Drachen

using System;
using System.Collections.Generic;
using System.IO;

namespace QnDRenamer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to the Quick 'n Dirty Folder Renamer!\n" +
				"Enter a path from which to work: ");

			var rootDir = Console.ReadLine();
			var subDir = Directory.GetDirectories(rootDir);

			Console.WriteLine(rootDir);
			List<String> artists =  ExtractArtist(subDir);

			String[] files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
			ReadArr(files);

			RenameFiles(files, artists);

			Console.ReadKey();
		}

		static void ReadArr<T>(T[] arr)
		{
			foreach (T t in arr)
			{
				Console.WriteLine(t.ToString());
			}
		}

		static List<String> ExtractArtist<T>(T[] arr)
		{
			List<String> artists = new List<string>();
			foreach (T t in arr)
			{
				String str = t.ToString();
				String artist = str.Substring(str.LastIndexOf("\\") + 1);
				artists.Add(artist);
			}
			return artists;
		}

		static void RenameFiles(String[] files, List<String> Artists)
		{
			foreach (String f in files)
			{
				foreach (String a in Artists)
				{
					if (f.Contains(a))
					{
						String path = f.Substring(0, f.LastIndexOf("\\"));
						String file = f.Substring(f.LastIndexOf("\\") + 1);
						if (file.Contains("[") || file.Contains("]")) { 
							String newPath = path + "\\[" + a + "] " + file;
							try
							{
								File.Move(f, newPath);
							}
							catch (Exception ex)
							{
								Console.WriteLine(newPath + " Doesn't exist??");
								continue;
							}
						} 
						continue;
					}
				}
			}
		}
	}
}
