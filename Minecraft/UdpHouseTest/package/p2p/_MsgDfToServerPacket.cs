using BaseCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest.package
{
	class MsgDfToServerPacket : Package
	{
		public MsgDfToServerPacket() { }
		public MsgDfToServerPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new MsgDfToServerPacket(null, 0,
				MainCommand.MC_P2P, SecondCommand.SC_P2P_MsgDfToServer);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			//Debug.WriteLine("当前在线人数：" + UdpServerManager.Instance.GetOnlineCount());
			//var ids = UdpServerManager.Instance.GetConnPtrIds();
			//if (ids != null)
			//{
			//	Debug.WriteLine("ID：" + string.Join(",", ids));
			//}
			
			////Debug.WriteLine("名字：" + string.Join(",", plist.Select(m => m.conID.ToInt32().ToString() + ":" + m.Name + "  ")));
			//Debug.WriteLine("--进过房间的玩家列表");
			//foreach (var item in PlayerMng.playerIncomingList)
			//{
			//	Debug.WriteLine(string.Join("-", item.Id, item.Name, item.conID.ToInt32().ToString(), item.IpAddress + "." + item.UdpPort));
			//}

			//var plist = UdpServerManager.Instance.GetAllOnlinePlayers();
			//Debug.WriteLine("--当前房间的玩家列表");
			//foreach (var item in plist)
			//{
			//	Debug.WriteLine(string.Join("-", item.Id, item.Name, item.conID.ToInt32().ToString(), item.IpAddress + "." + item.UdpPort));
			//}

			//var sessionlist = UdpServerManager.Instance.GetSessionStr();
			//Debug.WriteLine("--当前房间的玩家Session列表");
			//foreach (var item in sessionlist)
			//{
			//	Debug.WriteLine(item);
			//}
		}
	}
}
