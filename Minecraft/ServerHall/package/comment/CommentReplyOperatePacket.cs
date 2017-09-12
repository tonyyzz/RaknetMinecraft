using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 评论回复操作接口
	/// </summary>
	class CommentReplyOperatePacket : Package
	{
		public CommentReplyOperatePacket() { }
		public CommentReplyOperatePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new CommentReplyOperatePacket(null, 0,
				MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_replyOperate);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int commentId = ReadInt(); //评论Id
			string replyStr = ReadString(); //评论回复内容
			if (commentId <= 0)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(replyStr))
			{
				return;
			}
			replyStr = replyStr.GetRemoveExcessSpaceStr();
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			if (replyStr.Length > 100)
			{
				Package pack2 = new Package(MainCommand.MC_ERROR, SecondCommand.SC_ERROR_hall);
				pack2.Write((int)Error.reply_outofLength);
				player.SendMsg(pack2);
				return;
			}
			Model.ResourceCommentReplyModel replyModel = new Model.ResourceCommentReplyModel
			{
				Reply = replyStr,
				ReplyTime = DateTime.Now,
				CommentId = commentId,
				ParentId = 0,
				ReplyPersonId = player.Id
			};
			var keyId = BLL.BaseBLL.Insert(replyModel);
			Package pack = new Package(MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_replyOperate_ret);
			pack.Write(key);
			pack.Write(keyId);
			player.SendMsg(pack);
		}
	}
}
