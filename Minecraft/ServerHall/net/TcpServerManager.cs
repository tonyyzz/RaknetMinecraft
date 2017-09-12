using BaseCommon;
using HPSocketCS;
using Model;
using System;
using System.Linq;
using System.Net;

namespace ServerHall
{
	/// <summary>
	/// Tcp服务管理（主要用来进行社交通信）
	/// </summary>
	class TcpServerManager
	{
		public TcpServerManager() { }

		TcpPackServer server = new TcpPackServer();
		private static TcpServerManager self = null;

		public static TcpServerManager Instance
		{
			get
			{
				if (self == null)
				{
					self = new TcpServerManager();
				}
				return self;
			}
		}

		//public void SendAllServer(Package pack)
		//{
		//    IntPtr[] list = server.GetAllConnectionIDs();
		//    if (list == null || list.Length == 0)
		//        return;
		//    foreach (IntPtr con in list)
		//        server.Send(con, pack.GetBuffer(), pack.getLen());
		//}
		public void Send(IntPtr con, Package pack)
		{
			byte[] bytes = pack.GetBuffer();
			int len = pack.getLen();
			byte[] bytes_tmp = new byte[len];
			Array.Copy(bytes, 0, bytes_tmp, 0, len);
			//加密
			CustomDE.Encrypt(bytes_tmp, 0, bytes_tmp.Length);
			server.Send(con, bytes_tmp, len);
		}

		public void Disconnect(IntPtr conID)
		{
			server.Disconnect(conID);
		}

		public void Start(ushort port)
		{
			// 设置服务器事件
			server.OnPrepareListen += new TcpServerEvent.OnPrepareListenEventHandler(OnPrepareListen);
			server.OnAccept += new TcpServerEvent.OnAcceptEventHandler(OnAccept);
			server.OnSend += new TcpServerEvent.OnSendEventHandler(OnSend);
			server.OnReceive += new TcpServerEvent.OnReceiveEventHandler(OnReceive);
			server.OnClose += new TcpServerEvent.OnCloseEventHandler(OnClose);
			server.OnShutdown += new TcpServerEvent.OnShutdownEventHandler(OnShutdown);

			// 设置包头标识,与对端设置保证一致性
			server.PackHeaderFlag = 0x2c;
			// 设置最大封包大小
			server.MaxPackSize = 4096;
			server.WorkerThreadCount = 3;
			// 启动服务
			server.IpAddress = IPAddress.Any.ToString();
			server.Port = port;
			if (server.Start())
			{
				Log.WriteInfo(string.Format("TcpServerManager Start OK -> ({0}:{1})",
					server.IpAddress, port));
			}
			else
			{
				Log.WriteError(string.Format("TcpServerManager Start Error -> ({0}:{1})",
					server.ErrorMessage, server.ErrorCode));
			}
		}
		HandleResult OnPrepareListen(IntPtr soListen)
		{
			// 监听事件到达了,一般没什么用吧?
			return HandleResult.Ok;
		}

		HandleResult OnAccept(IntPtr connId, IntPtr pClient)
		{
			// 客户进入了
			// 获取客户端ip和端口
			string ip = string.Empty;
			ushort port = 0;
			if (server.GetRemoteAddress(connId, ref ip, ref port))
			{
				Log.WriteInfo(string.Format(" tcp - > [{0},OnAccept] -> PASS({1}:{2})",
					connId, ip.ToString(), port));
			}
			else
			{
				Log.WriteInfo(string.Format(" tcp - > [{0},OnAccept] -> Server_GetClientAddress() Error",
					connId));
			}
			//设置附加数据，保存用户连接，用来与客户端通信
			Session session = new Session
			{
				ConnId = connId,
				IpAddress = ip,
				Port = port,
				player = null
			};
			//设置
			if (server.SetExtra(connId, session) == false)
			{
				Log.WriteInfo(string.Format(" tcp - > [{0},OnAccept] -> SetConnectionExtra fail", connId));
			}
			return HandleResult.Ok;
		}

		HandleResult OnSend(IntPtr connId, byte[] bytes)
		{
			//服务器发数据了
			return HandleResult.Ok;
		}

		HandleResult OnReceive(IntPtr connId, byte[] bytes)
		{
			//接收数据
			try
			{
				var session = server.GetExtra<Session>(connId);
				if (session != null)
				{
					Log.WriteInfo(string.Format(" tcp - > [{0},OnReceive] -> {1}:{2} ({3} bytes)",
						session.ConnId, session.IpAddress, session.Port, bytes.Length));
				}
				else
				{
					Log.WriteInfo(string.Format(" tcp - session = null > [{0},OnReceive] -> ({1} bytes)",
						connId, bytes.Length));
				}
				//处理数据
				//数据解密
				CustomDE.Decrypt(bytes, 0, bytes.Length);
				int len = bytes.Count(); //数据长度
				short mainid = BitConverter.ToInt16(bytes, 0); //主协议
				short secondid = BitConverter.ToInt16(bytes, 2); //次协议
				Console.WriteLine("||||||tcp : ----package log: 【{0}】正在调用主协议为【{1}】，次协议为【{2}】的接口",
					DateTime.Now.ToString("HH:mm:ss"), mainid, secondid);
				Package pack = PackageManage.Instance.NewPackage(mainid, secondid);
				if (pack == null)
				{
					throw new Exception(
						string.Format("主协议为【{0}】，次协议为【{1}】的包体不存在或者还未注册",
						mainid, secondid));
				}
				pack.Write(bytes, len);
				pack.ReadHead();
				pack.SetSession(session);
				try
				{
					pack.Excute();
				}
				catch (Exception ex)
				{
					Log.WriteError(ex);
				}
				return HandleResult.Ok;
			}
			catch (Exception ex)
			{
				Log.WriteError(ex);
				return HandleResult.Ignore;
			}
		}

		HandleResult OnClose(IntPtr connId, SocketOperation enOperation, int errorCode)
		{
			if (errorCode == 0)
			{
				Log.WriteInfo(string.Format(" tcp - gameservermanager> [{0},OnClose]",
					connId));
			}
			else
			{
				Log.WriteInfo(string.Format(" tcp - gameservermanager> [{0},OnClose] -> OP:{1},CODE:{2}",
					connId, enOperation, errorCode));
			}
			var session = server.GetExtra<Session>(connId);
			if (session != null && session.player != null)
			{
				PlayerModel player = session.player as PlayerModel;
				if (player != null)
				{
					Console.WriteLine(string.Format(@" tcp - -------------玩家【{0}】在【{1}】时下线----------------", player.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
					player.Offline();//玩家下线
				}
				session = null;
				//移除该玩家与客户端的通信
				if (!server.RemoveExtra(connId))
				{
					Log.WriteInfo(string.Format(" tcp - > [{0},OnClose] -> SetConnectionExtra({0}, null) fail",
						connId));
				}
			}
			return HandleResult.Ok;
		}

		HandleResult OnShutdown()
		{
			// 服务关闭了
			Log.WriteInfo(" tcp - > [OnShutdown]");
			return HandleResult.Ok;
		}
	}
}
