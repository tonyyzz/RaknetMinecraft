using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class FriendBLL
	{
		/// <summary>
		/// 获取某个玩家的朋友列表
		/// </summary>
		/// <param name="playerId"></param>
		public static List<Model.FriendSimpleModel> GetFriendList(int playerId)
		{
			return DAL.FriendDAL.GetFriendList(playerId);
		}

		/// <summary>
		/// 搜索添加好友列表信息（已经成为好友的除外）
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static List<Model.FriendSimpleModel> GetFriendSearchAddExceptFriendList(int playerId, int friendId, string friendName)
		{
			return DAL.FriendDAL.GetFriendSearchAddExceptFriendList(playerId, friendId, friendName);
		}

		/// <summary>
		/// 搜索添加好友列表的第一条信息（包括已经成为好友）
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public static Model.FriendSimpleModel GetFriendSearchAddIncludeFriendTop1List(int playerId, int friendId, string friendName)
		{
			return DAL.FriendDAL.GetFriendSearchAddIncludeFriendTop1List(playerId, friendId, friendName);
		}

		/// <summary>
		/// 获取好友详细信息
		/// </summary>
		/// <param name="id">玩家Id</param>
		public static Model.PlayerModel GetFriendDetail(int playerId, int friendId)
		{
			return DAL.FriendDAL.GetFriendDetail(playerId, friendId);
		}

		/// <summary>
		/// 查看玩家与朋友是否是好友关系
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <returns></returns>
		public static bool IsFriendShip(int playerId, int friendId)
		{
			return DAL.FriendDAL.IsFriendShip(playerId, friendId);
		}

		/// <summary>
		/// 删除好友关系
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendId"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		public static bool Delete(int playerId, int friendId)
		{
			return DAL.FriendDAL.Delete(playerId, friendId);
		}

		/// <summary>
		/// 朋友推荐列表
		/// </summary>
		/// <param name="playerId">该玩家Id</param>
		public static List<Model.FriendSimpleModel> GetRecommendList(int playerId, ref Model.PageInfo pageInfo)
		{
			return DAL.FriendDAL.GetRecommendList(playerId, ref pageInfo);
		}
	}
}
