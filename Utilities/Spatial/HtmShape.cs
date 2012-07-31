using System;

/*=====================================================================

  File:      HtmShape.cs for Spatial Sample
  Summary:   Implements base class for simple regions.
  Date:	     August 16, 2005

---------------------------------------------------------------------
  This spatial search library was developed by Alexander Szalay, Robert Brunner, 
  Peter Kunszt, and Gyorgy Fekete at the Department of Physics and Astronomy, 
  The Johns Hopkins University, in collaboration with Jim Gray, Microsoft Research.
  Details of the algorithms can be found at http://www.sdss.jhu.edu/htm/.
 
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.
======================================================= */

namespace Microsoft.Samples.SqlServer {
	/// <summary>
	/// mplements base class for simple regions.
	/// </summary>
	public class HtmShape {

		[CLSCompliant(false)]
		protected Region _reg = null;
		[CLSCompliant(false)]
		protected Convex _con = null;

		protected void init(){
			_con = new Convex(Convex.Mode.Normal); // this convex gets inserted into region
		}

		public HtmShape() {
			//
			// TODO: Add constructor logic here
			//
		}
		public string normalForm(){
			string result;
			result = new string('A', 1);
			return result;
		}
	}  
}
