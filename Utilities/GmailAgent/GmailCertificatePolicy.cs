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
using System.Diagnostics;

namespace Johnvey.GmailAgent {

	/// <summary>
	/// Provides security certificate validation bypass services.
	/// </summary>
	public class GmailCertificatePolicy : System.Net.ICertificatePolicy {

		/// <summary>
		/// Initializes a new instance of the GmailCertificatePolicy class.
		/// </summary>
		public GmailCertificatePolicy() {
		}

		/// <summary>
		/// Simulates a certificate verification.
		/// </summary>
		/// <param name="sp">The associated ServicePoint.</param>
		/// <param name="cert">The certificate to examine.</param>
		/// <param name="req">The WebRequest to use.</param>
		/// <param name="problem">I have no idea.</param>
		/// <returns>Always returns true.</returns>
		public bool CheckValidationResult(ServicePoint sp, System.Security.Cryptography.X509Certificates.X509Certificate cert, WebRequest req, int problem) {
			try {
				Debug.WriteLine("ICertificatePolicy: Address: " + sp.Address);
				Debug.WriteLine("ICertificatePolicy: ProtocolVersion: " + sp.ProtocolVersion);
				Debug.WriteLine("ICertificatePolicy: Expect100Continue: " + sp.Expect100Continue);
			} catch(Exception ex) {
				Debug.WriteLine("ICertificatePolicy: Exception: " + ex.Message);
			}
			return true;
		}
	}
}
