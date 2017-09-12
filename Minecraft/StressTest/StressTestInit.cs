using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 压力测试初始化
	/// </summary>
	public class StressTestInit
	{
		public static void Init()
		{
			Console.WriteLine("");
			Console.WriteLine("		【压力测试】");
			PackageConfig.Register();

			TcpClientMng.tcpClient.Start();
			TcpClientMng.tcpClient.StartClient(TcpClientMng.hall_server_ip, ushort.Parse(TcpClientMng.hall_server_port));

			ThreadPoolReq.Start();
		}
	}
}
