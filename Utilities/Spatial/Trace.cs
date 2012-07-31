using System;
using System.IO;

/*=====================================================================

  File:      Trace.cs for Spatial Sample
  Summary:   Implements trace debugging functionality
  Date:	     August 16, 2005

---------------------------------------------------------------------
  This spatial search library was developed by Alexander Szalay, Robert Brunner, 
  Peter Kunszt, and Gyorgy Fekete at the Department of Physics and Astronomy, 
  The Johns Hopkins University, in collaboration with Jim Gray, Microsoft Research.
  Details of the algorithms can be found at http://www.sdss.jhu.edu/htm/.
 
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.
======================================================= */

namespace Microsoft.Samples.SqlServer
{
	/// <summary>
	/// Summary description for Trace.
	/// Usage: Trace t = new Trace("C:\htmtmp\dump.txt");
	/// t.dump("A line of stuff", true);
	/// /* there are other forms of dump... */
	/// t.close()
	/// </summary>
	public class Trace
	{
		protected StreamWriter ws = null;
		//READONLY
		//private char[] colorchar = {'B', 'R', 'B', 'G', 'Y', 'M', 'C'};
		private Trace() {
		}
		/// <summary>
		/// Creates an object for managing a trace or dump file
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="append">if true, the file is appended, otherwise
		/// the file is truncated to zero first </param>
		public Trace(string filename, bool append) {
			ws = new StreamWriter(filename, append);
		}
		/// <summary>
		/// Writes a description of a halfspace with color
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="d"></param>
		public void dump(double x, double y, double z, double d, int color){
			char[] colorchar = {'B', 'R', 'B', 'G', 'Y', 'M', 'C'};
			if (ws != null){
				ws.WriteLine("{0} {1} {2} {3} {4}", x, y, z, d, colorchar[color]);
			}
		}
		/// <summary>
		/// Writes a description of a halfpace: x y z d
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="d"></param>
		public void dump(double x, double y, double z, double d){
			if (ws != null){
				ws.WriteLine("{0} {1} {2} {3}", x, y, z, d);
			}
		}
		/// <summary>
		/// Dumps a string with HID and an integer parameter
		/// </summary>
		/// <param name="fmt"></param>
		/// <param name="hid"></param>
		/// <param name="vsum">integer parameter</param>
		public void dump(string fmt, Int64 hid, int vsum){
			if (ws != null){
				ws.WriteLine(fmt, hid, vsum);
				return;
			}
		}

		/// <summary>
		/// Dumps a string with HID and a "savecode"
		/// </summary>
		/// <param name="fmt"></param>
		/// <param name="hid"></param>
		/// <param name="scode">Convex.Savecode</param>
		public void dump(string fmt, Int64 hid, Convex.Savecode scode, int level){
			if (ws != null){
				ws.WriteLine(fmt, hid, scode, level);
				return;
			}
		}
		/// <summary>
		/// Dumps a string with HID and a "markup"
		/// </summary>
		/// <param name="fmt"></param>
		/// <param name="hid"></param>
		/// <param name="markup">Convex.Markup</param>
		public void dump(string fmt, Int64 hid, Convex.Markup markup){
			if (ws != null){
				ws.WriteLine(fmt, hid, markup);
				return;
			}
		}
		/// <summary>
		/// Dumps the hid. Message implicit in the fmt string
		/// </summary>
		/// <param name="fmt"></param>
		/// <param name="hid"></param>
		public void dump(string fmt, Int64 hid){
			if (ws != null){
				ws.WriteLine(fmt, hid);
				return;
			}
		}
		public void dump(string fmt, Int64 hid1, Int64 hid2){
			if (ws != null){
				ws.WriteLine(fmt, hid1, hid2);
				return;
			}
		}
		public void close(){
			ws.Flush();
			ws.Close();
			ws = null;
			return;
		}
	}
}
