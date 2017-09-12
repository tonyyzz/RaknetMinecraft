using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 资源评分接口
	/// </summary>
	class ResourceScorePacket : Package
	{
		public ResourceScorePacket() { }
		public ResourceScorePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceScorePacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_score);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int resourceId = ReadInt(); //资源Id
			int score = ReadInt(); //资源分数（分数必须为 1-5 的整数）
			if (resourceId <= 0)
			{
				return;
			}
			if (score < 1 || score > 5)
			{
				return;
			}
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			lock (LockConfig.lock_resourceScore)
			{
				var flag = BLL.ResourceBLL.UpdateScore(resourceId, score);

				Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_score_ret);
				pack.Write(key);
				pack.Write((byte)(flag ? 1 : 0));
				if (flag)
				{
					var resourceModel = BLL.ResourceBLL.GetResourceDetail(resourceId);
					//计算评分
					double scoreNew = 0;
					if (resourceModel.ScoreNum > 0)
					{
						scoreNew = resourceModel.ScoreTotal * 1.0 / resourceModel.ScoreNum;
						if (scoreNew > 5)
						{
							scoreNew = 5;
						}
						else if (scoreNew < 0)
						{
							scoreNew = 0;
						}
					}
					pack.Write(scoreNew.ToString("0.0")); //评分
				}
				player.SendMsg(pack);
			}
		}
	}
}
