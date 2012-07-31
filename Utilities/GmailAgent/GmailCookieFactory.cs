/**************************************************************************
Gmail Agent API
Copyright (C) 2004 Johnvey Hwang
http://sourceforge.net/projects/gmail-api/

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
**************************************************************************/

using System;
using System.Net;

namespace Johnvey.GmailAgent
{
	/// <summary>
	/// Provides Gmail-specific cookie generation services.
	/// </summary>
	public class GmailCookieFactory
	{

		/// <summary>
		/// Initializes a new instance of the GmailCookieFactory class.
		/// </summary>
		public GmailCookieFactory()
		{
		}

		/// <summary>
		/// Generates a <see cref="Cookie"/> for use with Gmail.
		/// </summary>
		/// <param name="cookieName">The name of the cookie to generate.</param>
		/// <returns>The <see cref="Cookie"/> that can be sent along with a <see cref="HttpWebRequest"/>.</returns>
		public Cookie GenerateCookie(string cookieName) {
			Cookie output = new Cookie("none","");
			output.Path = "/";
			output.Domain = ".google.com";

			switch(cookieName) {
				case "GMAIL_LOGIN":
					double jsGetTime = Math.Round(((TimeSpan)(DateTime.Now - new DateTime(1970,1,1))).TotalMilliseconds);
					output.Name = "GMAIL_LOGIN";
					output.Value = "T" + (jsGetTime - 11701) + "/" + (jsGetTime - 11425) + "/" + jsGetTime;
					break;

				case "TZ":
					string tz = Math.Abs(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalMinutes).ToString();
					output.Name = "TZ";
					output.Value = tz;
					break;

				default:
					throw new ArgumentException("Cannot generate cookie. '" + cookieName + "' is not a registered cookie.");

			}

			return output;

		}
	}
}
