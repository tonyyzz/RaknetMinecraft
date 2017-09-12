using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class ResourceScoreRetPacket : Package
	{
		public ResourceScoreRetPacket() { }
		public ResourceScoreRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceScoreRetPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_score_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolResourceScore.UpdateEndTime(key);
		}
	}
}
