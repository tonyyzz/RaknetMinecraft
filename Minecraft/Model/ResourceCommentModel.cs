using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public partial class ResourceCommentModel
	{
		public ResourceCommentModel()
		{
			Id = 0;
			Content = "";
			ContentTime = new DateTime(1900, 1, 1);
			ResourceId = 0;
			PlayerId = 0;
		}
		/// <summary>
		/// 评论Id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 评论内容
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 评论时间
		/// </summary>
		public DateTime ContentTime { get; set; }
		/// <summary>
		/// 资源Id
		/// </summary>
		public int ResourceId { get; set; }
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int PlayerId { get; set; }
	}
	public partial class ResourceCommentModel
	{
		/// <summary>
		/// 玩家名称
		/// </summary>
		public string PlayerName = "";
		/// <summary>
		/// 玩家头像
		/// </summary>
		public string PlayerHeadImg = "";
		/// <summary>
		/// 评论者等级
		/// </summary>
		public int PlayerLevel = 0;
		/// <summary>
		/// 回复列表
		/// </summary>
		public List<ResourceCommentReplyModel> resourceCommentReplyList = new List<ResourceCommentReplyModel>();
	}
}
