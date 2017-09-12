using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	/// <summary>
	/// 玩家房间管理类
	/// </summary>
	public static class PlayerHouseManager
	{
		/// <summary>
		/// 玩家房间列表
		/// </summary>
		private volatile static List<Model.PlayerHouseModel> playerHouseList = new List<Model.PlayerHouseModel>();

		/// <summary>
		/// 随机自增种子
		/// </summary>
		private volatile static int seedInt = 0;

		/// <summary>
		/// 获取所有房间列表（按照创建时间倒序）
		/// </summary>
		/// <param name="func">筛选条件</param>
		/// <returns></returns>
		public static List<Model.PlayerHouseModel> GetPlayerHouseList(Func<Model.PlayerHouseModel, bool> func = null)
		{
			if (func != null)
			{
				return playerHouseList.Where(func).OrderByDescending(m => m.CreateTime).ToList();
			}
			else
			{
				return playerHouseList.OrderByDescending(m => m.CreateTime).ToList();
			}
		}
		/// <summary>
		/// 获取某个房间的信息
		/// </summary>
		/// <param name="houseId"></param>
		/// <returns></returns>
		public static Model.PlayerHouseModel GetPlayerHouseInfo(long houseId)
		{
			if (houseId <= 0)
			{
				return null;
			}
			return playerHouseList.FirstOrDefault(m => m.Id == houseId);
		}
		/// <summary>
		/// 创建房间
		/// </summary>
		/// <param name="player">玩家实例</param>
		/// <param name="houseName">房间名称</param>
		/// <param name="description">房间描述</param>
		/// <param name="housePwd">房间密码</param>
		/// <param name="tagId">地图标签Id</param>
		/// <param name="tagName">地图标签名称</param>
		/// <param name="houseSize">房间大小</param>
		public static Model.PlayerHouseModel CreateHouse(this Model.PlayerModel player
			, string houseName, string description, string housePwd, int tagId, string tagName, int houseSize, int resourceId)
		{
			if (playerHouseList.Any(m => m.HouseOwnerId == player.Id)) //一个玩家最多只能创建一个房间
			{
				return null; //创建失败
			}

			
			int realSeedInt = DateHelper.GetTotalSecondsInt() + (seedInt++);
			Random random = new Random(realSeedInt);
			if (realSeedInt >= int.MaxValue - 1)
			{
				seedInt = 0;
			}
			long houseId = 0;
			do
			{
				//房间号组成：玩家Id + 8位的随机数  的整数
				int rn = Convert.ToInt32(Math.Pow(10, 8));
				long rNumber = random.Next(rn / 10, rn - 1);
				long.TryParse(string.Join("", player.Id, rNumber), out houseId);
			}
			while (playerHouseList.Any(m => m.Id == houseId));
			player.HouseId = houseId;

			var onlineP = PlayerManager.playerOnlineList.FirstOrDefault(m => m.Id == player.Id);
			if (onlineP != null)
			{
				onlineP.HouseId = player.HouseId;
			}

			Model.PlayerHouseModel playerHouseModel = new Model.PlayerHouseModel
			{
				Id = houseId, //随机产生唯一值
				Name = houseName,
				Description = description,
				HousePwd = housePwd,
				TagId = tagId,
				TagName = tagName,
				ThumbnailImg = "",
				HouseSize = houseSize,
				ResourceId = resourceId,
				HouseOwnerId = player.Id,
				HouseOwnerName = player.Name,
				HouseIpAddress = player.IpAddress,
				HouseTcpPort = player.TcpPort,
				CreateTime = DateTime.Now,
				playerList = new List<Model.PlayerModel>() { player }
			};
			playerHouseList.Add(playerHouseModel);
			return playerHouseModel;
		}

		/// <summary>
		/// 退出房间
		/// </summary>
		/// <param name="player">玩家</param>
		/// <param name="houseId">房间号</param>
		public static void WithdrawFromHouse(this Model.PlayerModel player)
		{
			if (player.HouseId <= 0)
			{
				return;
			}
			var house = playerHouseList.FirstOrDefault(m => m.Id == player.HouseId);
			if (house != null)
			{
				if (house.HouseOwnerId != player.Id) //不是房主
				{
					house.playerList.RemoveAll(m => m.Id == player.Id);
					player.HouseId = 0;
				}
				else //是房主
				{
					playerHouseList.RemoveAll(m => m.Id == player.HouseId);
				}
			}
		}
	}
}
