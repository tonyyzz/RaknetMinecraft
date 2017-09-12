using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class ResourceDownloadRetPacket : Package
	{
		public ResourceDownloadRetPacket() { }
		public ResourceDownloadRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceDownloadRetPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_download_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolResourceDownload.UpdateEndTime(key);
		}
	}
}
