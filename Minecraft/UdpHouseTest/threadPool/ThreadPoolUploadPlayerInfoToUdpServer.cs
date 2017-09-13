using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest
{
    public static class ThreadPoolUploadPlayerInfoToUdpServer
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
                    //将玩家信息上传至udp服务器
                    Package pack = new Package(MainCommand.MC_UDPAGENT, SecondCommand.SC_UDPAGENT_login);
                    pack.Write(PlayerMng.player.Id);
                    UdpAgentClientManager.Instance?.Send(pack);
                    Thread.Sleep(2000);
                }
            });
        }
    }
}
