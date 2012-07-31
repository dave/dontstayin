using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace GeoCoder
{
	public class GoogleGeoCoder
	{
		public static KeyValuePair<double, double> GeoCodeAddress(string address)
		{
			WebRequest request = WebRequest.Create("http://maps.google.com/maps/geo?q=" + address.Replace(" ", "+")
				+ "&output=csv&key=ABQIAAAArtnzOH7TK00m4RFuKeW8GBT2yXp_ZAY8_ufC3CFXhHIE1NvwkxQahvgsKrJsoQvYcINHJ2Thm1Zceg");
			
			// Get the response.
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			// Display the status.
			//Console.WriteLine(response.StatusDescription);
			// Get the stream containing content returned by the server.
			Stream dataStream = response.GetResponseStream();
			// Open the stream using a StreamReader for easy access.
			StreamReader reader = new StreamReader(dataStream);
			// Read the content.
			string responseFromServer = reader.ReadToEnd();
			// Display the content.
			//Console.WriteLine(responseFromServer);


			//XmlDocument xml = new XmlDocument();
			//xml.Load(reader);
			////Console.WriteLine(xml["Point"]["coordinates"]);

			//XmlNodeList nodeList = xml.GetElementsByTagName("Point");

			//foreach (XmlNode node in nodeList)
			//{
			//    Console.WriteLine(node["coordinates"].InnerText);
			//}

			// Cleanup the streams and the response.
			reader.Close();
			dataStream.Close();
			response.Close();


			string[] result = responseFromServer.Split(',');
			return new KeyValuePair<double, double>(double.Parse(result[2]), double.Parse(result[3]));
		}
	}
}
