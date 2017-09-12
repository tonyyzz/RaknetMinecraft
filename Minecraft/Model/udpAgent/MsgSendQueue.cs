using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	/// <summary>
	/// 消息发送队列
	/// </summary>
	public class MsgSendQueue
	{
		/// <summary>
		/// 表示一段时间后未收到udpServer返回的数据，则向udpAgent发送数据
		/// </summary>
		private int _timeSpMiniSecond = 400;

		private int _dfYear = 1900;

		public MsgSendQueue()
		{
			timeSrart = new DateTime(_dfYear, 1, 1);
			timeEnd = new DateTime(_dfYear, 1, 1);
		}

		/// <summary>
		/// 表示一段时间后未收到udpServer返回的数据，则向udpAgent发送数据
		/// </summary>
		/// <returns></returns>
		public int GetTimeSpMiniSeconds()
		{
			return _timeSpMiniSecond;
		}

		/// <summary>
		/// 获取默认年份
		/// </summary>
		/// <returns></returns>
		public int GetDfYear()
		{
			return _dfYear;
		}

		/// <summary>
		/// 发送的开始时间
		/// </summary>
		public DateTime timeSrart { get; set; }
		/// <summary>
		/// 发送的结束时间
		/// </summary>
		public DateTime timeEnd { get; set; }

		/// <summary>
		/// 发送udpServer主协议
		/// </summary>
		public MainCommand sendUdpServerMainC { get; set; }
		/// <summary>
		/// 发送udpServer次协议
		/// </summary>
		public SecondCommand sendUdpServerSecondC { get; set; }
		/// <summary>
		/// 接收udpServer主协议
		/// </summary>
		public MainCommand receiveUdpServerMainC { get; set; }
		/// <summary>
		/// 接收udpServer次协议
		/// </summary>
		public SecondCommand receiveUdpServerSecondC { get; set; }
		/// <summary>
		/// 发送udpAgent主协议
		/// </summary>
		public MainCommand sendUdpAgentMainC { get; set; }
		/// <summary>
		/// 发送udpAgent次协议
		/// </summary>
		public SecondCommand sendUdpAgentSecondC { get; set; }
		/// <summary>
		/// 初始发送包体
		/// </summary>
		public Package pack { get; set; }
	}
}
