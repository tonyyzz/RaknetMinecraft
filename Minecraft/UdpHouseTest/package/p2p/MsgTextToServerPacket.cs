using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgTextToServerPacket : Package
	{
		public MsgTextToServerPacket() { }
		public MsgTextToServerPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgTextToServerPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToServer);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			string playerName = ReadString(); 
			string txtMsg = ReadString();
			if (CommonForm.Obj.chatForm.IsHandleCreated)
				CommonForm.Obj.chatForm.BeginInvoke(new Action<string,string>(CommonForm.Obj.chatForm.AppendMsg), new object[] { playerName, txtMsg });
			Package pack = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToClient);
			pack.Write(playerName);
			pack.Write(txtMsg);
			UdpServerManager.Instance.SendAllClient(pack);
		}
	}
}
