using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using System.IO;

namespace MediaEncoder
{

	public abstract class EncoderBase
	{
		//public virtual int MinutesUntilKill1 { get { return 8; } }
		//public virtual int MaxRetries { get { return 2; } }
		//public abstract Q PhotoQueryCondition { get; }
		public abstract bool Encode(Photo p, Photo.EncoderStatusDelegate Status, Photo.MeaningfulActivityDelegate Active, int MinutesUntilKill1);
		public abstract void Cancel();

		#region GetNextPhoto()
		public static Photo GetNextPhoto(Q PhotoQueryCondition, int MaxRetries)
		{

			Query q = new Query();
			q.QueryCondition = new And(
				PhotoQueryCondition,
				new Q(Photo.Columns.Status, Photo.StatusEnum.Processing),
				new Q(Photo.Columns.IsProcessing, false),
				new Or(
					new Q(Photo.Columns.ProcessingAttempts, QueryOperator.IsNull, null),
					new Q(Photo.Columns.ProcessingAttempts, QueryOperator.LessThan, MaxRetries)
				)
			);
			q.OrderBy = new OrderBy(new OrderBy(Photo.Columns.ProcessingAttempts), new OrderBy(Photo.Columns.K));
			q.TopRecords = 1;
			PhotoSet ps = new PhotoSet(q);
			if (ps.Count > 0)
				return ps[0];
			else
				return null;

		}
		#endregion

		#region GetNumberOfQueuedPhotos()
		public static int GetNumberOfQueuedPhotos(Q PhotoQueryCondition, int MaxRetries)
		{
			Query q = new Query();
			q.ReturnCountOnly = true;
			q.QueryCondition = new And(
				PhotoQueryCondition,
				new Q(Photo.Columns.Status, Photo.StatusEnum.Processing),
				new Q(Photo.Columns.IsProcessing, false),
				new Or(
					new Q(Photo.Columns.ProcessingAttempts, QueryOperator.IsNull, null),
					new Q(Photo.Columns.ProcessingAttempts, QueryOperator.LessThan, MaxRetries)
				)
			);
			PhotoSet ps = new PhotoSet(q);
			return ps.Count;
		}
		#endregion

		#region ProcessPhoto(EncoderBase e, Photo p)
		public static bool ProcessPhoto(EncoderBase CurrentEncoder, Photo CurrentPhoto, Photo.EncoderStatusDelegate Status, Photo.MeaningfulActivityDelegate Active, int MinutesUntilKill1)
		{
			#region Update photo
			CurrentPhoto.ProcessingServerName = System.Environment.MachineName;
			CurrentPhoto.IsProcessing = true;
			CurrentPhoto.ProcessingAttempts++;
			CurrentPhoto.ProcessingProgress = 0;
			CurrentPhoto.ProcessingStartDateTime = DateTime.Now;
			CurrentPhoto.ProcessingLastChange = CurrentPhoto.ProcessingStartDateTime;
			CurrentPhoto.Bob.OptimisticLocking = true;
			int updated = CurrentPhoto.Update();
			if (updated == 0)
				return false;
			#endregion
			CurrentPhoto.Bob.OptimisticLocking = false;

			try
			{
				Status("Encoding PhotoK-" + CurrentPhoto.K);
				Active();
				return CurrentEncoder.Encode(CurrentPhoto, Status, Active, MinutesUntilKill1);
			}
			catch (Exception ex)
			{
				Status("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
				Status("Exception encoding PhotoK-" + CurrentPhoto.K + ". Details:\n" + ex.ToString());
				EncoderBase.ResetPhoto(CurrentPhoto, Status);
				return false;
			}
			finally
			{
				CurrentPhoto = null;
			}
		}
		#endregion

		#region ResetPhoto(Photo p)
		public static void ResetPhoto(Photo p, Photo.EncoderStatusDelegate Status)
		{

			Status("Resetting PhotoK-" + p.K + " (" + p.ProcessingAttempts + " retries).");

			#region Delete videos
			if (p.MediaType == Photo.MediaTypes.Video)
			{
				Storage.RemoveFromStore(Storage.Stores.Pix, p.VideoMed, "flv");
				Storage.RemoveFromStore(Storage.Stores.Master, p.VideoMaster, p.VideoFileExtention);
				#region Delete temp preview jpg
				try
				{
					File.Delete(Storage.TemporaryFilesystemPath(p.VideoMed, "flv") + ".jpg");
				}
				catch { }
				#endregion
			}
			#endregion

			#region Delete images
			Storage.RemoveFromStore(Storage.Stores.Pix, p.Web, "jpg");
			Storage.RemoveFromStore(Storage.Stores.Pix, p.Thumb, "jpg");
			Storage.RemoveFromStore(Storage.Stores.Pix, p.Icon, "jpg");
			Storage.RemoveFromStore(Storage.Stores.Master, p.Master, "jpg");
			#endregion

			#region Reset the photo (retry)
			p.Status = Photo.StatusEnum.Processing;
			p.IsProcessing = false;
			p.ProcessingProgress = 0;
			p.ProcessingStartDateTime = DateTime.MinValue;
			p.ProcessingLastChange = DateTime.MinValue;
			p.Update();
			#endregion

		}
		#endregion

		#region StartDateTime
		public DateTime StartDateTime
		{
			get
			{
				return startDateTime;
			}
			set
			{
				startDateTime = value;
			}
		}
		DateTime startDateTime;
		#endregion

		#region StatusString
		public string StatusString
		{
			get
			{
				return statusString;
			}
			set
			{
				statusString = value;
			}
		}
		string statusString;
		#endregion

		#region CurrentPhoto
		public Photo CurrentPhoto
		{
			get
			{
				return currentPhoto;
			}
			set
			{
				currentPhoto = value;
			}
		}
		Photo currentPhoto;
		#endregion

		#region IsIdle
		public bool IsIdle
		{
			get
			{
				return (bool)idle;
			}
		}
		#endregion
		#region TakeIfIdle()
		public bool TakeIfIdle()
		{
			lock (this.idle)
			{
				if ((bool)this.idle)
				{
					this.idle = false;
					return true;
				}
				else
					return false;
			}
		}
		#endregion
		#region Release()
		public void Release()
		{
			this.idle = true;
		}
		#endregion
		object idle = true;

	}




}
