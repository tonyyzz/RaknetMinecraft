using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class UdpAgentTestToUdpServerPacket : Package
	{
		public UdpAgentTestToUdpServerPacket() { }
		public UdpAgentTestToUdpServerPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new UdpAgentTestToUdpServerPacket(null, 0,
				MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_testToUdpServer);
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
			//将消息返回发给代理服务器，再通过代理服务器发给所在房间内的所有人
			Package pack = new Package(MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_testUdpServerToUdpAgentServer);
			pack.Write(playerName);
			pack.Write(txtMsg);
			UdpAgentClientManager.Instance?.Send(pack);
		}
	}
}
