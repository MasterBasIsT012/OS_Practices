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
		readonly string f1Name = "1.json";
		readonly string f2Name = "2.json";
		readonly string f3Name = "3.json";

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
			WriteToFile(f1Name, vkPostJson);
		}
		private void WriteVkPostsImagesHrefs(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostImagesHrefs(vkPosts);
			WriteToFile(f2Name, vkPostJson);
		}
		private void WriteVkPostsHrefs(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostHrefs(vkPosts);
			WriteToFile(f3Name, vkPostJson);
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

			List<PostTextDTO> postTextDTOs = new List<PostTextDTO>();
			List<PostImagesHrefsDTO> postImagesHrefsDTOs = new List<PostImagesHrefsDTO>();
			List<PostSharedHrefsDTO> postSharedHrefsDTOs = new List<PostSharedHrefsDTO>();

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
		private List<PostTextDTO> ReadVkPostsText()
		{
			List<PostTextDTO> vkPostsTexts = new List<PostTextDTO>();

			if (File.Exists(f1Name))
			{
				string content = File.ReadAllText(f1Name);

				vkPostsTexts = jsonWorker.GetPostTextFromJson(content);
			}

			return vkPostsTexts;
		}
		private List<PostImagesHrefsDTO> ReadVkPostImagesHrefs()
		{
			List<PostImagesHrefsDTO> vkPostsImageHrefs = new List<PostImagesHrefsDTO>();

			if (File.Exists(f2Name))
			{
				string content = File.ReadAllText(f2Name);

				vkPostsImageHrefs = jsonWorker.GetPostImagesHrefsFromJson(content);
			}

			return vkPostsImageHrefs;
		}
		private List<PostSharedHrefsDTO> ReadVkPostHrefs()
		{
			List<PostSharedHrefsDTO> vkPostsHrefs = new List<PostSharedHrefsDTO>();

			if (File.Exists(f3Name))
			{
				string content = File.ReadAllText(f3Name);

				vkPostsHrefs = jsonWorker.GetSharedHrefsFromJson(content);
			}

			return vkPostsHrefs;
		}
	}
}
