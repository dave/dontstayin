using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Runtime.InteropServices;
using Bobs;
using System.IO;
using MediaEncoder;

namespace EncoderService
{
	public abstract class EncoderServiceBase : ServiceBase
	{
		public EncoderBase[] Encoders;
		//DateTime StartDateAndHour;
		DateTime StartDate;
		System.Threading.Timer Clock, HousekeepingClock;
		public virtual int MinutesUntilKill1 { get { return 8; } } //In the loop while the encoder is working
		public virtual int MinutesUntilKill2 { get { return 10; } } //In the main Tick function
		public virtual int MinutesUntilKill3 { get { return 12; } } //HouseKeepingTick (reset)
		public virtual int MinutesUntilKill4 { get { return 15; } } //HouseKeepingTick (delete)
		public virtual int MaxRetries { get { return 2; } }
		public virtual int TickDuration { get { return 5000; } }
		public abstract string NameAndVersion { get; }
		public abstract string StatusFileName { get; }
		public abstract Q PhotoQueryCondition { get; }
		public abstract int GetEncoder();
		public abstract void DoInitializeComponent();
		public abstract int Processes { get; }
		bool resetAtEarliestOpportunity = false;
		public void RegisterMeaningfulActivity()
		{
			lock (meaningfulActivityLockObject)
				meaningfulActivityDateTime = DateTime.Now;
		}
		DateTime meaningfulActivityDateTime = DateTime.Now;
		object meaningfulActivityLockObject = (object)1;
		

		#region EncoderServiceBase()
		public EncoderServiceBase()
		{
			Status("");
			Status("");
			Status("                                           >" + NameAndVersion + " Init...");
			DoInitializeComponent();

			//Encoders = new EncoderBase[System.Environment.ProcessorCount];
			Encoders = new EncoderBase[Processes];
			Status("                                           >Starting with " + Processes + " processes.");
			Status("                                           >Done Init.");

		}
		#endregion

		#region OnStart(string[] args)
		protected override void OnStart(string[] args)
		{
			Start();
		}
		#endregion

		public void Start()
		{
			try
			{
				Status("                                           >OnStart...");

				//StartDateAndHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
				StartDate = DateTime.Today;

				Clock = new System.Threading.Timer(new System.Threading.TimerCallback(Tick), null, 0, TickDuration);
				HousekeepingClock = new System.Threading.Timer(new System.Threading.TimerCallback(HousekeepingTick), null, 0, 60 * 1000); // tick each minute
				RegisterMeaningfulActivity();

				Status("                                           >Done OnStart...");
			}
			catch(Exception ex)
			{
				Status(ex.ToString());
			}
		}

		#region OnStop()
		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.

			Status("                                           >OnStop...");

			try
			{
				Clock.Change(-1, -1);
				HousekeepingClock.Change(-1, -1);
			}
			catch { }

			for (int i = 0; i < Encoders.Length; i++)
			{
				try
				{
					if (Encoders[i] != null)
						Encoders[i].Cancel();
				}
				catch { }
			}

			Status("                                           >Done OnStop.");
			Status("");
		}
		#endregion
		#region HousekeepingTick
		public void HousekeepingTick(object sender)
		{
			int numberOfQueuedPhotos = -1;
			try
			{
				numberOfQueuedPhotos = EncoderBase.GetNumberOfQueuedPhotos(PhotoQueryCondition, MaxRetries);
			}
			catch 
			{
				Status("                                           >HousekeepingTick... Exception in GetNumberOfQueuedPhotos!");
			}

			Status("                                           >HousekeepingTick... " + numberOfQueuedPhotos.ToString() + " in the queue.");

			
			#region reset encoder if no progress after X minutes
			try
			{
				//Update u = new Update();
				//u.Table = TablesEnum.Photo;
				//u.Changes.Add(new Assign(Photo.Columns.IsProcessing, 0));
				//u.Changes.Add(new Assign(Photo.Columns.ProcessingStartDateTime, null));
				//u.Changes.Add(new Assign(Photo.Columns.ProcessingProgress, 0));
				//u.Changes.Add(new Assign(Photo.Columns.ProcessingLastChange, null));
				//u.Where = new And(
				//    PhotoQueryCondition,
				//    new Q(Photo.Columns.Status, Photo.StatusEnum.Processing),
				//    new Q(Photo.Columns.ProcessingLastChange, QueryOperator.LessThan, DateTime.Now.AddMinutes(0 - MinutesUntilKill3))
				//);
				//u.Run();
				Query q = new Query();
				q.QueryCondition = new And(
					PhotoQueryCondition,
					new Q(Photo.Columns.Status, Photo.StatusEnum.Processing),
					new Q(Photo.Columns.ProcessingLastChange, QueryOperator.LessThan, DateTime.Now.AddMinutes(0 - MinutesUntilKill3))
				);
				PhotoSet ps = new PhotoSet(q);
				if (ps.Count > 0)
				{
					Status("                                           >HousekeepingTick... Resetting " + ps.Count.ToString() + " photo(s)");
					foreach (Photo p in ps)
					{
						p.IsProcessing = false;
						p.ProcessingStartDateTime = null;
						p.ProcessingProgress = 0;
						p.ProcessingLastChange = null;
						p.Update();
					}
					Status("                                           >HousekeepingTick... Found stuck encoders... resetting...");
					resetAtEarliestOpportunity = true;
				}
				

			}
			catch (Exception ex)
			{
				Status("                                           >HousekeepingTick... Exception trying to reset the encoder! - " + ex.ToString());
			}
			#endregion

			#region If tick isn't running, exit the whole process
			try
			{
				if (Ticks == 0)
				{

					Status("                                           >HousekeepingTick - Tick is not running!!! Killing process...");
					//this.Stop();
					System.Diagnostics.Process.GetCurrentProcess().Kill();
				}
				Ticks = 0;
			}
			catch 
			{
				Status("                                           >HousekeepingTick... Exception while 'If tick isn't running, exit the whole process'!");
			}
			#endregion

			#region delete if we've tried X times (disabled while testing)
			/*
			PhotoSet ps = new PhotoSet(
				new Query(
					new And(
						PhotoQueryCondition,
						new Q(Photo.Columns.ProcessingAttempts, QueryOperator.GreaterThanOrEqualTo, MaxRetries),
						new Or(
							new Q(Photo.Columns.ProcessingLastChange, QueryOperator.LessThan, DateTime.Now.AddMinutes(0 - MinutesUntilKill4)),
							new Q(Photo.Columns.ProcessingLastChange, QueryOperator.IsNull, null)
						)
					)
				)
			);
			foreach (Photo p in ps)
			{
				Mailer m = new Mailer();
				m.UsrRecipient = p.Usr;
				m.Subject = "We've had to delete a video you tried to upload.";
				m.Body = "<p>You recently uploaded a " + p.VideoFileExtention + " video. We've tried to encode it, but it's not working! We've tried five times, but its failed each time. If you can't get your video uploaded, but you can play it on your computer, send an email to dave@dontstayin.com and I'll have a look into it. Mention the filename <b>" + p.VideoMaster.ToString() + "." + p.VideoFileExtention + "</b>.</p>";
				m.Send();

				Mailer mAdmin = new Mailer();
				mAdmin.To = "dave@dont-stay-in.com";
				mAdmin.TemplateType = Mailer.TemplateTypes.AdminNote;
				mAdmin.Subject = "Video encode failed.";
				mAdmin.Body = "<p>Video failed. Filename: " + p.VideoMaster.ToString() + "." + p.VideoFileExtention + ".</p>";
				mAdmin.Send();

				File.Copy(p.FileSystemPathVideoMaster, @"\\pix\d$\DontStayIn\Live\FailedVideos\" + p.VideoMaster.ToString() + "." + p.VideoFileExtention);

				p.DeleteAll(null);
			}
			*/
			#endregion

		}
		#endregion
		#region Tick(object sender)
		public int Ticks = 100;
		public void Tick(object sender)
		{

			Ticks++;

			bool doneSomething = false;

			
			try
			{
				#region Check for dead encoder
				for (int i = 0; i < Encoders.Length; i++)
				{
					if (Encoders[i] != null && !Encoders[i].IsIdle && Encoders[i].CurrentPhoto != null && Encoders[i].CurrentPhoto.ProcessingLastChange < DateTime.Now.AddMinutes(0 - MinutesUntilKill2))
					{
						Status("                                           >Tick: Encoder hung... Cancelling...");

						try
						{
							Encoders[i].Cancel();
						}
						catch { }

						try
						{
							((IDisposable)Encoders[i]).Dispose();
						}
						catch { }

						Encoders[i] = null;
						Status("                                           >Tick: Done cancelling.");
						doneSomething = true;
					}
				}
				#endregion
			}
			catch
			{
				Status("                                           >Tick: Exception while checking for dead encoders!");
			}
			

			// Only start new encoders on the same date as the encoder started. 
			// When the date changes, we're going to kill / restart to clear up after memory leaks.
			//DateTime hourNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
			//if (hourNow.Equals(StartDateAndHour) && !resetAtEarliestOpportunity)

			if (StartDate == DateTime.Today && !resetAtEarliestOpportunity)
			{
				try
				{
					#region Process...
					int idleEncoderIndex = GetEncoder();
					if (idleEncoderIndex > -1)
					{
						try
						{
							Photo p = EncoderBase.GetNextPhoto(PhotoQueryCondition, MaxRetries);

							if (p != null)
							{
								//Status("");
								Status("                                           >Tick: Encoding PhotoK " + p.K.ToString() + (p.ProcessingAttempts == 0 ? "" : String.Format(" (Retry #{0})", p.ProcessingAttempts)));
								//EncoderBase.ProcessPhoto(Encoders[idleEncoderIndex], p, new Photo.EncoderStatusDelegate(StatusNull), MinutesUntilKill1);
								EncoderBase.ProcessPhoto(Encoders[idleEncoderIndex], p, Status, RegisterMeaningfulActivity, MinutesUntilKill1);
							}
							//else if (Vars.DevEnv)
							//	Status("                                           >Tick: No photos to encode, encoder #" + idleEncoderIndex);

							doneSomething = true;
						}
						finally
						{
							Encoders[idleEncoderIndex].Release();
						}
					}
					#endregion
				}
				catch 
				{
					Status("                                           >Tick: Exception in processing block!");
				}

			}
			else
			{
				try
				{
					#region Start of the day... If all encoders are idle, kill this process
					bool allIdle = true;
					for (int i = 0; i < Encoders.Length; i++)
					{
						if (Encoders[i] != null && !Encoders[i].IsIdle)
						{
							allIdle = false;
							break;
						}
					}
					if (allIdle)
					{
						Status("                                           >Tick: RESET... killing current process...");
						System.Diagnostics.Process.GetCurrentProcess().Kill();
					}
					else
					{

						Status("                                           >Tick: RESET... waiting for active encoders to finish...");

						//numberOfTicksWaitingForEncodersToFinishAtTheStartOfTheDay++;

						//if (numberOfTicksWaitingForEncodersToFinishAtTheStartOfTheDay > 100)
						if (DateTime.Now.AddSeconds(-30) > meaningfulActivityDateTime)
						{
							Status("                                           >Tick: RESET... waited 30 sec for meaningful encoder activity, so killing now!");
							System.Diagnostics.Process.GetCurrentProcess().Kill();
						}
					}

					doneSomething = true;
					#endregion
				}
				catch 
				{
					Status("                                           >Tick: Exception in start-of-day block!");
				}
			}

		//	if (!doneSomething)
		//		Status("                                           >Tick - nothing done!");
		}
		#endregion

		//int numberOfTicksWaitingForEncodersToFinishAtTheStartOfTheDay = 0;

		#region Status(string message)
		public void StatusNull(string message)
		{
		}
		public void Status(string message)
		{
			if (Vars.DevEnv)
			{
				Console.WriteLine(message);
				return;
			}

			//Write to File...
			try
			{
				using (StreamWriter sw = File.AppendText("c:\\" + StatusFileName + "-" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString("00") + "-" + DateTime.Today.Day.ToString("00") + ".txt"))
				{
					if (message.Length == 0)
						sw.WriteLine("");
					else
						sw.WriteLine(DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + " " + message);
					sw.Flush();
					sw.Close();
					sw.Dispose();
				}
			}
			catch { }

		}
		#endregion

		



	}


}
