using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class FriendSearchAddListRetPacket : Package
	{
		public FriendSearchAddListRetPacket() { }
		public FriendSearchAddListRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendSearchAddListRetPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_searchAdd_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolFriendSearchAdd.UpdateEndTime(key);
		}
	}
}
