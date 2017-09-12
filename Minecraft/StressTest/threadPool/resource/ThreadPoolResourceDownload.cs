using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 资源下载
	/// </summary>
	class ThreadPoolResourceDownload
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "资源下载",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_download);
					pack.Write(key);
					pack.Write(1);
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
