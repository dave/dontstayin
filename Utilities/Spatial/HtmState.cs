using System;
using System.Text;
/*=====================================================================

  File:      HtmState.cs for Spatial Sample
  Summary:   Implements a singlton object for holding global state.
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
	/// This class maintains writeable global variables for otherwise static method
	/// This is a sigleton object
	/// </summary>
	/// Things have been flagged MAKESTATE in other source files that are affeced
	public sealed class HtmState
	{
		[CLSCompliant(false)]
		public const int _D_minlevel = 3;
		[CLSCompliant(false)]
		public bool _appendTrace = false;
		// //////////////////////////////////////////////
		private const string _versionstring = "C# HTM.DLL V.1.0.0; 2005 August 1";
		private int _hdelta      =  2;
		private int _magicnumber = 28;
		private int _halfspace_instance = 0; // each halfspace may have a unique number
		private int _minlevel = 3;  // HtmCover needs it
		//DSI likes it 23
		private int _maxlevel = 20; // HtmCover needs it
		private int _times = 0;

		// EXPERIMENTAL, GYF 
		private Int64 _tcount = 0L;
		// private int _accessed = 0;

		private double[] _featuresizes = null;
		static readonly HtmState instance = new HtmState();
		/// FOR MAKING STATIS HTMLOOKUP
		public  int[] magicnumbers;
		public  int[,] vertex_indeces;
		public  double[,] anchor;
		[CLSCompliant(false)]
		public _HtmBase[] bases;

		HtmState()
		{
			magicnumbers = new int[]{
						  10, 3,5,4,
						  13, 4,0,3,
						   9, 2,5,3,
						  14, 3,0,2,
						  11, 4,5,1,
						  12, 1,0,4,
						   8, 1,5,2,
						  15, 2,0,1
					  };
			anchor = new double[,] {
				{0.0, 0.0, 1.0}, {1.0, 0.0, 0.0}, {0.0, 1.0, 0.0},
				{-1.0, 0.0, 0.0}, {0.0, -1.0, 0.0}, {0.0, 0.0, -1.0}};


			vertex_indeces = new int[,] {{1, 5, 2}, {2, 5, 3}, {3, 5, 4}, {4, 5, 1}, {
                             1, 0, 4}, {4, 0, 3}, {3, 0, 2}, {2, 0, 1}};
			int i, j;

			bases = new _HtmBase[8];
			bases[0].name = new char[] { 'S', '2' };
			bases[1].name = new char[] { 'N', '1' };
			bases[2].name = new char[] { 'S', '1' };
			bases[3].name = new char[] { 'N', '2' };
			bases[4].name = new char[] { 'S', '3' };
			bases[5].name = new char[] { 'N', '0' };
			bases[6].name = new char[] { 'S', '0' };
			bases[7].name = new char[] { 'N', '3' };
			i = 0;
			j = 0;
			for (j = 0; j < 8; j++) {
				bases[j].HID = (long)magicnumbers[i++];
				bases[j].v1 = magicnumbers[i++];

				bases[j].v2 = magicnumbers[i++];
				bases[j].v3 = magicnumbers[i++];
			}

			_featuresizes = new double[] {
							  1.5707963267949,        //0
							  0.785398163397448,      //1
							  0.392699081698724,      //2
							  0.196349540849362,
							  0.098174770424681,
							  0.0490873852123405,     //5
							  0.0245436926061703,
							  0.0122718463030851,
							  0.00613592315154256,
							  0.00306796157577128,
							  0.00153398078788564,   //10
							  0.000766990393942821,
							  0.00038349519697141,
							  0.000191747598485705,
							  9.58737992428526E-05,
							  4.79368996214263E-05,  //15
							  2.39684498107131E-05,
							  1.19842249053566E-05,
							  5.99211245267829E-06,
							  2.99605622633914E-06,
							  1.49802811316957E-06,  //20
							  7.49014056584786E-07,  //21
							  3.74507028292393E-07,  //22
							  0.0,
							 -1.0 // to stop search beyond 0
						  };

		}
		static HtmState()
		{
			// 
			// TODO: Add constructor logic here
			//
		}
		public static HtmState Instance
		{
			get
			{
				return instance;
			}
		}
		public int times
		{
			get
			{
				return _times;
			}
		}
		public int minlevel
		{
			get
			{
				_times++;
				return _minlevel;
			}
			set
			{
				_times++;
				_minlevel = value;
			}
		}
		public int maxlevel
		{
			get
			{
				_times++;
				return _maxlevel;
			}
			set
			{
				_times++;
				_maxlevel = value;
			}
		}
		public Int64 tcount {
			get {
				return _tcount;
			}
			set {
				_tcount = value;
			}
		}
		public int hdelta {
			get {
				return _hdelta;
			}
			set {
				_hdelta = value;
			}
		}
		public int magicnumber {
			get {
				return _magicnumber;
			}
			set {
				_magicnumber = value;
			}
		}
		public int newhs
		{
			get
			{
				_times++;
				_halfspace_instance++;
				return _halfspace_instance;
			}
		}
		public string getVersion()
		{
			//this._accessed++;
			//StringBuilder sb = new StringBuilder();
			//sb.Append(_versionstring);
			//sb.AppendFormat(" accessed: {0}", _accessed);
			//return sb.ToString(); //
			return _versionstring;
		}
		public int getLevel(double radius)
		{
			int i = 0;
			while (this._featuresizes[i] > radius)
			{
				i++; // there is a -1 at the end, will force a break
			}
			return i;
		}
	}
}
