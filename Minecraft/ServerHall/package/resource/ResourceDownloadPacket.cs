using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 资源下载接口
	/// </summary>
	class ResourceDownloadPacket : Package
	{
		public ResourceDownloadPacket() { }
		public ResourceDownloadPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceDownloadPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_download);
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

			lock (LockConfig.lock_resourceDownload)
			{
				var flag = BLL.ResourceBLL.UpdateDownloadNumPlusOne(resourceId);
				Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_download_ret);
				pack.Write(key);
				pack.Write((byte)(flag ? 1 : 0));
				player.SendMsg(pack);
			}
		}
	}
}
