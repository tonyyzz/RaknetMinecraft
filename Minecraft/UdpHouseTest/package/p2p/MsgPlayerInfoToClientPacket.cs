using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgPlayerInfoToClientPacket : Package
	{
		public MsgPlayerInfoToClientPacket() { }
		public MsgPlayerInfoToClientPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgPlayerInfoToClientPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_msgPlayerInfoToClient);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			long houseId = ReadLong();
			int count = ReadInt();

			List<Model.PlayerModel> pList = new List<Model.PlayerModel>();
			for (int i = 0; i < count; i++)
			{
				int playerId = ReadInt();
				string playerName = ReadString();
				pList.Add(new Model.PlayerModel() { Id = playerId, Name = playerName });
			}
			if (CommonForm.Obj.chatForm.IsHandleCreated)
				CommonForm.Obj.chatForm.BeginInvoke(new Action<int, long, List<Model.PlayerModel>>(CommonForm.Obj.chatForm.ShowPlayerInfoList), new object[] { PlayerMng.player.Id, houseId, pList });
		}
	}
}
