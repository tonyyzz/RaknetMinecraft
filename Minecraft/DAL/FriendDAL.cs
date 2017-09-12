using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace DAL
{
	public class FriendDAL : BaseDAL
	{
		/// <summary>
		/// 获取某个玩家的朋友列表
		/// </summary>
		/// <param name="playerId"></param>
		public static List<Model.FriendSimpleModel> GetFriendList(int playerId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select id as FriendId, Name as FriendName,HeadImg as FriendHeadImg,`Level` as FriendLevel from player where id 
				in( select FriendId from friend where playerId = @playerId);");
				return Conn.Query<Model.FriendSimpleModel>(sql, new { playerId = playerId }).ToList();
			}

		}

		/// <summary>
		/// 搜索添加好友列表信息（已经成为好友的除外）
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static List<Model.FriendSimpleModel> GetFriendSearchAddExceptFriendList(int playerId, int friendId, string friendName)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@" select id as FriendId, Name as FriendName,HeadImg as FriendHeadImg,`Level` as FriendLevel from player
				where (id ={0} or name ='{1}') and id<>{2}
				and id not in (select FriendId from friend where friend.playerId={2});",
				friendId, friendName, playerId);
				return Conn.Query<Model.FriendSimpleModel>(sql).ToList();
			}

		}

		/// <summary>
		/// 搜索添加好友列表的第一条信息（包括已经成为好友）
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.FriendSimpleModel GetFriendSearchAddIncludeFriendTop1List(int playerId, int friendId, string friendName)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@" select id as FriendId, Name as FriendName,HeadImg as FriendHeadImg,`Level` as FriendLevel from player
				where (id ={0} or name ='{1}') and id<>{2} limit 0,1;",
				friendId, friendName, playerId);
				return Conn.QueryFirstOrDefault<Model.FriendSimpleModel>(sql);
			}

		}

		/// <summary>
		/// 获取好友详细信息
		/// </summary>
		/// <param name="playerId">本玩家Id</param>
		/// <param name="friendId">好友Id</param>
		/// <returns></returns>
		public static Model.PlayerModel GetFriendDetail(int playerId, int friendId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select *
				,(select COUNT(0) from playerattention where PlayerAttentionId=p.Id )as FansNum
				,(select COUNT(0) from playerattention where PlayerId=p.Id )as AttentionNum
				,(select count(0) from resource where PlayerId=p.id and IsOfficial=false and ResourceTypeId=1) as MapNum
				,(select count(0) from resource where PlayerId=p.id and IsOfficial=false and ResourceTypeId=2) as DrawSheetNum
				,(select count(0)>0 from friend where PlayerId=@PlayerId and FriendId=p.id) as IsFriendShip
				,(select count(0)>0 from playerattention where PlayerId=@PlayerId and playerattentionId=p.id) as IsAttention
				 from player p where p.id=@Id;");
				return Conn.QueryFirstOrDefault<Model.PlayerModel>(sql, new { PlayerId = playerId, Id = friendId });
			}
		}

		/// <summary>
		/// 删除好友关系
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		public static bool Delete(int playerId, int friendId, IDbTransaction trans = null)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = "delete from friend where (playerId=@playerId and friendId=@friendId) or (playerId=@friendId and friendId=@playerId);";
				return Conn.Execute(sql, new { playerId = playerId, friendId = friendId }, trans) > 0;
			}

		}
		/// <summary>
		/// 添加好友关系
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		public static bool Add(int playerId, int friendId, DateTime addTime, IDbTransaction trans = null)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = @"insert into friend(playerId,friendId,addTime) values(@playerId,@friendId,@addTime);
						   insert into friend(playerId,friendId,addTime) values(@friendId,@playerId,@addTime);";
				return Conn.Execute(sql, new { playerId = playerId, friendId = friendId, addTime = addTime }, trans) > 0;
			}

		}

		/// <summary>
		/// 查看玩家与朋友是否是好友关系
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <returns></returns>
		public static bool IsFriendShip(int playerId, int friendId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select count(0) from friend 
				where (playerId = @playerId and friendId = @friendId) 
				   or (PlayerId = @friendId and FriendId = @playerId);");
				return Conn.ExecuteScalar<int>(sql, new { playerId = playerId, friendId = friendId }) > 0;

			}
		}
		/// <summary>
		/// 朋友推荐列表
		/// </summary>
		/// <param name="playerId">该玩家Id</param>
		public static List<Model.FriendSimpleModel> GetRecommendList(int playerId, ref Model.PageInfo pageInfo)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select count(0) from player p 
					where p.Id<>@playerId 
					and p.id not in (select friendId from friend where playerId=@playerId);");
				pageInfo.TotalCount = Conn.ExecuteScalar<int>(sql, new { playerId = playerId });
				sql = string.Format(@"select id as FriendId, Name as FriendName,HeadImg as FriendHeadImg,`Level` as FriendLevel from player p 
					where p.Id<>@playerId
					and p.id not in (select friendId from friend where playerId=@playerId) 
					ORDER BY p.LastLoginTime DESC,FriendLevel desc,FriendId desc limit {0},{1};",
					pageInfo.PageSize * (pageInfo.PageIndex - 1), pageInfo.PageSize);
				return Conn.Query<Model.FriendSimpleModel>(sql, new { playerId = playerId }).ToList();
			}
		}
	}
}
