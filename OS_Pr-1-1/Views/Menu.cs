using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OS_Pr_1_1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OS_Pr_1_1.Views
{
	public class Menu
	{
		private ChromeWorker chromeWorker;

		public Menu(ChromeWorker chromeWorker)
		{
			this.chromeWorker = chromeWorker;
		}

		public void Loop()
		{
			int commandNumber = -1;

			while (commandNumber != 0)
			{
				commandNumber = GetUserInput();

				switch (commandNumber)
				{
					case 1:
						GetAndSaveVkPost();
						break;
					case 0:
						Console.WriteLine("Bye!");
						break;
					default:
						Console.WriteLine("You enter non-existent number of action, repeat the entrance");
						break;
				}
			}
		}
		private void GetAndSaveVkPost()
		{
			chromeWorker.GetAndSaveVkPosts();
		}
		private static int GetUserInput()
		{
			Console.Clear();
			Console.WriteLine(
							"1. Get vk posts and write them to file\n" +
							"2. Upload new data to vk posts files\n" +
							"3. Read data from vk post files\n" +
							"0. Quit\n");

			Console.Write("Enter the command number: ");
			int commandNumber;
			while (!int.TryParse(Console.ReadLine(), out commandNumber))
				Console.WriteLine("You input something strange, repeat the entrance; ");
			return commandNumber;
		}
	}
}
