using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class PlayerSuitBLL : BaseBLL
	{
		/// <summary>
		/// 获取某个玩家的套装信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.PlayerSuitModel GetPlayerSuit(int playerId)
		{
			return DAL.PlayerSuitDAL.GetPlayerSuit(playerId);
		}
		/// <summary>
		/// 更新玩家套装信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="suitStr"></param>
		/// <returns></returns>
		public static bool UpdatePlayerSuit(int playerId, string suitStr)
		{
			return DAL.PlayerSuitDAL.UpdatePlayerSuit(playerId, suitStr);

		}
	}
}
