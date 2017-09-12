using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall.package
{
	/// <summary>
	/// 获取官方通告列表
	/// </summary>
	class AnnunciateListPacket : Package
	{
		public AnnunciateListPacket() { }
		public AnnunciateListPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new AnnunciateListPacket(null, 0,
				MainCommand.MC_ANNUNCIATE, SecondCommand.SC_ANNUNCIATE_list);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			Model.PlayerModel player = (Model.PlayerModel)session.player;
			if (player == null || !player.online)
			{
				return;
			}
			var annunciateList = BLL.OfficialAnnunciateBLL.GetListAll();
			Package pack = new Package(MainCommand.MC_ANNUNCIATE, SecondCommand.SC_ANNUNCIATE_list_ret);
			pack.Write(key);
			pack.Write(annunciateList.Count());
			annunciateList.ForEach(item =>
			{
				pack.Write(item.Title);
				pack.Write(item.Img);
			});
			player.SendMsg(pack);
		}
	}
}
