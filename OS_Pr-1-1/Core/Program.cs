using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using OS_Pr_1_1.Data;

namespace OS_Pr_1_1.Core
{
	internal class Program
	{
		static ChromeDriver chromeDriver;
		static List<VkPost> vkPosts;

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
				ChromeOptions chromeOptions = new ChromeOptions();
				chromeOptions.AddArgument(@"user-data-dir=C:\Users\saa_0\AppData\Local\Google\Chrome\User Data");
				chromeDriver = new ChromeDriver(chromeOptions);
				chromeDriver.Navigate().GoToUrl("https://vk.com/feed?section=recommended");

				vkPosts = new List<VkPost>();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Processing()
		{
			List<IWebElement> webElements = chromeDriver.FindElements(By.ClassName("feed_row")).ToList();
			foreach (var webElement in webElements)
			{
				try
				{
					if (!webElement.Displayed)
						continue;

					VkPost vkPost = new VkPost(webElement);
					if (!string.IsNullOrEmpty(vkPost.Id))
						vkPosts.Add(vkPost);
				}
				catch (Exception)
				{
					continue;
				}
			}

			JsonWorker.SetPostText(vkPosts);
			JsonWorker.SetPostImagesHrefs(vkPosts);
			JsonWorker.SetPostHrefs(vkPosts);
		}

		static void Deinitialize()
		{
			chromeDriver.Quit();
		}
	}
}
