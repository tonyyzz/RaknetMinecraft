using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class AvatarBackpackGoodsRetPacket : Package
	{
		public AvatarBackpackGoodsRetPacket() { }
		public AvatarBackpackGoodsRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AvatarBackpackGoodsRetPacket(null, 0,
				MainCommand.MC_AVATAR, SecondCommand.SC_AVATAR_backpackGoods_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolAvatarBackpackGoods.UpdateEndTime(key);
		}
	}
}
