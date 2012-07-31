using System;
using System.Text;
using System.IO;
/*=====================================================================

  File:      Halfspace.cs for Spatial Sample
  Summary:   Implements "circular" regions on surface of the sphere.
  Date:	     August 15, 2005

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
	/// Halfspaces are "circular" regions on surface of the
	/// sphere. In many applications the relationship between
	/// many halfspaces, hafspaces and points is important, so
	/// this class implements many functions that serve those applications.
	///
	/// A plane that cuts through the sphere divides the surface into two
	/// partition. Because of this, the cutting plane is oriented, and the
	/// direction of the cutting plane's normal is used to indicate precisely
	/// which of the two partition is described by the cutting plane. The unique
	///
	/// TODO: See if Mixed is required or not
	/// </summary>
	public class Halfspace {
		
		/// <summary>
		/// The Sign characterize the halfspace. 
		///
		/// Positive halfspaces 
		/// are smaller than a hemisphere, whereas negative halfspaces are larger.
		/// Hemishpheres are called "zero sign" halfspaces in this context.
		/// </summary>
		public enum Sign {
			Zero, 		/*!< halfspace is a hemisphere */
			Positive,	/*!< halfspace is smaller than hemisphere */
			Negative,	/*!< halfspace is greater than hemisphere */
			Mixed		///< Unused
		};

		/// <summary>
		/// The Position relates this halfspace to another halfspace
		/// </summary>
		public enum Position {
			Disjoint      = -1,  ///< The two halfspaces have no common points
			Intersecting  =  0,  ///< nontrivial intersectoin
			Surrounds     =  1,  ///< This halfspace contains the other
			Contained     =  2,  ///< This halfspace is surrounded by hte other
			Identical     =  3   ///< The two halfspaces cover the same region
		}

		/// <summary>
		/// The Status is a flag used during Convex simplification
		///
		/// Some properties of a halfspace only make sense in the context of 
		/// other halspaces, such as Convexes.
		/// </summary>
		public enum Status { // Used for simplification
			Reject = -1,  ///< This halfspace will be eliminated from a convex
			Unknown = 0,  ///< This halfspace has no special satus
			Hole = 1,  ///< This halfspace is a Hole
			Root = 2,  ///< see elsewhere...
			Boundingcircle = 3   ///< This halfspace is a bounding circle
		}

		/// <summary>
		/// Visibility has to do with Convex simplification algorithm
		/// </summary>
		public enum Visibility {
			Hide = 0,	///< This halfspace is occluded
			Show = 1	///< This halfspace is visible
		}

		/// <summary>
		/// (Formerly constraint ID) used for tagging with metadata from a database
		/// </summary>
		[CLSCompliant(false)]
		protected Int64 _ID; 

		/// <summary>
		/// Simplification uses this field for flagging various
		/// conditions
		/// </summary>
		[CLSCompliant(false)]
		protected Status _status;

		/// <summary>
		/// Simplification uses this field too 
		/// </summary>
		[CLSCompliant(false)]
		protected Visibility _visibility;

		/// <summary>
		/// The directed distance of the cutting
		/// plane defining the hafspace from the
		/// origin. 
		///
		/// Legal values are between -1 and +1.
		///
		/// In many papers this vas the 'c' in (x, y, z, c)
		/// </summary>
		private double _D;

		/// <summary>
		/// The unit vector normal to the cutting plane.
		/// </summary>
		private SpatialVector _SV;

		/// <summary>
		/// The half-angle of the cone defined by the
		/// halfspace intersecting the sphere and the origin
		/// </summary>
		private double _hangle;

		/// <summary>
		///The Sign of this halfspace
		/// </summary>
		private Sign _sign;

		/// <summary>
		/// West of center point
		/// </summary>
		private SpatialVector _West;

		// TODO: best make another "inside" or "contains" function
		// to test west point within circle... This is a quick hack
		// Convex2Arc.cs is th only affected class.
		//

		/// <summary>
		/// x component of west of center point
		/// </summary>
		public double wx {
			get {
				return _West.x;
			}
		}

		/// <summary>
		/// y component of west of center point
		/// </summary>
		public double wy {
			get {
				return _West.y;
			}
		}

		/// <summary>
		/// z component of west of center point
		/// </summary>
		public double wz {
			 get {
				 return _West.z;
			 }
		}

		/// <summary>
		/// Allocate some internal veriables
		/// </summary>
		private void init(){
			_ID = HtmState.Instance.newhs; // a unique ID for this halfspace
			_D = 0.0;
			_SV = new SpatialVector(0.0, 0.0, 1.0);
			_West = new SpatialVector();
			_hangle = SpatialVector.Pi / 2.0;
			_sign = Sign.Zero;
		}

		/// <summary>
		/// efault constructor makes a hemisphere
		/// sitting on the XY plane, and pointing up the Z axis
		/// </summary>
		public Halfspace() {
			//
			// reminder: _D is the cosine of the half-angle
			init();
		}

		/// <summary>
		/// Make a halfspace with parameters (x, y, c, D)
		/// 
		/// It is not necessary that the directional vector, though normal
		///	to the cutting plane, by definition, be a normal vector.
		/// </summary>
		/// <param name="x">The x coordinate of the cutting plane normal vector</param>
		/// <param name="y">The y coordinate of the cutting plane normal vector</param>
		/// <param name="z">The z coordinate of the cutting plane normal vector</param>
		/// <param name="d">The distance of the cutting plane along the normal
		/// vector's direction. Can be negative.</param>
		public Halfspace(double x, double y, double z, double d){
			init();
			if      (d < -1.0) _D = -1.0;
			else if (d >  1.0) _D = 1.0;
			else               _D = d;
			_SV.assign(x, y, z);
			_SV.normalizeMeSafely();
			if (_D < -SpatialVector.Epsilon) {
				_sign = Sign.Negative;
			} else if (_D >= SpatialVector.Epsilon) {
				_sign = Sign.Positive;
			} else {
				_sign = Sign.Zero;
			}
			_hangle = Math.Acos(_D);
		}

		/// <summary>
		/// Make a halfspace with parameters (V, D)
		/// 
		/// It is not necessary that the directional vector, though normal
		///	to the cutting plane, by definition, be a normal vector.
		/// </summary>
		/// <param name="v">The cutting plane's normal vector</param>
		/// <param name="d">The distance of the cutting plane along the normal
		/// vector's direction. Can be negative.</param>
		public Halfspace(Cartesian v, double d) {
			init();
			if (d < -1.0) _D = -1.0;
			else if (d > 1.0) _D = 1.0;
			else _D = d;
			_SV.assign(v);
			_SV.normalizeMeSafely();
			if (_D < -SpatialVector.Epsilon) {
				_sign = Sign.Negative;
			} else if (_D >= SpatialVector.Epsilon) {
				_sign = Sign.Positive;
			} else {
				_sign = Sign.Zero;
			}
			_hangle = Math.Acos(_D);
		}

		/// <summary>
		/// Make a halfspace with parameters (V, D)
		/// 
		/// It is not necessary that the directional vector, though normal
		///	to the cutting plane, by definition, be a normal vector.
		/// </summary>
		/// <param name="v">The cutting plane's normal vector</param>
		/// <param name="d">The distance of the cutting plane along the normal
		/// vector's direction. Can be negative.</param>
		public Halfspace(SpatialVector v, double d) {
			init();
			if (d < -1.0) _D = -1.0;
			else if (d > 1.0) _D = 1.0;
			else _D = d;
			_SV.assign(v);
			_SV.normalizeMeSafely();
			if (_D < -SpatialVector.Epsilon) {
				_sign = Sign.Negative;
			} else if (_D >= SpatialVector.Epsilon) {
				_sign = Sign.Positive;
			} else {
				_sign = Sign.Zero;
			}
			_hangle = Math.Acos(_D);
		}


		/// <summary>
		/// Copy given halfspace into this one.
		/// </summary>
		/// <param name="other">the halfspace to be copied</param>
		public void copy(Halfspace other){
			_ID = other._ID;
			_D = other._D;
			_SV.x = other.sv.x;
			_SV.y = other.sv.y;
			_SV.z = other.sv.z;
			_hangle = other._hangle;
			_sign = other._sign;
		}

		/// <summary>
		/// Sync all internal variables for self-consistancy
		/// </summary>
		private void updateInternal(){
			if (_D < -1.0) _D = -1.0;
			if (_D >  1.0) _D =  1.0;

			if (_D < -SpatialVector.Epsilon) {
				_sign = Sign.Negative;
				_hangle = Math.Acos(_D);
			} else if (_D >= SpatialVector.Epsilon) {
				_sign = Sign.Positive;
				_hangle = Math.Acos(_D);
			} else {
				_sign = Sign.Zero;
				_hangle = SpatialVector.Pi/2.0;
			}
		}

		/// <summary>
		/// Compute internal West-of-center vector from primary
		/// values (ra, dec, D)
		/// </summary>
		public void updateWest(){
			sv.updateRaDec();
			this._West.x = d * sv.x - Math.Sin(Math.Acos(d)) * Math.Sin(sv.ra);
			this._West.y = d * sv.y + Math.Sin(Math.Acos(d)) * Cartesian.Cos(sv.ra);
			this._West.z = d * sv.z;
		}

		/// <summary>
		/// Set this halfspace to (x, y, z, D)
		/// This function does not explicitly normalize the directional vector
		/// so the user needs to do this explicitly before performing other tasks
		/// that require this vector to be normalized.
		/// </summary>
		/// <param name="x">The X coordinate of cutting plane's normal vector</param>
		/// <param name="y">The Y coordinate of cutting plane's normal vector</param>
		/// <param name="z">The Z coordinate of cutting plane's normal vector</param>
		/// <param name="d">The signed distance of cutting plane along the normal vector</param>
		public void set(double x, double y, double z, double d) {
			if (d < -1.0) _D = -1.0;
			else if (d > 1.0) _D = 1.0;
			else _D = d;
			_SV.x = x;
			_SV.y = y;
			_SV.z = z;
		}
		[CLSCompliant(false)]
		public Visibility visibility
		{
			get {
				return _visibility;
			}
			set {
				_visibility = value;
			}
		}
		[CLSCompliant(false)]
		public Status status
		{
			get {
				return _status;
			}
			set {
				_status = value;
			}
		}
		public Int64 ID {
			get {
				return _ID;
			}
			set {
				_ID = value;
			}
		}
		public SpatialVector sv {
			get {
				return _SV;
			}
		}
		[CLSCompliant(false)]
		public Sign sign
		{
			get {
				return _sign;
			}
		}
		public double hangle {
			get {
				return _hangle;
			}
		}
		public double d {
			get {
				return _D;
			}
			set {
				_D = value;
				this.updateInternal();
			}
		}

		/// <summary>
		/// Test whether or not Halfspace is "null".
		/// It is null, if the area it represents is (almost) zero. Then it is
		/// (almost) a point.
		/// </summary>
		/// <returns>true, if the area it represents is (almost) zero</returns>
		public bool isNull() {
			double c;
			c = this._D;
			if (c >= 1.0 - SpatialVector.Epsilon) // Close enough to +1
				return true;
			else 
				return false;
		}

		/// <summary>
		/// Test whether or not Halfspace is "universal".
		/// It is universal, if the area it represents the entire sphere.
		/// </summary>
		/// <returns>true if the area represented is the entire sphere</returns>
		public bool isUniversal() {
			double c;
			c = this._D;
			if (c <= -1.0 + SpatialVector.Epsilon) // Close enough to -1
				return true;
			else 
				return false;
		}

		/// <summary>
		/// Test whether or not this halfspace completely surrounds
		/// (contains) another halfspace
		/// </summary>
		/// <param name="other">The other halfspace</param>
		/// <returns>true if this halfspace completely surrounds other</returns>
		public bool contains(Halfspace other){
			return (this.relativePosition(other) == Position.Surrounds);
		}

		/// <summary>
		/// Test whether or not point(x, y, z) is inside this halfspace
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns>true if the specified point is inside this halfspace</returns>
		public bool contains(double x, double y, double z){
			return (this.sv.x * x + this.sv.y * y + this.sv.z * z >= _D);
		}

		/// <summary>
		/// Test whether or not point described by vector v is inside
		/// this halfspace
		/// </summary>
		/// <param name="v">A point</param>
		/// <returns>true if the specified point described by v is inside this halfspace</returns>
		public bool contains(Cartesian v) {
			
			// v is inside the halfspace, if the angle between v and
			// the direction vector is smaller than the cone half-angle.
			// Note that _D = cos(half-angle)
			//
			return (v.dot(_SV.x, _SV.y, _SV.z) >= _D);
		}

		/// <summary>
		/// Test whether or not point described by vector v is inside
		/// this halfspace
		/// </summary>
		/// <param name="v">A point</param>
		/// <returns>true if the specified point described by v is inside this halfspace</returns>
		public bool contains(SpatialVector v) {
			
			// v is inside the halfspace, if the angle between v and
			// the direction vector is smaller than the cone half-angle.
			// Note that _D = cos(half-angle)
			//
			return (v.dot(_SV) >= _D);
		}

		/// <summary>
		/// Tests relative position of other halfspace.
		/// There are four possibilities 
		/// they intersect, they are disjoint, this halfspace
		/// contains the other, or it is contained in the other
		/// </summary>
		/// <param name="other"></param>
		/// <returns>Position (one of Intersecting, Disjoint, Contained, or Surrounds)</returns>
		public Position  relativePosition(Halfspace other){
			double phi;
			double dot;
			//double alter = 1.0;

			dot = this._SV.dot(other._SV);
			//
			// If the halfspace is "negative", then flip the
			// normal vector. This has the affect of negating the
			// dot product of the original two vectors
			// Note that (a. -b) == - (a.b) where . is vector inner product

			//
			// There was some old stuff here, that flipped
			// the angles, etc, when D < 0, but I don't think
			// that's right. The inclusion exclusion works
			// even if D < 0

//			if (this._sign == Sign.Negative	|| other._sign == Sign.Negative){
//				alter = -1.0;
//			}
			//
			// if one is positive and one is negative, then look for the relationship
			// between the hole of the negative halfspace and the positive halfspace

//			dot *= alter;
	
			// The following to keep the Acos library happy. Sometimes
			// it can be just a wee outside (-1, +1), and Acos is known
			// do blow up in the Windows environment, when something else
			// is in control of the math environs.

			if (dot <= -1.0 + SpatialVector.Epsilon){
				phi = SpatialVector.Pi;
			} else if (dot >= 1.0 - SpatialVector.Epsilon){
				phi = 0.0;
			} else {
				phi = Math.Acos(dot);
			}
			double a1 = this._hangle;
			double a2 = other._hangle;
//			if (this._sign != Sign.Positive) a1 = SpatialVector.Pi - a1;
//			if (other._sign != Sign.Positive) a2 = SpatialVector.Pi - a2;
			if (phi > a1 + a2) {
				return Position.Disjoint;
			}
			if (phi <= SpatialVector.Epsilon && Math.Abs(a1 - a2) <= SpatialVector.Epsilon) {
				return Position.Identical;
			}
			if (a1 > phi + a2) {
				return Position.Surrounds;
			}
			if (a2 > phi + a1) {
				return Position.Contained;
			}
			return Position.Intersecting;
		}

		/// <summary>
		/// Text conversion of this halfspace
		/// The string representation calls for four whitespace separated
		/// numbers 20 characters each, 15 places after the deminal point
		/// </summary>
		/// <returns>The string representation of this halfspace</returns>
		public override String ToString(){
			StringBuilder sb;
			sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			sw.Write("{0,20:0.000000000000000} {1,20:0.000000000000000} {2,20:0.000000000000000} {3,20:0.000000000000000} ",
				this.sv.x, this.sv.y, this.sv.z, this.d);
			return sb.ToString();
		}
	}
}
