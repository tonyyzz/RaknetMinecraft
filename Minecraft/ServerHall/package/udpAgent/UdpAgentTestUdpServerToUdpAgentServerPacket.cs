using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	class UdpAgentTestUdpServerToUdpAgentServerPacket : Package
	{
		public UdpAgentTestUdpServerToUdpAgentServerPacket() { }
		public UdpAgentTestUdpServerToUdpAgentServerPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new UdpAgentTestUdpServerToUdpAgentServerPacket(null, 0,
				MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_testUdpServerToUdpAgentServer);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			string playerName = ReadString();
			string txtMsg = ReadString();

			//房主
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null)
			{
				return;
			}

			var house = PlayerHouseManager.GetPlayerHouseList(m => m.HouseOwnerId == player.Id).FirstOrDefault();

			if (house == null)
			{
				return;
			}
			var playerIdLi = house.playerList.Select(m => m.Id).ToList();
			playerIdLi.RemoveAll(m => m == house.HouseOwnerId);
			if (!playerIdLi.Any())
			{
				return;
			}
			Package pack = new Package(MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_testUdpAgentServerToUdpClient);
			pack.Write(playerName);
			pack.Write(txtMsg);
			UdpAgentServerManager.Instance.SendToPlayers(playerIdLi, pack);
		}
	}
}
