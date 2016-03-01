using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace mRemoteNG.Connection
{
	namespace Protocol
	{
		public class Telnet : Connection.Protocol.PuttyBase
		{

			public Telnet()
			{
				this.PuttyProtocol = Putty_Protocol.telnet;
			}

			public enum Defaults
			{
				Port = 23
			}
		}
	}
}
