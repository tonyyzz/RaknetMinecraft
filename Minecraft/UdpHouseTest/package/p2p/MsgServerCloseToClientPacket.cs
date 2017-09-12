using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgServerCloseToClientPacket : Package
	{
		public MsgServerCloseToClientPacket() { }
		public MsgServerCloseToClientPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgServerCloseToClientPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_msgServerCloseToClient);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			ThreadPool.QueueUserWorkItem(o =>
			{
				//关闭udp服务器前收到的通知
				if (CommonForm.Obj.chatForm.IsHandleCreated)
					CommonForm.Obj.chatForm.BeginInvoke(new Action<byte>(CommonForm.Obj.chatForm.HouseClose), new object[] { (byte)2 });
			});
		}
	}
}
