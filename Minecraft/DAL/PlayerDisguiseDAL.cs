using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
	public class PlayerDisguiseDAL : BaseDAL
	{
		/// <summary>
		/// 获取某个玩家的装扮信息
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.PlayerDisguiseModel GetPlayerDisguise(int playerId, int modelSexInt)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "select * from PlayerDisguise where playerId = @playerId and ModelSex=@ModelSex;";
				return Conn.QueryFirstOrDefault<Model.PlayerDisguiseModel>(sql, new { playerId = playerId, ModelSex = modelSexInt });
			}
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
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"update PlayerDisguise set {0}=@disguiseVal where playerId=@playerId and ModelSex=@ModelSex;",
					disguiseTypeStr);
				return Conn.Execute(sql, new { playerId = playerId, ModelSex = modelSexInt, disguiseVal = disguiseVal }) > 0;
			}
		}
	}
}
