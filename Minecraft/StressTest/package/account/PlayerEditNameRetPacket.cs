﻿using BaseCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest.package
{
	class PlayerEditNameRetPacket : Package
	{
		public PlayerEditNameRetPacket() { }
		public PlayerEditNameRetPacket(byte[] buffer, int msgLen, MainCommand mainid, SecondCommand secondid) : base(buffer, msgLen, mainid, secondid) { }
		public override Package Clone()
		{
			Package pack = new PlayerEditNameRetPacket(null, 0,
				MainCommand.MC_ACCOUNT, SecondCommand.SC_ACCOUNT_playerEditName_ret);
			return pack;
		}
		public override void ReadPackage() { }
		public override void WritePackage() { }
		public override void Excute()
		{
			int key = ReadInt();
			ThreadPoolPlayerEditName.UpdateEndTime(key);
		}
	}
}
