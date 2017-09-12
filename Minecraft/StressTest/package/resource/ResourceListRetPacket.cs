using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class ResourceListRetPacket : Package
	{
		public ResourceListRetPacket() { }
		public ResourceListRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceListRetPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_list_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolResourceList.UpdateEndTime(key);
		}
	}
}
