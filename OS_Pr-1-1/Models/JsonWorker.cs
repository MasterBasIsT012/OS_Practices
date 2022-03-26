using Newtonsoft.Json;
using OS_Pr_1_1.DTOs;
using System.Collections.Generic;

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
		
		public List<PostTextDTO> GetPostTextFromJson(string postJson)
		{
			List<PostTextDTO> postTextDTOs = new List<PostTextDTO>();

			JsonConvert.DeserializeObject<List<PostTextDTO>>(postJson);

			return postTextDTOs;
		}
		public List<PostImagesHrefsDTO> GetPostImagesHrefsFromJson(string postJson)
		{
			List<PostImagesHrefsDTO> postsHrefsDTOs = new List<PostImagesHrefsDTO>();

			postsHrefsDTOs = JsonConvert.DeserializeObject<List<PostImagesHrefsDTO>>(postJson);

			return postsHrefsDTOs;
		}
		public List<PostSharedHrefsDTO> GetSharedHrefsFromJson(string postJson)
		{
			List<PostSharedHrefsDTO> postSharedHrefsDTOs = new List<PostSharedHrefsDTO>();

			postSharedHrefsDTOs = JsonConvert.DeserializeObject<List<PostSharedHrefsDTO>>(postJson);

			return postSharedHrefsDTOs;
		}
	}
}
