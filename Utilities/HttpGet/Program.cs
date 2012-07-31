using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace HttpGet
{
	class Program
	{
		static void Main(string[] args)
		{
			//string url = "http://server8.dontstayin.com/pages/home";
			//if (args.Length > 0)
			//	url = args[0];
			WebRequest myWebRequest = WebRequest.Create(args[0]);
			myWebRequest.Timeout = 30000;
			Console.WriteLine("Getting " + args[0] + " ...");
			WebResponse myWebResponse = myWebRequest.GetResponse();
			Console.WriteLine("Done... response length = " + myWebResponse.ContentLength);
		}
	}
}
