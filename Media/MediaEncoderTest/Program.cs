using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Runtime.InteropServices;
using Bobs;
using System.IO;
using MediaEncoderBase;

namespace MediaEncoderTest
{
	public class MediaEncoderTest
	{
		[STAThread]
		static void Main(string[] args)
		{

			TurbineEncoder Encoder = new TurbineEncoder();

			Console.WriteLine("Main...");

			//	if (Vars.DevEnv)
			//		VideoTest("c:\\vidtest\\sw4a.mov", Guid.NewGuid());


			//	for (int i = 0; i < 100; i++)
			//	{
			//		VideoTest("c:\\vidtest\\test.3gp", Guid.NewGuid());
			//	}
			//	return;

			Query q = new Query();

			q.QueryCondition = new Q(Photo.Columns.K, 3460180);
			
			//Broken 04/10/2006:
			//3353642
			//3623970

			//q.QueryCondition = new Q(Photo.Columns.ProcessingAttempts, QueryOperator.GreaterThan, 1);
			//	q.QueryCondition = new And(
			//		new Q(Photo.Columns.ProcessingAttempts, QueryOperator.GreaterThan, 1),
			//		new Q(Photo.Columns.VideoFileExtention, QueryOperator.NotEqualTo, "3gp"));
			//	q.QueryCondition = new Or(
			//		new Q(Photo.Columns.K, 2450665),
			//		new Q(Photo.Columns.K, 2450649));

			PhotoSet ps = new PhotoSet(q);




			//for (int i = 0; i < 100; i++)
			while (true)
			{
				foreach (Photo p in ps)
				{

					EncoderBase.ProcessPhoto(Encoder, p, false, new EncoderBase.StatusDelegate(ConsoleWriteLine), 8);

				}
			}

			return;

		}
		public static void ConsoleWriteLine(string message)
		{
			Console.WriteLine(message);
		}
		
	}
}
