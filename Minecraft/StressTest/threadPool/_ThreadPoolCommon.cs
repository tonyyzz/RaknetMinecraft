using BaseCommon;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StressTest
{
	class ThreadPoolCommon
	{
		private static int dfYear = 1900;
		private ConcurrentQueue<ThreadPoolModel> queue = new ConcurrentQueue<ThreadPoolModel>();

		/// <summary>
		/// 线程池启动
		/// </summary>
		/// <param name="testName">测试名称</param>
		/// <param name="reqCount">请求数量</param>
		/// <param name="beforeAction">请求前的处理</param>
		/// <param name="operateAction">要进行的请求</param>
		/// <param name="afterAction">请求后的处理</param>
		public void Start(string testName, int reqCount,
			Action beforeAction = null,
			Action<int> operateAction = null,
			Action afterAction = null)
		{
			ThreadPool.QueueUserWorkItem(o =>
			{
				beforeAction?.Invoke();

				DateTime timeStart = DateTime.Now;
				
				for (int i = 1; i <= reqCount; i++)
				{
					queue.Enqueue(new ThreadPoolModel { key = i, timeStart = DateTime.Now });
					operateAction?.Invoke(i);
				}

				Console.WriteLine("--{0}--正在处理数据...", testName);
				while (true)
				{
					Console.WriteLine("--{0}--当前队列要处理的数据有 {1} 个", testName, queue.Count(m => m.timeEnd.Year == dfYear));
					if (queue.Any(m => m.timeEnd.Year == dfYear))
					{
						Thread.Sleep(10);  //该值决定误差（越小越精确，但越小越消耗性能，建议取一个比较合适的值）
					}
					else
					{
						break;
					}
				}

				DateTime timeEnd = DateTime.Now;
				Console.WriteLine("--{0}-- ---线程池结束------------- ", testName);
				Console.WriteLine("--{0}--总数据量：{1}", testName, reqCount);
				Console.WriteLine("--{0}--开始时间：{1}", testName, timeStart.ToStr());
				Console.WriteLine("--{0}--结束时间：{1}", testName, timeEnd.ToStr());
				Console.WriteLine("--{0}--持续时间（秒）：{1}", testName, (timeEnd-timeStart).TotalSeconds);

				StressQuota stressQuota = new StressQuota
				{
					RequestPerSecond = reqCount * 1.0 / (timeEnd - timeStart).TotalSeconds,
					TimePerRequest = (timeEnd - timeStart).TotalSeconds / (reqCount * 1.0 / reqCount),
					ServerReqWaitTime = (timeEnd - timeStart).TotalSeconds / reqCount
				};

				Console.WriteLine("--{0}----吞吐率：		{1}", testName, stressQuota.RequestPerSecond);
				Console.WriteLine("--{0}----用户平均请求等待时间（秒）：	{1}", testName, stressQuota.TimePerRequest);
				Console.WriteLine("--{0}----服务器平均请求等待时间（秒）：	{1}", testName, stressQuota.ServerReqWaitTime);

				//var list = queue.ToList();
				//Console.WriteLine(" --{0}----my- 用户平均请求等待时间（秒）：	{1}", testName, list.Average(m => (m.timeEnd - m.timeStart).TotalSeconds));

				Console.WriteLine("");

				afterAction?.Invoke();

			});
		}

		public void UpdateEndTime(int key)
		{
			var info = queue.FirstOrDefault(m => m.key == key);
			if (info != null)
			{
				info.timeEnd = DateTime.Now;
			}
		}

		class ThreadPoolModel
		{
			public ThreadPoolModel()
			{
				timeStart = new DateTime(dfYear, 1, 1);
				timeEnd = new DateTime(dfYear, 1, 1);
			}
			public int key { get; set; }
			public DateTime timeStart { get; set; }
			public DateTime timeEnd { get; set; }
		}

		/// <summary>
		/// 压力测试指标
		/// </summary>
		class StressQuota
		{
			/// <summary>
			/// 吞吐率（总请求数/处理完成这些请求数所花费的时间）（越大越好）
			/// </summary>
			public double RequestPerSecond { get; set; }
			/// <summary>
			/// 用户平均请求等待时间（处理完成所有请求数所花费的时间/(总请求书/并发用户数））（越小越好）
			/// </summary>
			public double TimePerRequest { get; set; }
			/// <summary>
			/// 服务器平均请求等待时间（处理完成所有的请求数所花费的时间/总请求数）（越小越好）
			/// </summary>
			public double ServerReqWaitTime { get; set; }
		}
	}
}
