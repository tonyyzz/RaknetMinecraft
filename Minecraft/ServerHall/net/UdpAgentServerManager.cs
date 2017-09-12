using BaseCommon;
using HPSocketCS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerHall
{
	/// <summary>
	/// Udp代理服务（用于游戏信息交互）
	/// </summary>
	class UdpAgentServerManager
	{
		public UdpAgentServerManager() { }

		UdpServer server = new UdpServer();
		private static UdpAgentServerManager self = null;

		public static UdpAgentServerManager Instance
		{
			get
			{
				if (self == null)
				{
					self = new UdpAgentServerManager();
				}
				return self;
			}
		}

		public void SendToPlayers(List<int> playerIdLi, Package pack)
		{
			var ids = server.GetAllConnectionIDs();
			if (ids != null)
			{
				foreach (var item in ids)
				{
					var session = server.GetExtra<Session>(item);
					if (session != null)
					{
						Model.PlayerModel player = PlayerAgentManager.onlineAgentPlayerList.FirstOrDefault(m => m.IpAddress == session.IpAddress && m.UdpPort == session.Port);
						if (player != null && playerIdLi.Any(m => m == player.Id))
						{
							Send(session.ConnId, pack);
						}
					}
				}
			}
		}

		public void Send(IntPtr connId, Package pack)
		{
			byte[] bytes = pack.GetBuffer();
			int len = pack.getLen();
			byte[] bytes_tmp = new byte[len];
			Array.Copy(bytes, 0, bytes_tmp, 0, len);
			CustomDE.Encrypt(bytes_tmp, 0, bytes_tmp.Length);
			server.Send(connId, bytes_tmp, len);
		}
		public void Disconnect(IntPtr conID)
		{
			server.Disconnect(conID);
		}
		public void Start(ushort port)
		{
			// 设置服务器事件
			server.OnPrepareListen += new UdpServerEvent.OnPrepareListenEventHandler(OnPrepareListen);
			server.OnAccept += new UdpServerEvent.OnAcceptEventHandler(OnAccept);
			server.OnSend += new UdpServerEvent.OnSendEventHandler(OnSend);
			server.OnReceive += new UdpServerEvent.OnReceiveEventHandler(OnReceive);
			server.OnClose += new UdpServerEvent.OnCloseEventHandler(OnClose);
			server.OnShutdown += new UdpServerEvent.OnShutdownEventHandler(OnShutdown);

			//// 设置包头标识,与对端设置保证一致性
			//server.PackHeaderFlag = 0x2c;
			//// 设置最大封包大小
			//server.MaxPackSize = 4096;
			server.WorkerThreadCount = 3;
			// 启动服务
			server.IpAddress = IPAddress.Any.ToString();
			server.Port = port;
			if (server.Start())
			{
				Log.WriteInfo(string.Format("UdpAgentServerManager Start OK -> ({0}:{1})",
					server.IpAddress, port));
			}
			else
			{
				Log.WriteInfo(string.Format("UdpAgentServerManager Start Error -> ({0}:{1})",
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
			Debug.WriteLine("---有玩家进入");
			string ip = string.Empty;
			ushort port = 0;
			if (server.GetRemoteAddress(connId, ref ip, ref port))
			{
				Log.WriteInfo(string.Format(" > [{0},OnAccept] -> PASS({1}:{2})",
					connId, ip.ToString(), port));
			}
			else
			{
				Log.WriteInfo(string.Format(" > [{0},OnAccept] -> Server_GetClientAddress() Error",
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
				Log.WriteInfo(string.Format(" > [{0},OnAccept] -> SetConnectionExtra fail", connId));
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
				CustomDE.Decrypt(bytes, 0, bytes.Length);
				bytes = bytes.ToList().GetRange(4, bytes.Length - 4).ToArray();
				//定制协议
				var session = server.GetExtra<Session>(connId);
				if (session != null)
				{
					Log.WriteInfo(string.Format(" > [{0},OnReceive] -> {1}:{2} ({3} bytes)",
						session.ConnId, session.IpAddress, session.Port, bytes.Length));
				}
				else
				{
					Log.WriteInfo(string.Format("session = null > [{0},OnReceive] -> ({1} bytes)",
						connId, bytes.Length));
				}
				short mainid = BitConverter.ToInt16(bytes, 0); //主协议
				short secondid = BitConverter.ToInt16(bytes, 2); //次协议
				Debug.WriteLine("||||||udpAgent : ----package log: 【{0}】正在调用主协议为【{1}】，次协议为【{2}】的接口",
					DateTime.Now.ToString("HH:mm:ss"), mainid, secondid);
				Package pack = PackageManage.Instance.NewPackage(mainid, secondid);
				if (pack == null)
				{
					throw new Exception(string.Format("主协议为【{0}】，次协议为【{1}】的包体不存在或者还未注册",
						mainid, secondid));
				}
				int len = bytes.Count(); //数据长度
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
				Log.WriteInfo(string.Format("udpAgent gameservermanager> [{0},OnClose]",
					connId));
			}
			else
			{
				Log.WriteInfo(string.Format("udpAgent gameservermanager> [{0},OnClose] -> OP:{1},CODE:{2}",
					connId, enOperation, errorCode));
			}
			var session = server.GetExtra<Session>(connId);
			if (session != null && session.player != null)
			{
				Model.PlayerModel player = session.player as Model.PlayerModel;
				if (player != null)
				{
					//移除该玩家与客户端的通信
					if (server.RemoveExtra(connId))
					{
						Debug.WriteLine("udpAgent服务器消息：有玩家退出成功");
					}
					else
					{
						Log.WriteInfo(string.Format(" > [{0},OnClose] -> SetConnectionExtra({0}, null) fail",
							connId));
						Debug.WriteLine("udpAgent服务器消息：有玩家退出失败");
					}
				}
			}
			return HandleResult.Ok;
		}

		HandleResult OnShutdown()
		{
			// 服务关闭了
			Log.WriteInfo(" udp - > [OnShutdown]");
			return HandleResult.Ok;
		}
	}
}
