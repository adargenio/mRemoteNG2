using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace mRemoteNG.Images
{
	public class Enums
	{
		public enum TreeImage
		{
			Root = 0,
			Container = 1,
			ConnectionOpen = 2,
			ConnectionClosed = 3,
			PuttySessions = 4
		}

		public enum ErrorImage
		{
			_Information = 0,
			_Warning = 1,
			_Error = 2
		}
	}
}
