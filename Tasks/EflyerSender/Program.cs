using System;
using System.IO;
using Bobs;

namespace EflyerSender
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("EflyerSender v3 - with IsEmailBroken flag...");

			int flyerK;
			if (Vars.DevEnv)
			{
				flyerK = 2503;
			}
			else if (args.Length != 1 || !int.TryParse(args[0], out flyerK))
			{
				Console.Error.WriteLine("USAGE: EflyerSender.exe <FlyerK>");
				Environment.Exit(1);
				return;
			}

			FileStream fs1 = new FileStream("C:\\Flyer-" + flyerK + "-Errors.txt", FileMode.OpenOrCreate);
			StreamWriter sw1 = new StreamWriter(fs1);
			Console.SetError(sw1);

			try
			{
				new Flyer(flyerK).SendMail();
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e.Message);
			}

			sw1.Close();
		}

	}
}
