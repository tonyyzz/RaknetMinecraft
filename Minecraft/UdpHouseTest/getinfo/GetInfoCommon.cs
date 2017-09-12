using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpHouseTest
{
	public  class GetInfoCommon
	{
		/// <summary>
		/// 获取房间列表
		/// </summary>
		public static void ShowHouseList()
		{
			int isWifiInt = 1; //wifi联机：1， 局域网：2
			int tagId = 0; //传地图标签Id（0表示全部）
			string keywords = ""; //关键字（传 房主名称 或者 房主Id ）（可以传空字符串，即双引号）
			int pageIndex = 1; //页码从1开始
			int pageSize = 10; //每页数量（客户端自己控制）

			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_list);
			pack.Write(isWifiInt);
			pack.Write(tagId);
			pack.Write(keywords);
			pack.Write(pageIndex);
			pack.Write(pageSize);
			TcpClientMng.tcpClient.Send(pack);
		}
	}
}
