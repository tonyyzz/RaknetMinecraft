using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 获取官方通告列表压测
	/// </summary>
	class ThreadPoolAnnunciateList
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "获取官方通告列表",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_ANNUNCIATE, SecondCommand.SC_ANNUNCIATE_list);
					pack.Write(key);
					pack.SendToTcpServer();
				},
				afterAction: afterAction);
		}

		public static void UpdateEndTime(int key)
		{
			threadPool.UpdateEndTime(key);
		}
	}
}
