using System;
using System.Collections.Generic;
using System.Text;
using MediaEncoder;
using Bobs;
using EncoderService;

namespace MediaEncoderConsole
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.Write("Starting...");
			//PhotoEncoderService p = new PhotoEncoderService();
			//p.Start();


			//while (true) { System.Threading.Thread.Sleep(1000); }


			PhotoEncoder Encoder = new PhotoEncoder();
			//TurbineEncoder Encoder = new TurbineEncoder();
			Photo CurrentPhoto = null;
			bool Success = true;

			do
			{
				CurrentPhoto = EncoderBase.GetNextPhoto(new Q(Photo.Columns.MediaType, Photo.MediaTypes.Image), 10);

				//CurrentPhoto = EncoderBase.GetNextPhoto(new Q(Photo.Columns.K, 12102040), 10);
				
				//CurrentPhoto = EncoderBase.GetNextPhoto(new Q(Photo.Columns.MediaType, Photo.MediaTypes.Video), 3);
				if (CurrentPhoto != null)
				{
					ConsoleWriteLine("");
					ConsoleWriteLine(">>>>>>>>>>>>>>Encoding PhotoK " + CurrentPhoto.K.ToString());
					Success = EncoderBase.ProcessPhoto(Encoder, CurrentPhoto, new Photo.EncoderStatusDelegate(ConsoleWriteLine), new Photo.MeaningfulActivityDelegate(ConsoleActive), 8);
				}
				Console.Write(".");
				System.Threading.Thread.Sleep(300);
			}
			while (true);//CurrentPhoto != null);
		}
		public static void ConsoleWriteLine(string message)
		{
			Console.WriteLine(message);
		}
		public static void ConsoleActive()
		{
			Console.WriteLine("(active)");
		}
	}
}
