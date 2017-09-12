using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
	public class PlayerBackpackGoodsDAL : BaseDAL
	{
		/// <summary>
		/// 获取玩家背包信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.PlayerBackpackGoodsModel GetPlayerBackpackGoodsInfo(int playerId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "select * from PlayerBackpackGoods where playerId=@playerId;";
				return Conn.QueryFirstOrDefault<Model.PlayerBackpackGoodsModel>(sql, new { playerId = playerId });

			}
		}
	}
}
