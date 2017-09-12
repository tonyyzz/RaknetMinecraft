using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
	public class PlayerDAL : BaseDAL
	{

		
		/// <summary>
		/// 获取某个玩家基本信息
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public static Model.PlayerModel QuerySingle(int Id)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "select * from player where Id=@Id";
				return Conn.QueryFirstOrDefault<Model.PlayerModel>(sql, new { Id = Id });
			}
		}
		/// <summary>
		/// 更新玩家最后登陆时间
		/// </summary>
		/// <param name="player"></param>
		public static bool UpdateLastLoginTime(Model.PlayerModel player)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update player set LastLoginTime = @LastLoginTime where Id = @Id";
				return Conn.Execute(sql, new { LastLoginTime = player.LastLoginTime, Id = player.Id }) > 0;

			}
		}

		/// <summary>
		/// 玩家关注
		/// </summary>
		/// <param name="playerAttentionModel"></param>
		/// <returns></returns>
		public static bool AttentionPlayer(Model.PlayerAttentionModel playerAttentionModel)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = @"delete from PlayerAttention where PlayerId=@PlayerId and PlayerAttentionId=@PlayerAttentionId;
							insert into PlayerAttention(PlayerId,PlayerAttentionId,AttentionTime) values(@PlayerId,@PlayerAttentionId,@AttentionTime);";
				return Conn.Execute(sql, playerAttentionModel) > 0;

			}
		}

		/// <summary>
		/// 获取玩家详细信息
		/// </summary>
		/// <param name="id">玩家Id</param>
		public static Model.PlayerModel GetPlayerDetail(int id)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select *
					,(select COUNT(0) from playerattention where PlayerAttentionId=p.Id )as FansNum
					,(select COUNT(0) from playerattention where PlayerId=p.Id )as AttentionNum
					,(select count(0) from resource where PlayerId=p.id and IsOfficial=false and ResourceTypeId=1) as MapNum
					,(select count(0) from resource where PlayerId=p.id and IsOfficial=false and ResourceTypeId=2) as DrawSheetNum
					,(select count(0) from friend where PlayerId=p.id) as FriendNum
					 from player p where p.id=@Id;");
				return Conn.QueryFirstOrDefault<Model.PlayerModel>(sql, new { Id = id });
			}
		}

		/// <summary>
		/// 更新玩家性别
		/// </summary>
		/// <param name="id">玩家Id</param>
		/// <param name="sex">新性别</param>
		/// <returns></returns>
		public static bool UpdatePlayerSex(int id, int sex)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Player set Sex=@Sex where id=@Id;";
				return Conn.Execute(sql, new { Id = id, Sex = sex }) > 0;
			}
		}

		/// <summary>
		/// 更新model性别
		/// </summary>
		/// <param name="id"></param>
		/// <param name="modelSexInt"></param>
		/// <returns></returns>
		public static bool UpdateModelSex(int id, int modelSexInt)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Player set ModelSex=@ModelSex where id=@Id;";
				return Conn.Execute(sql, new { Id = id, ModelSex = modelSexInt }) > 0;
			}
		}

		/// <summary>
		/// 更新介绍
		/// </summary>
		/// <param name="id">玩家Id</param>
		/// <param name="description">新介绍</param>
		/// <returns></returns>
		public static bool UpdateDescription(int id, string description)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Player set Description=@Description where id=@Id;";
				return Conn.Execute(sql, new { Id = id, Description = description }) > 0;
			}
		}
		/// <summary>
		/// 更新名称
		/// </summary>
		/// <param name="id">玩家Id</param>
		/// <param name="description">新名称</param>
		/// <returns></returns>
		public static bool UpdateName(int id, string name, IDbTransaction trans = null)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Player set Name=@Name where id=@Id;";
				return Conn.Execute(sql, new { Id = id, Name = name }, trans) > 0;
			}
		}
		/// <summary>
		/// 更新金币
		/// </summary>
		/// <param name="id"></param>
		/// <param name="money"></param>
		/// <returns></returns>
		public static bool UpdateMoney(int id, int money, IDbTransaction trans = null)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "update Player set Money=@Money where id=@Id;";
				var flag = Conn.Execute(sql, new { Id = id, Money = money }, trans) > 0;
				return flag;
			}
		}

		/// <summary>
		/// 更新玩家名称，并所花费金币
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		/// <param name="money"></param>
		/// <returns></returns>
		public static bool UpdatePlayerName(int id, string name, int money)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				IDbTransaction trans = Conn.BeginTransaction();
				var flag = UpdateMoney(id, money, trans);
				if (flag)
				{
					flag = UpdateName(id, name, trans);
					if (flag)
					{
						trans.Commit();
					}
					else
					{
						trans.Rollback();
					}
				}
				else
				{
					trans.Rollback();
				}
				return flag;
			}
		}
	}
}
