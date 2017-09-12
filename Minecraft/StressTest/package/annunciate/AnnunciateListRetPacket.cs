using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class AnnunciateListRetPacket : Package
	{
		public AnnunciateListRetPacket() { }
		public AnnunciateListRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AnnunciateListRetPacket(null, 0,
				MainCommand.MC_ANNUNCIATE, SecondCommand.SC_ANNUNCIATE_list_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolAnnunciateList.UpdateEndTime(key);
		}
	}
}
