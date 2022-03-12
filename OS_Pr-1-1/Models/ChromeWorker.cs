using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OS_Pr_1_1.Models
{
	public class ChromeWorker : IDisposable
	{
		ChromeDriver chromeDriver;

		FileWriter fileWriter;
		List<VkPost> vkPosts;

		public ChromeWorker(string URL, FileWriter fileWriter)
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
			Thread textWriteThread = new Thread(() => fileWriter.WriteVkPostsText(vkPosts));
			Thread imagesHrefsWriteThread = new Thread(() => fileWriter.WriteVkPostsImagesHrefs(vkPosts));
			Thread hrefsWriteThread = new Thread(() => fileWriter.WriteVkPostsImagesHrefs(vkPosts));

			textWriteThread.Start();
			imagesHrefsWriteThread.Start();
			hrefsWriteThread.Start();
		}
	}
}
