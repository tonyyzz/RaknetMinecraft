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
	/// 资源评论操作接口
	/// </summary>
	class CommentOperatePacket : Package
	{
		public CommentOperatePacket() { }
		public CommentOperatePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new CommentOperatePacket(null, 0,
				MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_operate);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int resourceId = ReadInt(); //资源Id
			string comment = ReadString(); //评论内容
			if (resourceId <= 0)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(comment))
			{
				return;
			}
			comment = comment.GetRemoveExcessSpaceStr();
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			if (comment.Length < 5 || comment.Length > 20)
			{
				Package pack2 = new Package(MainCommand.MC_ERROR, SecondCommand.SC_ERROR_hall);
				pack2.Write((int)Error.comment_outofLength);
				player.SendMsg(pack2);
				return;
			}
			Model.ResourceCommentModel commentModel = new Model.ResourceCommentModel
			{
				Content = comment,
				ContentTime = DateTime.Now,
				ResourceId = resourceId,
				PlayerId = player.Id,
			};
			var keyId = BLL.BaseBLL.Insert(commentModel);
			Package pack = new Package(MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_operate_ret);
			pack.Write(key);
			pack.Write(keyId);
			player.SendMsg(pack);
		}
	}
}
