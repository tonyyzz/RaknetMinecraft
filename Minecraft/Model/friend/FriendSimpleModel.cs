using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 朋友简略信息（用于朋友列表显示）
	/// </summary>
	public partial class FriendSimpleModel
	{
		/// <summary>
		/// 朋友Id
		/// </summary>
		public int FriendId { get; set; }
		/// <summary>
		/// 朋友名称
		/// </summary>
		public string FriendName { get; set; }
		/// <summary>
		/// 朋友头像
		/// </summary>
		public string FriendHeadImg { get; set; }
		/// <summary>
		/// 朋友等级
		/// </summary>
		public int FriendLevel { get; set; }

	}
	public partial class FriendSimpleModel
	{
		/// <summary>
		/// 是否在线
		/// </summary>
		public bool isOnline = false;
		/// <summary>
		/// 好友申请状态（0：正在申请添加好友，1：接受添加好友，2：拒绝添加好友）（只在申请记录列表中使用）
		/// </summary>
		public int RequestState = 0;
		/// <summary>
		/// 好友申请时间（只在申请记录列表中使用）
		/// </summary>
		public DateTime RequestTime = new DateTime(1900, 1, 1);
	}
}
