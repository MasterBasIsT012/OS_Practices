using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OS_Pr_1_1.DTOs;

namespace OS_Pr_1_1.Models
{
	public class JsonWorker
	{
		static JsonSerializer serializer = new JsonSerializer();

		public string GetJsonPostText(List<VkPost> vkPosts)
		{
			List<PostTextDTO> postTextDTOs = new List<PostTextDTO>();
			foreach (VkPost vkPost in vkPosts)
				postTextDTOs.Add(new PostTextDTO() { Id = vkPost.Id, Text = vkPost.Text });

			string vkPostJson = JsonConvert.SerializeObject(postTextDTOs);
			
			return vkPostJson;
		}
		
		public string GetJsonPostImagesHrefs(List<VkPost> vkPosts)
		{
			List<PostImagesHrefsDTO> postImagesHrefsDTOs = new List<PostImagesHrefsDTO>();
			foreach (VkPost vkPost in vkPosts)
				postImagesHrefsDTOs.Add(new PostImagesHrefsDTO() { Id = vkPost.Id, ImagesHrefs = vkPost.ImagesHrefs });

			string vkPostJson = JsonConvert.SerializeObject(postImagesHrefsDTOs);
			
			return vkPostJson;
		}
		
		public string GetJsonPostHrefs(List<VkPost> vkPosts)
		{
			List<PostSharedHrefsDTO> postTextDTOs = new List<PostSharedHrefsDTO>();
			foreach (VkPost vkPost in vkPosts)
				postTextDTOs.Add(new PostSharedHrefsDTO() { Id = vkPost.Id, SharedHrefs = vkPost.SharedHrefs });

			string vkPostJson = JsonConvert.SerializeObject(postTextDTOs);

			return vkPostJson;
		}
	}
}
