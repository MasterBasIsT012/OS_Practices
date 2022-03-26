using OS_Pr_1_1.Entities;
using System.Collections.Generic;

namespace OS_Pr_1_1.Interactors
{
	public interface IFileWorker
	{
		List<VkPost> ReadVkPosts();
		void WriteVkPosts(List<VkPost> vkPosts);
	}
}