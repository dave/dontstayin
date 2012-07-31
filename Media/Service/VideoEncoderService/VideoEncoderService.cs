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
	public partial class VideoEncoderService : EncoderServiceBase
	{
		public override int MinutesUntilKill1 { get { return 8; } }  //In the loop while the encoder is working
		public override int MinutesUntilKill2 { get { return 10; } } //In the main Tick function
		public override int MinutesUntilKill3 { get { return 12; } } //HouseKeepingTick (reset)
		public override int MinutesUntilKill4 { get { return 15; } } //HouseKeepingTick (delete)
		public override int MaxRetries { get { return 2; } }
		public override int TickDuration { get { return 5000; } }
		public override string NameAndVersion { get { return "VideoEncoder v2.0.0"; } }
		public override string StatusFileName { get { return "videoencoder"; } }
		//public override Q PhotoQueryCondition { get { return new And(new Q(Photo.Columns.MediaType, Photo.MediaTypes.Video), new Q(Photo.Columns.UsrK, 4)); } }
		public override Q PhotoQueryCondition { get { return new Q(Photo.Columns.MediaType, Photo.MediaTypes.Video); } }
		public override int Processes { get { return 1; } }

		#region GetEncoder()
		public override int GetEncoder()
		{
			//#region to test...

			//if (Encoders[0] == null)
			//    Encoders[0] = new TurbineEncoder();

			//if (Encoders[0].Idle)
			//{
			//    Encoders[0].Idle = false;
			//    return 0;
			//}
			//return -1;

			//#endregion

			for (int i = 0; i < Encoders.Length; i++)
			{
				if (Encoders[i] == null)
				{
					Encoders[i] = new TurbineEncoder();
				}
				if (Encoders[i].TakeIfIdle())
					return i;
			}
			return -1;
		}
		#endregion

		#region DoInitializeComponent()
		public override void DoInitializeComponent()
		{
			InitializeComponent();
		}
		#endregion

	}
}
