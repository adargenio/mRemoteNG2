using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using mRemoteNG.App.Runtime;

namespace mRemoteNG.UI
{
	namespace Window
	{
		public class UltraVNCSC : UI.Window.Base
		{

			#region "Form Init"
			internal System.Windows.Forms.ToolStrip tsMain;
			internal System.Windows.Forms.Panel pnlContainer;
			private System.Windows.Forms.ToolStripButton withEventsField_btnDisconnect;
			internal System.Windows.Forms.ToolStripButton btnDisconnect {
				get { return withEventsField_btnDisconnect; }
				set {
					if (withEventsField_btnDisconnect != null) {
						withEventsField_btnDisconnect.Click -= btnDisconnect_Click;
					}
					withEventsField_btnDisconnect = value;
					if (withEventsField_btnDisconnect != null) {
						withEventsField_btnDisconnect.Click += btnDisconnect_Click;
					}
				}

			}
			private void InitializeComponent()
			{
				System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UltraVNCSC));
				this.tsMain = new System.Windows.Forms.ToolStrip();
				this.btnDisconnect = new System.Windows.Forms.ToolStripButton();
				this.pnlContainer = new System.Windows.Forms.Panel();
				this.tsMain.SuspendLayout();
				this.SuspendLayout();
				//
				//tsMain
				//
				this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
				this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.btnDisconnect });
				this.tsMain.Location = new System.Drawing.Point(0, 0);
				this.tsMain.Name = "tsMain";
				this.tsMain.Size = new System.Drawing.Size(446, 25);
				this.tsMain.TabIndex = 0;
				this.tsMain.Text = "ToolStrip1";
				//
				//btnDisconnect
				//
				this.btnDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
				this.btnDisconnect.Image = (System.Drawing.Image)resources.GetObject("btnDisconnect.Image");
				this.btnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
				this.btnDisconnect.Name = "btnDisconnect";
				this.btnDisconnect.Size = new System.Drawing.Size(63, 22);
				this.btnDisconnect.Text = "Disconnect";
				//
				//pnlContainer
				//
				this.pnlContainer.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
				this.pnlContainer.Location = new System.Drawing.Point(0, 27);
				this.pnlContainer.Name = "pnlContainer";
				this.pnlContainer.Size = new System.Drawing.Size(446, 335);
				this.pnlContainer.TabIndex = 1;
				//
				//UltraVNCSC
				//
				this.ClientSize = new System.Drawing.Size(446, 362);
				this.Controls.Add(this.pnlContainer);
				this.Controls.Add(this.tsMain);
				this.Icon = global::mRemoteNG.My.Resources.UVNC_SC_Icon;
				this.Name = "UltraVNCSC";
				this.TabText = "UltraVNC SC";
				this.Text = "UltraVNC SC";
				this.tsMain.ResumeLayout(false);
				this.tsMain.PerformLayout();
				this.ResumeLayout(false);
				this.PerformLayout();

			}
			#endregion

			#region "Declarations"
			//Private WithEvents vnc As AxCSC_ViewerXControl
			#endregion

			#region "Public Methods"
			public UltraVNCSC(DockContent Panel)
			{
				Load += UltraVNCSC_Load;
				this.WindowType = Type.UltraVNCSC;
				this.DockPnl = Panel;
				this.InitializeComponent();
			}
			#endregion

			#region "Private Methods"
			private void UltraVNCSC_Load(object sender, System.EventArgs e)
			{
				ApplyLanguage();

				StartListening();
			}

			private void ApplyLanguage()
			{
				btnDisconnect.Text = mRemoteNG.My.Language.strButtonDisconnect;
			}

			private void StartListening()
			{
				try {
					//If vnc IsNot Nothing Then
					//    vnc.Dispose()
					//    vnc = Nothing
					//End If

					//vnc = New AxCSC_ViewerXControl()
					//SetupLicense()

					//vnc.Parent = pnlContainer
					//vnc.Dock = DockStyle.Fill
					//vnc.Show()

					//vnc.StretchMode = ViewerX.ScreenStretchMode.SSM_ASPECT
					//vnc.ListeningText = My.Language.strInheritListeningForIncomingVNCConnections & " " & My.Settings.UVNCSCPort

					//vnc.ListenEx(My.Settings.UVNCSCPort)
				} catch (Exception ex) {
					mRemoteNG.App.Runtime.MessageCollector.AddMessage(mRemoteNG.Messages.MessageClass.ErrorMsg, "StartListening (UI.Window.UltraVNCSC) failed" + Constants.vbNewLine + ex.Message, false);
					Close();
				}
			}

			private void SetupLicense()
			{
				try {
					//Dim f As System.Reflection.FieldInfo
					//f = GetType(AxHost).GetField("licenseKey", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
					//f.SetValue(vnc, "{072169039103041044176252035252117103057101225235137221179204110241121074}")
				} catch (Exception ex) {
					mRemoteNG.App.Runtime.MessageCollector.AddMessage(mRemoteNG.Messages.MessageClass.ErrorMsg, "VNC SetupLicense failed (UI.Window.UltraVNCSC)" + Constants.vbNewLine + ex.Message, true);
				}
			}

			//Private Sub vnc_ConnectionAccepted(ByVal sender As Object, ByVal e As AxViewerX._ISmartCodeVNCViewerEvents_ConnectionAcceptedEvent) Handles vnc.ConnectionAccepted
			//    mC.AddMessage(Messages.MessageClass.InformationMsg, e.bstrServerAddress & " is now connected to your UltraVNC SingleClick panel!")
			//End Sub

			//Private Sub vnc_Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles vnc.Disconnected
			//    StartListening()
			//End Sub

			private void btnDisconnect_Click(System.Object sender, System.EventArgs e)
			{
				//vnc.Dispose()
				Dispose();
				mRemoteNG.App.Runtime.Windows.Show(Type.UltraVNCSC);
			}
			#endregion

		}
	}
}
