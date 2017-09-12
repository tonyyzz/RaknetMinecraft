using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class FriendRecommendRetPacket : Package
	{
		public FriendRecommendRetPacket() { }
		public FriendRecommendRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendRecommendRetPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_recommend_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolFriendRecommend.UpdateEndTime(key);
		}
	}
}
