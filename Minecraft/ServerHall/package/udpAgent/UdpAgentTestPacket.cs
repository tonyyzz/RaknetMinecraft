using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	class UdpAgentTestPacket : Package
	{
		public UdpAgentTestPacket() { }
		public UdpAgentTestPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new UdpAgentTestPacket(null, 0,
				MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_test);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			string playerName = ReadString();
			string txtMsg = ReadString();



			//发送消息者的Id
			int sendPlayerId = ReadInt();

			long houseId = 0;
			var onlineP = PlayerManager.playerOnlineList.FirstOrDefault(m => m.Id == sendPlayerId);
			if (onlineP == null)
			{
				return;

			}
			houseId = onlineP.HouseId;
			if (houseId <= 0)
			{
				return;
			}
			//找到房主Id
			var house = PlayerHouseManager.GetPlayerHouseList(m => m.Id == houseId).FirstOrDefault();
			if (house == null)
			{
				return;
			}
			var ownerId = house.HouseOwnerId;
			//将消息发送给房主
			var p = PlayerAgentManager.onlineAgentPlayerList.FirstOrDefault(m => m.Id == ownerId);
			if (p == null)
			{
				return;
			}
			Package pack = new Package(MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_testToUdpServer);
			pack.Write(playerName);
			pack.Write(txtMsg);
			UdpAgentServerManager.Instance.Send(p.conID, pack);
		}
	}
}
