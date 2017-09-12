using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 对某个资源点赞操作
	/// </summary>
	class ResourcePointsLikePacket : Package
	{
		public ResourcePointsLikePacket() { }
		public ResourcePointsLikePacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourcePointsLikePacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_pointsLike);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			int resourceId = ReadInt(); //资源Id
			if (resourceId <= 0)
			{
				return;
			}
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			lock (LockConfig.lock_resourcePointLike)
			{
				var flag = BLL.ResourceBLL.UpdatePointLineNumPlusOne(resourceId);
				Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_pointsLike_ret);
				pack.Write(key);
				pack.Write((byte)(flag ? 1 : 0));
				player.SendMsg(pack);
			}
		}
	}
}
