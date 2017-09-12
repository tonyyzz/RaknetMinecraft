using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class CommentListRetPacket : Package
	{
		public CommentListRetPacket() { }
		public CommentListRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new CommentListRetPacket(null, 0,
				MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_list_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolCommentList.UpdateEndTime(key);
		}
	}
}
