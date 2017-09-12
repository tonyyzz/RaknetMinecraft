using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class AvatarDisguiseSetRetPacket : Package
	{
		public AvatarDisguiseSetRetPacket() { }
		public AvatarDisguiseSetRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AvatarDisguiseSetRetPacket(null, 0,
				MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_disguiseSet_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolAvatarDisguiseSet.UpdateEndTime(key);
		}
	}
}
