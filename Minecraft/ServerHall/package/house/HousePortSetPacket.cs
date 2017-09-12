using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 设置房间端口号
	/// </summary>
	class HousePortSetPacket : Package
	{
		public HousePortSetPacket() { }
		public HousePortSetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HousePortSetPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_portSet);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int port = ReadUShort(); //房间端口

			if (port < 1024 || port > 65535)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			var houseInfo = PlayerHouseManager.GetPlayerHouseList(house => house.HouseOwnerId == player.Id).FirstOrDefault();
			if (houseInfo == null)
			{
				return;
			}
			houseInfo.HouseUdpPort = (ushort)port;
			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_portSet_ret);
			pack.Write((byte)1);
			player.SendMsg(pack);
		}
	}
}
