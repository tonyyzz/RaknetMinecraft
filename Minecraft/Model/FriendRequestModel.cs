using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 好友申请记录表
	/// </summary>
	public class FriendRequestModel
	{
		/// <summary>
		/// 
		/// </summary>
		public FriendRequestModel()
		{
			Id = 0;
			PlayerId = 0;
			FriendId = 0;
			RequestTime = new DateTime(1900, 1, 1);
			RequestState = 0;
		}
		/// <summary>
		/// 好友申请记录Id
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 要申请添加的好友Id
		/// </summary>
		public int FriendId { get; set; }
		/// <summary>
		/// 申请时间
		/// </summary>
		public DateTime RequestTime { get; set; }
		/// <summary>
		/// 申请状态（0：正在申请添加好友，1：同意添加，2：拒绝添加）
		/// </summary>
		public int RequestState { get; set; }
	}
}
