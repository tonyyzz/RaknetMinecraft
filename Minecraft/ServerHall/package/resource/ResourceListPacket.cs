using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 资源列表接口
	/// </summary>
	class ResourceListPacket : Package
	{
		public ResourceListPacket() { }
		public ResourceListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceListPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_list);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int resourceTypeId = ReadInt();//资源类型Id
			string classIdStrs = ReadString(); //【推荐：1|流行：2|关注：3】ID字符串集合，传值格式如：“1|2|3”，“1”，“2”，“3”
			int resourceTagId = ReadInt();//资源标签Id（传0表示全部）
			int pageIndex = ReadInt(); //页码从1开始
			int pageSize = ReadInt(); //每页数量（客户端自己控制）

			if (resourceTypeId <= 0)
			{
				return;
			}
			if (resourceTagId <= -1)
			{
				return;
			}
			if (pageIndex < 1 || pageSize < 1)
			{
				return;
			}

			var classIdList = classIdStrs.Split('|').ToList().ConvertAll(m =>
			{
				int id = 0; int.TryParse(m, out id); return id;
			})
			.Where(m => m == 1 || m == 2 || m == 3).ToList();
			if (!classIdList.Any())
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_list_ret);
			pack.Write(key);
			pack.Write(string.Join("|", classIdList.ToArray()));
			if (classIdList.Any(m => m == 1)) //推荐
			{
				var pageInfo = new Model.PageInfo(pageIndex, pageSize);
				var recommendList = BLL.ResourceBLL.GetRecommendList(resourceTypeId, resourceTagId, ref pageInfo);
				pack.Write(recommendList.Count);
				recommendList.ForEach(item =>
				{
					pack.Write(item);
				});
				pack.Write(pageInfo.PageCount); //总页数
			}
			if (classIdList.Any(m => m == 2)) //流行
			{
				var pageInfo = new Model.PageInfo(pageIndex, pageSize);
				var popularList = BLL.ResourceBLL.GetPopularList(resourceTypeId, resourceTagId, ref pageInfo);
				pack.Write(popularList.Count);
				popularList.ForEach(item =>
				{
					pack.Write(item);
				});
				pack.Write(pageInfo.PageCount); //总页数
			}
			if (classIdList.Any(m => m == 3)) //关注
			{
				var pageInfo = new Model.PageInfo(pageIndex, pageSize);
				var attentionList = BLL.ResourceBLL.GetAttentionList(player.Id, resourceTypeId, resourceTagId, ref pageInfo);
				pack.Write(attentionList.Count);
				attentionList.ForEach(item =>
				{
					pack.Write(item);
				});
				pack.Write(pageInfo.PageCount); //总页数
			}
			player.SendMsg(pack);
		}
	}
}
