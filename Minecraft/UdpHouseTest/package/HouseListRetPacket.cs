using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class HouseListRetPacket : Package
	{
		public HouseListRetPacket() { }
		public HouseListRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new HouseListRetPacket(null, 0,
				MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_list_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int count = ReadInt(); //房间个数

			List<Model.PlayerHouseModel> houseList = new List<Model.PlayerHouseModel>();

			for (int i = 0; i < count; i++)
			{
				long id = ReadLong();
				string name = ReadString();
				string tagName = ReadString();
				string thumbnailImg = ReadString();

				int houseSize = ReadInt();
				int playerCount = ReadInt();
				int udpPport = ReadInt();

				houseList.Add(new Model.PlayerHouseModel()
				{
					Id = id,
					Name = name,
					TagName = tagName,
					ThumbnailImg = thumbnailImg,
					HouseSize = houseSize,
					HouseUdpPort = (ushort)udpPport,
					playerCount = playerCount
				});
			}
			ThreadPool.QueueUserWorkItem(o =>
			{
				if (CommonForm.Obj.playerInfoForm.IsHandleCreated)
					CommonForm.Obj.playerInfoForm.BeginInvoke(new Action<List<Model.PlayerHouseModel>>(CommonForm.Obj.playerInfoForm.ShowHouseList), new object[] { houseList });
			});

		}
	}
}
