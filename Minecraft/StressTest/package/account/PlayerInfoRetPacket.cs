using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class PlayerInfoRetPacket : Package
	{
		public PlayerInfoRetPacket() { }
		public PlayerInfoRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerInfoRetPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerInfo_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolPlayerInfo.UpdateEndTime(key);
		}
	}
}
