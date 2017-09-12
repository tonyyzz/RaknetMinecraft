using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 玩家套装表
	/// </summary>
	public class PlayerSuitModel
	{
		/// <summary>
		/// 
		/// </summary>
		public PlayerSuitModel()
		{
			PlayerId = 0;
			SuitStr = "";
		}
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 套装字符串
		/// </summary>
		public string SuitStr { get; set; }
	}
}
