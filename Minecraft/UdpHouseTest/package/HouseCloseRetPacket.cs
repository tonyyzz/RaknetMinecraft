using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class HouseCloseRetPacket : Package
	{
		public HouseCloseRetPacket() { }
		public HouseCloseRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseCloseRetPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_close_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			byte isServer = ReadByte(); //是否是房主的房间

			if (CommonForm.Obj.chatForm.IsHandleCreated)
				CommonForm.Obj.chatForm.BeginInvoke(new Action<byte>(CommonForm.Obj.chatForm.HouseClose), new object[] { isServer });
		}
	}
}
