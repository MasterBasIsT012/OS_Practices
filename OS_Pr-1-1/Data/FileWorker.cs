using OS_Pr_1_1.Data.DTOs;
using OS_Pr_1_1.Entities;
using OS_Pr_1_1.Interactors;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace OS_Pr_1_1.Data
{
	public class FileWorker : IDataWorker
	{
		readonly string vkPostTextsFileName = "1.json";
		readonly string vkPostsImagesHrefsFileName = "2.json";
		readonly string vkPostsHrefsFileName = "3.json";

		private readonly JsonWorker jsonWorker;

		public FileWorker(JsonWorker jsonWorker)
		{
			this.jsonWorker = jsonWorker;
		}


		public void WriteVkPosts(List<VkPost> vkPosts)
		{
			Thread textWriteThread = new Thread(() => WriteVkPostsText(vkPosts));
			Thread imagesHrefsWriteThread = new Thread(() => WriteVkPostsImagesHrefs(vkPosts));
			Thread hrefsWriteThread = new Thread(() => WriteVkPostsHrefs(vkPosts));

			textWriteThread.Start();
			imagesHrefsWriteThread.Start();
			hrefsWriteThread.Start();
		}
		private void WriteVkPostsText(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostText(vkPosts);
			WriteToFile(vkPostTextsFileName, vkPostJson);
		}
		private void WriteVkPostsImagesHrefs(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostImagesHrefs(vkPosts);
			WriteToFile(vkPostsImagesHrefsFileName, vkPostJson);
		}
		private void WriteVkPostsHrefs(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostHrefs(vkPosts);
			WriteToFile(vkPostsHrefsFileName, vkPostJson);
		}
		private void WriteToFile(string fileName, string vkPostJson)
		{
			if (File.Exists(fileName))
				File.Delete(fileName);

			using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
			{
				fs.Write(Encoding.UTF8.GetBytes(vkPostJson));
			}
		}


		public List<VkPost> ReadVkPosts()
		{
			List<VkPost> vkPosts = new List<VkPost>();

			List<VkPostTextDTO> postTextDTOs = new List<VkPostTextDTO>();
			List<VkPostImagesHrefsDTO> postImagesHrefsDTOs = new List<VkPostImagesHrefsDTO>();
			List<VkPostSharedHrefsDTO> postSharedHrefsDTOs = new List<VkPostSharedHrefsDTO>();

			Thread textReadThread = new Thread(() => postTextDTOs = ReadVkPostsText());
			Thread imagesHrefsReadThread = new Thread(() => postImagesHrefsDTOs = ReadVkPostImagesHrefs());
			Thread hrefsReadThread = new Thread(() => postSharedHrefsDTOs = ReadVkPostHrefs());

			textReadThread.Start();
			imagesHrefsReadThread.Start();
			hrefsReadThread.Start();
			textReadThread.Join();
			imagesHrefsReadThread.Join();
			hrefsReadThread.Join();

			for (int i = 0; i < postTextDTOs.Count; i++)
			{
				vkPosts.Add(new VkPost()
				{
					Id = postTextDTOs[i].Id,
					Text = postTextDTOs[i].Text,
					ImagesHrefs = postImagesHrefsDTOs[i].ImagesHrefs,
					SharedHrefs = postSharedHrefsDTOs[i].SharedHrefs,
				});
			}

			return vkPosts;
		}
		private List<VkPostTextDTO> ReadVkPostsText()
		{
			List<VkPostTextDTO> vkPostsTexts = new List<VkPostTextDTO>();

			if (File.Exists(vkPostTextsFileName))
			{
				string content = File.ReadAllText(vkPostTextsFileName);

				vkPostsTexts = jsonWorker.GetPostTextFromJson(content);
			}

			return vkPostsTexts;
		}
		private List<VkPostImagesHrefsDTO> ReadVkPostImagesHrefs()
		{
			List<VkPostImagesHrefsDTO> vkPostsImageHrefs = new List<VkPostImagesHrefsDTO>();

			if (File.Exists(vkPostsImagesHrefsFileName))
			{
				string content = File.ReadAllText(vkPostsImagesHrefsFileName);

				vkPostsImageHrefs = jsonWorker.GetPostImagesHrefsFromJson(content);
			}

			return vkPostsImageHrefs;
		}
		private List<VkPostSharedHrefsDTO> ReadVkPostHrefs()
		{
			List<VkPostSharedHrefsDTO> vkPostsHrefs = new List<VkPostSharedHrefsDTO>();

			if (File.Exists(vkPostsHrefsFileName))
			{
				string content = File.ReadAllText(vkPostsHrefsFileName);

				vkPostsHrefs = jsonWorker.GetSharedHrefsFromJson(content);
			}

			return vkPostsHrefs;
		}
	}
}
