using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class FriendChatRetPacket : Package
	{
		public FriendChatRetPacket() { }
		public FriendChatRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new FriendChatRetPacket(null, 0,
				MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_chat_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolFriendChat.UpdateEndTime(key);
		}
	}
}
