using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using mRemoteNG.My;

namespace mRemoteNG.Forms.OptionsPages
{
	public partial class TabsPanelsPage
	{
		public override string PageName {
			get { return Language.strTabsAndPanels.Replace("&&", "&"); }
			set { }
		}

		public override void ApplyLanguage()
		{
			base.ApplyLanguage();

			chkAlwaysShowPanelTabs.Text = Language.strAlwaysShowPanelTabs;
			chkOpenNewTabRightOfSelected.Text = Language.strOpenNewTabRight;
			chkShowLogonInfoOnTabs.Text = Language.strShowLogonInfoOnTabs;
			chkShowProtocolOnTabs.Text = Language.strShowProtocolOnTabs;
			chkIdentifyQuickConnectTabs.Text = Language.strIdentifyQuickConnectTabs;
			chkDoubleClickClosesTab.Text = Language.strDoubleClickTabClosesIt;
			chkAlwaysShowPanelSelectionDlg.Text = Language.strAlwaysShowPanelSelection;

			chkUseOnlyErrorsAndInfosPanel.Text = Language.strUseOnlyErrorsAndInfosPanel;
			lblSwitchToErrorsAndInfos.Text = Language.strSwitchToErrorsAndInfos;
			chkMCInformation.Text = Language.strInformations;
			chkMCWarnings.Text = Language.strWarnings;
			chkMCErrors.Text = Language.strErrors;
		}

		public override void LoadSettings()
		{
			base.SaveSettings();

			chkAlwaysShowPanelTabs.Checked = Settings.AlwaysShowPanelTabs;
			chkOpenNewTabRightOfSelected.Checked = Settings.OpenTabsRightOfSelected;
			chkShowLogonInfoOnTabs.Checked = Settings.ShowLogonInfoOnTabs;
			chkShowProtocolOnTabs.Checked = Settings.ShowProtocolOnTabs;
			chkIdentifyQuickConnectTabs.Checked = Settings.IdentifyQuickConnectTabs;
			chkDoubleClickClosesTab.Checked = Settings.DoubleClickOnTabClosesIt;
			chkAlwaysShowPanelSelectionDlg.Checked = Settings.AlwaysShowPanelSelectionDlg;

			chkUseOnlyErrorsAndInfosPanel.Checked = Settings.ShowNoMessageBoxes;
			chkMCInformation.Checked = Settings.SwitchToMCOnInformation;
			chkMCWarnings.Checked = Settings.SwitchToMCOnWarning;
			chkMCErrors.Checked = Settings.SwitchToMCOnError;
		}

		public override void SaveSettings()
		{
			base.SaveSettings();

			Settings.AlwaysShowPanelTabs = chkAlwaysShowPanelTabs.Checked;
			My.MyProject.Forms.frmMain.ShowHidePanelTabs();

			Settings.OpenTabsRightOfSelected = chkOpenNewTabRightOfSelected.Checked;
			Settings.ShowLogonInfoOnTabs = chkShowLogonInfoOnTabs.Checked;
			Settings.ShowProtocolOnTabs = chkShowProtocolOnTabs.Checked;
			Settings.IdentifyQuickConnectTabs = chkIdentifyQuickConnectTabs.Checked;
			Settings.DoubleClickOnTabClosesIt = chkDoubleClickClosesTab.Checked;
			Settings.AlwaysShowPanelSelectionDlg = chkAlwaysShowPanelSelectionDlg.Checked;

			Settings.ShowNoMessageBoxes = chkUseOnlyErrorsAndInfosPanel.Checked;
			Settings.SwitchToMCOnInformation = chkMCInformation.Checked;
			Settings.SwitchToMCOnWarning = chkMCWarnings.Checked;
			Settings.SwitchToMCOnError = chkMCErrors.Checked;
		}

		private void chkUseOnlyErrorsAndInfosPanel_CheckedChanged(object sender, EventArgs e)
		{
			chkMCInformation.Enabled = chkUseOnlyErrorsAndInfosPanel.Checked;
			chkMCWarnings.Enabled = chkUseOnlyErrorsAndInfosPanel.Checked;
			chkMCErrors.Enabled = chkUseOnlyErrorsAndInfosPanel.Checked;
		}
		public TabsPanelsPage()
		{
			InitializeComponent();
		}
	}
}
