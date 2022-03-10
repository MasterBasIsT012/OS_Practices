using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OS_Pr_1_1.DTOs;

namespace OS_Pr_1_1.Data
{
	public class JsonWorker
	{
		static JsonSerializer serializer = new JsonSerializer();

		static string f1Name = "1.json";
		static string f2Name = "2.json";
		static string f3Name = "3.json";

		//id, text
		public static void SetPostText(List<VkPost> vkPosts)
		{
			List<PostTextDTO> postTextDTOs = new List<PostTextDTO>();
			foreach (VkPost vkPost in vkPosts)
				postTextDTOs.Add(new PostTextDTO() { Id = vkPost.Id, Text = vkPost.Text });

			string vkPostJson = JsonConvert.SerializeObject(postTextDTOs);
			using (FileStream sw = new FileStream(f1Name, FileMode.Create))
			{
				sw.Write(Encoding.UTF8.GetBytes(vkPostJson));
			}
		}
		
		//id, images
		public static void SetPostImagesHrefs(List<VkPost> vkPosts)
		{
			List<PostImagesHrefsDTO> postImagesHrefsDTOs = new List<PostImagesHrefsDTO>();
			foreach (VkPost vkPost in vkPosts)
				postImagesHrefsDTOs.Add(new PostImagesHrefsDTO() { Id = vkPost.Id, ImagesHrefs = vkPost.ImagesHrefs });

			string vkPostJson = JsonConvert.SerializeObject(postImagesHrefsDTOs);
			using (FileStream sw = new FileStream(f2Name, FileMode.Create))
			{
				sw.Write(Encoding.UTF8.GetBytes(vkPostJson));
			}
		}
		
		//id, hrefs
		public static void SetPostHrefs(List<VkPost> vkPosts)
		{
			List<PostSharedHrefsDTO> postTextDTOs = new List<PostSharedHrefsDTO>();
			foreach (VkPost vkPost in vkPosts)
				postTextDTOs.Add(new PostSharedHrefsDTO() { Id = vkPost.Id, SharedHrefs = vkPost.SharedHrefs });

			string vkPostJson = JsonConvert.SerializeObject(postTextDTOs);
			using (FileStream sw = new FileStream(f3Name, FileMode.Create))
			{
				sw.Write(Encoding.UTF8.GetBytes(vkPostJson));
			}
		}
	}
}
