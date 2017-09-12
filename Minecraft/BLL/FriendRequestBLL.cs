using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class FriendRequestBLL : BaseBLL
	{
		/// <summary>
		/// 获取最近一次 玩家和某个好友 申请添加好友的记录（任意状态）
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <returns></returns>
		public static Model.FriendRequestModel GetFriendReqRecordByLastTime(int playerId, int friendId)
		{
			return DAL.FriendRequestDAL.GetFriendReqRecordByLastTime(playerId, friendId);
		}

		/// <summary>
		/// 获取最近一次 玩家和某个好友 申请添加好友的记录（只是在申请中，还未接受或者拒绝）
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <returns></returns>
		public static Model.FriendRequestModel GetFriendRequestingRcdByLastTime(int playerId, int friendId)
		{
			return DAL.FriendRequestDAL.GetFriendRequestingRcdByLastTime(playerId, friendId);
		}

		/// <summary>
		/// 更新好友申请时间（必须是还在申请状态中的）
		/// </summary>
		/// <returns></returns>
		public static bool UpdateReqTime(int friendReqId, DateTime newReqTime)
		{
			return DAL.FriendRequestDAL.UpdateReqTime(friendReqId, newReqTime);
		}

		/// <summary>
		/// 更新申请状态
		/// </summary>
		/// <param name="friendReqId"></param>
		/// <param name="requestState"></param>
		/// <returns></returns>
		public static bool UpdateReqState(int friendReqId, int requestState)
		{
			return DAL.FriendRequestDAL.UpdateReqState(friendReqId, requestState);
		}
		/// <summary>
		/// 更新申请状态为接受，并添加好友数据
		/// </summary>
		/// <param name="friendRequestModel"></param>
		public static bool UpdateReqStateAndAddFriend(Model.FriendRequestModel friendRequestModel)
		{
			return DAL.FriendRequestDAL.UpdateReqStateAndAddFriend(friendRequestModel);
		}

		/// <summary>
		/// 获取玩家的好友申请记录
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static List<Model.FriendSimpleModel> GetFriendReqList(int playerId)
		{
			return DAL.FriendRequestDAL.GetFriendReqList(playerId);
		}
	}
}
