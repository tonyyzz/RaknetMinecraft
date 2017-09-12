using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	public static class PackageHelper
	{
		/// <summary>
		/// 将数据发送至tcp服务器
		/// </summary>
		/// <param name="pack"></param>
		public static void SendToTcpServer(this Package pack)
		{
			TcpClientMng.tcpClient.Send(pack);
		}
	}
}
