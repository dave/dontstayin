using System;

/*=====================================================================

  File:      Cartesian.cs for Spatial Sample
  Summary:   Implements simple 3D vectors in coordinate geometry
  Date:	     August 10, 2005

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
	/// The Cartesian class implements simple 3D vectors in coordinate geometry.
	/// </summary>
	public class Cartesian {
		//  X coordinate
		[CLSCompliant(false)]
		protected double _x;
		//  Y coordinate
		[CLSCompliant(false)]
		protected double _y;
		//  Z coordinate
		[CLSCompliant(false)]
		protected double _z;

		/// <summary>
		/// The _Cartesian struct implements simple 3D vectors in coordinate geometry.
		/// </summary>
		[CLSCompliant(false)]
		protected struct _Cartesian
		{
			// X coordinate
			public double xs;
			// Y cordinate
			public double ys;
			// Z coordinate
			public double zs;
		};
		/// Accesses the X component of the vector
		public double x {
			get {
				return _x;
			}
			set {
				_x = value;
			}
		}
		/// Accesses the Y component of the vector
		public double y {
			get {
				return _y;
			}
			set {
				_y = value;
			}
		}
		/// Accesses the Z component of the vector
		public double z {
			get {
				return _z;
			}
			set {
				_z = value;
			}
		}

		/// <summary>
		/// Default constructor makes unit vector point along Z axis
		/// </summary>
		public Cartesian() {
			//
			// Default Cartesian is unit vector pointing "up")
			//
			_x = _y = 0.0;
			_z = 1.0;
		}

		/// <summary>
		/// Create from array of doubles
		/// </summary>
		/// <param name="v">An array of x, y, z values</param>
		public Cartesian(double[] v){
			_x = v[0];
			_y = v[1];
			_z = v[2];
		}

		/// <summary>
		/// Create from three doubles.
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <param name="z">Z coordinate</param>
		public Cartesian(double x, double y, double z) {
			//
			// Constructed from input
			//
			_x = x;
			_y = y;
			_z = z;
		}

		/// <summary>
		/// Copy creator
		/// </summary>
		/// <param name="v">Another Cartesian</param>
		public Cartesian (Cartesian v){
			_x = v.x;
			_y = v.y;
			_z = v.z;
		}

		/// <summary>
		/// Set argument v's internals for vector (x, y, z)
		/// </summary>
		/// <param name="v">target vector </param>
		/// <param name="x">The X coordinate</param>
		/// <param name="y">The Y coordinate</param>
		/// <param name="z">The Z coordinate</param>
		[CLSCompliant(false)]
		static protected void set(_Cartesian v, double x, double y, double z) {
			v.xs = x;
			v.ys = y;
			v.zs = z;
		}

		/// <summary>
		/// Set this vector's internals for vector (x, y, z)
		/// </summary>
		/// <param name="x">The X coordinate</param>
		/// <param name="y">The Y coordinate</param>
		/// <param name="z">The Z coordinate</param>
		public void set(double x, double y, double z)
		{
			_x = x;
			_y = y;
			_z = z;
		}

		/// <summary>
		/// Test if other Cartesian is "close enough" to this.
		/// The test is based on the magnitude of the 
		/// difference between this vector and
		/// the given other Cartesian. Test succeeds if the
		/// difference is Epsilon or less.
		/// </summary>
		/// <param name="a">The other Cartesian</param>
		/// <returns>true if the other cartesian is close enough</returns>
		public bool Equivalent(Cartesian a){
			double dx, dy, dz, diff;
			dx = this._x - a._x;
			dy = this._y - a._y;
			dz = this._z - a._z;
			diff = dx * dx + dy * dy + dz * dz;
			return (diff <= Epsilon2);
		}

		/// <summary>
		/// Text representation of Cartesian is (x, y, z)
		/// </summary>
		/// <returns>The string representation of a cartesian</returns>
		public override string ToString(){
			return "(" + x +  ", " + y + ", " + z + ")";
		}
		
		/// <summary>
		/// Check to see if the length of this vector
		/// is close of enough to given length.
		/// Both the length and the tolerance must be supplied.
		/// </summary>
		/// <param name="len">Target length against which to compare 
		/// this Cartesian's length</param>
		/// <param name="tolerance"></param>
		/// <returns>true if length is close enough</returns>
		public bool isLength(double len, double tolerance){
			double ftmp = this.length;
			if (ftmp < len - tolerance)
				return false;
			if (ftmp > len + tolerance)
				return false;
			return true;
		}

		/// <summary>
		/// Compute the square norm (length^2) of this Cartesian.
		/// </summary>
		public double norm2 {
			get {
				return (x * x + y * y + z * z);
			}
		}

		/// <summary>
		/// Compute the length of this Cartesian.
		/// </summary>
		public double length {
			get {
				return System.Math.Sqrt(x*x + y*y + z*z);
			}
		}

		/// <summary>
		/// Compute dot product of this and a given vector
		/// </summary>
		/// <param name="that">The other Cartesian</param>
		/// <returns>The dot product</returns>
		public double dot(Cartesian that) {
			return x*that.x + y*that.y + z*that.z;
		}

		/// <summary>
		/// Compute dot product of this and 
		/// vector given by (x,y,z)
		/// </summary>
		/// <param name="ox">Other vector's x coordinate</param>
		/// <param name="oy">Other vector's y coordinate</param>
		/// <param name="oz">Other vector's z coordinate</param>
		/// <returns></returns>
		public double dot(double ox, double oy, double oz){
			return x*ox + y*oy + z*oz;
		}

		/// <summary>
		/// Compute dot product of two given vectors
		/// </summary>
		/// <param name="u">First (Cartesian) vector</param>
		/// <param name="v">Second (Cartesian) vector</param>
		/// <returns>The dot product</returns>
		public static double dot(Cartesian u, Cartesian v){
			return u.x*v.x + u.y*v.y + u.z*v.z;
		}

		/// <summary>
		/// Compute cross product  of two given vectors.
		/// Result is written into a suppied Cartesian
		/// </summary>
		/// <param name="t">the result of operation</param>
		/// <param name="u">Array of 3 doubles with (x,y,z)</param>
		/// <param name="v">Array of 3 doubles with (x,y,z)</param>
		public static void cross(Cartesian t, double[] u, double[] v){
			t.x = u[1] * v[2] - u[2] * v[1];
			t.y = u[2] * v[0] - u[0] * v[2];
			t.z = u[0] * v[1] - u[1] * v[0];
			return;
		}

		/// <summary>
		/// Compute cross product of two given vectors.
		/// Result is written into a suppied array of 3 doubles.
		/// </summary>
		/// <param name="t">Array of 3 doubles, the result of the operation</param>
		/// <param name="u">Array of 3 doubles with (x,y,z)</param>
		/// <param name="v">Array of 3 doubles with (x,y,z)</param>
		public static void cross(double[] t, double[] u, double[] v){
			t[0] = u[1] * v[2] - u[2] * v[1];
			t[1] = u[2] * v[0] - u[0] * v[2];
			t[2] = u[0] * v[1] - u[1] * v[0];
			return;
		}

		/// <summary>
		/// Compute cross product  of two given vectors.
		/// Result is written into a suppied Cartesian
		/// </summary>
		/// <param name="t">The result of operation</param>
		/// <param name="u">Array of 3 doubles with (x,y,z)</param>
		/// <param name="v">Array of 3 doubles with (x,y,z)</param>
		[CLSCompliant(false)]
		protected static void cross(_Cartesian t, ref _Cartesian u, ref _Cartesian v){
			t.xs = u.ys * v.zs - u.zs * v.ys;
			t.ys = u.zs * v.xs - u.xs * v.zs;
			t.zs = u.xs * v.ys - u.ys * v.xs;
		}

		/// <summary>
		/// Compute the centroid of three points.
		/// The result is normalized.
		/// If the centroid is close to the origin,
		/// then the result is (0, 0, 0).
		/// </summary>
		/// <param name="v0">(x1, y1, z1)</param>
		/// <param name="v1">(x2, y2, z2)</param>
		/// <param name="v2">(x3, y3, z3)</param>
		/// <param name="ox">Centroid's X-coordinate</param>
		/// <param name="oy">Centroid's Y-coordinate</param>
		/// <param name="oz">Centroid's Z-coordinate</param>
		public static void normalCentroid(double[] v0, double[] v1, double[] v2,
			out double ox, out double oy, out double oz){
			double norm;
			ox = v0[0] + v1[0] + v2[0];
			oy = v0[1] + v1[1] + v2[1];
			oz = v0[2] + v1[2] + v2[2];
			norm = ox*ox + oy*oy + oz*oz;
			norm = Math.Sqrt(norm);
			// GYF:03/24/2005 check for < epsilon
			if (norm <= Cartesian.Epsilon){
				ox = oy = oz = 0.0;
			} else {
				ox /= norm;
				oy /= norm;
				oz /= norm;
			}
			return;
		}

		/// <summary>
		/// Make a new instance of Cartesian
		/// the contents of which is a vector obtained by adding
		///	this to another Cartesian.
		/// </summary>
		/// <param name="that">The other Cartesian</param>
		/// <returns>The sum as a new instance of a Cartesian</returns>
		public Cartesian add(Cartesian that){
			return new Cartesian(
				_x + that._x,
				_y + that._y,
				_z + that._z);
		}

		/// <summary>
		/// Make this vector the same as the other (given) one.
		/// </summary>
		/// <param name="that">The other Cartesian</param>
		public void assign(Cartesian that){
			_x = that._x;
			_y = that._y;
			_z = that._z;
		}

		/// <summary>
		/// Make this vector (x, y, z).
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void assign(double x, double y, double z) {
			_x = x;
			_y = y;
			_z = z;
		}

		/// <summary>
		/// Add another cartesian to this.
		/// </summary>
		/// <param name="that">The other Cartesian</param>
		public void addMe(Cartesian that){
			_x += that._x;
			_y += that._y;
			_z += that._z;
		}

		/// <summary>
		/// Subtract other vector from this, but create a new instance with 
		///	the result.
		/// </summary>
		/// <param name="that">The other Cartesian</param>
		/// <returns>the difference as a new Catesian</returns>
		public Cartesian sub(Cartesian that){
			return new Cartesian(
				_x - that._x,
				_y - that._y,
				_z - that._z);
		}

		/// <summary>
		/// Subtract another vector from this one.
		/// </summary>
		/// <param name="that">The other vector</param>
		public void subMe(Cartesian that){
			_x -= that._x;
			_y -= that._y;
			_z -= that._z;
		}

		/// <summary>
		/// Perform cross product of this and another vector.
		/// The result is a new cartesian instance.
		/// </summary>
		/// <param name="that">The other vector</param>
		/// <returns>The cross product as a new Cartesian</returns>
		public Cartesian cross(Cartesian that){
			return new Cartesian(
				y*that.z - z*that.y,
				z*that.x - x*that.z,
				x*that.y - y*that.x);
		}

		/// <summary>
		/// Perform cross product of this and (x, y, z)
		/// and replace this with the result.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void crossMe(double x, double y, double z){
			double tx, ty, tz;
			tx = _y*z - _z*y;
			ty = _z*x - _x*z;
			tz = _x*y - _y*x;
			_x = tx;
			_y = ty;
			_z = tz;
		}

		/// <summary>
		/// Perform cross prodcut of this and another
		/// vector. Replaced with the result.
		/// </summary>
		/// <param name="that">The other vector</param>
		public void crossMe(Cartesian that){
			double tx, ty, tz;
			tx = _y*that.z - _z*that.y;
			ty = _z*that.x - _x*that.z;
			tz = _x*that.y - _y*that.x;
			_x = tx;
			_y = ty;
			_z = tz;
		}

		/// <summary>
		/// Scale this vector.
		/// </summary>
		/// <param name="s">Scale factor</param>
		public void scaleMe(double s){
			x *= s;
			y *= s;
			z *= s;
		}

		/// <summary>
		/// Normalizes this vector.
		/// If vector is null, will cause an exception
		/// </summary>
		public void normalizeMe(){
			double norm = System.Math.Sqrt(x*x + y*y + z*z);
			x /= norm;
			y /= norm;
			z /= norm;
			return;
		}

		/// <summary>
		/// Normalize this vector safely.
		/// This function will not throw exceptions, but will test
		/// the length of the vector. If the length is less than
		/// or equal to Epsilon, it will do nothing.
		/// TODO: Combine the two forms into one, catch the exception
		/// </summary>
		/// <returns>true if normalization succeeded, false otherwise</returns>
		public bool normalizeMeSafely(){
			double norm = (x*x + y*y + z*z);
			if (norm > Epsilon2){
				norm = Math.Sqrt(norm);
				_x /= norm;
				_y /= norm;
				_z /= norm;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Test whether or not this vector is equal to
		/// another vector
		/// </summary>
		/// <param name="that">The other vector</param>
		/// <returns>true if other vector is close enough,
		/// false otherwise</returns>
		public bool eq(Cartesian that){
			if (Math.Abs(that._x - _x) > Epsilon)
				return false;
			if (Math.Abs(that._y - _y) > Epsilon)
				return false;
			if (Math.Abs(that._z - _z) > Epsilon)
				return false;
			return true;
		}

		/// <summary>
		/// Cosine wrapper to avoid Microsoft bug
		/// 
		/// When a variable is assigned Math.Cos(Math.PI /2.0) *and*
		/// the debugger stops at a place where the variable is displayed
		/// Studio, debugger and the application all crash. This should
		/// be removed when a new release of the developer suite fixes
		/// the bug... er addresses this issue.
		/// </summary>
		/// <param name="arg">The argument to Cosine</param>
		/// <returns>The cosine of the argument</returns>
		public static double Cos(double arg) {
			double result;
			if (Math.Abs(arg - Cartesian.Pi05) < Cartesian.Epsilon) {
				result = 0.0;
			} else {
				result = Math.Cos(arg);
			}
			return result;
		}

		
		/// <summary>
		/// Test whether or not this vector is the opposite
		/// of another vector.
		/// Opposite means same magnitude, opposite direction.
		/// </summary>
		/// <param name="that">The other vector</param>
		/// <returns>true if other vector is close enough to 
		/// being opposite, false otherwise</returns>
		public bool opposite(Cartesian that){
			if (Math.Abs(that._x + _x) > Epsilon)
				return false;
			if (Math.Abs(that._y + _y) > Epsilon)
				return false;
			if (Math.Abs(that._z + _z) > Epsilon)
				return false;
			return true;
		}
		// The constant pi, about 3.14
		public const double Pi = 3.14159265358979323846264338327950288E0;
		public const double Pi05 = Pi/2.0D;
		// Anything below this is considered to be zero. 1E-15
		public const double Epsilon = 1.0e-15;

		// Epsilon squared
		public const double Epsilon2 = Epsilon * Epsilon;

		// Constant to multiply radians to get degrees, 180/Pi
		public const double RTOD = 180.0/Pi;

		// Constant to multiply degrees to get radians, Pi/180
		public const double DTOR = Pi/180;
	}
}
