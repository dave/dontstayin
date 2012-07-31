using System;

/*=====================================================================

  File:      Circle.cs for Spatial Sample
  Summary:   Implements a circular region of interest on the celestial sphere.
  Date:	     August 10, 2005

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
	/// Summary description for Circle.
	/// </summary>
	public class Circle : HtmShape
	{
		public Circle(Region  reg) {
			init();
			_reg = reg;				// this convex gets inserted into region
		}
		private Circle()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public void make (double x, double y, double z, double radius){
			double arg = Cartesian.Pi * radius / 10800.0;
			double d;
			d = Cartesian.Cos(arg);
			//if (Math.Abs(arg - Cartesian.Pi05) < Cartesian.Epsilon){
			//    d = 0.0;
			//} else {
			//    d = Math.Cos(arg);
			//}
			this._con.add(x, y, z, d);
			_reg.add(_con);
		}
		public void make (double ra, double dec, double radius){
			double x, y, z;
			double arg = Cartesian.Pi * radius / 10800.0;
			double d;
			d = Cartesian.Cos(arg);
			//if (Math.Abs(arg - Cartesian.Pi05) < Cartesian.Epsilon) {
			//    d = 0.0;
			//} else {
			//    d = Math.Cos(arg);
			//}
			SpatialVector.radec2cartesian(ra, dec, out x, out y, out z);
			this._con.add(x, y, z, d);
			_reg.add(_con);
		}
	}
}
