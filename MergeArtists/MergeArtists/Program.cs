// est. 11/11/2018 by Drachen

using System;
using System.Collections.Generic;
using System.IO;

namespace MergeArtists
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Time to Merge Artists\n" +
				"Enter a path from which to work: ");

			var rootDir = Console.ReadLine();
			var subDir = Directory.GetDirectories(rootDir);

			ReadArr(subDir);

			foreach (String dir in subDir)
			{
				String artist = dir.Substring(dir.LastIndexOf("\\") + 1);
				bool ping = false;
				String[] subDir2 = Directory.GetDirectories(dir);

				foreach (String s in subDir2)
				{
					String testString = s.Substring(s.LastIndexOf("\\") + 1);
					if (testString.Equals(artist))
					{
						Directory.Move(s, dir);
						break;
					}
				}
			}

			Console.WriteLine(rootDir);
			List<String> artists = ExtractArtist(subDir);
			ReadList(artists);

	//		String[] files = Directory.GetFiles(rootDir, "*.*", SearchOption.AllDirectories);
			ReadList(artists);

			Console.ReadKey();
		}

		static void ReadArr<T>(T[] arr)
		{
			foreach (T t in arr)
			{
				Console.WriteLine(t.ToString());
			}
		}

		static void ReadList<T>(List<T> list)
		{
			foreach (T t in list)
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
						if (file.Contains("[") || file.Contains("]"))
						{
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
