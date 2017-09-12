using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 更新介绍
	/// </summary>
	class ThreadPoolPlayerEditDescription
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "更新介绍",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditDescription);
					pack.Write(key);
					pack.Write("压测介绍");
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
