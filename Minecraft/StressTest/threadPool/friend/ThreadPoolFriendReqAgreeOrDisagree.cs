using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 接受/拒绝好友申请
	/// </summary>
	class ThreadPoolFriendReqAgreeOrDisagree
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "接受/拒绝好友申请",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_FRIEND, SecondCommand.SC_FRIEND_requestAgreeOrDisagree);
					pack.Write(key);
					pack.Write(key + 1); //friendId
					pack.Write(1); //operateInt
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
