using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class AvatarChangeModelSexPacket : Package
	{
		public AvatarChangeModelSexPacket() { }
		public AvatarChangeModelSexPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AvatarChangeModelSexPacket(null, 0,
				MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_changeModelSex_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolAvatarChangeModelSex.UpdateEndTime(key);
		}
	}
}
