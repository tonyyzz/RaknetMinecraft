using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest
{
    public static class ThreadPoolClientSendMsgToServer
    {
        private static bool isRunning = true;

        /// <summary>
        /// 停止线程池
        /// </summary>
        public static void Stop()
        {
            isRunning = false;
        }
        /// <summary>
        /// 线程池启动
        /// </summary>
        public static void Start()
        {
            isRunning = true;
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    if (!isRunning)
                    {
                        break;
                    }
                    //每隔3秒，向服务器发送数据（空消息）
                    Package pack = new Package(MainCommand.MC_P2P, SecondCommand.SC_P2P_MsgDfToServer);
                    UdpClientManager.Instance?.Send(pack);
                    Thread.Sleep(5000);
                }
            });
        }
    }

}
