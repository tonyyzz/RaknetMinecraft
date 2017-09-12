using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgTextToClientPacket : Package
	{
		public MsgTextToClientPacket() { }
		public MsgTextToClientPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgTextToClientPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToClient);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			string playerName = ReadString();
			string txtMsg = ReadString();
			if (CommonForm.Obj.chatForm.IsHandleCreated)
				CommonForm.Obj.chatForm.BeginInvoke(new Action<string, string>(CommonForm.Obj.chatForm.AppendMsg), new object[] { playerName, txtMsg });

			Package pack = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToClientRet);
			UdpClientManager.Instance.Send(pack);

			//消息送达，记录消息送达时间
			ThreadPoolSendMsgQueue.UpdateEndTime(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToClient);
		}
	}
}
