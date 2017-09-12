using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 玩家信息压测
	/// </summary>
	class ThreadPoolPlayerInfo
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "玩家信息",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerInfo);
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
