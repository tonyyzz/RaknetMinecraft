using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class ResourceCommentReplyModel
	{
		public ResourceCommentReplyModel()
		{
			ReplyId = 0;
			ParentId = 0;
			CommentId = 0;
			Reply = "";
			ReplyTime = new DateTime(1900, 1, 1);
			ReplyPersonId = 0;
		}
		/// <summary>
		/// 回复Id
		/// </summary>
		public int ReplyId { get; set; }
		/// <summary>
		/// 父Id
		/// </summary>
		public int ParentId { get; set; }
		/// <summary>
		/// 评论Id
		/// </summary>
		public int CommentId { get; set; }
		/// <summary>
		/// 回复内容
		/// </summary>
		public string Reply { get; set; }
		/// <summary>
		/// 回复时间
		/// </summary>
		public DateTime ReplyTime { get; set; }
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int ReplyPersonId { get; set; }
	}
}
