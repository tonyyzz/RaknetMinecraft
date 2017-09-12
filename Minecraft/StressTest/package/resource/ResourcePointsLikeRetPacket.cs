using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	class ResourcePointsLikeRetPacket : Package
	{
		public ResourcePointsLikeRetPacket() { }
		public ResourcePointsLikeRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourcePointsLikeRetPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_pointsLike_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolResourcePointsLike.UpdateEndTime(key);
		}
	}
}
