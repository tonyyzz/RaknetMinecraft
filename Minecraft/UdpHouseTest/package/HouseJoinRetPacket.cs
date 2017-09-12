using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class HouseJoinRetPacket : Package
	{
		public HouseJoinRetPacket() { }
		public HouseJoinRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseJoinRetPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_join_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int flag = ReadInt();
			int udpPort = ReadInt();
			ThreadPool.QueueUserWorkItem(o =>
			{
				if (CommonForm.Obj.playerInfoForm.IsHandleCreated)
					CommonForm.Obj.playerInfoForm.BeginInvoke(new Action<int, int>(CommonForm.Obj.playerInfoForm.HouseJoin), new object[] { flag, udpPort });
			});
		}
	}
}
