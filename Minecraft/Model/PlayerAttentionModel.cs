using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 玩家关注model
	/// </summary>
	public class PlayerAttentionModel
	{
		/// <summary>
		/// 构造函数初始化
		/// </summary>
		public PlayerAttentionModel()
		{
			PlayerId = 0;
			PlayerAttentionId = 0;
			AttentionTime = new DateTime(1900, 1, 1);
		}
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 玩家关注的玩家Id
		/// </summary>
		public int PlayerAttentionId { get; set; }
		/// <summary>
		/// 关注时间
		/// </summary>
		public DateTime AttentionTime { get; set; }
	}
}
