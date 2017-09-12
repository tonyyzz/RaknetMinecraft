using RakNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest
{
    public class UdpRaknetPeerManager
    {
        private static UdpRaknetPeerManager Instance = null;

        private RakPeerInterface rakPeer = null;
        private readonly static object obj = new object();
        private UdpRaknetPeerManager() { }
        public static UdpRaknetPeerManager GetInstance()
        {
            if (Instance == null)
            {
                lock (obj)
                {
                    if (Instance == null)
                    {
                        Instance = new UdpRaknetPeerManager();
                        Instance.rakPeer = RakPeerInterface.GetInstance();
                        NatPunchthroughServer natPunchthroughServer = new NatPunchthroughServer();
                        Instance.rakPeer.AttachPlugin(natPunchthroughServer);
                    }
                }
            }
            return Instance;
        }

        /// <summary>
        /// 作为服务器启动
        /// </summary>
        /// <param name="maxConnCount"></param>
        /// <returns></returns>
        public bool Start(ushort maxConnCount = 6)
        {
            rakPeer.SetMaximumIncomingConnections(maxConnCount);
            var result = rakPeer.Startup(1, new SocketDescriptor(), 1);
            if (result == StartupResult.RAKNET_ALREADY_STARTED)
            {
                ReceiveThreadStart();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 作为客户端链接服务器，同时也可作为服务器被连接
        /// </summary>
        public void Connect(ushort maxConnCount = 6)
        {

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void Send()
        {

        }

        /// <summary>
        /// 接收消息线程池
        /// </summary>
        private void ReceiveThreadStart()
        {
            while (true)
            {
                using (Packet testPacket = rakPeer.Receive())
                {
                    if (testPacket == null || testPacket.data.Count() <= 0)
                    {
                        Thread.Sleep(1);
                        continue;
                    }

                    DefaultMessageIDTypes defaultMessageIDType = (DefaultMessageIDTypes)testPacket.data[0];
                    Console.WriteLine(string.Format(@"{0} - {1}", testPacket.data[0], defaultMessageIDType.ToString()));

                    switch (defaultMessageIDType)
                    {
                        case DefaultMessageIDTypes.ID_NEW_INCOMING_CONNECTION: //有客户端链接
                            {

                            }
                            break;
                        case DefaultMessageIDTypes.ID_USER_PACKET_ENUM: //接收消息
                            {
                                
                            }
                            break;
                        case DefaultMessageIDTypes.ID_DISCONNECTION_NOTIFICATION: //有客户端断开
                            {

                            }
                            break;
                        default: //操作失败
                            {

                            }
                            break;
                    }
                    //rakPeer.DeallocatePacket(testPacket);
                }
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="beforeAction"></param>
        public void Stop(Action beforeAction = null)
        {
            beforeAction?.Invoke();
            Thread.Sleep(5);
            rakPeer.Shutdown(300);
            RakPeerInterface.DestroyInstance(rakPeer);
            rakPeer = null;
            Instance = null;
        }
    }
}
