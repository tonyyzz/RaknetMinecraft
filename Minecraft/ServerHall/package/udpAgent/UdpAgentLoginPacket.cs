using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	class UdpAgentLoginPacket : Package
	{
		public UdpAgentLoginPacket() { }
		public UdpAgentLoginPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new UdpAgentLoginPacket(null, 0,
				MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_login);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int playerId = ReadInt();

			Model.PlayerModel player = new Model.PlayerModel()
			{
				Id = playerId,
				conID = session.ConnId,
				IpAddress = session.IpAddress,
				UdpPort = session.Port,
			};

			session.player = player;
			PlayerAgentManager.onlineAgentPlayerList.RemoveAll(m => m.IpAddress == session.IpAddress && m.UdpPort == session.Port);
			PlayerAgentManager.onlineAgentPlayerList.Add(player);
		}
	}
}
