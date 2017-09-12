using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgPlayerLoginUdpServerPacket : Package
	{
		public MsgPlayerLoginUdpServerPacket() { }
		public MsgPlayerLoginUdpServerPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgPlayerLoginUdpServerPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_msgPlayerLoginUdpServer);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int playerId = ReadInt();
			string playerName = ReadString();

			Model.PlayerModel player = new Model.PlayerModel()
			{
				Id = playerId,
				Name = playerName,
				conID = session.ConnId,
				IpAddress = session.IpAddress,
				UdpPort = session.Port,
			};
			session.player = player;
			PlayerMng.playerIncomingList.RemoveAll(m => m.IpAddress == session.IpAddress && m.UdpPort == session.Port);
			PlayerMng.playerIncomingList.Add(player);

			OwnerCommon.RefreshPlayerList();
		}
	}
}
