using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using OS_Pr_1_1.Models;
using OS_Pr_1_1.Views;

namespace OS_Pr_1_1.Core
{
	internal class Program
	{
		static string URL = "https://vk.com/feed?section=recommended";

		static FileWriter fileWriter;
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
				fileWriter = new FileWriter(new JsonWorker());
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
