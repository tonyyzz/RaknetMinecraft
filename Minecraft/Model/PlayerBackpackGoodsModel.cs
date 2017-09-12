using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 玩家背包model
	/// </summary>
	public partial class PlayerBackpackGoodsModel
	{
		/// <summary>
		/// 
		/// </summary>
		public PlayerBackpackGoodsModel()
		{
			PlayerId = 0;
			HeadStr = "";
			CoatStr = "";
			PantStr = "";
		}
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 运动帽（多个用‘|’分割，如果有数字0，则保留）
		/// </summary>
		public string HeadStr { get; set; }
		/// <summary>
		/// 运动服
		/// </summary>
		public string CoatStr { get; set; }
		/// <summary>
		/// 运动裤
		/// </summary>
		public string PantStr { get; set; }
	}
}
