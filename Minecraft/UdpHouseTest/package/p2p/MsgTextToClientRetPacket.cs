using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgTextToClientRetPacket : Package
	{
		public MsgTextToClientRetPacket() { }
		public MsgTextToClientRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgTextToClientRetPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToClientRet);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			//记录udpServer发送消息给客户端成功后的时间
			ThreadPoolSendMsgQueue.UpdateEndTime(MainCommand.MC_P2P, SecondCommand.SC_P2P_msgTextToClientRet);
		}
	}
}
