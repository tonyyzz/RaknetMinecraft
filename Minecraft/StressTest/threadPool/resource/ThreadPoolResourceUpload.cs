using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 资源上传
	/// </summary>
	class ThreadPoolResourceUpload
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "资源上传",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_RESOURCE, SecondCommand.SC_RESOURCE_upload);
					pack.Write(key);
					pack.Write("压测标题" + key); //title
					pack.Write("description" + key); //description
					pack.Write(1); //resourceTypeId
					pack.Write("1"); //resourceTags
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
