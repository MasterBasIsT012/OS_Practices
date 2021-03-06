using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OS_Pr_1_1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OS_Pr_1_1.Interactors
{
	public class ChromeWorker : IDisposable
	{
		ChromeDriver chromeDriver;

		IDataWorker dataWorker;
		List<VkPost> vkPosts;
		public List<VkPost> VkPosts { get { return vkPosts; } }

		public ChromeWorker(string URL, IDataWorker dataWorker)
		{
			ChromeOptions chromeOptions = new ChromeOptions();
			chromeOptions.AddArgument(@"user-data-dir=C:\Users\saa_0\AppData\Local\Google\Chrome\User Data");
			chromeDriver = new ChromeDriver(chromeOptions);
			chromeDriver.Navigate().GoToUrl(URL);

			this.dataWorker = dataWorker;
			vkPosts = new List<VkPost>();
		}


		public void ReadAndSaveVkPostsFromBrowser()
		{
			vkPosts = GetVkPostsFromBrowser();
			WriteVkPostsToFiles();
		}
		public void ReadVkPostsFromFiles()
		{
			vkPosts = dataWorker.ReadVkPosts();
		}

		public void ReloadVkPosts()
		{
			chromeDriver.Navigate().Refresh();
			GetNewVkPosts();
			WriteVkPostsToFiles();
		}
		private List<VkPost> GetNewVkPosts()
		{
			List<VkPost> newVkPosts = GetVkPostsFromBrowser();

			foreach (VkPost vkPost in newVkPosts)
				if (!vkPosts.Contains(vkPost))
					vkPosts.Add(vkPost);

			return newVkPosts;
		}

		private void WriteVkPostsToFiles()
		{
			dataWorker.WriteVkPosts(vkPosts);
		}
		private List<VkPost> GetVkPostsFromBrowser()
		{
			List<VkPost> newVkPosts = new List<VkPost>();

			List<IWebElement> webElements = chromeDriver.FindElements(By.ClassName("feed_row")).ToList();
			foreach (var webElement in webElements)
			{
				try
				{
					if (!webElement.Displayed)
						continue;

					VkPost vkPost = new VkPost(webElement);
					if (!string.IsNullOrEmpty(vkPost.Id))
						newVkPosts.Add(vkPost);
				}
				catch (Exception)
				{
					continue;
				}
			}

			return newVkPosts;
		}


		public void Dispose()
		{
			chromeDriver?.Quit();
		}
	}
}
