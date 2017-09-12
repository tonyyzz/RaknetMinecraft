using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
	public class PlayerSuitDAL : BaseDAL
	{
		/// <summary>
		/// 获取某个玩家的套装信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.PlayerSuitModel GetPlayerSuit(int playerId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "select * from PlayerSuit where playerId = @playerId;";
				return Conn.QueryFirstOrDefault<Model.PlayerSuitModel>(sql, new { playerId = playerId });
			}
		}
		/// <summary>
		/// 更新玩家套装信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="suitStr"></param>
		/// <returns></returns>
		public static bool UpdatePlayerSuit(int playerId, string suitStr)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"update PlayerSuit set suitStr=@suitStr where playerId=@playerId;");
				return Conn.Execute(sql, new { playerId = playerId, suitStr = suitStr }) > 0;
			}
		}
	}
}
