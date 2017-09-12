using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	/// <summary>
	/// 设置端口的返回
	/// </summary>
	class HousePortSetRetPacket : Package
	{
		public HousePortSetRetPacket() { }
		public HousePortSetRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HousePortSetRetPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_portSet_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			byte flag = ReadByte();
			if (flag == 1)
			{
				//刷新house列表
				GetInfoCommon.ShowHouseList();
			}
		}
	}
}
