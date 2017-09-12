using BaseCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 资源上传接口
	/// </summary>
	class ResourceUploadPacket : Package
	{
		public ResourceUploadPacket() { }
		public ResourceUploadPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new ResourceUploadPacket(null, 0,
				MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_upload);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			string title = ReadString(); //资源标题
			string description = ReadString(); //资源介绍
			int resourceTypeId = ReadInt(); //资源类型
			string resourceTags = ReadString(); //资源标签字符串集合（如果有多个，传过来时请用‘|’分割）
												
			//byte[] buffer = ReadByteArray(); //资源文件二进制流

			if (string.IsNullOrWhiteSpace(title))
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(resourceTags))
			{
				return;
			}
			if (resourceTypeId <= 0)
			{
				return;
			}

			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}

			title = title.GetRemoveExcessSpaceStr();
			description = description.GetRemoveExcessSpaceStr();
			resourceTags = resourceTags.Trim();
			var resourceTagList = resourceTags.Split('|').ToList().ConvertAll(m => { int tagId = 0; int.TryParse(m, out tagId); return tagId; }).Where(m => m > 0).Distinct().ToList();
			if (!resourceTagList.Any())
			{
				return;
			}
			DateTime timeNow = DateTime.Now;
			lock (LockConfig.lock_resourceUpload)
			{
				//string path = "";
				//switch (resourceTypeId)
				//{
				//	case 1: { path = FilePathConfig.PlayerMapPath; } break;
				//	case 2: { path = FilePathConfig.PlayerDrawSheetPath; } break;
				//}
				//string newFileName = Path.Combine(path, Guid.NewGuid().ToString().Replace("-", "") + ".zip");
				//buffer.Bytes2File(newFileName);
				Model.ResourceModel resourceModel = new Model.ResourceModel
				{
					ResourceTypeId = resourceTypeId,
					Title = title,
					Description = description,
					AddTime = timeNow,
					LastUpdateTime = timeNow,
					PlayerId = player.Id,
					PublishTime = timeNow
					//FileUrl = newFileName,
					//FileSizeKb = Convert.ToInt32(buffer.Length * 1.0 / 1024)
				};
				var keyId = BLL.BaseBLL.Insert(resourceModel);
				var flag = BLL.ResourceBLL.UpdateResourceTags(keyId, resourceTagList);
				Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_upload_ret);
				pack.Write(key);
				pack.Write(keyId); //插入后生成的该资源Id
				player.SendMsg(pack);
			}
		}
	}
}
