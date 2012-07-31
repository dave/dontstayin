using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Controls
{
	public partial class Pic : System.Web.UI.UserControl
	{
		protected Panel NoPicPanel, PicPanel, PanelCropper;
		protected HtmlImage PicImg;
		protected PicCropper PicCropper;

		#region SelectedBob
		public IBob InputObject
		{
			set
			{
				inputObject = value;
			}
		}
		IBob inputObject;
		#endregion

		#region SelectedPic
		IPic ThisPic
		{
			get
			{
				if (ThisBob != null)
					return (IPic)ThisBob;
				else
					return null;
			}
		}
		IBob ThisBob
		{
			get
			{
				return inputObject;
			}
		}
		#endregion

		public void NoImageClick(object o, System.EventArgs e)
		{
			if (ActionNoPic != null)
				ActionNoPic(this, EventArgs.Empty);
		}

		public void SaveImageClick(object o, System.EventArgs e)
		{
			if (ActionSaved != null)
				ActionSaved(this, EventArgs.Empty);
		}

		public void OkClick(object o, System.EventArgs e)
		{
			Save();
		}

		public event EventHandler ActionSaved, ActionNoPic, ActionDeleted;

		public void Save()
		{
			ChangePanel(PicPanel);
			BindPicDisplay();
		}

		void BindPicDisplay()
		{
			PicImg.Src = ThisPic.PicPath + "?" + Cambro.Misc.Utility.GenRandomText(5);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (ThisPic != null)
				PicCropper.Pic = ThisPic;
		}
		public void InitPic()
		{
			PicCropper.Pic = ThisPic;
			PicCropper.BindPic();
			if (ThisPic.HasPic)
			{
				ChangePanel(PicPanel);
				BindPicDisplay();
			}
			else
				ChangePanel(NoPicPanel);

		}
		public void AddImageClick(object o, System.EventArgs e)
		{
			ChangePanel(PanelCropper);
			PicCropper.Pic = ThisPic;
			PicCropper.BindPic();
		}
		public void CancelCropperClick(object o, System.EventArgs e)
		{
			if (ThisPic.HasPic)
				ChangePanel(PicPanel);
			else
				ChangePanel(NoPicPanel);
		}
		public void DeleteImageClick(object o, System.EventArgs e)
		{
			if (!ThisPic.Pic.Equals(Guid.Empty))
			{
				Guid oldPic = ThisPic.Pic;
				int oldPicMiscK = ThisPic.PicMisc != null ? ThisPic.PicMiscK : 0;

				ThisPic.Pic = Guid.Empty;
				ThisPic.PicMiscK = 0;
				ThisPic.PicPhotoK = 0;
				ThisPic.PicState = "";
				ThisBob.Update();

				if (oldPic != Guid.Empty)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

				if (oldPicMiscK > 0)
				{
					Misc oldMisc = new Misc(oldPicMiscK);
					oldMisc.DeleteAll(null);
				}
			}

			if (ActionDeleted != null)
				ActionDeleted(this, EventArgs.Empty);

			ChangePanel(NoPicPanel);

		}
		void ChangePanel(Panel p)
		{
			NoPicPanel.Visible = p.Equals(NoPicPanel);
			PicPanel.Visible = p.Equals(PicPanel);
			PanelCropper.Visible = p.Equals(PanelCropper);
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
