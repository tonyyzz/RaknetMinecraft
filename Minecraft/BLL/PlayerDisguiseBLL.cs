using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class PlayerDisguiseBLL : BaseBLL
	{
		/// <summary>
		/// 获取某个玩家的装扮信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.PlayerDisguiseModel GetPlayerDisguise(int playerId,int modelSexInt)
		{
			return DAL.PlayerDisguiseDAL.GetPlayerDisguise(playerId, modelSexInt);
		}
		/// <summary>
		/// 更新玩家装扮
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="disguiseTypeStr"></param>
		/// <param name="disguiseVal"></param>
		/// <returns></returns>
		public static bool UpdatePlayerDisguise(int playerId, int modelSexInt, string disguiseTypeStr, string disguiseVal)
		{
			return DAL.PlayerDisguiseDAL.UpdatePlayerDisguise(playerId, modelSexInt, disguiseTypeStr, disguiseVal);
		}
	}
}
