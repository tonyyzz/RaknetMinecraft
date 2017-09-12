using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class FriendRequestAddRetPacket : Package
	{
		public FriendRequestAddRetPacket() { }
		public FriendRequestAddRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendRequestAddRetPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestAdd_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolFriendRequestAdd.UpdateEndTime(key);
		}
	}
}
