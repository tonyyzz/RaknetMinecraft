using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 玩家装扮model
	/// </summary>
	public class PlayerDisguiseModel
	{
		/// <summary>
		/// 
		/// </summary>
		public PlayerDisguiseModel()
		{
			PlayerId = 0;
			ModelSex = 0;
			Head = "";
			Coat = "";
			Pant = "";
		}
		/// <summary>
		/// 玩家Id
		/// </summary>
		public int PlayerId { get; set; }
		/// <summary>
		/// 模型性别（1：男，2：女）
		/// </summary>
		public int ModelSex { get; set; }
		/// <summary>
		/// 运动帽
		/// </summary>
		public string Head { get; set; }
		/// <summary>
		/// 运动服
		/// </summary>
		public string Coat { get; set; }
		/// <summary>
		/// 运动裤
		/// </summary>
		public string Pant { get; set; }
	}
}
