using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class UdpAgentTestUdpAgentServerToUdpClientPacket : Package
	{
		public UdpAgentTestUdpAgentServerToUdpClientPacket() { }
		public UdpAgentTestUdpAgentServerToUdpClientPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new UdpAgentTestUdpAgentServerToUdpClientPacket(null, 0,
				MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_testUdpAgentServerToUdpClient);
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
			//通知udp服务器是否发送成功
		}

	}
}
