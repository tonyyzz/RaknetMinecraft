using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon
{
	/// <summary>
	/// 自定义加密解密算法
	/// </summary>
	public class CustomDE
	{
		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="byteArray"></param>
		/// <param name="offset"></param>
		/// <param name="len"></param>
		public static void Encrypt(byte[] byteArray, int offset, int len)
		{
			for (int i = 0; i < len; i++)
				byteArray[i + offset] ^= 0xa7;

		}
		/// <summary>
		/// 解密
		/// </summary>
		/// <param name="byteArray"></param>
		/// <param name="offset"></param>
		/// <param name="len"></param>
		public static void Decrypt(byte[] byteArray, int offset, int len)
		{
			for (int i = 0; i < len; i++)
				byteArray[i + offset] ^= 0xa7;

		}
	}
}
