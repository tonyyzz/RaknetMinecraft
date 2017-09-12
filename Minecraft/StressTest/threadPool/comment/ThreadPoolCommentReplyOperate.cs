using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	/// <summary>
	/// 评论回复操作
	/// </summary>
	class ThreadPoolCommentReplyOperate
	{
		public static ThreadPoolCommon threadPool = new ThreadPoolCommon();
		public static void Start(int reqCount = 100, Action beforeAction = null, Action afterAction = null)
		{
			threadPool.Start(
				testName: "评论回复操作",
				reqCount: reqCount,
				beforeAction: beforeAction,
				operateAction: (key) =>
				{
					Package pack = new Package(MainCommand.MC_COMMENT, SecondCommand.SC_COMMENT_replyOperate);
					pack.Write(key);
					pack.Write(1); //commentId
					pack.Write("压测回复内容" + key); //replyStr
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
