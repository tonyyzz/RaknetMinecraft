using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest
{
	public class PlayerMng
	{
		/// <summary>
		/// 玩家信息（房主和其他玩家通用）
		/// </summary>
		public static Model.PlayerModel player = new Model.PlayerModel();
		/// <summary>
		/// 房主信息（包括房间信息，以及该房间的所有玩家信息）
		/// </summary>
		public static Model.PlayerHouseModel OwnerPlayerHouse = new Model.PlayerHouseModel();

		/// <summary>
		/// 存储进过房间的用户集合
		/// </summary>
		public static List<Model.PlayerModel> playerIncomingList = new List<Model.PlayerModel>();

	}
}
