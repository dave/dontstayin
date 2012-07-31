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

namespace Spotted.Admin
{
	public partial class AddPic : AdminUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack && Request.QueryString["Type"] != null)
			{
				ObjectTypeList.SelectedValue = Request.QueryString["Type"];
				ObjectKTextBox.Text = int.Parse(Request.QueryString["K"]).ToString();
			}

			if (ObjectKTextBox.Text.Length > 0)
				BindPicDisplay();

			if (CurrentPic != null)
			{
				PicCropper.Pic = CurrentPic;
				if (!Page.IsPostBack)
					PicCropper.BindPic();
			}

			if (!Page.IsPostBack)
			{
				if (CurrentPic != null && (CurrentPic.PicMisc != null || CurrentPic.PicPhoto != null))
					ChangePanel(CropperPanel);
				else
					ChangePanel(ObjectPanel);
			}

		}

		#region ObjectK
		int ObjectK
		{
			get
			{
				if (ObjectKTextBox.Text.Length == 0)
					return 0;
				else
					return int.Parse(ObjectKTextBox.Text);
			}
		}
		#endregion
		#region CurrentBob
		IBob CurrentBob
		{
			get
			{
				if (currentBob == null)
				{
					if (ObjectTypeList.SelectedValue.Equals("Event"))
						currentBob = new Event(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Venue"))
						currentBob = new Venue(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Comp"))
						currentBob = new Comp(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Gallery"))
						currentBob = new Gallery(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Para"))
						currentBob = new Para(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Place"))
						currentBob = new Place(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Article"))
						currentBob = new Article(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Brand"))
						currentBob = new Brand(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Promoter"))
						currentBob = new Promoter(ObjectK);
					else if (ObjectTypeList.SelectedValue.Equals("Usr"))
						currentBob = new Usr(ObjectK);
					else
						throw new Exception("must select type");
				}
				return currentBob;
			}
			set
			{
				currentBob = value;
			}
		}
		IBob currentBob;
		#endregion
		#region CurrentPic
		IPic CurrentPic
		{
			get
			{
				return (IPic)CurrentBob;
			}
		}
		#endregion

		#region ObjectPanel
		protected Panel ObjectPanel;
		protected DropDownList ObjectTypeList;
		protected TextBox ObjectKTextBox;
		public void ObjectPanelAddClick(object o, System.EventArgs e)
		{
			ChangePanel(CropperPanel);
			PicCropper.BindPic();
		}
		protected Panel ViewPicPanel;
		protected HtmlImage ViewPicImg;
		public void ObjectPanelViewPicClick(object o, System.EventArgs e)
		{
			BindPicDisplay();
		}
		public void DeleteImage(object o, System.EventArgs e)
		{

			if (!CurrentPic.Pic.Equals(Guid.Empty))
			{
				Guid oldPic = CurrentPic.Pic;
				int oldPicMiscK = CurrentPic.PicMisc != null ? CurrentPic.PicMiscK : 0;

				CurrentPic.Pic = Guid.Empty;
				CurrentPic.PicMiscK = 0;
				CurrentPic.PicPhotoK = 0;
				CurrentPic.PicState = "";
				CurrentBob.Update();

				if (oldPic != Guid.Empty)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

				if (oldPicMiscK > 0)
				{
					Misc oldMisc = new Misc(oldPicMiscK);
					oldMisc.DeleteAll(null);
				}
			}

			BindPicDisplay();
			if (CurrentBob is IPage)
				Response.Redirect(((IPage)CurrentBob).Url());
		}
		void BindPicDisplay()
		{
			CurrentBob = null;
			if (CurrentPic.HasPic)
			{
				ViewPicPanel.Visible = true;
				ViewPicImg.Src = CurrentPic.PicPath + "?" + Cambro.Misc.Utility.GenRandomText(5);
			}
			else
			{
				ViewPicPanel.Visible = false;
			}
		}
		#endregion

		#region CropperPanel
		protected Panel CropperPanel;
		protected Spotted.Controls.PicCropper PicCropper;
		public void PicCropperBackClick(object o, System.EventArgs e)
		{
			ChangePanel(ObjectPanel);
		}
		public void PicCropperSaved(object o, System.EventArgs e)
		{
			BindPicDisplay();
			ChangePanel(ObjectPanel);
			if (CurrentBob is IPage)
				Response.Redirect(((IPage)CurrentBob).Url());
		}
		#endregion

		void ChangePanel(Panel p)
		{
			if (p.Equals(ObjectPanel))
				p.Visible = true;
			else
				ObjectPanel.Visible = false;

			if (p.Equals(CropperPanel))
				p.Visible = true;
			else
				CropperPanel.Visible = false;
		}
	}
}
