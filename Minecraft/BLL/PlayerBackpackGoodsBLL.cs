using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class PlayerBackpackGoodsBLL : BaseBLL
	{
		/// <summary>
		/// 获取玩家背包信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.PlayerBackpackGoodsModel GetPlayerBackpackGoodsInfo(int playerId)
		{
			return DAL.PlayerBackpackGoodsDAL.GetPlayerBackpackGoodsInfo(playerId);
		}
	}
}
