using System;
/*=====================================================================

  File:      Polygon.cs for Spatial Sample
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
	/// Implements a simple region.
	/// </summary>
	public class Polygon : HtmShape {
		public enum Error {
			Ok,
			errZeroLength,
			errBowtieOrConcave,
			errToomanyPoints,
			errUnkown
		}
		public Polygon(Region  reg) {
			init();
			_reg = reg;				// this convex gets inserted into region
		}
		public Polygon.Error add(double[] x, double[] y, double[] z, int len){
			
			bool DIRECTION = false;
			bool FIRST_DIRECTION = false;
			// The constraint we have for each side is a 0-constraint (great circle)
			// passing through the 2 corners. Since we are in counterclockwise order,
			// the vector product of the two successive corners just gives the correct
			// constraint.
			// 
			// Polygons should be counterclockwise
			// Polygons are assumed to be convex, otherwise windingerror is
			// computed wrong.
			int ix;
			Cartesian v = new Cartesian();
			Convex cvx = 
				new Convex(Convex.Mode.Normal);
			int i;

			/* PASS 1: check for winding error */

			for(i = 0; i < len; i++) {
				// Keep track of winding direction. Should be positive
				// that is, CCW.
				ix = (i==len-1 ? 0 : i+1); 
				if (i>0){
					// test third corner against the constraint just formed
					// v is computed in the previous iteration
					// Look at a corner dot v

					if (v.dot(x[ix], y[ix], z[ix]) < Cartesian.Epsilon) {
						DIRECTION = true;
						if (i == 1) {
							FIRST_DIRECTION = true;
						}
						// break; 		// Move to pass 2
					} else {
						DIRECTION = false;
						if (i == 1) {
							FIRST_DIRECTION = false;
						}
					}
					if (i > 1) {
						if (DIRECTION != FIRST_DIRECTION) {
							// C++: must clea up new Cartesian and convex
							// or better yet, do no allocate on top until you know
							// you need it
							return Polygon.Error.errBowtieOrConcave; // BOWTie error
						}
					}
				}
				// v = corners[i] ^ corners[ i == len-1 ? 0 : i + 1];
				v.assign(x[i], y[i], z[i]);
				v.crossMe(x[ix], y[ix], z[ix]);
				if (v.isLength(0.0, Cartesian.Epsilon)) {
					return Polygon.Error.errZeroLength;
				}
				// WARNING! if v = zerovector, then edge error!!!
			}
			/* PASS 2: build convex in either original or reverse
			   order */

			/* forward:
			   Go from 0 to len-1 by +1, cross i and i+1 (or 0)
			   reverse:
			   Go from len-1 to 0 by -1, cross i and i-1 (or len-1)
			*/
			if (DIRECTION) {
				for(i=len-1; i>=0; i--){
					// v = corners[i] ^ corners[ i == 0 ? len-1 : i-1];
					// v.normalize();
					ix = (i==0? len-1 : i-1);
					v.assign(x[i], y[i], z[i]);
					v.crossMe(x[ix], y[ix], z[ix]);
					// WARNING! if v = zerovector, then edge error!!!

					v.normalizeMe();
					Halfspace c = new Halfspace(v, 0.0);
					// SpatialConstraint c(v,0);
					cvx.add(c);
				}
			} else {
				for(i=0; i<len; i++){

					//v = corners[i] ^ corners[ i == len-1 ? 0 : i+1];
					// v.normalize();
					ix = (i==len-1 ? 0 : i+1);
					v.assign(x[i], y[i], z[i]);
					v.crossMe(x[ix], y[ix], z[ix]);
					// WARNING! if v = zerovector, then edge error!!!

					v.normalizeMe();
					Halfspace c = new Halfspace(v, 0.0);
					cvx.add(c);
				}
			}
			_reg.add(cvx);
			return Polygon.Error.Ok;
		}
		public Polygon() {
			//
			// TODO: Add constructor logic here
			//
		}
	}
}
