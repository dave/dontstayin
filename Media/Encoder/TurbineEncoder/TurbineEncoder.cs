using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using System.IO;

namespace MediaEncoder
{
	#region TurbineEncoder
	public class TurbineEncoder : EncoderBase, IDisposable
	{

		#region TurbineEncoder()
		public TurbineEncoder()
		{
			StatusString = "[----]";
		}
		Turbine.TVE2 Encoder;
		#endregion

		#region Encode
		public override bool Encode(Photo p, Photo.EncoderStatusDelegate Status, Photo.MeaningfulActivityDelegate Active, int MinutesUntilKill1)
		{
			try
			{
				if (Active != null) Active();
				bool justCopiedFromMaster = false;
				if (!Storage.ExistsInStore(Storage.Stores.Temporary, p.UploadTemporary, p.UploadTemporaryExtention))
				{
					//we don't have a temporary upload file... 
					if (Storage.ExistsInStore(Storage.Stores.Master, p.VideoMaster, p.VideoFileExtention))
					{
						//we have a master video file, so lets copy it to a new UploadTemporary location...
						p.UploadTemporary = Guid.NewGuid();
						p.UploadTemporaryExtention = p.VideoFileExtention;
						byte[] bytes = Storage.GetFromStore(Storage.Stores.Master, p.VideoMaster, p.VideoFileExtention);
						Storage.AddToStore(
							bytes,
							Storage.Stores.Temporary,
							p.UploadTemporary,
							p.UploadTemporaryExtention,
							p,
							"UploadTemporary");
						p.VideoMasterFileSize = bytes.Length;
						bytes = null;
						justCopiedFromMaster = true;
						p.Update();
					}
					else
					{
						//we don't have an upload temporary file or a master file, so lets throw an exception.
						throw new Exception("Can't find video file to encode!");
					}
				}

				StatusString = "[STRT]";

				CurrentPhoto = p;

				if (Active != null) Active();
				Status(p.K + " Initialising encoder...");

				#region Initialise encoder
				if (Encoder == null)
					Encoder = new Turbine.TVE2();
				Encoder.Key1 = 193279817;
				Encoder.Key2 = 1538568029;
				Encoder.BinDirectory = @"C:\Program Files\TVE2SDK\plugins";// set BinDirectory to the right location for the plugins/ directory and mp3 encoder dll to be found:
				#endregion

				#region Set processing progress to -1
				CurrentPhoto.ProcessingProgress = -1;
				CurrentPhoto.ProcessingLastChange = DateTime.Now;
				CurrentPhoto.Update();
				#endregion

				if (Active != null) Active();
				Status(p.K + " adding to Master store...");

				#region Add to Master store
				if (!justCopiedFromMaster)
				{
					byte[] bytes = Storage.GetFromStore(Storage.Stores.Temporary, p.UploadTemporary, p.UploadTemporaryExtention);
					Storage.AddToStore(
						bytes,
						Storage.Stores.Master,
						p.VideoMaster,
						p.VideoFileExtention,
						p,
						"VideoMaster");
					p.VideoMasterFileSize = bytes.Length;
					bytes = null;
				}
				#endregion

				if (Active != null) Active();
				Status(p.K + " Getting info...");

				#region Get info...
				bool info = Encoder.InfoOpen(Storage.TemporaryFilesystemPath(p.UploadTemporary, p.UploadTemporaryExtention));
				CurrentPhoto.VideoDuration = (int)Encoder.InfoGet("totalDurationMs");
				CurrentPhoto.VideoMasterHeight = (int)Encoder.InfoGet("videoHeight");
				CurrentPhoto.VideoMasterWidth = (int)Encoder.InfoGet("videoWidth");
				CurrentPhoto.VideoMasterFramerate = (double)Encoder.InfoGet("videoWidth");
				int audioSampleRate = (int)Encoder.InfoGet("audioSampleRate");
				int audioBitsPerSample = (int)Encoder.InfoGet("audioBitsPerSample");
				Encoder.InfoClose();
				#endregion

				#region Set processing progress to -2
				CurrentPhoto.ProcessingProgress = -2;
				CurrentPhoto.ProcessingLastChange = DateTime.Now;
				CurrentPhoto.Update();
				#endregion

				if (Active != null) Active();
				Status(p.K + " Getting JPG preview...");

				#region Get JPG preview
				int msHalf = (int)Math.Floor(CurrentPhoto.VideoDuration / 2.0);
				Encoder.SetOutputFile(Storage.TemporaryFilesystemPath(p.VideoMed, "flv"));
				Encoder.OutputExportFrameJPEGTimeMs = msHalf;
				Encoder.OutputExportFrameJPEGQuality = 85;
				Encoder.Encode(Storage.TemporaryFilesystemPath(p.UploadTemporary, p.UploadTemporaryExtention), msHalf - 1, msHalf + 1, true, true, true);
				Encoder.EncodeFlush();

				if (!File.Exists(Storage.TemporaryFilesystemPath(p.VideoMed, "flv") + ".jpg"))
				{
					Status(p.K + " Couldn't find JPG preview - throwing exception...");
					throw new Exception("Couldn't find captured frame... Maybe a currupt video?");
				}

				Storage.AddToStore(
					File.ReadAllBytes(Storage.TemporaryFilesystemPath(p.VideoMed, "flv") + ".jpg"),
					Storage.Stores.Master,
					p.Master,
					"jpg",
					p,
					"Master");
				
				CurrentPhoto.SaveWeb(Photo.SaveWebFileSourceLocations.Master, 0, Status, Active, false);
				#endregion

				#region Set processing progress to -3
				CurrentPhoto.ProcessingProgress = -3;
				CurrentPhoto.ProcessingLastChange = DateTime.Now;
				CurrentPhoto.Update();
				#endregion

				if (Active != null) Active();
				Status(p.K + " Preparing for encode...");

				#region Reset encoder
				Encoder.Reset();
				Encoder.Key1 = 193279817;
				Encoder.Key2 = 1538568029;
				Encoder.BinDirectory = @"C:\Program Files\TVE2SDK\plugins";// set BinDirectory to the right location for the plugins/ directory and mp3 encoder dll to be found:
				#endregion

				#region Video settings
				Encoder.VideoRelativeSize = 0;
				if (CurrentPhoto.VideoMasterWidth >= CurrentPhoto.VideoMasterHeight)
				{
					if (CurrentPhoto.VideoMasterWidth >= 600)
					{
						Encoder.VideoWidth = 600;
						Encoder.VideoHeight = (int)Math.Floor((600.0 / CurrentPhoto.VideoMasterWidth) * CurrentPhoto.VideoMasterHeight);
					}
					else
					{
						Encoder.VideoWidth = CurrentPhoto.VideoMasterWidth;
						Encoder.VideoHeight = CurrentPhoto.VideoMasterHeight;
					}
				}
				else
				{
					if (CurrentPhoto.VideoMasterHeight >= 600)
					{
						Encoder.VideoHeight = 600;
						Encoder.VideoWidth = (int)Math.Floor((600.0 / CurrentPhoto.VideoMasterHeight) * CurrentPhoto.VideoMasterWidth);
					}
					else
					{
						Encoder.VideoWidth = CurrentPhoto.VideoMasterWidth;
						Encoder.VideoHeight = CurrentPhoto.VideoMasterHeight;
					}
				}
				CurrentPhoto.VideoMedWidth = Encoder.VideoWidth;
				CurrentPhoto.VideoMedHeight = Encoder.VideoHeight;
				int maxEncodedSide = Encoder.VideoWidth > Encoder.VideoHeight ? Encoder.VideoWidth : Encoder.VideoHeight;


				if (CurrentPhoto.VideoMasterFramerate > 30.0)
				{
					Encoder.VideoRelativeFrameRate = 0;
					Encoder.VideoFrameRate = 30;
					CurrentPhoto.VideoMedFramerate = 30.0;
				}
				else
				{
					Encoder.VideoRelativeFrameRate = 100;
					Encoder.VideoFrameRate = 0;
					CurrentPhoto.VideoMedFramerate = CurrentPhoto.VideoMasterFramerate;
				}



				#region Set processing progress = -4
				CurrentPhoto.ProcessingProgress = -4;
				CurrentPhoto.ProcessingLastChange = DateTime.Now;
				CurrentPhoto.Update();
				#endregion

				Encoder.VideoMethod = "VBR1"; //Video encoding methods available: CBR (constant bitrate) VBR1 (variable bitrate 1-pass) VBR2 (variable bitrate 2-pass) KQ (constant quality)
				
				if (maxEncodedSide > 1000)
					Encoder.VideoBitRate = 1572864;
				else if (maxEncodedSide > 600)
					Encoder.VideoBitRate = 1048576;
				else if (maxEncodedSide > 300)
					Encoder.VideoBitRate = 786432;
				else
					Encoder.VideoBitRate = 524288;

				Encoder.VideoQuantizer = 0; //If encoding method is KQ, the constant quality quantizer, from 1 (best quality) to 31 (best compression)
				Encoder.VideoMotionEstimation = "optimal"; // The motion estimation algorithm: "optimal" (best speed) or "deep" (best quality but slower)
				Encoder.VideoBufferTimeMs = 0; //The assumed download buffer interval (size) in milliseconds. Use 0 for the default of 3000 milliseconds

				Encoder.VideoIntraFramePeriod = 0; //Force an intra-frame every x frames: The player can only seek to intra-frames, so this value should be reasonable if seeking is to be allowed. Only valid if relativeIntraFramePeriod is 0, otherwise that setting is used 
				Encoder.VideoRelativeIntraFramePeriod = 2; //Force an intra-frame every x seconds
				Encoder.VideoDeblocking = true; //Enable/disable image deblocking in the player. Use 1 or 0
				Encoder.VideoSmoothing = true; //will enable extra smoothing in the player
				Encoder.VideoLastFrameIntra = true; //will encode the last frame as an intra-frame, which will allow player seeking to the end of the video

				Encoder.VideoDeInterlace = "none"; //Deinterlace settings: none, odd (deinterlace odd fields), even (deinterlace even fields), auto (deinterlace if source has sizes 720x480-486 or 720x576-584)

				Encoder.VideoCropAreaBottom = 0; //The left,top,right,bottom pixel coordinates of the crop window in the source video. Set to 0,0,0,0 to disable
				Encoder.VideoCropAreaLeft = 0;
				Encoder.VideoCropAreaRight = 0;
				Encoder.VideoCropAreaTop = 0;

				Encoder.VideoNoiseReduction = 0; //Video noise reduction: 0=none, 1=light, 2=heavy
				Encoder.VideoGamma = 0; //Gamma correction from -100 (gamma=0.2) to +100 (gamma=5), use 0 for none
				Encoder.VideoContrast = 0; //Contrast from -100 to +100, use 0 for none
				Encoder.VideoBrightness = 0; //Brightness from -100 to +100, use 0 for none
				Encoder.VideoBlackRestore = 0; //Black pixel restore operation between 0 and 200, use 0 for none. Higher values will set more pixels to black
				Encoder.VideoWhiteRestore = 0; //White pixel restore operation between 0 and 200, use 0 for none. Higher values will set more pixels to white
				Encoder.VideoFx = "";

				Encoder.VideoImageOverlay = @"c:\overlay.png"; //Vars.MapPath("~/gfx/overlay-video.png"); //The location of an image to use as overlay for the video. Possible formats are: JPEG, PNG, GIF, BMP. Use "" for none
				Encoder.VideoImageOverlayAlignX = 0; //The horizontal alignment for the image overlay: 0 (align left), -1 (align center), -2 (align right). Or a positive coordinate that will be used for the left side of the overlay image
				Encoder.VideoImageOverlayAlignY = -2; //The vertical alignment for the image overlay: 0 (align top), -1 (align center), -2 (align bottom). Or a positive coordinate that will be used for the top side of the overlay image
				Encoder.VideoImageOverlayAlpha = 100; //A transparency value for the overlay, between 0 (fully transparent) and 100 (opaque)
				#endregion

				#region Audio options
				Encoder.AudioEncoder = "MP3"; //The audio encoder: none, ADPCM, MP3. Use "none" for no audio encoding
				Encoder.AudioSamplingRate = 22050; //The sampling rate at which to encode audio: 5512(ADPCM only), 11025, 22050, 44100
				Encoder.AudioChannels = 2; //Number of channels: 1 (mono), 2 (stereo). If source is mono will always encode as mono
				Encoder.AudioADPCMBits = 4; //ADPCM encoding only: bits per sample quality: 2, 3, 4, 5 bits per sample
				//Encoder.AudioMP3BitRate = 128000; //The MP3 encoding bitrate in bits per second, one of: 8000,16000,24000,32000,40000,48000,56000,64000,80000,96000,112000,128000,160000,192000,224000,256000,320000

				if (maxEncodedSide > 1000)
					Encoder.AudioMP3BitRate = 192000;
				else if (maxEncodedSide > 600)
					Encoder.AudioMP3BitRate = 128000;
				else if (maxEncodedSide > 300)
					Encoder.AudioMP3BitRate = 96000;
				else
					Encoder.AudioMP3BitRate = 48000;

				Encoder.AudioMP3ABR = true;	//Audio flags: "mp3ABR" encodes using Average BitRate, a variable bitrate encoding method that aims to reach the average bitrate specified in the mp3BitRate parameter, by using higher bitrates where necessary and lower bitrates where possible
				Encoder.AudioMixTrack = ""; //Location of an audio file to use as additional audio track
				Encoder.AudioMixTrackPercentage = 0; //The audio mixTrack level between 0 and 100 (0=no mixTrack, 100=mixTrack only)
				Encoder.AudioMasterVolumePercentage = 100; //The master volume amplification as a percentage: 100 means pass-through, 200 means amplify to double
				#endregion

				#region Output options
				Encoder.OutputExportFrameJPEGTimeMs = -1; //If set to a value >= 0 means the instant in milliseconds at which to export a processed video frame into a JPEG file with the quality set in exportFrameJPEGQuality. The exported image file will have the name of the output file appended with ".jpg"
				Encoder.OutputExportFrameJPEGQuality = 85; //JPEG quality for exported video frame
				Encoder.OutputFormat = "flv"; //Output format, one of "FLV" or "SWF"
				Encoder.SetOutputFile(Storage.TemporaryFilesystemPath(p.VideoMed, "flv")); // set output location
				#endregion

				if (Active != null) Active();
				Status(p.K + " Starting encode...");

				Encoder.EncodeAsync(Storage.TemporaryFilesystemPath(p.UploadTemporary, p.UploadTemporaryExtention)); // other params: sourceFileName , [sourceBeginMs], [sourceEndMs], [isLast], [encodeVideo], [encodeAudio]

				#region Loop while encoder working
				while (Encoder.EncodeAsyncIsEncoding)
				{

					int progress = (int)Encoder.EncodeAsyncPercentage;
					if (CurrentPhoto.ProcessingProgress != progress)
					{
						if (Active != null) Active();
						CurrentPhoto.ProcessingProgress = progress;
						CurrentPhoto.ProcessingLastChange = DateTime.Now;
						CurrentPhoto.Update();
					}
					else
					{
						if (CurrentPhoto.ProcessingLastChange < DateTime.Now.AddMinutes(0 - MinutesUntilKill1))
						{
							Status(p.K + " XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
							Status(p.K + " Last change more than " + MinutesUntilKill1 + " mins ago - attempting cancel...");
							this.Cancel();
							Status(p.K + " Cancelled...");
							return false;
						}
					}

					Status(p.K + " encoding in progress: " + CurrentPhoto.ProcessingProgress.ToString("000") + "% done.");
					StatusString = "[" + CurrentPhoto.ProcessingProgress.ToString("000") + "%]";

					System.Threading.Thread.Sleep(2000);
				}
				#endregion

				if (Active != null) Active();
				Status(p.K + " Encode finished. Flushing encoder...");

				#region Flush encoder
				Encoder.EncodeFlush();// done - flush encoding:
				#endregion

				#region Let's double check it's actually worked...
				if (!File.Exists(Storage.TemporaryFilesystemPath(p.VideoMed, "flv")))
					throw new Exception("Encoder failed because output file did not exist.");

				FileInfo f = new FileInfo(Storage.TemporaryFilesystemPath(p.VideoMed, "flv"));
				if (f.Length < 1024)
					throw new Exception("Encoder failed because output file size less than 1KB");
				#endregion

				if (Active != null) Active();
				Status(p.K + " Replicating files...");

				#region Replicating files
				Storage.AddToStore(
					Storage.GetFromStore(Storage.Stores.Temporary, p.VideoMed, "flv"),
					Storage.Stores.Pix,
					p.VideoMed,
					"flv",
					p,
					"VideoMed");
				#endregion

				#region Deleting upload temporary
				Storage.RemoveFromStore(Storage.Stores.Temporary, p.UploadTemporary, p.UploadTemporaryExtention);
				Storage.RemoveFromStore(Storage.Stores.Temporary, p.VideoMed, "flv");
				#endregion

				if (Active != null) Active();
				Status(p.K + " Completing...");

				#region Set processing progress to 100
				CurrentPhoto.IsProcessing = false;
				CurrentPhoto.ProcessingProgress = 100;
				CurrentPhoto.ProcessingLastChange = DateTime.Now;
				CurrentPhoto.Update();
				#endregion

				if (Active != null) Active();
				Status(p.K + " Updating gallery etc...");

				Photo.FinishProcessUploadedFile(p, p.Gallery, p.Usr);

				if (Active != null) Active();
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
				if (Active != null) Active();
				Status(p.K + " Finalising...");
				if (Encoder.EncodeAsyncIsEncoding)
				{
					Status(p.K + " Encoder seems to be still encoding... Aborting...");
					Encoder.EncodeAsyncAbort();
					Status(p.K + " Done aborting...");
				}
				if (Active != null) Active();
				Status(p.K + " Resetting encoder...");
				Encoder.Reset();
				StatusString = "[----]";
				Status(p.K + " Finalising done.");
				if (Active != null) Active();
			}
			return true;
		}
		#endregion

		#region Cancel()
		public override void Cancel()
		{
			Encoder.EncodeAsyncAbort();
			Encoder.Reset();

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
			if (Encoder != null)
				Encoder.Dispose();
		}

		#endregion

	}
	#endregion
}
