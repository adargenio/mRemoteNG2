using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using mRemoteNG.Messages;
using mRemoteNG.Tools.LocalizedAttributes;
using mRemoteNG.My;
using mRemoteNG.App.Runtime;
using mRemoteNG.Tools;

namespace mRemoteNG.Connection
{
	namespace PuttySession
	{
		public class Info : Connection.Info, IComponent
		{

			#region "Commands"
			[Command(), LocalizedDisplayName("strPuttySessionSettings")]
			public void SessionSettings()
			{
				try {
					PuttyProcessController puttyProcess = new PuttyProcessController();
					if (!puttyProcess.Start())
						return;
					if (puttyProcess.SelectListBoxItem(PuttySession)) {
						puttyProcess.ClickButton("&Load");
					}
					puttyProcess.SetControlText("Button", "&Cancel", "&Close");
					puttyProcess.SetControlVisible("Button", "&Open", false);
					puttyProcess.WaitForExit();
				} catch (Exception ex) {
					mRemoteNG.App.Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg, Language.strErrorCouldNotLaunchPutty + Constants.vbNewLine + ex.Message, false);
				}
			}
			#endregion

			#region "Properties"
			[Browsable(false)]
			public Root.PuttySessions.Info RootPuttySessionsInfo { get; set; }

			[ReadOnly(true)]
			public override string PuttySession { get; set; }

			[ReadOnly(true)]
			public override string Name { get; set; }

			[ReadOnly(true), Browsable(false)]
			public override string Description { get; set; }

			[ReadOnly(true), Browsable(false)]
			public override string Icon {
				get { return "PuTTY"; }

				set { }
			}

			[ReadOnly(true), Browsable(false)]
			public override string Panel {
				get { return RootPuttySessionsInfo.Panel; }

				set { }
			}

			[ReadOnly(true)]
			public override string Hostname { get; set; }

			[ReadOnly(true)]
			public override string Username { get; set; }

			[ReadOnly(true), Browsable(false)]
			public override string Password { get; set; }

			[ReadOnly(true)]
			public override Protocol.Protocols Protocol { get; set; }

			[ReadOnly(true)]
			public override int Port { get; set; }

			[ReadOnly(true), Browsable(false)]
			public override string PreExtApp { get; set; }

			[ReadOnly(true), Browsable(false)]
			public override string PostExtApp { get; set; }

			[ReadOnly(true), Browsable(false)]
			public override string MacAddress { get; set; }

			[ReadOnly(true), Browsable(false)]
			public override string UserField { get; set; }
			#endregion

			#region "IComponent"
			[Browsable(false)]
			public ISite Site {
				get { return new PropertyGridCommandSite(this); }
				set {
					throw new NotImplementedException();
				}
			}

			public void Dispose()
			{
				if (Disposed != null) {
					Disposed(this, EventArgs.Empty);
				}
			}

			public event EventHandler IComponent.Disposed;
			#endregion
		}
	}
}

