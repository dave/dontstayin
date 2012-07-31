using System;

/*=====================================================================

  File:      SpacitalVector.cs for Spatial Sample
  Summary:   Implements a cartesian vector with right ascension and declination
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
	/// Very much like Cartesian, with the addition of
	/// Right Ascension and Declination
	/// </summary>
	public class SpatialVector : Cartesian {
		
		internal double _ra;
		internal double _dec;
		internal bool _okRaDec;
		internal bool _okXYZ;

		/// <summary>
		/// Parameter based constructor for and instance.
		/// The SpatialVector is constructed from parameters (x, y, z)
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public SpatialVector(double x, double y, double z) {
			_x = x;
			_y = y;
			_z = z;
			_okXYZ = false;
			// updateRaDec();
		}

		/// <summary>
		/// Default (parameterless) constructor.
		/// Initial value is (0, 0, 1) or ra = 0, dec = 90
		/// </summary>
		public SpatialVector() {
			_okRaDec = true;
			_ra = 0.0;
			_dec = 90.0;
			_okXYZ = true;
			_okRaDec = true;
			
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="v">A SpatialVector from which to copy</param>
		public SpatialVector(SpatialVector v){
			_x = v._x;
			_y = v._y;
			_z = v._z;
			_ra = v._ra;
			_dec = v._dec;
			_okRaDec = v._okRaDec;
			_okXYZ = v._okXYZ;
		}

		/// <summary>
		/// Copy constructor.
		/// </summary>
		/// <param name="v">A Cartesian from which to copy</param>
		public SpatialVector(Cartesian v) {
			_x = v.x;
			_y = v.y;
			_z = v.z;
			_okXYZ = false;
			// updateRaDec();
		}

		/// <summary>
		/// Perform cross prodcut of this and another
		/// vector. Operation writes result into this vector
		/// </summary>
		/// <param name="that">The other vector</param>
		public void crossMe(SpatialVector that){
			Cartesian a = that;
			this.crossMe(a);
		}

		/// <summary>
		/// Perform cross prodcut of this and another
		/// vector. Operation returns a new instance of a
		/// Cartesian. 
		/// </summary>
		/// <param name="that">The other vector</param>
		/// <returns>A new Cartesian</returns>
		public Cartesian cross(SpatialVector that){
			Cartesian a = this;
			Cartesian b = that;
			return a.cross(b);
		}

		/// <summary>
		/// Perform dot product between this and another
		/// vector.
		/// </summary>
		/// <param name="that">The other vector</param>
		/// <returns>The dot product</returns>
		public double dot(SpatialVector that){
			Cartesian a = this;
			Cartesian b = that;
			return a.dot(b);
		}
		

		/// <summary>
		/// The RA of a SpatialVector
		/// </summary>
		public double ra {
			get {
				if(!_okRaDec){
					updateRaDec();
				}
				return _ra;
			}
			set {
				_ra = value;
				_okXYZ = false;
			}
		}

		/// <summary>
		/// The DEC of a SpatialVector
		/// </summary>
		public double dec {
			get {
				if(!_okRaDec){
					updateRaDec();
				}
				return _dec;
			}
			set {
				_dec = value;
				_okXYZ = false;
			}
		}

		/// <summary>
		/// Force the update of internal state variables.
		/// When x, y, or z changes, the internal ra/dec variables
		/// are not immediately updated to avoid unnecessary
		/// calculations. When this function is called, the RA and
		/// DEC values are calculated and stored
		/// </summary>
		public void updateRaDec() {
			_dec = Math.Asin(_z) * RTOD; // easy.
			double cd = Cartesian.Cos(_dec * DTOR);
			if(cd> Epsilon || cd < -Epsilon)
				if(_y> Epsilon || _y < -Epsilon)
					if (_y < 0.0)
						_ra = 360 - Math.Acos(_x/cd) * RTOD;
					else
						_ra = Math.Acos(_x/cd) * RTOD;
				else
					_ra = (_x < 0.0 ? 180.0 : 0.0);
			else 
				_ra = 0.0;
			_okRaDec = true;
		}

		/// <summary>
		/// Force the update of internal state variables.
		/// When RA or DEC changes, the internal x, y, z variables
		/// are not immediately updated to avoid unnecessary
		/// calculations. When this function is called, the x, y and z
		/// values are calculated from the internal RA/DEC variables
		/// and stored
		/// </summary>
		public void updateXYZ() {
			double cd = Cartesian.Cos(_dec * DTOR);
  
			double diff;
			diff = 90.0 - _dec;
			/// First, compute Z, consider cases, where declination is almost
			/// +/- 90 degrees
			if (diff < Epsilon && diff > -Epsilon){
				_x = 0.0;
				_y = 0.0;
				_z = 1.0;
				_okXYZ = true;
				return;
			}
			diff = -90.0 - _dec;
			if (diff < Epsilon && diff > -Epsilon){
				_x = 0.0;
				_y = 0.0;
				_z = -1.0;
				_okXYZ = true;
				return;
			}
			z = Math.Sin(dec * DTOR);
			///
			/// If we get here, then 
			/// at least z is not singular
			/// 
			double quadrant;
			double qint;
			int iint;
			quadrant = ra / 90.0; // how close is it to an integer?
			// if quadrant is (almost) an integer, force x, y to particular
			// values of quad:
			// quad,   (x,y)
			// 0       (1,0)
			// 1,      (0,1)
			// 2,      (-1,0)
			// 3,      (0,-1)
			// q>3, make q = q mod 4, and reduce to above
			// q mod 4 should be 0.
			qint = (double) ((int) quadrant);
			if(Math.Abs(qint - quadrant) < Epsilon){
				iint = (int) qint;
				iint %= 4;
				if (iint < 0) iint += 4;
				switch(iint){
					case 0:
						_x = cd;
						_y = 0.0;
						break;
					case 1:
						_x = 0.0;
						_y = cd;
						break;
					case 2:
						_x = -cd;
						_y = 0.0;
						break;
					case 3:
					default:
						_x = 0.0;
						_y = -cd;
						break;
				}
			} else {
				_x = Cartesian.Cos(_ra * DTOR) * cd;
				_y = Math.Sin(_ra * DTOR) * cd;
			}
			_okXYZ = true;
			return;
		}

		/// <summary>
		/// Convert (x,y,z) to RA,DEC.
		/// (x,y,z) must be normalized.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="ra">out in degrees</param>
		/// <param name="dec">out in degrees</param>
		static public void xyz2radec(double x, double y, double z, out double ra, out double dec){
			dec = Math.Asin(z) * RTOD;
			double cd = Cartesian.Cos(dec * DTOR);
			if(cd> Epsilon || cd < -Epsilon)
				if(y> Epsilon || y < -Epsilon)
					if (y < 0.0)
						ra = 360 - Math.Acos(x/cd) * RTOD;
					else
						ra = Math.Acos(x/cd) * RTOD;
				else
					ra = (x < 0.0 ? 180.0 : 0.0);
			else 
				ra = 0.0;
			return;
		}

		/// <summary>
		/// Convert (RA, DEC) to (x, y, z).
		/// RA, DEC must be well defined.
		/// </summary>
		/// <param name="ra">in degrees</param>
		/// <param name="dec">in degrees</param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		static public void radec2cartesian(double ra, double dec, out double x, out double y, out double z){
			double diff;
			double cd = Cartesian.Cos(dec * DTOR);
			diff = 90.0 - dec;
			/// First, compute Z, consider cases, where declination is almost
			/// +/- 90 degrees
			if (diff < Epsilon && diff > -Epsilon){
				x = 0.0;
				y = 0.0;
				z = 1.0;
				return;
			}
			diff = -90.0 - dec;
			if (diff < Epsilon && diff > -Epsilon){
				x = 0.0;
				y = 0.0;
				z = -1.0;
				return;
			}
			z = Math.Sin(dec * DTOR);
			///
			/// If we get here, then 
			/// at least z is not singular
			///
			double quadrant;
			double qint;
			int iint;
			quadrant = ra / 90.0; // how close is it to an integer?
			// if quadrant is (almost) an integer, force x, y to particular
			// values of quad:
			// quad,   (x,y)
			// 0       (1,0)
			// 1,      (0,1)
			// 2,      (-1,0)
			// 3,      (0,-1)
			// q>3, make q = q mod 4, and reduce to above
			// q mod 4 should be 0.
			qint = (double) ((int) quadrant);
			if(Math.Abs(qint - quadrant) < Epsilon){
				iint = (int) qint;
				iint %= 4;
				if (iint < 0) iint += 4;
				switch(iint){
					case 0:
						x = cd;
						y = 0.0;
						break;
					case 1:
						x = 0.0;
						y = cd;
						break;
					case 2:
						x = -cd;
						y = 0.0;
						break;
					case 3:
					default:
						x = 0.0;
						y = -cd;
						break;
				}
			} else {
				x = Cartesian.Cos(ra * DTOR) * cd;
				y = Math.Sin(ra * DTOR) * cd;
			}
			return;
		}
	}
}
