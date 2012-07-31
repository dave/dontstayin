using System;
using System.Collections;

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
	/// his class provides the public interface for all things HTM. The user
	/// provides a description of a region, and the range of HTM-ID's
	/// (hids) are returned in a two column table of 64 bit numbers. 
	///
	/// If the region is empty
	/// a table with o rows is returned, when there is an error, null is returned. The empty table
	/// is not the same as a null, and they are intistibguishable from each other in SQL, since
	/// a table valued function can not return null.
	///
	/// Circles, Halfspaces, Rectangles can be specified directly with numbers
	/// but there is also a generic text interface that allows the specification
	/// of complex regions. 
	/// 
	/// Normally, you do not make in instance of this object, most functions
	/// are static. The instance methods are only used internally, when a
	/// a trace of the algorithms is required
	/// June 7 2005 Changed Arraylist to sorted list
	/// June 12 2005 Changed back to ArrayList
	/// EXP denotes experimental stuff 
	/// </summary>
	public class HtmCover
	{
		/// <summary>
		/// Computes the list of hid ranges for a halfspace given as a "circle"
		/// with a given location in RA/DEC and a radius (in arc minutes).
		/// </summary>
		/// <param name="depth">Do not compute trixels beyond this depth</param>
		/// <param name="ra">RA in degrees</param>
		/// <param name="dec">DEC in degrees</param>
		/// <param name="radius">Radius of cone in arc minutes</param>
		/// <returns>A table of HtmIDs</returns>
		public static Int64[,] Circle(double depth, double ra, double dec, double radius){
			Int64[,] returnResult; // interface to caller
			ArrayList lohis; // interface to xphtm Convex
			lohis = new ArrayList();
			
			// Convert arc minutes to Radians. 60 * 180 = 10800;
			//
			double x, y, z;
			double d = Cartesian.Cos(Cartesian.Pi * radius/10800.0);
			SpatialVector.radec2cartesian(ra, dec, out x, out y, out z);
			Convex c = new Convex(Convex.Mode.Normal);
			c.add(x, y, z, d);
			c.intersect(false, HtmState.Instance.maxlevel, lohis);// lohis is list of pairs

			int rows = lohis.Count/2;
			int cols = 2;
			int k = 0;
			returnResult = new Int64[rows,cols];
			for(int i=0; i<rows; i++){
				returnResult[i,0] = (Int64) lohis[k++];
				returnResult[i,1] = (Int64) lohis[k++];
			}
			return returnResult;
		}

		/// <summary>
		/// Computes the table for a single halfspace.
		/// Identical in function
		/// to Circle, only the parameters are different.
		/// </summary>
		/// <param name="depth">do not compute trixels beyond this depth</param>
		/// <param name="x">x-coordinate of halfspace's direction vector</param>
		/// <param name="y">y-coordinate of halfspace's direction vector</param>
		/// <param name="z">z-coordinate of halfspace's direction vector</param>
		/// <param name="d">distance of cutting plane from origin</param>
		/// <returns></returns>
		public static Int64[,] Halfspace(double depth, double x, double y, double z, double d){

			Int64[,] returnResult; // interface to caller
			ArrayList lohis; // interface to xphtm Convex
			lohis = new ArrayList();
			
			Convex c = new Convex(Convex.Mode.Normal);
			c.add(x, y, z, d);
			c.intersect(false, HtmState.Instance.maxlevel, lohis);// lohis is list of pairs

			int rows = lohis.Count/2;
			int cols = 2;
			int k = 0;
			returnResult = new Int64[rows,cols];
			for(int i=0; i<rows; i++){
				returnResult[i,0] = (Int64) lohis[k++];
				returnResult[i,1] = (Int64) lohis[k++];
			}
			return returnResult;
		}

		/// <summary>
		/// Create a domain with a single convex defined 
		/// by a rectangle.
		/// The rectangle is defined by 
		/// RA/DEC limits. 
		/// The dec may be in any order,
		/// but the RA order is significant, because the spehere wraps around.
		/// In other words RA range 350 - 10 is distinguished from 10 - 350
		/// There are two great circles (ra) and two small circles (dec)
		/// </summary>
		/// <param name="depth">do not compute trixels beyond this depth</param>
		/// <param name="ra1"></param>
		/// <param name="dec1"></param>
		/// <param name="ra2"></param>
		/// <param name="dec2"></param>
		/// <returns>A table of HtmIDs</returns>
		public static Int64[,] Rectangle(double depth, double ra1, double dec1, double ra2, double dec2)
			{
			//
			// Create four halfspaces. Two great circles for RA and
			// two small circles for DEC
			Int64[,] returnResult;
			double dlo, dhi; // offset from center, parameter for constraint
			double declo, dechi;
			double costh, sinth; // sine and cosine of theta (RA) for rotation of vector
			double x, y, z;
			ArrayList lohis; // interface to xphtm Convex
			Convex c = new Convex(Convex.Mode.Normal);
			lohis = new ArrayList();

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
	
			c.add(0.0, 0.0,  1.0, dlo); // Halfspace #1
			c.add(0.0, 0.0, -1.0, dhi); // Halfspace #1

			costh = Cartesian.Cos(ra1 * Cartesian.DTOR);
			sinth = Math.Sin(ra1 * Cartesian.DTOR);
			x =  -sinth;
			y =   costh;
			z =       0.0;
			c.add(x, y, z, 0.0);// Halfspace #3

			costh = Cartesian.Cos(ra2 * Cartesian.DTOR);
			sinth = Math.Sin(ra2 * Cartesian.DTOR);
			x =   sinth;
			y =  -costh;
			z =       0.0;

			c.add(x, y, z, 0.0);// Halfspace #4
			/////////////////////////////////////// INTERSECT

			c.intersect(false, HtmState.Instance.maxlevel, lohis);// lohis is list of pairs
			/////////////////////////////////////// STORE RESULT
			int rows = lohis.Count/2;
			int cols = 2;
			int k = 0;
			returnResult = new Int64[rows,cols];
			for(int i=0; i<rows; i++){
				returnResult[i,0] = (Int64) lohis[k++];
				returnResult[i,1] = (Int64) lohis[k++];
			}
			return returnResult;
		}
		public static Double[,] CoverToHalfspaces ( String textSpec ) {
			Region reg = new Region ( );
			Parser par = new Parser ( );
			double cid, hid;
			int rowcount, row;
			par.input = textSpec;
			par.buildto ( reg );
			if ( par. parse ( ) == false ) {
				return null;
			}
			reg.normalize ( );
			

			rowcount = 0;
			for (int i = 0; i < reg.Count; i++) {
				rowcount += reg.getNth(i).Count;
			}
			row = 0;
			Double[,] result = new Double[rowcount, 6];
			cid = 0.0;
			for (int i = 0; i < reg.Count; i++) {
				Convex con = reg.getNth (i);
				hid = 0.0;
				for (int j=0; j< con.Count; j++ ) {
					Halfspace h = con.hsAt (j);
					result[row, 0] = cid;
					result[row, 1] = hid;
					result[row, 2] = h.sv.x;
					result[row, 3] = h.sv.y;
					result[row, 4] = h.sv.z;
					result[row, 5] = h.d;
					hid += 1.0;
					row++;
				}
				cid += 1.0;
			}
			return result;
		}

		/// <summary>
		/// Normalize the region specification.
		/// The text interface allow a region to be specified in terms of
		/// rectangles, circles, polygons, convex hulls of points, etc.
		/// A so called <i>normal form</i> is a string that contains only
		/// a union of convexes.
		/// </summary>
		/// <param name="textSpec">Legacy style descritption of region</param>
		/// <returns>A specification consisting of a union of Convexes</returns>
		public static string NormalForm ( String textSpec ) {
			Region reg = new Region();
			Parser par = new Parser();
			par.input = textSpec;
			par.buildto(reg);
			if (par.parse() == false) {
				return null;
			}
			reg.normalize();
			return reg.ToString();
		}
		public static string NormalForm(String textSpec, out String errmsg) {
			Region reg = new Region();
			Parser par = new Parser();
			par.input = textSpec;
			par.buildto(reg);
			if (par.parse() == false) {
				errmsg = par.errmsg();
				return null;
			} else {
				errmsg = "ok";
			}
			reg.normalize();
			return reg.ToString();
		}

		/// <summary>
		/// Use the legacy text interface to build a region.
		/// </summary>
		/// <param name="minlevel">Compute trixels to at least this level</param>
		/// <param name="textSpec">Legacy style descritption of region</param>
		/// <param name="directory">Trace information is deposited here
		///                    Use <i>null</i> if no trace is required</param>
		/// <returns>A table of HtmIDs or null</returns>
		public static Int64[,] Region(int minlevel, String textSpec, string directory){
			HtmState.Instance.minlevel = minlevel;
			return Region(textSpec, directory);
		}

		/// <summary>
		/// Use the legacy text interface to build a region.
		/// </summary>
		/// <param name="textSpec">Legacy style descritption of region</param>
		/// <param name="directory">Trace information is deposited here.
		///                    Use <i>null</i> if no trace is require</param>
		/// <returns>A table of HtmIDs or null</returns>
		public static Int64[,] Region(String textSpec, string directory){
			Int64[,] returnResult;
			Region reg = new Region(directory);
			Parser par;
			Parser.Geometry g;
			ArrayList lohis = new ArrayList();
			
			par = new Parser();
			par.input = textSpec;
			g = par.peekGeometry(); // Peeking into the spec decides what kind of object we build
			switch(g){
				case Parser.Geometry.Region:
				case Parser.Geometry.Convex:
				case Parser.Geometry.Rect:
				case Parser.Geometry.Circle:
				case Parser.Geometry.Poly:
					par.buildto(reg); //We give the location of the target object. There parser will assemble the region here
					if (par.parse() == false){ // Start the parser. if it returns true, all is well, else error
						return null;
					}
					reg.intersect(false,
						HtmState.Instance.minlevel,
						HtmState.Instance.maxlevel, lohis); // lohis remains unchanged when called from polygon
					break;
				case Parser.Geometry.Chull:
					par.buildto(reg);
					if (par.parse() == false){
						return null;
					}
					reg.intersect(false,
						HtmState.Instance.minlevel,
						HtmState.Instance.maxlevel, lohis);
					break;
				case Parser.Geometry.Null:
					return null;
			}
			int rows = lohis.Count/2;
			int cols = 2;
			int k = 0;
			returnResult = new Int64[rows,cols];
			for(int i=0; i<rows; i++){
				returnResult[i,0] = (Int64) lohis[k++];
				returnResult[i,1] = (Int64) lohis[k++];
			}
			return returnResult;
		}
		

		/// <summary>
		/// Get the error message that describes why xphtm.Region may have returned
		/// a null table
		/// </summary>
		/// <param name="textSpec">Legacy style descritption of region</param>
		/// <returns>String containing error message</returns>
		public static String Error(String textSpec) {
			Region reg = new Region(null); // no Trace output!
			Parser par;
			ArrayList lohis = new ArrayList();
			par = new Parser();
			par.input = textSpec;

			par.buildto(reg); //We give the location of the target object. There parser will assemble the region here
			if (par.parse() == false) { // Start the parser. if it returns true, all is well, else error
				return par.errmsg();
			}
			//reg.intersect(false,
			//    HtmState.Instance.minlevel,
			//    HtmState.Instance.maxlevel, lohis); // lohis remains unchanged when called from polygon
			return "ok";
		}
		private string _directory = null;
		public HtmCover(string in_directory){
			_directory = in_directory;
			// When you create an instance, you have tracing ability.
		}
		public HtmCover()
		{
			//
			// TODO: Add constructor logic here
			// Normally, this class works in static mode.
			
		}
		[CLSCompliant(false)]
		public Int64[,] region(int minlevel, String textSpec)
		{
			// 
			// Must propagate directory info to Region and Convex
			// 
			return Region(minlevel, textSpec, _directory);
		}

		/// <summary>
		/// 		// EXP May 24, 2005 GYF
		/// Use the legacy text interface to build a region. This version uses
		/// a two-pass method to tighten the fit a bit.
		/// </summary>
		/// <param name="textSpec">Legacy style descritption of region</param>
		/// <param name="directory">Trace information is deposited here.
		///                    Use <em>null</em> if no trace is required</param>
		/// <returns>A table of HtmIDs or null</returns>
		public static Int64[,] RegionTighter(String textSpec, string directory) {
			Int64[,] returnResult;
			Region reg = new Region(directory);
			Parser par;
			ArrayList lohis = new ArrayList();
			par = new Parser();
			par.input = textSpec;
			par.buildto(reg);
			if (par.parse() == false) {
				return null;
			}
			reg.smartintersect(false,
				HtmState.Instance.minlevel,
				HtmState.Instance.maxlevel, lohis);

			// EXP EXP EXP EXP //  
			// I moved the below stuff to be done inside each convex...
			// and then merge..
			// reg.smartintersect() is like reg.intersect, but
			// does this stuff below for each convex
			//
			//if (lohis.Count > 1) { //repeat with the heuristic rule
			//    int fudle;
			//    int hlevel;
			//    int magic = HtmState.Instance.magicnumber;
			//    // make magic 31 for tight fit
			//    // make magic 30 for relaxed fit (unlike jeans)
			//    // 
			//    Int64 blorp = HtmState.Instance.tcount;
			//    for (fudle = 0; blorp > 0; fudle++) {
			//        blorp >>= 2;
			//    }
			//    hlevel = (magic - fudle) / 2;
			//    HtmState.Instance.minlevel = hlevel; // Do you reset this anytime?
			//    HtmState.Instance.maxlevel = hlevel + 4; // reasonable cutoff? <EXP> 
			//    lohis.Clear();
			//    reg.intersect(false,
			//        HtmState.Instance.minlevel,
			//        HtmState.Instance.maxlevel, lohis);
			//}
			//
			int rows = lohis.Count / 2;
			int cols = 2;
			int k = 0;
			returnResult = new Int64[rows, cols];
			for (int i = 0; i < rows; i++) {
				returnResult[i, 0] = (Int64)lohis[k++];
				returnResult[i, 1] = (Int64)lohis[k++];
			}
			return returnResult;
		}
	}
}
