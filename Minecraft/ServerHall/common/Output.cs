using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	public static class Output
	{
		/// <summary>
		/// 统一写入资源信息（适合资源列表信息）
		/// </summary>
		/// <param name="pack"></param>
		/// <param name="item"></param>
		public static void Write(this Package pack, Model.ResourceModel item)
		{
			//计算评分
			double score = 0;
			if (item.ScoreNum > 0)
			{
				score = item.ScoreTotal * 1.0 / item.ScoreNum;
				if (score > 5)
				{
					score = 5;
				}
				else if (score < 0)
				{
					score = 0;
				}
			}
			pack.Write(item.Id); //资源Id
			pack.Write(item.Title); //资源标题
			pack.Write(item.ThumbnailImg); //缩略图
			pack.Write(item.DownloadNum); //下载量
			pack.Write(score.ToString("0.0")); //评分
			pack.Write(item.resourceTagList.Count()); //标签数量
			item.resourceTagList.ForEach(m =>
			{
				pack.Write(m.Name); //标签名称
			});
		}
		/// <summary>
		/// 统一写入房间信息（适合房间列表信息）
		/// </summary>
		/// <param name="pack"></param>
		/// <param name="item"></param>
		public static void Write(this Package pack, Model.PlayerHouseModel item)
		{
			pack.Write(item.Id); //房间Id
			pack.Write(item.Name); //房间名称
			pack.Write(item.TagName); //标签名称
			pack.Write(item.ThumbnailImg); //缩略图

			pack.Write(item.HouseSize); //房间大小
			pack.Write(item.playerList.Count()); //目前该房间的人数
			pack.Write((int)item.HouseUdpPort);
		}

		/// <summary>
		/// 统一写入好友信息列表
		/// </summary>
		/// <param name="pack"></param>
		/// <param name="item"></param>
		/// <param name="isExtension">是否写入扩展参数值，默认为false，如果为true，则还向客户端传递申请好友记录相关的参数信息</param>
		public static void Write(this Package pack, Model.FriendSimpleModel item, bool isExtension = false)
		{
			pack.Write(item.FriendId);
			pack.Write(item.FriendName);
			pack.Write(item.FriendHeadImg);
			pack.Write(item.FriendLevel);
			pack.Write((byte)(item.isOnline ? 1 : 0));
			if (isExtension)
			{
				pack.Write(item.RequestState);
			}
		}

		/// <summary>
		/// 统一写入装扮信息
		/// </summary>
		/// <param name="pack"></param>
		/// <param name="item"></param>
		public static void Write(this Package pack, Model.PlayerDisguiseModel item)
		{
			pack.Write(item.Head);
			pack.Write(item.Coat);
			pack.Write(item.Pant);
		}
	}
}
