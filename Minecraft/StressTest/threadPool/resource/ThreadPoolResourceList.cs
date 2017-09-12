using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 资源列表压测
	/// </summary>
	class ThreadPoolResourceList
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "资源列表",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_list);
					pack.Write(key);
					pack.Write(1); //resourceTypeId
					pack.Write("1"); //classIdStrs
					pack.Write(0); //resourceTagId
					pack.Write(1); //pageIndex
					pack.Write(10); //pageSize
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
