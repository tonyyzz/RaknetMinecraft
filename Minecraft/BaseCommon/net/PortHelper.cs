using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon
{
	public class PortHelper
	{
		/// <summary>
		/// 获取随机的可用端口
		/// </summary>
		/// <returns></returns>
		public static int GetRandomPort()
		{
			var portList = PortIsUsed();
			int rNumber = 0;
			int seedInt = 0;
			do
			{
				int realSeedInt = DateHelper.GetTotalSecondsInt() + (seedInt++);
				Random random = new Random(realSeedInt);
				rNumber = random.Next(1024, 65536);
			}
			while (portList.Any(m => m == rNumber));
			return rNumber;
		}

		/// <summary>
		/// 获取操作系统已用的端口号
		/// </summary>
		/// <returns></returns>
		public static List<int> PortIsUsed()
		{
			//获取本地计算机的网络连接和通信统计数据的信息            
			IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			//返回本地计算机上的所有Tcp监听程序            
			IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();
			//返回本地计算机上的所有UDP监听程序            
			IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();
			//返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。            
			TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
			var allPorts = new List<int>();
			foreach (IPEndPoint ep in ipsTCP)
			{
				allPorts.Add(ep.Port);
			}
			foreach (IPEndPoint ep in ipsUDP)
			{
				allPorts.Add(ep.Port);
			}
			foreach (TcpConnectionInformation conn in tcpConnInfoArray)
			{
				allPorts.Add(conn.LocalEndPoint.Port);
			}
			return allPorts.Distinct().ToList();
		}
	}
}
