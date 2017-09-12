using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 其他玩家加入房间
	/// </summary>
	class HouseJoinPacket : Package
	{
		public HouseJoinPacket() { }
		public HouseJoinPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseJoinPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_join);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			long houseId = ReadLong(); //房间Id（不允许加入自己的房间，即如果房间Id是自己创建的，不允许调用这个接口）

			if (houseId <= 0)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			var flag = 0;
			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_join_ret);

			var houseInfo = PlayerHouseManager.GetPlayerHouseInfo(houseId);
			
			if (houseInfo == null)
			{
				flag = 4; //房间不存在
				pack.Write(flag);
				pack.Write(1);
			}
			else
			{
				if (houseInfo.HouseOwnerId == player.Id)
				{
					flag = 3;  //玩家不许加入自己的房间
				}
				else
				{
					if (houseInfo.HouseSize <= houseInfo.playerList.Count())
					{
						flag = 2; //房间满员
					}
					else
					{
						flag = 1; //可以加入房间
						player.HouseId = houseId;

						var onlineP = PlayerManager.playerOnlineList.FirstOrDefault(m => m.Id == player.Id);
						if (onlineP != null)
						{
							onlineP.HouseId = player.HouseId;
						}

						houseInfo.playerList.Add(player);
					}
				}
				pack.Write(flag);
				pack.Write((int)houseInfo.HouseUdpPort);
			}
			player.SendMsg(pack);
		}
	}
}
