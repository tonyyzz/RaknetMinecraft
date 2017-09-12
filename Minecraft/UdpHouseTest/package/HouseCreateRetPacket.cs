using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class HouseCreateRetPacket : Package
	{
		public HouseCreateRetPacket() { }
		public HouseCreateRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseCreateRetPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_create_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			byte flag = ReadByte(); //创建成功或者失败
			if (flag == 1)
			{
				long houseId = ReadLong(); //房间Id
				PlayerMng.OwnerPlayerHouse.Id = houseId;
			}
			ThreadPool.QueueUserWorkItem(o =>
			{
				if (CommonForm.Obj.playerInfoForm.IsHandleCreated)
					CommonForm.Obj.playerInfoForm.BeginInvoke(new Action<byte>(CommonForm.Obj.playerInfoForm.CreateHouseSuccess), new object[] { flag });
			});
		}
	}
}
