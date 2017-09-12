using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class PlayerDetailRetPacket : Package
	{
		public PlayerDetailRetPacket() { }
		public PlayerDetailRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerDetailRetPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerDetail_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolPlayerDetail.UpdateEndTime(key);
		}
	}
}
