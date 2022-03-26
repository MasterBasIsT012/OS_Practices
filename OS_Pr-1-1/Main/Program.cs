using OS_Pr_1_1.Data;
using OS_Pr_1_1.Interactors;
using OS_Pr_1_1.Views;
using System;

namespace OS_Pr_1_1.Main
{
	internal class Program
	{
		static string URL = "https://vk.com/feed";

		static FileWorker fileWriter;
		static ChromeWorker chromeWorker;
		static Menu menu;

		static void Main(string[] args)
		{
			Initialize();
			Processing();
			Deinitialize();
		}


		static void Initialize()
		{
			try
			{
				fileWriter = new FileWorker(new JsonWorker());
				chromeWorker = new ChromeWorker(URL, fileWriter);
				menu = new Menu(chromeWorker);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Processing()
		{
			menu.Loop();
		}

		static void Deinitialize()
		{
			chromeWorker.Dispose();
		}
	}
}
