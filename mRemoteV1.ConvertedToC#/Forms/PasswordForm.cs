using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using mRemoteNG.My;

namespace mRemoteNG.Forms
{
	public partial class PasswordForm
	{
		#region "Public Properties"
		public bool Verify { get; set; }

		public string Password {
			get {
				if (Verify) {
					return txtVerify.Text;
				} else {
					return txtPassword.Text;
				}
			}
		}
		#endregion


		#region "Constructors"
		public PasswordForm(string passwordName = null, bool verify = true)
		{
			Load += frmPassword_Load;
			// This call is required by the designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			_passwordName = passwordName;
			this.Verify = verify;
		}
		#endregion

		#region "Event Handlers"
		private void frmPassword_Load(object sender, EventArgs e)
		{
			ApplyLanguage();

			if (!Verify) {
				Height = Height - (txtVerify.Top - txtPassword.Top);
				lblVerify.Visible = false;
				txtVerify.Visible = false;
			}
		}

		private void btnCancel_Click(System.Object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void btnOK_Click(System.Object sender, EventArgs e)
		{
			if (Verify) {
				if (VerifyPassword())
					DialogResult = DialogResult.OK;
			} else {
				DialogResult = DialogResult.OK;
			}
		}

		private void txtPassword_TextChanged(System.Object sender, EventArgs e)
		{
			HideStatus();
		}
		#endregion

		#region "Private Fields"
			#endregion
		private readonly string _passwordName;

		#region "Private Methods"
		private void ApplyLanguage()
		{
			if (string.IsNullOrEmpty(_passwordName)) {
				Text = Language.strTitlePassword;
			} else {
				Text = string.Format(Language.strTitlePasswordWithName, _passwordName);
			}

			lblPassword.Text = Language.strLabelPassword;
			lblVerify.Text = Language.strLabelVerify;

			btnCancel.Text = Language.strButtonCancel;
			btnOK.Text = Language.strButtonOK;
		}

		private bool VerifyPassword()
		{
			if (txtPassword.Text.Length >= 3) {
				if (txtPassword.Text == txtVerify.Text) {
					return true;
				} else {
					ShowStatus(Language.strPasswordStatusMustMatch);
					return false;
				}
			} else {
				ShowStatus(Language.strPasswordStatusTooShort);
				return false;
			}
		}

		private void ShowStatus(string status)
		{
			lblStatus.Visible = true;
			lblStatus.Text = status;
		}

		private void HideStatus()
		{
			lblStatus.Visible = false;
		}
		#endregion
	}
}
