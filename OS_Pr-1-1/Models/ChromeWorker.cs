using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OS_Pr_1_1.Models
{
	public class ChromeWorker : IDisposable
	{
		ChromeDriver chromeDriver;

		FileWorker fileWriter;
		List<VkPost> vkPosts;

		public ChromeWorker(string URL, FileWorker fileWriter)
		{
			ChromeOptions chromeOptions = new ChromeOptions();
			chromeOptions.AddArgument(@"user-data-dir=C:\Users\saa_0\AppData\Local\Google\Chrome\User Data");
			chromeDriver = new ChromeDriver(chromeOptions);
			chromeDriver.Navigate().GoToUrl(URL);

			this.fileWriter = fileWriter;
			vkPosts = new List<VkPost>();
		}
		~ChromeWorker()
		{
			chromeDriver?.Quit();
		}

		public void Dispose()
		{
			chromeDriver?.Quit();
		}

		public void GetAndSaveVkPosts()
		{
			GetVkPostsFromBrowser();
			WriteVkPostsToFiles();
		}
		private void WriteVkPostsToFiles()
		{
			Thread textWriteThread = new Thread(() => fileWriter.WriteVkPostsText(vkPosts));
			Thread imagesHrefsWriteThread = new Thread(() => fileWriter.WriteVkPostsImagesHrefs(vkPosts));
			Thread hrefsWriteThread = new Thread(() => fileWriter.WriteVkPostsHrefs(vkPosts));

			textWriteThread.Start();
			imagesHrefsWriteThread.Start();
			hrefsWriteThread.Start();
		}

		public void ReloadVkPosts()
		{
			GetVkPostsFromBrowser();
			ReloadVkPostsToFiles();
		}
		private void ReloadVkPostsToFiles()
		{
			Thread textWriteThread = new Thread(() => fileWriter.ReloadVkPostsText(vkPosts));
			Thread imagesHrefsWriteThread = new Thread(() => fileWriter.ReloadVkPostsImagesHrefs(vkPosts));
			Thread hrefsWriteThread = new Thread(() => fileWriter.ReloadVkPostsHrefs(vkPosts));

			textWriteThread.Start();
			imagesHrefsWriteThread.Start();
			hrefsWriteThread.Start();
		}
		
		private void GetVkPostsFromBrowser()
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
		}
	}
}
