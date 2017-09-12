using BaseCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class PlayerBLL : BaseBLL
	{
		
		/// <summary>
		/// 获取某个玩家基本信息
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public static Model.PlayerModel QuerySingle(int Id)
		{
			return DAL.PlayerDAL.QuerySingle(Id);
		}
		/// <summary>
		/// 更新玩家最后登陆时间
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public static bool UpdateLastLoginTime(Model.PlayerModel player)
		{
			return DAL.PlayerDAL.UpdateLastLoginTime(player);
		}

		/// <summary>
		/// 玩家关注
		/// </summary>
		/// <param name="playerAttentionModel"></param>
		/// <returns></returns>
		public static bool AttentionPlayer(Model.PlayerAttentionModel playerAttentionModel)
		{
			return DAL.PlayerDAL.AttentionPlayer(playerAttentionModel);
		}




		/// <summary>
		/// 获取玩家详细信息
		/// </summary>
		/// <param name="id">玩家Id</param>
		public static Model.PlayerModel GetPlayerDetail(int id)
		{
			return DAL.PlayerDAL.GetPlayerDetail(id);
		}

		/// <summary>
		/// 更新性别
		/// </summary>
		/// <param name="id">玩家Id</param>
		/// <param name="sex">新性别</param>
		/// <returns></returns>
		public static bool UpdateSex(int id, int sex)
		{
			return DAL.PlayerDAL.UpdatePlayerSex(id, sex);
		}
		/// <summary>
		/// 更新model性别
		/// </summary>
		/// <param name="id"></param>
		/// <param name="modelSexInt"></param>
		/// <returns></returns>
		public static bool UpdateModelSex(int id, int modelSexInt)
		{
			return DAL.PlayerDAL.UpdateModelSex(id, modelSexInt);
		}
		/// <summary>
		/// 更新介绍
		/// </summary>
		/// <param name="id">玩家Id</param>
		/// <param name="description">新介绍</param>
		/// <returns></returns>
		public static bool UpdateDescription(int id, string description)
		{
			return DAL.PlayerDAL.UpdateDescription(id, description);
		}
		/// <summary>
		/// 更新名称
		/// </summary>
		/// <param name="id">玩家Id</param>
		/// <param name="description">新名称</param>
		/// <returns></returns>
		public static bool UpdateName(int id, string name)
		{
			return DAL.PlayerDAL.UpdateName(id, name);
		}
		/// <summary>
		/// 更新金币
		/// </summary>
		/// <param name="id"></param>
		/// <param name="money"></param>
		/// <returns></returns>
		public static bool UpdateMoney(int id, int money)
		{
			return DAL.PlayerDAL.UpdateMoney(id, money);
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
			return DAL.PlayerDAL.UpdatePlayerName(id, name, money);
		}
	}
}
