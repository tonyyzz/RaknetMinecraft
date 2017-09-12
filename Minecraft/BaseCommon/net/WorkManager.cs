using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BaseCommon
{
	/// <summary>
	/// 工作线程管理
	/// </summary>
	public class WorkManager
	{

		//private List<ThreadWork> list = new List<ThreadWork>();
		private ConcurrentDictionary<int, ThreadWork> threadDict = new ConcurrentDictionary<int, ThreadWork>();
		private object allocLock = new object();
		private int curAllocIndex = 0;//当前分配索引
		private int maxNum = 0;
		public WorkManager(int threadNum = 3)
		{
			for (int i = 0; i < threadNum; i++)
			{
				ThreadWork tw = new ThreadWork();
				threadDict.TryAdd(i, tw);
			}
			maxNum = threadNum;
		}
		public void StartWorkThread()
		{
			foreach (ThreadWork tw in threadDict.Values)
			{
				tw.Start();
			}
		}
		public void allocThread(Package pack)
		{
			//int index = 0;


			//Interlocked.Exchange(ref curAllocIndex, index);


			ThreadWork tw = null;
			if (threadDict.TryGetValue(curAllocIndex, out tw))
				tw.Enqueue(pack);

			int newIndex = Interlocked.Increment(ref curAllocIndex);
			if (newIndex >= maxNum)
				Interlocked.Exchange(ref curAllocIndex, 0);


		}
		/// <summary>
		/// 打印工作线程情况
		/// </summary>
		public void printWorkInfo()
		{
			int i = 0;
			foreach (ThreadWork tw in threadDict.Values)
			{
				Console.WriteLine(i + "线程队列长度：" + tw.GetQueueLen());
				i++;
			}
		}

	}
}
