using Newtonsoft.Json;
using OS_Pr_1_1.Data.DTOs;
using OS_Pr_1_1.Entities;
using System.Collections.Generic;

namespace OS_Pr_1_1.Data
{
	public class JsonWorker
	{
		static JsonSerializer serializer = new JsonSerializer();


		public string GetJsonPostText(List<VkPost> vkPosts)
		{
			List<VkPostTextDTO> postTextDTOs = new List<VkPostTextDTO>();
			foreach (VkPost vkPost in vkPosts)
				postTextDTOs.Add(new VkPostTextDTO() { Id = vkPost.Id, Text = vkPost.Text });

			string vkPostJson = JsonConvert.SerializeObject(postTextDTOs);

			return vkPostJson;
		}
		public string GetJsonPostImagesHrefs(List<VkPost> vkPosts)
		{
			List<VkPostImagesHrefsDTO> postImagesHrefsDTOs = new List<VkPostImagesHrefsDTO>();
			foreach (VkPost vkPost in vkPosts)
				postImagesHrefsDTOs.Add(new VkPostImagesHrefsDTO() { Id = vkPost.Id, ImagesHrefs = vkPost.ImagesHrefs });

			string vkPostJson = JsonConvert.SerializeObject(postImagesHrefsDTOs);

			return vkPostJson;
		}
		public string GetJsonPostHrefs(List<VkPost> vkPosts)
		{
			List<VkPostSharedHrefsDTO> postTextDTOs = new List<VkPostSharedHrefsDTO>();
			foreach (VkPost vkPost in vkPosts)
				postTextDTOs.Add(new VkPostSharedHrefsDTO() { Id = vkPost.Id, SharedHrefs = vkPost.SharedHrefs });

			string vkPostJson = JsonConvert.SerializeObject(postTextDTOs);

			return vkPostJson;
		}


		public List<VkPostTextDTO> GetPostTextFromJson(string postJson)
		{
			List<VkPostTextDTO> postTextDTOs = new List<VkPostTextDTO>();

			postTextDTOs = JsonConvert.DeserializeObject<List<VkPostTextDTO>>(postJson);

			return postTextDTOs;
		}
		public List<VkPostImagesHrefsDTO> GetPostImagesHrefsFromJson(string postJson)
		{
			List<VkPostImagesHrefsDTO> postsHrefsDTOs = new List<VkPostImagesHrefsDTO>();

			postsHrefsDTOs = JsonConvert.DeserializeObject<List<VkPostImagesHrefsDTO>>(postJson);

			return postsHrefsDTOs;
		}
		public List<VkPostSharedHrefsDTO> GetSharedHrefsFromJson(string postJson)
		{
			List<VkPostSharedHrefsDTO> postSharedHrefsDTOs = new List<VkPostSharedHrefsDTO>();

			postSharedHrefsDTOs = JsonConvert.DeserializeObject<List<VkPostSharedHrefsDTO>>(postJson);

			return postSharedHrefsDTOs;
		}

		private string NormalizeToJson(string content)
		{
			content = content.Replace("\\", "");
			content = content.Trim('\\', '\"');
			return content;
		}
	}
}
