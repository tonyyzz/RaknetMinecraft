using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BaseCommon;
using HPSocketCS;

namespace UdpHouseTest
{
	enum ServerProtocol2
	{
		WebSocket,
		TCP,
	}
	class MySession2 : Session
	{
		public byte[] buffer = null;
		public int bufferLen = 0;
	}


	class TcpClient
	{
		public TcpPackClient tcpClient = new TcpPackClient();
		private int maxBufferSize = 1024;
		public TcpClient instance = null;
		//public bool bStart = false;
		//public ServerClient client = null;

		public TcpClient(int workThreadNum = 6, int _maxBufferSize = 1024 * 10)
		{
			instance = this;
			maxBufferSize = _maxBufferSize;
			//work = new WorkManager(workThreadNum);
		}
		public bool Send(Package pack)
		{
			byte[] bytes = pack.GetBuffer();
			bytes = bytes.ToList().GetRange(4, bytes.Count() - 4).ToArray();
			CustomDE.Encrypt(bytes, 0, bytes.Length);
			return instance.tcpClient.Send(bytes, bytes.Length);
		}
		public void Stop()
		{
			tcpClient.Stop();
		}
		//public void Disconnect(IntPtr con)
		//{
		//	tcpClient.Stop();
		//}
		//public MySession2 GetSession(IntPtr con)
		//{
		//	//return tcpClient.GetExtra<MySession2>(con);
		//}
		public bool Start()
		{
			tcpClient.OnSend += TcpClient_OnSend;
			tcpClient.OnReceive += TcpClient_OnReceive;

			////server.OnPrepareListen += new TcpServerEvent.OnPrepareListenEventHandler(OnPrepareListen);
			//tcp.OnAccept += new TcpServerEvent.OnAcceptEventHandler(OnAccept);
			////tcp.OnSend += new TcpServerEvent.OnSendEventHandler(OnSend);
			////tcp.OnReceive += new TcpServerEvent.OnReceiveEventHandler(OnReceive);
			//tcp.OnClose += new TcpServerEvent.OnCloseEventHandler(OnClose);
			//tcp.OnShutdown += new TcpServerEvent.OnShutdownEventHandler(OnShutdown);
			////tcp.OnHandShake += new TcpServerEvent.OnHandShakeEventHandler(OnHandShake);
			//tcp.OnWSMessageBody += new WebSocketEvent.OnWSMessageBodyEventHandler(OnWSMessageBody);
			////tcp.OnWSMessageHeader += new WebSocketEvent.OnWSMessageHeaderEventHandler(OnWSMessageHeader);

			//// 设置包头标识,与对端设置保证一致性
			tcpClient.PackHeaderFlag = 0x2c;
			tcpClient.SocketBufferSize = 1024 * 6;
			// 设置最大封包大小
			tcpClient.MaxPackSize = 4096;

			//tcpClient.IpAddress = "0.0.0.0";
			//tcpClient.Port = port;
			//// 启动服务
			//if (tcp.Start())
			//{
			//	//this.Text = string.Format("{2} - ({0}:{1})", ip, port, title);
			//	//SetAppState(AppState.Started);
			//	Log.WriteInfo(string.Format("服务器启动成功 -> ({0}:{1})", tcp.IpAddress, port));
			//}
			//else
			//{
			//	Log.WriteInfo("服务器启动失败");
			//	return false;
			//}
			//bStart = true;



			////work.StartWorkThread();
			return true;
		}

		private HandleResult TcpClient_OnReceive(HPSocketCS.TcpClient sender, byte[] bytes)
		{
			CustomDE.Decrypt(bytes, 0, bytes.Length);
			bytes = bytes.ToList().GetRange(4, bytes.Length - 4).ToArray();
			short msgmainid = BitConverter.ToInt16(bytes, 0);
			short msgsecondid = BitConverter.ToInt16(bytes, sizeof(short));
			Package package = PackageManage.Instance.NewPackage(msgmainid, msgsecondid);
			if (package != null)
			{
				package.Write(bytes, bytes.Length);//写入包数据
				package.ReadHead();//读取包头

				package.Excute();
			}
			return HandleResult.Ok;
		}

		private HandleResult TcpClient_OnSend(HPSocketCS.TcpClient sender, byte[] bytes)
		{
			return HandleResult.Ok;
		}

		public void StartClient(string ip, ushort port)
		{
			tcpClient.Connect(ip, port);
		}
		HandleResult OnAccept(IntPtr connId, IntPtr pClient)
		{
			string ip = string.Empty;
			ushort port = 0;
			//if (tcpClient.GetRemoteAddress(connId, ref ip, ref port))
			//{
			//	Log.WriteInfo(string.Format(" > [{0},OnAccept] -> PASS({1}:{2})", connId, ip.ToString(), port));
			//}
			//else
			//{
			//	Log.WriteInfo(string.Format(" > [{0},OnAccept] -> Server_GetClientAddress() Error", connId));
			//}



			// 设置附加数据
			MySession2 session = new MySession2();
			session.ConnId = connId;
			session.IpAddress = ip;
			session.Port = port;
			session.buffer = new byte[maxBufferSize];//设置用户网络缓冲区

			//session.player = new Player(connId, tcp);//设置用户数据

			//if (tcpClient.SetExtra(connId, session) == false)
			//{
			//	Log.WriteInfo(string.Format(" > [{0},OnAccept] -> SetConnectionExtra fail", connId));
			//}

			return HandleResult.Ok;
		}
		HandleResult OnWSMessageBody(IntPtr connId, byte[] bytes)
		{
			//MySession2 session = tcpClient.GetExtra<MySession2>(connId);
			//if (session == null || bytes.Length <= 0)
			//{
			//	return HandleResult.Error;
			//}

			//if (session.bufferLen + bytes.Length > maxBufferSize)//超出缓冲区
			//	return HandleResult.Ok;


			//try
			//{
			//	Array.Copy(bytes, 0, session.buffer, session.bufferLen, bytes.Length);
			//	session.bufferLen += bytes.Length;
			//	int msgLen = BitConverter.ToInt32(session.buffer, 0);

			//	if (msgLen <= 0 || msgLen >= maxBufferSize)
			//	{//如果长度异常 或者超出 则断开
			//		return HandleResult.Error;
			//	}

			//	while (session.bufferLen >= msgLen + 4 && msgLen > 0)
			//	{
			//		if (msgLen <= 0 || msgLen + 4 >= maxBufferSize)
			//			break;

			//		//解密
			//		byte[] tmp_buff = new byte[msgLen + 4];
			//		Array.Copy(session.buffer, 0, tmp_buff, 0, msgLen + 4);
			//		CustomDE.Decrypt(tmp_buff, 4, msgLen);
			//		short msgmainid = BitConverter.ToInt16(tmp_buff, sizeof(int));
			//		short msgsecondid = BitConverter.ToInt16(tmp_buff, sizeof(int) + sizeof(short));

			//		Package package = PackageManage.Instance.NewPackage(msgmainid, msgsecondid);
			//		if (package != null)
			//		{

			//			package.Write(tmp_buff, tmp_buff.Length);//写入包数据
			//			package.ReadHead();//读取包头

			//			package.SetSession(session);

			//			//执行包体
			//			try
			//			{
			//				package.Excute();
			//			}
			//			catch (Exception ex)
			//			{
			//				Log.WriteError(ex);
			//			}


			//		}
			//		else
			//		{
			//			Log.WriteError(string.Format("实例化Package失败 mainid={0},secondid={1},msglen={2}", msgmainid, msgsecondid, msgLen));
			//		}


			//		//Log.WriteInfo(string.Format(" 收到消息：{0}字节", msgLen));
			//		session.bufferLen -= msgLen + 4;
			//		if (session.bufferLen > 0)
			//		{
			//			Array.Copy(session.buffer, msgLen + 4, session.buffer, 0, session.bufferLen);
			//			msgLen = BitConverter.ToInt32(session.buffer, 0);
			//		}
			//		else
			//		{
			//			//Array.Clear(session.buffer, 0, msgLen+4);
			//			session.bufferLen = 0;
			//			msgLen = 0;
			//		}

			//	}
			//}
			//catch (Exception ex)
			//{
			//	Log.WriteError(ex);
			//	return HandleResult.Error;
			//}

			return HandleResult.Ok;
		}
		HandleResult OnReceive(IntPtr connId, byte[] bytes)
		{
			//Log.WriteError("收到消息：" + bytes.Length);
			//MySession2 session = tcpClient.GetExtra<MySession2>(connId);
			//if (session == null || bytes.Length <= 0 || bytes.Length >= maxBufferSize)
			//{
			//	return HandleResult.Error;
			//}
			//CustomDE.Decrypt(bytes, 0, bytes.Length);//解密的部分
			//short msgmainid = BitConverter.ToInt16(bytes, 0);
			//short msgsecondid = BitConverter.ToInt16(bytes, sizeof(short));
			////Console.WriteLine(string.Format("长度：{0},main：{1},secend：{2}", msgLen,msgmainid,msgsecondid));
			//Package package = PackageManage.Instance.NewPackage(msgmainid, msgsecondid);
			//if (package != null)
			//{

			//	package.Write(bytes, bytes.Length);//写入包数据
			//	package.ReadHead();//读取包头

			//	//分配线程
			//	package.SetSession(session);
			//	package.Excute();
			//	////分配线程
			//	//TableManager.Instance.allocThread(session.player.tableID, package);



			//}
			//else
			//{
			//	Log.WriteError(string.Format("实例化Package失败 mainid={0},secondid={1}", msgmainid, msgsecondid));
			//}

			return HandleResult.Ok;
		}
		HandleResult OnClose(IntPtr connId, SocketOperation enOperation, int errorCode)
		{
			//if (errorCode == 0)
			//	Log.WriteInfo(string.Format(" > [{0},OnClose]", connId));
			//else
			//	Log.WriteInfo(string.Format(" > [{0},OnError] -> OP:{1},CODE:{2}", connId, enOperation, errorCode));
			//// return HPSocketSdk.HandleResult.Ok;
			//MySession2 session = tcpClient.GetExtra<MySession2>(connId);
			//try
			//{
			//	//Player player = (Player)session.player;
			//	//if (player.gameid >= 0 && player.tableID >= 0)
			//	//{
			//	//	Table table = GameManager.Instance.leaveTable(player, player.gameid, player.tableID);
			//	//	if (table != null)
			//	//	{
			//	//		//群发通知其他用户离开了
			//	//		KeyValuePair<int, User>[] list = table.GetAllUser();
			//	//		Package pack1 = new Package(MainCommand.MC_GAME, SecondCommand.SC_GAME_leave_notice);
			//	//		pack1.Write(player.pid);
			//	//		pack1.Write(player.pName);
			//	//		for (int i = 0; i < list.Length; i++)
			//	//		{
			//	//			KeyValuePair<int, User> kvp = list[i];
			//	//			kvp.Value.Send(pack1);
			//	//		}
			//	//	}
			//	//}

			//	//PlayerManager.Instance.PlayerLeave(player);

			//	if (tcpClient.State == ServiceState.Stoping)
			//	{
			//		Console.WriteLine(string.Format(" > [{0},玩家离开]", connId));
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Log.WriteError(ex);
			//}
			//if (tcpClient.RemoveExtra(connId) == false)
			//{
			//	Log.WriteInfo(string.Format(" > [{0},OnClose] -> SetConnectionExtra({0}, null) fail", connId));
			//}



			return HandleResult.Ok;
		}

		HandleResult OnShutdown()
		{
			Log.WriteInfo(" > [OnShutdown]");
			return HandleResult.Ok;
		}
		public bool CheckStop()
		{
			if (tcpClient.State == ServiceState.Stoped)
				return true;
			else
				return false;
		}
	}
}
