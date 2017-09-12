using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgPlayerLeaveToServerPacket : Package
	{
		public MsgPlayerLeaveToServerPacket() { }
		public MsgPlayerLeaveToServerPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgPlayerLeaveToServerPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_msgPlayerLeaveToServer);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int playerId = ReadInt(); //要离开房间的玩家
			var playar = PlayerMng.playerIncomingList.FirstOrDefault(m => m.Id == playerId);
			if (playar != null)
			{
				UdpServerManager.Instance.RemoveSession(playar.conID);
			}
			PlayerMng.playerIncomingList.RemoveAll(m => m.Id == playerId);
		}
	}
}
