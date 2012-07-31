using System;
/*=====================================================================

  File:      HtmTrixel.cs for Spatial Sample
  Summary:   Implements conversions between several representations of a HID
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

	[CLSCompliant(false)]
	public struct _HtmBase {
		public char[] name;
		public long HID;
		public int v1, v2, v3;
	};

	/// <summary>
	/// HtmTrixel implements operations needed to convert between
	/// several representations of a HID, and locations expressed
	/// in either Cartesian or J2000 (ra/dec) space.
	/// </summary>
	public class HtmTrixel {
		/// \
		private enum Startindex {
			_S2 = 0, 
			_N1 = 1,
			_S1 = 2,
			_N2 = 3,
			_S3 = 4,
			_N0 = 5,
			_S0 = 6,
			_N3 = 7
		};
		// public static 
		const int eHIDBits = 64;

		/// <summary>
		/// Maximum number of characters in text descriptions
		/// Whenever character arrays are used to keep the text
		/// description of a trixel, they must be exactly eMaxNameSize
		/// characters long
		/// </summary>
		public const int eMaxNameSize = 32;
		private const long IDHIGHBIT  = 0x2000000000000000L;

		// never used, but may one day:
		// private static long IDHIGHBIT2 = 0x1000000000000000L;
		// never used, but may one day: static int eHIDInvalid = 1;

		// The constuctor. No special stuff here, but you must create one in order to use
		// the object. It initializes some internal stuff.
		public HtmTrixel() {
			int i, j;
			anchor = new double[6,3];
			anchor[0,0] = 0.0;
			anchor[0,1] = 0.0;
			anchor[0,2] = 1.0;

			anchor[1,0] = 1.0;
			anchor[1,1] = 0.0;
			anchor[1,2] = 0.0;

			anchor[2,0] = 0.0;
			anchor[2,1] = 1.0;
			anchor[2,2] = 0.0;

			anchor[3,0] = -1.0;
			anchor[3,1] =  0.0;
			anchor[3,2] =  0.0;

			anchor[4,0] =  0.0;
			anchor[4,1] = -1.0;
			anchor[4,2] =   0.0;

			anchor[5,0] =  0.0;
			anchor[5,1] =  0.0;
			anchor[5,2] = -1.0;
			
			bases = new _HtmBase[8];
			bases[0].name = new char[] {'S', '2'};
			bases[1].name = new char[] {'N', '1'};
			bases[2].name = new char[] {'S', '1'};
			bases[3].name = new char[] {'N', '2'};
			bases[4].name = new char[] {'S', '3'};
			bases[5].name = new char[] {'N', '0'};
			bases[6].name = new char[] {'S', '0'};
			bases[7].name = new char[] {'N', '3'};
			i=0;
			j=0;
			for(j=0; j<8; j++){
				bases[j].HID = (long) magicnumbers[i++];
				bases[j].v1  = magicnumbers[i++];

				bases[j].v2  = magicnumbers[i++];
				bases[j].v3 =  magicnumbers[i++];
			}
			vertex_indeces = new int[,] {{1, 5, 2}, {2, 5, 3}, {3, 5, 4}, {4, 5, 1}, {
																						 1, 0, 4}, {4, 0, 3}, {3, 0, 2}, {2, 0, 1}};

			//			S_indexes = new int[,] { {1, 5, 2}, {2, 5, 3}, {3, 5, 4}, {4, 5, 1}}; 
			//			N_indexes = new int[,] { {1, 0, 4}, {4, 0, 3}, {3, 0, 2}, {2, 0, 1}};

			//			newNode(1,5,2,8,0);  // S0
			//			newNode(2,5,3,9,0);  // S1
			//			newNode(3,5,4,10,0); // S2
			//			newNode(4,5,1,11,0); // S3
			//			newNode(1,0,4,12,0); // N0
			//			newNode(4,0,3,13,0); // N1
			//			newNode(3,0,2,14,0); // N2
			//			newNode(2,0,1,15,0); // N3
		}

		/// <summary>
		/// This array contains the cartesian coordinates of
		/// the octahedron
		/// </summary>
		private double[,] anchor;
		private _HtmBase[] bases; 

		/* static */ private int[] magicnumbers =
			new int []{
						  10, 3,5,4,
						  13, 4,0,3,
						  9, 2,5,3,
						  14, 3,0,2,
						  11, 4,5,1,
						  12, 1,0,4,
						  8, 1,5,2,
						  15, 2,0,1
					  };
		private int[,] vertex_indeces;
		//		int[,] S_indexes;
		//		int[,] N_indexes;

		/// <summary>
		/// Convert a Cartesian coordinate to a HID.
		/// The most often used function. Given a cartesian coordinate
		/// and a level number, it returns the 64 bit HID.
		/// <b>WARNING!</b>
		/// x, y, z are assumed to be normalized, so this function
		/// doesn't waste time normalzing.
		/// </summary>
		/// <param name="x">X coordinate of location</param>
		/// <param name="y">Y coordinate of location</param>
		/// <param name="z">Z coordinate of location</param>
		/// <param name="depth">The level of the HID</param>
		/// <returns>HID</returns>
		public Int64 cartesian2HID(double x, double y, double z, int depth){

			long startID;
			Int64 hid;

			double[] v1 = new double[3];
			double[] v2 = new double[3];
			double[] v0 = new double[3];

			double[] w1 = new double[3];
			double[] w2 = new double[3];
			double[] w0 = new double[3];
			double[] p  = new double[3];

			p[0] = x;
			p[1] = y;
			p[2] = z;

			//
			// Get the ID of the level0 triangle, and its starting vertices
			//

			startID = startpane(v0, v1, v2, x, y, z, null);
  
			if (startID < 8) {
				hid = 1;
				return hid;
			}
			hid = startID;
			//
			// Start searching for the children
			///
			while(depth-- > 0){
				hid <<= 2;
				midpoint(v0, v1, w2);
				midpoint(v1, v2, w0);
				midpoint(v2, v0, w1);
				if (isinside(p, v0, w2, w1)) {
					// hid |= 0;
					copy_vec(v1, w2);
					copy_vec(v2, w1);
				}
				else if (isinside(p, v1, w0, w2)) {
					hid |= 1;
					copy_vec(v0, v1);
					copy_vec(v1, w0);
					copy_vec(v2, w2);
				}
				else if (isinside(p, v2, w1, w0)) {
					hid |= 2;
					copy_vec(v0, v2);
					copy_vec(v1, w1);
					copy_vec(v2, w0);
				}
				else if (isinside(p, w0, w1, w2)) {
					hid |= 3;
					copy_vec(v0, w0);
					copy_vec(v1, w1);
					copy_vec(v2, w2);
				}
				else {
					// CATASTROPHIC ERROR!!! THROW Something!
					throw new Exception("Panic in cartesian2hid");
				}
			}
			
			return hid;
		}

		/// <summary>
		/// Convert the location given by (x, y, z) to the symbolic text
		/// name of the HID
		/// <strong>WARNING</strong>:
		/// x, y, z are assumed to be normalized, so this function
		/// doesn't waste time normalizing.
		/// </summary>
		/// <param name="nam">Character array that holds the text for
		/// the trixel's name</param>
		/// <param name="x">X coordinate of location</param>
		/// <param name="y">Y coordinate of location</param>
		/// <param name="z">Z coordinate of location</param>
		/// <param name="depth">The level of the HID</param>
		/// <returns>true if conversion was successful</returns>
		public bool cartesian2name(char[] nam,double x, double y, double z, int depth){

			long startID;
			int len = 0;

			double[] v1 = new double[3];
			double[] v2 = new double[3];
			double[] v0 = new double[3];

			double[] w1 = new double[3];
			double[] w2 = new double[3];
			double[] w0 = new double[3];
			double[] p  = new double[3];

			p[0] = x;
			p[1] = y;
			p[2] = z;

			//
			// Get the ID of the level0 triangle, and its starting vertices
			//

			startID = startpane(v0, v1, v2, x, y, z, nam);
  
			if (startID < 8) {
				nam[0] = 'X';
				nam[1] = 'X';
				nam[2] = '\0';
				// strcpy(nam, "BS Illegal startpane");
			}
			len = 2;
			//
			// Start searching for the children
			///
			while(depth-- > 0){
				midpoint(v0, v1, w2);
				midpoint(v1, v2, w0);
				midpoint(v2, v0, w1);
				if (isinside(p, v0, w2, w1)) {
					nam[len++] = '0';
					copy_vec(v1, w2);
					copy_vec(v2, w1);
				}
				else if (isinside(p, v1, w0, w2)) {
					nam[len++] = '1';
					copy_vec(v0, v1);
					copy_vec(v1, w0);
					copy_vec(v2, w2);
				}
				else if (isinside(p, v2, w1, w0)) {
					nam[len++] = '2';
					copy_vec(v0, v2);
					copy_vec(v1, w1);
					copy_vec(v2, w0);
				}
				else if (isinside(p, w0, w1, w2)) {
					nam[len++] ='3';
					copy_vec(v0, w0);
					copy_vec(v1, w1);
					copy_vec(v2, w2);
				}
				else {
					// CATASTROPHIC ERROR!!! THROW Something!
					throw new Exception("Panic in cartesian2name");
				}
			}
			nam[len] = '\0';
			return true;
		}

		/// <summary>
		/// Convert the named trixel to a triangle described
		/// by three vertices.
		/// The vertices are given by three arrays of three doubles.
		/// The coordinates of the triangles are given in
		/// the order (x, y, z) and so that the location
		/// is on the surface of a unit sphere
		/// </summary>
		/// <param name="name">The text description of the trixel</param>
		/// <param name="v0">The X coordinate</param>
		/// <param name="v1">The Y coordinate</param>
		/// <param name="v2">The Z coordinate</param>
		/// <returns>true, if the conversion succeeded, false otherwise</returns>
		public bool name2Triangle(char[] name, double[] v0, double[] v1, double[] v2) {
			bool rstat = false;
			double[] w1 = new double[3];
			double[] w2 = new double[3];
			double[] w0 = new double[3];


			//
			// Get the top level hemi-demi-semi space
			//
			int k;
			int off0, off1, off2;
			k = (int) name[1] - '0';
			if (k < 0) {// Do not have a valid name
				return rstat;
			}
			if (name[0] != 'N' && name[0] != 'S'){
				return rstat;
			}

			if (name[0] == 'N') k += 4;
			off0 = vertex_indeces[k,0];
			off1 = vertex_indeces[k,1];
			off2 = vertex_indeces[k,2];

			v0[0] = anchor[off0, 0];
			v0[1] = anchor[off0, 1];
			v0[2] = anchor[off0, 2];

			v1[0] = anchor[off1, 0];
			v1[1] = anchor[off1, 1];
			v1[2] = anchor[off1, 2];
			
			v2[0] = anchor[off2, 0];
			v2[1] = anchor[off2, 1];
			v2[2] = anchor[off2, 2];

			k = 2;
			while(name[k] != '\0'){
				midpoint(v0, v1, w2);
				midpoint(v1, v2, w0);
				midpoint(v2, v0, w1);
				switch(name[k]) {
					case '0':
						copy_vec(v1, w2);
						copy_vec(v2, w1);
						break;
					case '1':
						copy_vec(v0, v1);
						copy_vec(v1, w0);
						copy_vec(v2, w2);
						break;
					case '2':
						copy_vec(v0, v2);
						copy_vec(v1, w1);
						copy_vec(v2, w0);
						break;
					case '3':
						copy_vec(v0, w0);
						copy_vec(v1, w1);
						copy_vec(v2, w2);
						break;
				}
				k++;
			}
			rstat = true;
			return rstat;
		}

		/// <summary>
		/// A faster way of getting to the vertices
		/// of a top level (level 0) trixel.
		/// You should use
		/// name2Triangle instead, unless you really know
		/// what you are doing
		/// </summary>
		/// <param name="index">a number between 0 and 7 (inclusive)</param>
		/// <param name="v0">First vertex</param>
		/// <param name="v1">Second vertex</param>
		/// <param name="v2">Third vertex</param>
		public void level0vertices(int index, double[] v0, double[] v1, double[] v2){
			//
			// index 0 is for S0, 1 for S1, etc...
			//
			// t = vertex_index[index, j] is vertex j
			// xj = anchor(j,k)
			//
			int t;
			//
			//
			t = vertex_indeces[index, 0];
			v0[0] = anchor[t, 0];
			v0[1] = anchor[t, 1];
			v0[2] = anchor[t, 2];

			t = vertex_indeces[index, 1];
			v1[0] = anchor[t, 0];
			v1[1] = anchor[t, 1];
			v1[2] = anchor[t, 2];

			t = vertex_indeces[index, 2];
			v2[0] = anchor[t, 0];
			v2[1] = anchor[t, 1];
			v2[2] = anchor[t, 2];

			return;
		}

		///////////////////////////////////////////// private METHODS
		/// startpane, strcpy, midpoint, isinside, copyvec
		/// 
		private long startpane(
			double[] v1, double[] v2, double[] v3,
			double xin, double yin, double zin,
			char[] name) {
			int t;
			long  baseID;
			int baseindex;

			if ((xin > 0) && (yin >= 0)){
				baseindex = (zin >= 0) ? (int) Startindex._N3 : (int) Startindex._S0;

			} else if ((xin <= 0) && (yin > 0)){
				baseindex = (zin >= 0) ? (int) Startindex._N2 : (int) Startindex._S1;

			} else if ((xin < 0) && (yin <= 0)){
				baseindex = (zin >= 0) ? (int) Startindex._N1 : (int) Startindex._S2;
 
			} else if ((xin >= 0) && (yin < 0)){
				baseindex = (zin >= 0) ? (int) Startindex._N0 : (int) Startindex._S3;
			} else {
				baseindex = (zin >= 0) ? (int) Startindex._N3 : (int) Startindex._S0;
				// was: baseindex = -1;
			}
			if (baseindex < 0)
				return -1;

			baseID = bases[baseindex].HID;
	
			t = bases[baseindex].v1;
			v1[0] = anchor[t, 0];
			v1[1] = anchor[t, 1];
			v1[2] = anchor[t, 2];

			t = bases[baseindex].v2;
			v2[0] = anchor[t, 0];
			v2[1] = anchor[t, 1];
			v2[2] = anchor[t, 2];

			t = bases[baseindex].v3;
			v3[0] = anchor[t, 0];
			v3[1] = anchor[t, 1];
			v3[2] = anchor[t, 2];

			if (name != null){
				name[0] = bases[baseindex].name[0];
				name[1] = bases[baseindex].name[1];
				name[2] = '\0';}
			return bases[baseindex].HID;
		}
		private void midpoint(double[] v1, double[] v2, double[] w){
			double x, y, z, norm;
			x = v1[0] + v2[0];
			y = v1[1] + v2[1];
			z = v1[2] + v2[2];
			norm = x*x + y*y + z*z;
			norm = Math.Sqrt(norm);
			w[0] = x/norm;
			w[1] = y/norm;
			w[2] = z/norm;
			return;
		}
		private bool isinside(double[] p, double[] v1, double[] v2, double[] v3) {
			double[] crossp = new double[3];
			// if (v1 X v2) . p < epsilon then false 
			// same for v2 X v3 and v3 X v1.
			// else return true..
			crossp[0] = v1[1] * v2[2] - v2[1] * v1[2];
			crossp[1] = v1[2] * v2[0] - v2[2] * v1[0];
			crossp[2] = v1[0] * v2[1] - v2[0] * v1[1];
			if (p[0] * crossp[0] + p[1] * crossp[1] + p[2] * crossp[2] < -Cartesian.Epsilon)
				return false;


			crossp[0] = v2[1] * v3[2] - v3[1] * v2[2];
			crossp[1] = v2[2] * v3[0] - v3[2] * v2[0];
			crossp[2] = v2[0] * v3[1] - v3[0] * v2[1];
			if (p[0] * crossp[0] + p[1] * crossp[1] + p[2] * crossp[2] < -Cartesian.Epsilon)
				return false;


			crossp[0] = v3[1] * v1[2] - v1[1] * v3[2];
			crossp[1] = v3[2] * v1[0] - v1[2] * v3[0];
			crossp[2] = v3[0] * v1[1] - v1[0] * v3[1];
			if (p[0] * crossp[0] + p[1] * crossp[1] + p[2] * crossp[2] < -Cartesian.Epsilon)
				return false;

			return true;
		}
		private void copy_vec(double[] dest, double[] src){
			dest[0] = src[0];
			dest[1] = src[1];
			dest[2] = src[2];
		}

		/// <summary>
		/// Convert a 64-bit HID to a text desciption of the trixel.
		/// </summary>
		/// <param name="name">An array of HtmTrixel.eMaxNameSize (a const in this class)
		/// characters. The array is null terminated.</param>
		/// <param name="hid">The HID</param>
		/// <returns>The size, or the length of the text</returns>
		public int hid2name(char[] name, long hid) {
			int size=0, i;
			int c;                                   // a spare character;
			long shifted_bit;
			long shifted_hid;
			// determine index of first set bit, top 2 always assumed 0
			// this is to eliminate the problem of mixing ulongs with longs
			//
			if(hid < 0)
				return -2; // higy bit set

			if(hid < 8)
				return -1; // -1 means Bad hid

			for(i=2; i< eHIDBits; i+=2){
				shifted_hid = hid << (i-2);
				shifted_bit = (shifted_hid) & IDHIGHBIT;
				if (shifted_bit != 0)
					break;
			}
			

			size=(eHIDBits - i) >> 1;
			//
			// fill characters starting with the last one
			//
			for(i = 0; i < size-1; i++) {
				c =  '0' + (int) ((hid >> i*2) & (int) 3);
				name[size-i-1] = (char ) c;
			}
			//
			// put in first character
			//
			shifted_bit = (hid >> (size*2-2)) & 1;
			if(shifted_bit != 0) {
				name[0] = 'N';
			} else {
				name[0] = 'S';
			}
			name[size] = '\0'; // end string
			return size;
		}

		/// <summary>
		/// Convert the trixel from text to 64 bit HID.
		/// </summary>
		/// <param name="sname">The string with the text representation of 
		/// the trixel.</param>
		/// <returns>The 64 bit HID</returns>
		public Int64 name2ID(string sname){
			int length = sname.Length;
			char[] name = sname.ToCharArray();
			return name2ID(name, length);
		}

		/// <summary>
		/// Convert the trixel from text to 64 bit HID
		/// The character array must be of size eMaxNameSize, and
		/// it is not necessary to have null termination. The
		/// number of siginificant characters in the trixel's name
		/// is given as a parameter.
		/// </summary>
		/// <param name="name">The character array with the text 
		/// representation of the trixel.</param>
		/// <param name="effectivelength">The number of significant characters
		/// in the array.</param>
		/// <returns>The 64 bit HID</returns>
		public long name2ID(char[] name, int effectivelength){

			long result_hid=0;
			long shifted;
			int i;
			int siz = 0;
			long bits;
			int shift;
			siz = name.Length;
			if(name.Length < 2)
				return result_hid;	// 0 is an illegal HID

			if(name[0] != 'N' && name[0] != 'S')  // invalid name
				return result_hid;
			if(siz > eMaxNameSize)
				return result_hid;

			siz = effectivelength;
			for(i = siz-1; i > 0; i--) {// set bits starting from the end
				if(name[i] > '3' || name[i] < '0') {// invalid name
					return result_hid;
				}
				bits = ( (int)(name[i]-'0'));
				shift = 2*(siz - i -1);
				shifted = bits << shift;
				result_hid +=  shifted;
			}
			bits = 2;                     // set first pair of bits, first bit always set
			if(name[0]=='N') bits++;      // for north set second bit too
			shift = (2*siz - 2);
			shifted = bits << shift;
			result_hid += shifted;
			return result_hid;
		}

		/// <summary>
		/// Truncate the HID to fewer bits.
		/// The HID of a trixel implicitly contains the level of the trixel.
		/// When you need a lower level trixel that contains the trixel
		/// of the given HID, this function gives it to you.
		/// If the level of
		/// the given htmid is less than or equal to the desired level,
		/// then there is no change.
		/// </summary>
		/// <param name="htmid"></param>
		/// <param name="level"></param>
		/// <returns>HID of lower level trixel</returns>
		public Int64 truncatehid(Int64 htmid, int level){
			Int64 result = htmid;
			int currentlevel = levelOfHid(htmid);
			if (level < currentlevel){
				result = (htmid >> 2 * (currentlevel - level));
			}
			return result;
		}

		/// <summary>
		/// Extend given HID to a desired level.
		/// The opposite of truncate. However,
		/// because there are many descendents, the result is a range of
		/// consecutive HIDs. The low and hi values are returned in 
		/// the out variables.
		/// </summary>
		/// <param name="htmid">HID to extend</param>
		/// <param name="level">New level to which to extend</param>
		/// <param name="lo">Low HID value of range</param>
		/// <param name="hi">High HID value of range</param>
		/// <returns>0 (for no apparently good reason)</returns>
		public int extendhid(Int64 htmid, int level, out Int64 lo, out Int64 hi){
			int currentlevel;
			int changelevel;
			Int64 dif;
			currentlevel = levelOfHid(htmid);
			if (level > currentlevel){
				// amount to extend:
				changelevel = level - currentlevel;
				lo = htmid << (2 * changelevel);
				dif = 1L << (2 * changelevel); // Make sure 64 bit stuff works
				dif--;
				hi = lo + dif;
			} else {
				//truncate
				changelevel = currentlevel - level; // could be 0
				lo = htmid >> (2 * changelevel);
				hi = lo;
			}
			return 0;
		}

		/// <summary>
        /// Returns the level number of an HID
		/// <param name="htmid">The HID</param>
		/// <returns>The level number</returns>
		public static int levelOfHid(Int64 htmid){
			Int32 size=0, i;
			if (htmid < 0)
				return -1;
			for(i = 2; i < eHIDBits; i+=2) {
				if ( 0 != ((htmid << (i-2)) & IDHIGHBIT) )
					break;
			}
			size=(eHIDBits-i) / 2;
  			return size-2;
		}

		/// <summary>
		/// Get the internal angles of the trixel described by
		/// a given HID.
		/// </summary>
		/// <param name="hid">The 64-bit HID</param>
		/// <param name="t1">First internal angle</param>
		/// <param name="t2">Second internal angle</param>
		/// <param name="t3">Third internal angle</param>
		/// <returns>true if successful, false if HID is invalid</returns>
		public bool getAngles(Int64 hid, out double t1, out double t2, out double t3){
			// formula for area of spherical triangle
			// A = R^2[(t1 + t2 + t3 ) - Pi], but R=1.
			//
			Cartesian n0, n1, n2;
			double[] v0, v1, v2;
			v0 = new double[3];
			v1 = new double[3];
			v2 = new double[3];
			char[] name = new char[HtmTrixel.eMaxNameSize];
			int level = this.hid2name(name, hid);
			if (level < 0) {
				t1 = t2 = t3 = 0.0;
				return false;
			} else {

				this.name2Triangle(name, v0, v1, v2);
				//
				// Compute the plane normals for the three GC arcs
				//
				n0 = new Cartesian();
				n1 = new Cartesian();
				n2 = new Cartesian();
				Cartesian.cross(n2, v1, v0);
				Cartesian.cross(n0, v2, v1);
				Cartesian.cross(n1, v0, v2);
				n0.normalizeMe();
				n1.normalizeMe();
				n2.normalizeMe();
				t1 = Math.PI - Math.Acos(Cartesian.dot(n0, n1));
				t2 = Math.PI - Math.Acos(Cartesian.dot(n1, n2));
				t3 = Math.PI - Math.Acos(Cartesian.dot(n2, n0));
			}
			return true	;
		}

		/// <summary>
		/// Provides a rough idea of the length of a triangle side at a given
		/// level. This is based solely on the number of times a great circle
		/// segment is bisected. Actual lengths of trixels sides vary.
		/// </summary>
		/// <param name="level"></param>
		/// <returns>Nominal size of trixel edge in degrees.</returns>
		public static double getFeatureSize(int level){
			double angle = Cartesian.Pi/2.0; // level 0 is 90 degrees
			if (level == 0)
				return angle;

			for(int i=0; i<level; i++){
				angle /= 2.0;
			}
			return angle;
		}
	}
}
