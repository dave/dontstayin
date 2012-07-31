using System;

namespace HTM
{
	/// <summary>
	/// This class maintains writeable global variables for otherwise static method
	/// This is a sigleton object
	/// </summary>
	/// Things have been flagged MAKESTATE in other source files that are affeced
	public sealed class HtmState
	{
		// //////////////////////////////////////////////
		private const string _versionstring = "C# HTM.DLL V.1.0.0 15 May 2005";
		private int _halfspace_instance = 0; // each halfspace may have a unique number
		private int _minlevel = 3;  // HtmCover needs it
		private int _maxlevel = 20; // HtmCover needs it
		private int _times = 0;

		private double[] _featuresizes = null;
		static readonly HtmState instance = new HtmState();
		HtmState()
		{
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
			return HtmState._versionstring;
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
