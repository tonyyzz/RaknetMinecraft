using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 获取资源标签列表
	/// </summary>
	class ResourceTagListPacket : Package
	{
		public ResourceTagListPacket() { }
		public ResourceTagListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceTagListPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_tagList);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int resourceTypeId = ReadInt();//资源类型Id

			if (resourceTypeId <= 0)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			var list = BLL.ResourceTagBLL.GetResourceTagsByType(resourceTypeId);
			Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_tagList_ret);
			pack.Write(key);
			pack.Write(list.Count());
			list.ForEach(item =>
			{
				pack.Write(item.Id);
				pack.Write(item.Name);
			});
			player.SendMsg(pack);
		}
	}
}
