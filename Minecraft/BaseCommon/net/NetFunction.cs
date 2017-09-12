using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon
{
	public class NetFunction
	{
		//本地字节序转为网络字节序
		public static int hton(int value)
		{
			return System.Net.IPAddress.HostToNetworkOrder(value);
		}

		public static long hton(long value)
		{
			return System.Net.IPAddress.HostToNetworkOrder(value);
		}

		public static short hton(short value)
		{
			return System.Net.IPAddress.HostToNetworkOrder(value);
		}

		public static uint hton(uint value)
		{
			return (uint)System.Net.IPAddress.HostToNetworkOrder((int)value);
		}

		public static ulong hton(ulong value)
		{
			return (ulong)System.Net.IPAddress.HostToNetworkOrder((long)value);
		}

		public static ushort hton(ushort value)
		{
			return (ushort)System.Net.IPAddress.HostToNetworkOrder((short)value);
		}

		//网络字节序转为本地字节序
		public static int ntoh(int value)
		{
			return System.Net.IPAddress.NetworkToHostOrder(value);
		}

		public static long ntoh(long value)
		{
			return System.Net.IPAddress.NetworkToHostOrder(value);
		}

		public static short ntoh(short value)
		{
			return System.Net.IPAddress.NetworkToHostOrder(value);
		}

		public static uint ntoh(uint value)
		{
			return (uint)System.Net.IPAddress.NetworkToHostOrder((int)value);
		}

		public static ulong ntoh(ulong value)
		{
			return (ulong)System.Net.IPAddress.NetworkToHostOrder((long)value);
		}

		public static ushort ntoh(ushort value)
		{
			return (ushort)System.Net.IPAddress.NetworkToHostOrder((short)value);
		}



	}
}
