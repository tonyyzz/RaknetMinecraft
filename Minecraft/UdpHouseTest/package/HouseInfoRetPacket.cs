using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class HouseInfoRetPacket : Package
	{
		public HouseInfoRetPacket() { }
		public HouseInfoRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseInfoRetPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_info_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			byte flag = ReadByte(); //房间是否存在

			if (flag == 1)
			{
				long houseId = ReadLong(); //房间Id
				string name = ReadString();
				string tagName = ReadString();
				string description = ReadString();
				string thumbnailImg = ReadString();
				string housePwd = ReadString();
				int houseSize = ReadInt();
				int count = ReadInt();

				//存储房主信息
				if (PlayerMng.OwnerPlayerHouse.Id == houseId)
				{
					PlayerMng.OwnerPlayerHouse.Name = name;
					PlayerMng.OwnerPlayerHouse.TagName = tagName;
					PlayerMng.OwnerPlayerHouse.Description = description;
					PlayerMng.OwnerPlayerHouse.ThumbnailImg = thumbnailImg;
					PlayerMng.OwnerPlayerHouse.HousePwd = housePwd;
					PlayerMng.OwnerPlayerHouse.HouseSize = houseSize;

					//在客户端启动udp服务器
					var isStartSuccess = UdpServerManager.Instance.Start(PlayerMng.OwnerPlayerHouse);
					if (isStartSuccess)
					{
						ThreadPool.QueueUserWorkItem(o =>
						{
							CommonForm.Obj.playerInfoForm.BeginInvoke(new Action(CommonForm.Obj.playerInfoForm.StartUdpServer));
						});
					}
					else
					{
						//启动失败
					}
				}
			}
		}
	}
}
