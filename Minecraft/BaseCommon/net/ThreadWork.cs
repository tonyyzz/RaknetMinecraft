using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace BaseCommon
{
	/// <summary>
	/// 线程工作
	/// </summary>
	public class ThreadWork
	{
		private ConcurrentQueue<Package> queue = new ConcurrentQueue<Package>();
		private Thread thread = null;
		public ThreadWork()
		{
			thread = new Thread(new ThreadStart(ThreadFunc));

		}
		public void Start()
		{
			if (thread != null)
				thread.Start();
		}
		/// <summary>
		/// Packet 入处理队列
		/// </summary>
		/// <param name="pack"></param>
		public void Enqueue(Package pack)
		{
			queue.Enqueue(pack);
		}
		public int GetQueueLen()
		{
			return queue.Count;
		}

		public void ThreadFunc()
		{
			do
			{

				Package pack = null;
				for (int i = 0; i < 3; i++)
				{
					if (queue.TryDequeue(out pack))
					{//获取
						try
						{
							pack.Excute();//执行包体
						}
						catch (Exception ex)
						{
							Log.WriteError(ex);
						}
						pack = null;
					}
					else
						break;
				}
				Thread.Sleep(1);
			} while (true);
		}
	}
}
