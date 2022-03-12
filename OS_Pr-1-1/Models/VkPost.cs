using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OS_Pr_1_1.Models
{
	public class VkPost
	{
		public string Id;
		public string Text;
		public List<string> ImagesHrefs = new List<string>();
		public List<string> SharedHrefs = new List<string>();

		public VkPost()
		{

		}

		public VkPost(IWebElement post)
		{
			if (IsPost(post))
			{
				GetAndSaveId(post);
				IWebElement postContent = post.FindElement(By.ClassName("wall_text"));
				GetAndSaveText(postContent);
				GetAndSavePicturesHrefs(postContent);
				GetAndSaveSharedHrefs(postContent);
			}
		}
		private bool IsPost(IWebElement post)
		{
			if (post.Displayed)
			{
				List<IWebElement> webElements = post.FindElements(By.ClassName("feed_friends_recomm__title")).ToList();
				return webElements.Count == 0;
			}
			else
				return false;
		}
		private void GetAndSaveId(IWebElement webElement)
		{
			IWebElement postId = webElement.FindElement(By.TagName("div"));
			Id = postId.GetAttribute("id");
		}
		private void GetAndSaveText(IWebElement postContent)
		{
			Text = postContent.Text;
		}
		private void GetAndSavePicturesHrefs(IWebElement postContent)
		{
			List<IWebElement> pngElements = new List<IWebElement>();

			pngElements = postContent.FindElements(By.TagName("img")).ToList();
			foreach (IWebElement pngElement in pngElements)
			{
				if (pngElement.GetAttribute("src") == null)
					continue;

				ImagesHrefs.Add(pngElement.GetAttribute("src"));
			}

			pngElements = postContent.FindElements(By.TagName("a")).ToList();
			foreach (IWebElement pngElement in pngElements)
			{
				if (pngElement.GetAttribute("aria-label") == null)
					continue;

				ImagesHrefs.Add(GetPictureHrefFromAriaLabel(pngElement));
			}

		}
		private string GetPictureHrefFromAriaLabel(IWebElement pngElement)
		{
			string ariaLabel = pngElement.GetAttribute("onclick");

			return Regex.Match(ariaLabel, "https(.*?)album", RegexOptions.None).Value;
		}

		private void GetAndSaveSharedHrefs(IWebElement postContent)
		{
			List<IWebElement> hrefElements = postContent.FindElements(By.TagName("a")).ToList();

			foreach (IWebElement hrefElement in hrefElements)
			{
				if (hrefElement.GetAttribute("href") == null)
					continue;

				SharedHrefs.Add(hrefElement.GetAttribute("href"));

			}
		}
	}
}
