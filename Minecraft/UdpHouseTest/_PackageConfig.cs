using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdpHouseTest.package;

namespace UdpHouseTest
{
	public  class PackageConfig
	{
		/// <summary>
		/// 包体注册
		/// </summary>
		public static void Register()
		{
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ERROR, (short)SecondCommand.SC_ERROR_hall, new ERRORHallPacket());

			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_login_ret, new PlayerLoginRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_HOUSE, (short)SecondCommand.SC_HOUSE_create_ret, new HouseCreateRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_HOUSE, (short)SecondCommand.SC_HOUSE_list_ret, new HouseListRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_HOUSE, (short)SecondCommand.SC_HOUSE_join_ret, new HouseJoinRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_HOUSE, (short)SecondCommand.SC_HOUSE_info_ret, new HouseInfoRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_HOUSE, (short)SecondCommand.SC_HOUSE_portSet_ret, new HousePortSetRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_HOUSE, (short)SecondCommand.SC_HOUSE_close_ret, new HouseCloseRetPacket());

			#region p2p
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_MsgDfToServer, new MsgDfToServerPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_msgTextToServer, new MsgTextToServerPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_msgTextToClient, new MsgTextToClientPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_msgTextToClientRet, new MsgTextToClientRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_msgPlayerLoginUdpServer, new MsgPlayerLoginUdpServerPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_msgPlayerInfoToClient, new MsgPlayerInfoToClientPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_msgPlayerLeaveToServer, new MsgPlayerLeaveToServerPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_P2P, (short)SecondCommand.SC_P2P_msgServerCloseToClient, new MsgServerCloseToClientPacket());
			#endregion

			#region udpAgent
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_UDPAGENT, (short)SecondCommand.SC_UDPAGENT_testToUdpServer, new UdpAgentTestToUdpServerPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_UDPAGENT, (short)SecondCommand.SC_UDPAGENT_testUdpAgentServerToUdpClient, new UdpAgentTestUdpAgentServerToUdpClientPacket());
			#endregion
		}
	}
}
