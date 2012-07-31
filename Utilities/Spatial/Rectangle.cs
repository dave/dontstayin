using System;
/*=====================================================================

  File:      Rectangle.cs for Spatial Sample
  Summary:   Implements a simple region
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
	/// Implements a simple region
	/// </summary>
	public class Rectangle : HtmShape {

		public Rectangle(Region  reg) {
			init();
			_reg = reg;				// this convex gets inserted into region
		}
		private Rectangle()
		{
			// init();
			//
			// TODO: Add constructor logic here
			//
		}
		public void make(double ra1, double dec1, double ra2, double dec2){
			//
			// Create four halfspaces. Two great circles for RA and
			// two small circles for DEC
			
			double dlo, dhi; // offset from center, parameter for constraint
			double declo, dechi;
			double costh, sinth; // sine and cosine of theta (RA) for rotation of vector
			double x, y, z;
			
			//
			// Halfspaces belonging to declo and dechi are circles parallel
			// to the xy plane, their normal is (0, 0, +/-1)
			//
			// declo halfpsacet is pointing up (0, 0, 1)
			// dechi is pointing down (0, 0, -1)
			if (dec1 > dec2){
				declo = dec2;
				dechi = dec1;
			} else {
				declo = dec1;
				dechi = dec2;
			}
			dlo = Math.Sin(declo * Cartesian.DTOR);
			dhi = -Math.Sin(dechi * Cartesian.DTOR); // Yes, MINUS!
	
			_con.add(0.0, 0.0,  1.0, dlo); // Halfspace #1
			_con.add(0.0, 0.0, -1.0, dhi); // Halfspace #1

			costh = Cartesian.Cos(ra1 * Cartesian.DTOR);
			sinth = Math.Sin(ra1 * Cartesian.DTOR);
			x =  -sinth;
			y =   costh;
			z =       0.0;
			_con.add(x, y, z, 0.0);// Halfspace #3

			costh = Cartesian.Cos(ra2 * Cartesian.DTOR);
			sinth = Math.Sin(ra2 * Cartesian.DTOR);
			x =   sinth;
			y =  -costh;
			z =       0.0;

			_con.add(x, y, z, 0.0);// Halfspace #4
			_reg.add(_con);		
		}
	}
}
