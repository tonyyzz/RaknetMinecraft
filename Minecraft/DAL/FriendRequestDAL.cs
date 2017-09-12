using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace DAL
{
	public class FriendRequestDAL : BaseDAL
	{
		/// <summary>
		/// 获取最近一次 玩家和某个好友 申请添加好友的记录（任意状态）
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <returns></returns>
		public static Model.FriendRequestModel GetFriendReqRecordByLastTime(int playerId, int friendId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select * from friendrequest where playerId={0} and friendId={1} 
				ORDER BY requestTime desc limit 1;",
				playerId, friendId);
				return Conn.QueryFirstOrDefault<Model.FriendRequestModel>(sql);
			}
			
		}

		/// <summary>
		/// 获取最近一次 玩家和某个好友 申请添加好友的记录（只是在申请中，还未接受或者拒绝）
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <returns></returns>
		public static Model.FriendRequestModel GetFriendRequestingRcdByLastTime(int playerId, int friendId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select * from friendrequest where playerId={0} and friendId={1} and requestState=0
				ORDER BY requestTime desc limit 1;",
				playerId, friendId);
				return Conn.QueryFirstOrDefault<Model.FriendRequestModel>(sql);
			}
			
		}

		/// <summary>
		/// 更新好友申请时间（必须是还在申请状态中的）
		/// </summary>
		/// <returns></returns>
		public static bool UpdateReqTime(int friendReqId, DateTime newReqTime)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"update friendrequest set RequestTime = @RequestTime 
				where id=@friendReqId;");
				return Conn.Execute(sql, new { friendReqId = friendReqId, RequestTime = newReqTime }) > 0;

			}
		}
		/// <summary>
		/// 更新申请状态
		/// </summary>
		/// <param name="friendReqId"></param>
		/// <param name="requestState"></param>
		/// <returns></returns>
		public static bool UpdateReqState(int friendReqId, int requestState, IDbTransaction trans = null)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"update friendrequest set requestState = @requestState 
				where id=@friendReqId;");
				return Conn.Execute(sql, new { friendReqId = friendReqId, requestState = requestState }, trans) > 0;

			}
		}

		/// <summary>
		/// 更新申请状态为接受，并添加好友数据
		/// </summary>
		/// <param name="friendRequestModel"></param>
		public static bool UpdateReqStateAndAddFriend(Model.FriendRequestModel friendRequestModel)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				IDbTransaction trans = Conn.BeginTransaction();
				var flag = false;
				flag = UpdateReqState(friendRequestModel.Id, friendRequestModel.RequestState, trans);
				if (flag)
				{
					flag = FriendDAL.Delete(friendRequestModel.PlayerId, friendRequestModel.FriendId, trans);
					flag = FriendDAL.Add(friendRequestModel.PlayerId, friendRequestModel.FriendId, DateTime.Now, trans);
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
		/// <summary>
		/// 获取玩家的好友申请记录
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static List<Model.FriendSimpleModel> GetFriendReqList(int playerId)
		{
			using (var Conn = GetConn())
			{
				Conn.Open();
				string sql = string.Format(@"select p.id as FriendId, p.Name as FriendName,p.HeadImg as FriendHeadImg,p.`Level` as FriendLevel,T.RequestState,T.RequestTime from player p 
					inner join (select playerId,RequestState,RequestTime from friendrequest where id in
						(select max(Id) from friendrequest where FriendId=@playerId GROUP BY PlayerId ORDER BY RequestTime desc)) as T 
					on p.id=T.playerId order by T.RequestTime desc;");
				return Conn.Query<Model.FriendSimpleModel>(sql, new { playerId = playerId }).ToList();
			}
		}
	}
}
