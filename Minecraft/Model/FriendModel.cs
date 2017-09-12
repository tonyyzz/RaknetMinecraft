using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class FriendModel
	{
		/// <summary>
		/// 
		/// </summary>
		public FriendModel()
		{
			PlayerId = 0;
			FriendId = 0;
			AddTime = new DateTime(1900, 1, 1);
		}
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 好友Id
		/// </summary>
		public int FriendId { get; set; }
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime AddTime { get; set; }
	}
}
