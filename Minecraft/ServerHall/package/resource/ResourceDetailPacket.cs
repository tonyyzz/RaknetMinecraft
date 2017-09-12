using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 资源详情接口
	/// </summary>
	class ResourceDetailPacket : Package
	{
		public ResourceDetailPacket() { }
		public ResourceDetailPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceDetailPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_detail);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int resourceId = ReadInt();//资源Id
			if (resourceId <= 0)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			var model = BLL.ResourceBLL.GetResourceDetail(resourceId);
			if (model == null)
			{
				Console.WriteLine("Id为【{0}】的资源不存在，访问时间为【{1}】", resourceId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				return;
			}
			//计算评分
			double score = 0;
			if (model.ScoreNum > 0)
			{
				score = model.ScoreTotal * 1.0 / model.ScoreNum;
				if (score > 5)
				{
					score = 5;
				}
				else if (score < 0)
				{
					score = 0;
				}
			}

			Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_detail_ret);
			pack.Write(key);
			pack.Write(model.Id);
			pack.Write(model.Title);
			pack.Write(model.Description); //资源介绍
			pack.Write(model.ThumbnailImg);
			pack.Write(model.DownloadNum); //下载量
			pack.Write(score.ToString("0.0")); //评分
			pack.Write(model.FileSizeKb); //以KB为单位
			pack.Write(model.PublishTime.Ticks); //发布日期
			pack.Write((byte)(model.IsOfficial ? 1 : 0)); //是否是官方制作
			if (!model.IsOfficial) //如果是玩家上传的资源
			{
				var author = BLL.PlayerBLL.QuerySingle(model.PlayerId); //资源的作者
				if (author != null) //如果该作者存在
				{
					pack.Write((byte)1);
					pack.Write(author.Name); //玩家昵称
					pack.Write(author.HeadImg); //玩家头像
				}
				else
				{
					pack.Write((byte)0);
				}
			}
			player.SendMsg(pack);
		}
	}
}
