using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OS_Pr_1_1.Models
{
	public class FileWorker
	{
		readonly string f1Name = "1.json";
		readonly string f2Name = "2.json";
		readonly string f3Name = "3.json";

		private readonly JsonWorker jsonWorker;

		public FileWorker(JsonWorker jsonWorker)
		{
			this.jsonWorker = jsonWorker;
		}

		public void WriteVkPostsText(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostText(vkPosts);

			using (FileStream sw = new FileStream(f1Name, FileMode.OpenOrCreate))
			{
				sw.Write(Encoding.UTF8.GetBytes(vkPostJson));
			}
		}

		public void WriteVkPostsImagesHrefs(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostImagesHrefs(vkPosts);

			using (FileStream sw = new FileStream(f2Name, FileMode.OpenOrCreate))
			{
				sw.Write(Encoding.UTF8.GetBytes(vkPostJson));
			}
		}

		public void WriteVkPostsHrefs(List<VkPost> vkPosts)
		{
			string vkPostJson = jsonWorker.GetJsonPostHrefs(vkPosts);

			using (FileStream sw = new FileStream(f3Name, FileMode.OpenOrCreate))
			{
				sw.Write(Encoding.UTF8.GetBytes(vkPostJson));
			}
		}

	}
}
