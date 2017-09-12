using BaseCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UdpHouseTest
{
	class UdpClientManager
	{
		public static UdpClientManager Instance = null;

		private string IpAddress = "";
		private int Port = 0;

		private System.Net.Sockets.UdpClient client = null;
		public static UdpClientManager InitInstance(string ipAddress, int Port, Action<Model.PlayerModel> func = null, Model.PlayerModel player = null)
		{

			if (Instance == null)
			{

				Instance = new UdpClientManager()
				{
					IpAddress = ipAddress,
					Port = Port,
					client = new System.Net.Sockets.UdpClient(),
				};
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(ipAddress), Port);

				Instance.client.Connect(remoteEP);

				(new Thread(new ThreadStart(UdpClientReceive))
				{
					IsBackground = true
				}).Start();



			}
			if (player != null && func != null)
			{
				func(player);
			}
			return Instance;
		}
		public void Close()
		{
			if (Instance.client != null)
			{
				Instance.client.Close();
				//Instance.client = null;
				//Instance = null;
				Console.WriteLine("断开udp连接");
			}
			//通知tcp客户端
			Package pack = new Package(MainCommand.MC_HOUSE, SecondCommand.SC_HOUSE_close);
			pack.Write((byte)2);
			TcpClientMng.tcpClient.Send(pack);

		}
		private UdpClientManager() { }

		public void Send(Package pack)
		{
			byte[] bytes = pack.GetBuffer();
			int len = pack.getLen();
			byte[] bytes_tmp = new byte[len];
			Array.Copy(bytes, 0, bytes_tmp, 0, len);
			CustomDE.Encrypt(bytes_tmp, 0, bytes_tmp.Length);
			Send(bytes_tmp, len);
		}

		private void Send(byte[] dataSend, int length)
		{
			IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(Instance.IpAddress), Instance.Port);
			if (Instance.client != null && Instance.client.Client != null && Instance.client.Client.Connected)
			{
				Instance.client.Send(dataSend, dataSend.Length);
			}
		}

		private static void UdpClientReceive()
		{
			while (true)
			{
				if (Instance != null)
				{
					try
					{
						IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(Instance.IpAddress), Instance.Port);
						if (Instance.client != null && Instance.client.Client != null && Instance.client.Client.Connected)
						{
							try
							{
								byte[] bytes = Instance.client.Receive(ref remoteEP);
								bytes = bytes.ToList().GetRange(4, bytes.Length - 4).ToArray();
								CustomDE.Decrypt(bytes, 0, bytes.Length);
								//定制协议
								short mainid = BitConverter.ToInt16(bytes, 0); //主协议
								short secondid = BitConverter.ToInt16(bytes, 2); //次协议
								Console.WriteLine("||||||udp : ----package log: 【{0}】正在调用主协议为【{1}】，次协议为【{2}】的接口",
									DateTime.Now.ToString("HH:mm:ss"), mainid, secondid);
								Package pack = PackageManage.Instance.NewPackage(mainid, secondid);
								if (pack == null)
								{
									throw new Exception(
										string.Format("主协议为【{0}】，次协议为【{1}】的包体不存在或者还未注册",
										mainid, secondid));
								}
								int len = bytes.Count(); //数据长度
								pack.Write(bytes, len);
								pack.ReadHead();
								try
								{
									pack.Excute();
								}
								catch (Exception ex)
								{
									Log.WriteError(ex);
								}
							}
							catch
							{
								Console.WriteLine("-接收数据失败");
							}
							Thread.Sleep(1);
						}
					}
					catch
					{
					}
				}
			}
		}
	}
}
