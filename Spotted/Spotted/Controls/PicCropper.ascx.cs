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
using System.Drawing.Imaging;
using System.IO;

namespace Spotted.Controls
{
	public partial class PicCropper : System.Web.UI.UserControl
	{
		protected HtmlInputFile InputFile;
		public Cropper Cropper;
		protected Button SaveButton;
		protected Panel JpgErrorPanel;
		#region Pic
		public IPic Pic
		{
			get
			{
				return pic;
			}
			set
			{
				pic = value;
			}
		}
		private IPic pic;
		#endregion
		#region Upload
		public void Upload(object o, System.EventArgs e)
		{
			if (Pic == null)
				throw new Exception("No Pic!!!");
			if (InputFile.PostedFile != null && InputFile.PostedFile.ContentLength > 0)
			{
				if (Photo.GetMediaType(InputFile.PostedFile.FileName).Equals(Photo.MediaTypes.Image))
				{
				//	byte[] uploadedFileBytes = new byte[InputFile.PostedFile.InputStream.Length];
				//	InputFile.PostedFile.InputStream.Read(uploadedFileBytes, 0, (int)InputFile.PostedFile.InputStream.Length);

					//using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(uploadedFileBytes)))
					using (System.Drawing.Image image = System.Drawing.Image.FromStream(InputFile.PostedFile.InputStream))
					{
						if (image.Height < 100 || image.Width < 100)
						{
							throw new Bobs.DsiUserFriendlyException("Image is too small - it must be at least 100x100 pixels.");
						}
						else if (image.Height == 100 && image.Width == 100)
						{
							//save file straight away.
							if (!Pic.HasPic)
								Pic.Pic = Guid.NewGuid();

							int oldMiscK = Pic.PicMisc != null ? Pic.PicMiscK : 0;
							
							
							Pic.PicState = "";
							Pic.PicMiscK = 0;

							Storage.AddToStore(
								Photo.SaveJPGWithCompressionSetting(image, (long)85.0),
								Storage.Stores.Pix,
								Pic.Pic,
								"jpg",
								(IBob)Pic,
								"Pic");

							((IBob)Pic).Update();

							if (oldMiscK > 0)
							{
								Misc oldMisc = new Misc(oldMiscK);
								oldMisc.DeleteAll(null);
							}

							if (Saved != null)
								Saved(this, new EventArgs());

							return;

						}
						else
						{
							bool newMisc = false;
							Bobs.Misc m = null;
							if (Pic.PicMisc == null)
							{
								m = new Bobs.Misc();
								newMisc = true;
							}
							else
							{
								m = Pic.PicMisc;
							}

							m.Extention = "jpg";

							bool deletePreviousFile = false;
							Guid previousFile = Guid.Empty;
							if (!m.Guid.Equals(Guid.Empty))
							{
								deletePreviousFile = true;
								previousFile = m.Guid;
							}

							m.Guid = Guid.NewGuid();

							byte[] bytes = null;
							if (image.Height > 1000 || image.Width > 1000)
							{
								Photo.OperationReturn operation = Photo.Operation(image, Photo.OperationType.MaxSide, new Photo.OperationParams() { MaxSide = 1000, ReturnBytes = true, AllowMaxSideToEnlarge = true });
								m.Width = operation.ImageSize.Width;
								m.Height = operation.ImageSize.Height;
								bytes = operation.Bytes;
							}
							else
							{
								bytes = Photo.SaveJPGWithCompressionSetting(image, (long)85.0);
								m.Width = image.Width;
								m.Height = image.Height;
							}
							
							if (newMisc)
								m.Update();

							try
							{
								Storage.AddToStore(bytes, Storage.Stores.Pix, m.Guid, m.Extention, m, "");
							}
							catch(Exception ex)
							{
								if (newMisc)
								{
									m.Delete();
								}
								throw ex;
							}

							m.Size = bytes.Length;
							m.UsrK = Usr.Current.K;
							m.PromoterK = 0;
							m.DateTime = DateTime.Now;
							m.DateTimeExpires = DateTime.Now.AddHours(1);
							m.Name = InputFile.PostedFile.FileName.ToLower();

							m.Update();

							if (Pic.PicMiscK != m.K)
							{
								Pic.PicMiscK = m.K;
								((IBob)Pic).Update();
							}
							
							if (deletePreviousFile)
								Storage.RemoveFromStore(Storage.Stores.Pix, previousFile, "jpg");

							Cropper.ImageUrl = m.Url();
							Cropper.ImageGuid = m.Guid;
							Cropper.ImageStore = Storage.Stores.Pix;
							SaveButton.Visible = true;
							Cropper.Visible = true;
							JpgErrorPanel.Visible = false;

						}
					}
				}
				else
				{
					JpgErrorPanel.Visible = true;
				}
			}

		}
		#endregion
		public void BindPic()
		{
			if (Pic.PicPhoto != null)
			{
				Cropper.ImageUrl = Pic.PicPhoto.CropPath;
				Cropper.ImageGuid = Pic.Pic;
				Cropper.ImageStore = Storage.Stores.Pix;
			}
			else if (Pic.PicMisc != null)
			{
				Cropper.ImageUrl = Pic.PicMisc.Url();
				Cropper.ImageGuid = Pic.PicMisc.Guid;
				Cropper.ImageStore = Storage.Stores.Pix;
			}
			else
			{
				SaveButton.Visible = false;
				Cropper.Visible = false;
			}
			if (Pic.PicState.Length > 0)
				Cropper.SetState(Pic.PicState);
		}
		public void Page_Load(object o, System.EventArgs e)
		{

		}
		public void Page_PreRender(object o, System.EventArgs e)
		{
		}
		public void Back_Click(object o, System.EventArgs e)
		{
			if (BackClick != null)
				BackClick(this, new EventArgs());
		}
		public void Save_Click(object o, System.EventArgs e)
		{
			Guid g = Guid.NewGuid();

			bool hasOldPic = Pic.HasPic;
			Guid oldPic = Pic.HasPic ? Pic.Pic : Guid.Empty;
			
			Pic.Pic = g;
			Pic.PicState = Cropper.GetState();

			Cropper.Store(g, (IBob)Pic, "Pic");

			((IBob)Pic).Update();

			if (hasOldPic)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

			if (Saved != null)
				Saved(this, new EventArgs());
		}
		public event EventHandler Saved;
		public event EventHandler BackClick;

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
