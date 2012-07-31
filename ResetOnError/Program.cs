using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace ResetOnError
{
	class Program
	{
		static void Main(string[] args)
		{

			StringBuilder sb = new StringBuilder();

			// used on each read operation
			byte[] buf = new byte[8192];

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + System.Environment.MachineName.ToLower() + ".dontstayin.com/pages/blank");

			HttpWebResponse response;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (Exception e) 
			{
				if (e.Message.Contains("(500) Internal Server Error"))
				{
					Console.WriteLine(DateTime.Now.ToString() + " resetting...");

					Console.WriteLine("\n\n" + e.Message + "\n\n");


					Process iisreset = new Process();
					iisreset.StartInfo.FileName = @"C:\Windows\System32\iisreset.exe";
					//iisreset.StartInfo.Arguments = "computername";
					iisreset.Start();


				}

				return;
			}


			// we will read data via the response stream
			Stream resStream = response.GetResponseStream();

			string tempString = null;
			int count = 0;

			do
			{
				// fill the buffer with data
				count = resStream.Read(buf, 0, buf.Length);

				// make sure we read some data
				if (count != 0)
				{
					// translate from bytes to ASCII text
					tempString = Encoding.ASCII.GetString(buf, 0, count);

					// continue building the string
					sb.Append(tempString);
				}
			}
			while (count > 0); // any more data to read?

			string html = sb.ToString();

			//Console.WriteLine(html);

			if (html.Contains("Pipes") || html.Contains("Timeout expired"))
			{

				

				Console.WriteLine(DateTime.Now.ToString() + " resetting...");

				Console.WriteLine("\n\n" + html + "\n\n");


				Process iisreset = new Process();
				iisreset.StartInfo.FileName = @"C:\Windows\System32\iisreset.exe";
				//iisreset.StartInfo.Arguments = "computername";
				iisreset.Start();
			}
			else
			{
				Console.WriteLine(DateTime.Now.ToString() + " server ok...");
			}



		}
	}
}
