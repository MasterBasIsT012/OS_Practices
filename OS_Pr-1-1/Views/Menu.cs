using OS_Pr_1_1.Entities;
using OS_Pr_1_1.Interactors;
using System;
using System.Collections.Generic;
using System.Threading;

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
						GetAndSaveVkPosts();
						break;
					case 2:
						ReloadVkposts();
						break;
					case 3:
						ReadVkposts();
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

		private void GetAndSaveVkPosts()
		{
			Thread thread = new Thread(chromeWorker.ReadAndSaveVkPostsFromBrowser);
			thread.Start();
		}
		private void ReloadVkposts()
		{
			Thread thread = new Thread(chromeWorker.ReloadVkPosts);
			thread.Start();
		}
		private void ReadVkposts()
		{
			chromeWorker.ReadVkPostsFromFiles();
			ShowVkPost(chromeWorker.VkPosts);
			Console.ReadKey();
		}
		private void ShowVkPost(List<VkPost> vkPosts)
		{
			foreach (VkPost vkPost in vkPosts)
			{
				Console.WriteLine($"Id: {vkPost.Id}\n" +
					$"Text:\n {vkPost.Text.TrimEnd('\n')}\n" +
					$"Image hrefs: ");

				foreach (string imageHref in vkPost.ImagesHrefs)
					Console.WriteLine(imageHref);

				Console.WriteLine("Hrefs: ");
				foreach (string href in vkPost.SharedHrefs)
					Console.WriteLine(href);

				Console.WriteLine();
			}
		}

		private static int GetUserInput()
		{
			Console.Clear();
			Console.WriteLine(
							"1. Get vk posts and write them to files\n" +
							"2. Reload data in vk posts files\n" +
							"3. Read data from vk posts files\n" +
							"0. Quit\n");

			Console.Write("Enter the command number: ");
			int commandNumber;
			while (!int.TryParse(Console.ReadLine(), out commandNumber))
				Console.WriteLine("You input something strange, repeat the entrance: ");
			return commandNumber;
		}
	}
}
