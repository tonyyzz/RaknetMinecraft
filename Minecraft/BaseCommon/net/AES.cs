using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;

namespace BaseCommon
{
	public class AES
	{
		/// <summary>
		/// 获取密钥
		/// </summary>
		private static string Key = @"5p)O[NB]6,YF}+ef";
		/// <summary>
		/// 获取向量
		/// </summary>
		private static string IV = @"L+#f4,Ir)b$=pkfl";

		private static byte[] keyArray = null;
		private static byte[] IVArray = null;

		private static byte[] GetKeyArray()
		{
			if (keyArray == null)
			{
				byte[] tmp = Encoding.UTF8.GetBytes(Key);
				MD5 md5 = new MD5CryptoServiceProvider();
				keyArray = md5.ComputeHash(tmp);

			}
			return keyArray;
		}
		private static byte[] GetIVArray()
		{
			if (IVArray == null)
			{
				byte[] tmp = Encoding.UTF8.GetBytes(IV);
				MD5 md5 = new MD5CryptoServiceProvider();
				IVArray = md5.ComputeHash(tmp);
			}
			return IVArray;
		}
		/// <summary>
		/// AES加密
		/// </summary>
		/// <param name="byteArray">明文</param>
		/// <returns>密文</returns>
		public static byte[] AESEncrypt(byte[] byteArray)
		{
			byte[] bKey = GetKeyArray();
			byte[] bIV = GetIVArray();


			byte[] encrypt = null;
			try
			{
				using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
				{
					rijndaelCipher.Mode = CipherMode.CBC;
					rijndaelCipher.Padding = PaddingMode.PKCS7;
					rijndaelCipher.KeySize = 128;
					rijndaelCipher.BlockSize = 128;
					rijndaelCipher.Key = bKey;
					rijndaelCipher.IV = bIV;
					using (ICryptoTransform transform = rijndaelCipher.CreateEncryptor())
					{
						encrypt = transform.TransformFinalBlock(byteArray, 0, byteArray.Length);
					}

					rijndaelCipher.Clear();
				}
			}
			catch { }



			return encrypt;
		}


		/// <summary>
		/// AES解密
		/// </summary>
		/// <param name="byteArray">密文</param>
		/// <returns>明文</returns>
		public static byte[] AESDecrypt(byte[] byteArray, int offset, int len)
		{
			byte[] bKey = GetKeyArray();
			byte[] bIV = GetIVArray();

			byte[] decrypt = null;

			try
			{
				using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
				{
					rijndaelCipher.Mode = CipherMode.CBC;
					rijndaelCipher.Padding = PaddingMode.PKCS7;
					rijndaelCipher.KeySize = 128;
					rijndaelCipher.BlockSize = 128;
					rijndaelCipher.Key = bKey;
					rijndaelCipher.IV = bIV;
					using (ICryptoTransform transform = rijndaelCipher.CreateDecryptor())
					{
						decrypt = transform.TransformFinalBlock(byteArray, offset, len);
					}

					rijndaelCipher.Clear();
				}
			}
			catch (Exception ex)
			{
				Log.WriteError(ex);
			}



			return decrypt;
		}


	}
}
