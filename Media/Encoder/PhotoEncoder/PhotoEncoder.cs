using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using System.IO;

namespace MediaEncoder
{

	#region PhotoEncoder
	public class PhotoEncoder : EncoderBase, IDisposable
	{

		#region PhotoEncoder()
		public PhotoEncoder()
		{
			StatusString = "[----]";
		}
		#endregion

		#region Encode
		public override bool Encode(Photo p, Photo.EncoderStatusDelegate Status, Photo.MeaningfulActivityDelegate Active, int MinutesUntilKill1)
		{
			try
			{
				StatusString = "[STRT]";


				//lets see if upload temporary file exists...
				if (Storage.ExistsInStore(Storage.Stores.Temporary, p.UploadTemporary, p.UploadTemporaryExtention))
				{
					p.SaveWeb(Photo.SaveWebFileSourceLocations.UploadTemporary, p.Rotate, Status, Active);
				}
				else if (Storage.ExistsInStore(Storage.Stores.Master, p.Master, "jpg"))
				{
					p.SaveWeb(Photo.SaveWebFileSourceLocations.Master, p.Rotate, Status, Active);
				}
				else
				{
					//We don't have the upload temporary file OR the master file... so delete the photo?
					throw new Exception("Can't find image file to encode!");
				}

				Active();

				Photo.FinishProcessUploadedFile(p, p.Gallery, p.Usr);
				
				p.IsProcessing = false;
				p.Update();

				Active();

				Status(p.K + " Updating gallery etc...");

				Status(p.K + " Done!        ************************************");

			}
			catch (Exception ex)
			{
				Status(p.K + " Caught exception XXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
				Status(p.K + " Caught exception: " + ex.ToString());
				throw ex;
			}
			finally
			{
				Status(p.K + " Finalising...");
				StatusString = "[----]";
				Status(p.K + " Finalising done.");
			}
			return true;
		}
		#endregion

		#region Cancel()
		public override void Cancel()
		{
			//Encoder.EncodeAsyncAbort();
			//Encoder.Reset();

			if (CurrentPhoto.Status == Photo.StatusEnum.Enabled || CurrentPhoto.Status == Photo.StatusEnum.Moderate)
				return;

			Storage.RemoveFromStore(Storage.Stores.Master, CurrentPhoto.Master, "jpg");
			Storage.RemoveFromStore(Storage.Stores.Pix, CurrentPhoto.Web, "jpg");
			Storage.RemoveFromStore(Storage.Stores.Pix, CurrentPhoto.Thumb, "jpg");
			Storage.RemoveFromStore(Storage.Stores.Pix, CurrentPhoto.Icon, "jpg");
			if (CurrentPhoto.MediaType == Photo.MediaTypes.Video)
			{
				Storage.RemoveFromStore(Storage.Stores.Master, CurrentPhoto.VideoMaster, CurrentPhoto.VideoFileExtention);
				Storage.RemoveFromStore(Storage.Stores.Pix, CurrentPhoto.VideoMed, "flv");
			}
			if (CurrentPhoto.HasCrop)
				Storage.RemoveFromStore(Storage.Stores.Pix, CurrentPhoto.Crop, "jpg");

			CurrentPhoto.IsProcessing = false;
			CurrentPhoto.ProcessingProgress = 0;
			CurrentPhoto.ProcessingStartDateTime = DateTime.MinValue;
			CurrentPhoto.ProcessingLastChange = DateTime.MinValue;
			CurrentPhoto.Update();

			StatusString = "[----]";
		}
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			//if (Encoder != null)
			//	Encoder.Dispose();
		}

		#endregion



	}
	#endregion
}
