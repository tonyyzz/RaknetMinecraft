using BaseCommon;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest
{
    /// <summary>
    /// 消息发送队列线程池
    /// </summary>
    public static class ThreadPoolSendMsgQueue
    {
        private static Queue<MsgSendQueue> queue = new Queue<MsgSendQueue>();

        public static void Enqueue(MsgSendQueue msgSendQueue)
        {
            var info = queue.FirstOrDefault(m =>
                m.sendUdpServerMainC == msgSendQueue.sendUdpServerMainC
                && m.sendUdpServerSecondC == msgSendQueue.sendUdpServerSecondC);
            if (info == null)
            {
                queue.Enqueue(msgSendQueue);
            }
            else
            {
                info.sendUdpAgentMainC = msgSendQueue.sendUdpAgentMainC;
                info.sendUdpAgentSecondC = msgSendQueue.sendUdpAgentSecondC;
                info.pack = msgSendQueue.pack;
                info.timeSrart = msgSendQueue.timeSrart;
            }
        }

        public static void UpdateEndTime(MainCommand receiveUdpServerMainC, SecondCommand receiveUdpServerSecondC)
        {
            var info = queue.FirstOrDefault(m =>
                m.receiveUdpServerMainC == receiveUdpServerMainC
                && m.receiveUdpServerSecondC == receiveUdpServerSecondC);
            if (info != null)
            {
                info.timeEnd = DateTime.Now;
            }
        }

        /// <summary>
        /// 线程池启动
        /// </summary>
        public static void Start()
        {
            ThreadPool.QueueUserWorkItem(obj =>
            {
                int timeSp = (int)obj;

                MsgSendQueue info = null;

                while (true)
                {
                    Debug.WriteLine("---ThreadPoolSendMsgQueue执行");
                    if (queue.Count > 0)
                    {
                        try
                        {
                            while (queue.Count > 0)
                            {
                                Debug.WriteLine(string.Format(@"---||| ThreadPoolSendMsgQueue队列有【{0}】条数据", queue.Count));

                                try
                                {
                                    //从队列中取出  
                                    info = queue.Dequeue();
                                    //判断时间节点与协议，是否向代理服务器发送消息
                                    DateTime timeNow = DateTime.Now;

                                    if ((timeNow - info.timeSrart).TotalMilliseconds < info.GetTimeSpMiniSeconds() && info.timeEnd.Year == info.GetDfYear())
                                    {
                                        Enqueue(info);
                                    }
                                    else
                                    {

                                        if ((timeNow - info.timeSrart).TotalMilliseconds > info.GetTimeSpMiniSeconds())
                                        {
                                            if (info.timeEnd.Year == info.GetDfYear())
                                            {
                                                //向代理服务器发送消息
                                                Package pack = new Package(info.sendUdpAgentMainC, info.sendUdpAgentSecondC);
                                                //解析可用数据
                                                byte[] bytes = info.pack.GetBuffer();
                                                int len = info.pack.getLen();
                                                byte[] bytes_tmp = new byte[len - 8];
                                                Array.Copy(bytes, 8, bytes_tmp, 0, bytes_tmp.Length);
                                                foreach (var item in bytes_tmp)
                                                {
                                                    pack.Write(item);
                                                }
                                                UdpAgentClientManager.Instance.Send(pack);
                                            }
                                        }
                                    }

                                    Thread.Sleep(5);
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        //没有任务，休息一段时间  
                        Thread.Sleep(timeSp);
                    }
                }
            }, Convert.ToInt32(new MsgSendQueue().GetTimeSpMiniSeconds() * 1.2));
        }
    }
}
