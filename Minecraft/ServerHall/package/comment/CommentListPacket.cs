using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 评论列表
	/// </summary>
	class CommentListPacket : Package
	{
		public CommentListPacket() { }
		public CommentListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new CommentListPacket(null, 0,
				MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_list);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int resourceId = ReadInt(); //资源Id
			int pageIndex = ReadInt(); //页码从1开始
			int pageSize = ReadInt(); //每页数量（客户端自己控制）

			if (resourceId <= 0)
			{
				return;
			}
			if (pageIndex < 1 || pageSize < 1)
			{
				return;
			}
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			var pageInfo = new Model.PageInfo(pageIndex, pageSize);
			var connentList = BLL.ResourceCommentBLL.GetConnentListByPage(resourceId, ref pageInfo);
			Package pack = new Package(MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_list_ret);
			pack.Write(key);
			pack.Write(connentList.Count());
			connentList.ForEach(item =>
			{
				pack.Write(item.Id); //评论Id
				pack.Write(item.Content); //评论内容
				pack.Write(item.ContentTime.Ticks); //评论时间
				pack.Write(item.PlayerName); //评论者名称
				pack.Write(item.PlayerHeadImg); //评论者头像
				pack.Write(item.PlayerLevel); //评论者等级
				pack.Write(item.resourceCommentReplyList.Count());
				item.resourceCommentReplyList.ForEach(m =>
				{
					//作者回复
					pack.Write(m.Reply); //回复内容
					pack.Write(m.ReplyTime.Ticks); //回复时间
				});
			});
			pack.Write(pageInfo.PageCount); //总页数
			player.SendMsg(pack);
		}
	}
}
