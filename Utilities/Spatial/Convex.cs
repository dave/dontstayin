using System;
using System.Collections;

/*=====================================================================

  File:      Convex.cs for Geospatial Sample
  Summary:   Implements the intersection of Halfspaces
  Date:	     August 10, 2005

---------------------------------------------------------------------
  This spatial search library was developed by Alexander Szalay, Robert Brunner, 
  Peter Kunszt, and Gyorgy Fekete at the Department of Physics and Astronomy, 
  The Johns Hopkins University, in collaboration with Jim Gray, Microsoft Research.
  Details of the algorithms can be found at http://www.sdss.jhu.edu/htm/.
 
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.
======================================================= */

//
// Needs Halfspace, Cartesian
//
// EXPERIMENTAL code is flagged with // <EXP> or /* <EXP> */
// testPartial is where decision is made to recurse or not...
// June 15, 2005 GYF
// remove redundant universal halfspaces, and re-sign convex
//
namespace Microsoft.Samples.SqlServer {
	/// <summary>
	/// Internal struct 
	/// </summary>
	struct edgeStruct {
		public Cartesian e;		/// The half-sphere this edge delimits
		public double	  l;	/// length of edge
		public Cartesian e1;	/// first end
		public Cartesian e2;	/// second end
	};

    /// <summary>
	/// This class implements the intersection of Halfspaces.
	/// Most (interesting) operations are performed by Convex.
    /// </summary>
	public class Convex {
		/// <summary>
		/// In certain modes, this class produces trace
		/// information in specific files.
		///
		/// In normal mode, this class performs what it must. In File mode,
		/// it also produces 4 tracefiles that only means something
		/// to the authors and maintainers of this software.
		/// </summary>
		public enum Mode {
			Normal,		/// Normal operation without file i/o
			File,		/// In this mode trace files will be written
			NullMode	/// Never used mode
		};
		/// <summary>
		/// Used by algorithm that intersects trixels with convexes
		/// </summary>
		public enum Markup {
			Full,		/// trixel is completely inside convex
			Reject,		/// trixel is completely outside convex
			Partial,	/// trixel non-trivially intersects convex
			Dontknow	/// trixel's status is not yet known
		};
		/// <summary>
		/// These constants indicate local conditions that
		/// guide the heuristic algorithm that performs trixel-convex
		/// intersection.
    	/// </summary>
		public enum Savecode {
			Nosave,			/// do not save this trixel
			Reachedlevel,	/// reached maximum depth
			Peq4,			/// 4 children are partial
			Fge2,			/// at least 2 children are full
			Peq3Feq1,		/// 1 child full, AND 3 are partial
			Pgt1Ppreveq3,	/// at least 1 child is partial,
					        /// and parent had three partial children 
			Childisfull		///
			///TODO: Who is using child is full?
		};
		Int64 _ID = 0;
		Int64 ID {
			set {
				_ID = value;
			}
			get {
				return _ID;
			}
		}

		string _directory = null; // was: "C:\\htmtmp\\" ;
		/// <summary>
		/// Variable length (level) indicator
		/// 
		/// If set, trixels are saved at their natural level.
		/// Otherwise hid ranges at level 20 are used.
		/// </summary>
		bool _varlen = false;

		int _addlevel = 0;
		int _minlevel = 0;  // What is the difference between smallestLevel and _minlevel?

		double smallestRadius;
		double greatestD;
		int    _smallestLevel;
		int _cutoffLevel;

		ArrayList _halfspaces;
		ArrayList _corners;

		ArrayList _buildlist; // Build the result list here, no need to be sorted?
		private int _numberSaved_;
		private int _altSaved_;
		Halfspace _boundingCircle;
		HtmTrixel _htm;
		Halfspace.Sign _sign;

		// 
		// These are invoked only when debugging the class
		// or when these four files are necessary
		// _tracefile gets the actual trace of the intersect algorithm
		// _savedfile gets the HIDS saved as a result
		// _halfspacesfile gets a description of the halfspaces
		// _extendedfile gets the level 20 ranges. isomorphic
		// to _savedfile
		Trace _tracefile = null;
		Trace _savedfile = null;
		Trace _extendedfile = null;
		Trace _halfspacesfile = null;
		private Mode _mode = Mode.Normal;
		//
		// EXP
		//
		private Int64 _pseudoArea;

		/// <summary>
		/// Set directory for tracefiles
		/// </summary>
		/// <param name="dir">Tracefile location</param>
		public void set_debugdirectory(String dir){
			_mode = Mode.File;
			_directory = dir;
		}
		public void notrace(bool nothanks) {
			if (nothanks) {
				_mode = Mode.Normal;
			} else {
				_mode = Mode.File;
			}
		}

		/// <summary>
		/// Reset instance to a clean start state
		/// </summary>
		public void reinit(){
			clear();
			init();
		}
		/// <summary>
		/// Return the number of Halfspaces in this Convex
		/// </summary>
		public int Count {
			get {
				return this._halfspaces.Count;
			}
		}
		/// <summary>
		/// Return the number of Halfspaces that do not have
		/// the 'reject flag' set
		/// </summary>
		public int nonRejectedHalfspaces {
			get {
				int n;
				Halfspace hs;
				n = 0;
				for(int i=0; i<this._halfspaces.Count; i++){
					hs = (Halfspace) _halfspaces[i];
					if (hs.status != Halfspace.Status.Reject)
						n++;
				}
				return n;
			}
		}
		private  void clear(){
			_halfspaces     = null;
			_corners        = null;
			_sign           = Halfspace.Sign.Zero;
			_boundingCircle = null;
			_htm		    = new HtmTrixel();
		}
		private void init(){
			_halfspaces     = new ArrayList(); // will change
			_corners        = new ArrayList();
			_sign           = Halfspace.Sign.Zero;
			_boundingCircle = new Halfspace();
			_htm		    = new HtmTrixel();
		}
		/// <summary>
		/// Create an instance with specified mode
		///
		/// Normally, this constructor is used only by
		/// the developer
		/// </summary>
		/// <param name="mode">Trace setting</param>
		public Convex(Mode mode){
			_mode = mode;
			init();
		}
		/// <summary>
		/// Default contructor
		/// Nothing special
		/// </summary>
		public Convex() {
			_mode = Mode.Normal;
			init();
		}
		/// <summary>
		/// Change operating mode of this Convex
		///
		/// For developer use only. Convex's behavior
		/// with respect to leaving trace information
		/// can be changed in mid-flight
		/// </summary>
		[CLSCompliant(false)]
		public Mode mode
		{
			set {
				_mode = value;
			}
		}

		/// <summary>
		/// Mark the smallest Halfspace in this Convex
		///
		/// Find smallest feature size, that is smallest radius 
		/// of either the BC (D greater than 0) or hole (D less than 0)
		/// this.greatestD and this.smallestRadius (radians) is updated
		/// </summary>
		public void updateExtremes(){
			double big;
			double d;
			Halfspace hs;

			big = 0.0;
			for(int i=0; i<_halfspaces.Count; i++){
				hs = ((Halfspace) _halfspaces[i]);
				d = hs.d;
				if (d < 0.0) d = 0.0;// WAS: d = -d;
				if (d > big) big = d;
			}
			this.greatestD = big;
			this.smallestRadius = Math.Acos(big);
			// Now find level consistent with smallest
			// radius.
			this._smallestLevel = HtmState.Instance.getLevel(this.smallestRadius);
			this._cutoffLevel = this._smallestLevel + HtmState.Instance.hdelta; // <EXP> 
			if (this._cutoffLevel > HtmState.Instance.maxlevel) {
				this._cutoffLevel = HtmState.Instance.maxlevel;
			}
			
		}

		/// <summary>
		/// Return the Halfspace with the given ID 
		/// </summary>
		/// <param name="ID">ID the id of the halfspace</param>
		/// <returns>null if the requested Halfspace does not exist</returns>
		public Halfspace hsID(Int64 ID){
			Halfspace hs;
			for(int i=0; i<_halfspaces.Count; i++){
				hs = ((Halfspace) _halfspaces[i]);
				if (ID == hs.ID)
					return hs;
			}
			return null;
		}
	
		/// <summary>
		/// Return the Halfspace at location <i>ix</i>
		/// TODO: Check for bounds
		/// </summary>
		/// <param name="ix">location of Halfspace</param>
		/// <returns>null if ix is out of bounds</returns>
		public Halfspace hsAt(int ix){
			return (Halfspace) _halfspaces[ix];
		}
		private void prune() {
			// NEW: Check if this halfspace is the whole sphere, if so, remove it.
			int clen = _halfspaces.Count;
			bool hasPositive = false;
			bool hasNegative = false;
			bool hasZero = false;
			bool[] removethese = new bool[clen];
			bool removeall = false;

			if (clen < 1)
				return;

			for (int i = 0; i < clen; i++) {
				Halfspace hi = (Halfspace)_halfspaces[i];
				removethese[i] = false;
				if (clen > 1 && hi.isUniversal()) {
					// _halfspaces.RemoveAt(i);
					removethese[i] = true;
				}
				if (hi.isNull()) {
					removeall = true;
					break;
				}
			}
			if (removeall) {
				_halfspaces.Clear();
				hasZero = true; // force consistency.
			} else {
				for (int i = clen - 1; i >= 0; i--) {
					if (removethese[i]) {
						_halfspaces.RemoveAt(i);
					} else {
						Halfspace hi = (Halfspace)_halfspaces[i];
						switch (hi.sign) {
							case Halfspace.Sign.Negative: hasNegative = true; break;
							case Halfspace.Sign.Positive: hasPositive = true; break;
							case Halfspace.Sign.Zero: hasZero = true; break;
						}
					}
				}
			}

			if (hasPositive && hasNegative) {
				_sign = Halfspace.Sign.Mixed;
			} else if (hasPositive) {
				_sign = Halfspace.Sign.Positive;
			} else if (hasNegative) {
				_sign = Halfspace.Sign.Negative;
			} else {
				_sign = Halfspace.Sign.Zero;
			} 
			if (_sign == Halfspace.Sign.Zero && !hasZero) {
				throw new Exception("Convex.prune() internal inconsistency");
			}
			// end new.
		}
		/// <summary>
		/// Maintain halfspaces in ascending opening angle order
		/// Called after each add, which puts a new halfspace at the
		/// end of the arraylist. "Bubble" the new addition to its proper
		/// place. Also assign a "Sign" from to this Convex
		/// One of: Positive, negative, zero, mixed.
		/// </summary>
		/// <param name="sign">The sign of the most recently added halfspace</param>
		private void sortandsign(Halfspace.Sign sign) {
			int i;
			int cnt;
			Halfspace hs;
			Halfspace hsprev;
			Object o;
			cnt = _halfspaces.Count;
			for(i = cnt-1; i>0; i--){
				hs = (Halfspace) _halfspaces[i];
				hsprev = (Halfspace) _halfspaces[i-1];
				if (hs.hangle > hsprev.hangle){
					// New halfspace has wider angle than previous one
					// so we are done!
					break;
				} else { 
					// new halfspace needs to trade places with previous one
					o = hsprev;
					_halfspaces[i-1] = hs;
					_halfspaces[i] = o;
				}
			} // Done sorting, now update sign
			if(cnt == 1){
				this._sign = sign;
				return;
			}
			switch(this._sign){
				case Halfspace.Sign.Negative:
					if (sign == Halfspace.Sign.Positive) 
						this._sign = Halfspace.Sign.Mixed;
					break;
				case Halfspace.Sign.Positive:
					if (sign == Halfspace.Sign.Negative) 
						this._sign = Halfspace.Sign.Mixed;
					break;
				case Halfspace.Sign.Zero:
					this._sign = sign;
					break;
				case Halfspace.Sign.Mixed:
					// Mixed stays mixed
					break;
			}

		}
		/// <summary>
		/// Add a Halfspace to this Convex
		/// New Halfspace will be added to the end of the list
		/// </summary>
		/// <param name="hs">What to add to the convex</param>
		public void add(Halfspace hs){
			_halfspaces.Add(hs);
			sortandsign(hs.sign);
		}
		/// <summary>
		/// Get the number of Hafspaces in this Convex
		/// </summary>
		public int size {
			get {
				return _halfspaces.Count;
			}
		}
		/// <summary>
		/// Add a new Halfspace specified by the four 
		/// parameters
		/// </summary>
		/// <param name="x">X coordinate of vector</param>
		/// <param name="y">Y coordinate of vector</param>
		/// <param name="z">Z coordinate of vector</param>
		/// <param name="d">signed distance of cutting plane</param>
		public void add(double x, double y, double z, double d){
			Halfspace hs = new Halfspace(x, y, z, d);
			_halfspaces.Add(hs);
			sortandsign(hs.sign);
		}
		
		private Convex.Markup testTriangle(
			Cartesian v0, Cartesian v1, Cartesian v2,
			int vsum){

			Halfspace hs;
			if(vsum == 1 || vsum == 2){
				return Markup.Partial;
			}

			// If vsum = 3 then we have all vertices inside the convex.
			// Now use the following decision tree:
			//
			// * If the sign of the convex is POS or ZERO : mark as FULL intersection.
			//
			// * Else, test for holes inside the triangle. A 'hole' is a NEG constraint
			//   that has its center inside the triangle. If there is such a hole,
			//   return PARTIAL intersection.
			//
			// * Else (no holes, sign NEG or MIXED) test for intersection of NEG
			//   constraints with the edges of the triangle. If there are such,
			//   return PARTIAL intersection.
			//
			// * Else return FULL intersection.

			if(vsum == 3) {
				if(_sign == Halfspace.Sign.Positive || _sign == Halfspace.Sign.Zero){
					return Markup.Full;
				}
				if ( testHole(v0,v1,v2) ) {
					return Markup.Partial;
				}
				if ( testEdge(v0,v1,v2) ) {
					return Markup.Partial;
				}
				return Markup.Full;
			}

			// If we have reached that far, we have vsum=0. There is no definite
			// decision making possible here with our methods, the markup may result
			// in DONTKNOW. The decision tree is the following:
			//
			// * Test with bounding circle of the triangle.
			//
			//   # If the sign of the convex ZERO test with the precalculated
			//     bounding circle of the convex. If it does not intersect with the
			//     triangle's bounding circle, REJECT.
			//
			//   # If the sign of the convex is nonZERO: if the bounding circle
			//     lies outside of one of the constraints, REJECT.
			//
			// * Else: there was an intersection with the bounding circle.
			//
			//   # For ZERO convexes, test whether the convex intersects the edges.
			//     If none of the edges of the convex intersects with the edges of
			//     the triangle, we have a REJECT. Else, PARTIAL.
			//
			//   # If sign of convex is POS, or MIXED and the smallest constraint does
			//     not intersect the edges and has its center inside the triangle,
			//     return SWALLOW. If no intersection of edges and center outside
			//     triangle, return REJECT.
			//
			//   # So the smallest constraint DOES intersect with the edges. If
			//     there is another POS constraint which does not intersect with
			//     the edges, and has its center outside the triangle, return
			//     REJECT. If its center is inside the triangle return SWALLOW.
			//     Else, return PARTIAL for POS and DONTKNOW for MIXED signs.
			//
			// * If we are here, return DONTKNOW. There is an intersection with
			//   the bounding circle, none of the vertices is inside the convex and
			//   we have very strange possibilities left for POS and MIXED signs. For
			//   NEG, i.e. all constraints negative, we also have some complicated
			//   things left for which we cannot test further.

			if ( !testBoundingCircle(v0,v1,v2) ) {
				return Markup.Reject;
			}

			if ( _sign == Halfspace.Sign.Positive ||
				_sign == Halfspace.Sign.Mixed || 
				(_sign == Halfspace.Sign.Zero && _halfspaces.Count <= 2)) {
				// Does the smallest constraint intersect with the edges?
				if ( testEdgeConstraint(v0,v1,v2,0) ) {
					// Is there another positive constraint that does NOT intersect with
					// the edges? cIndex is the position in _halspaces
					int cIndex;
					if ((cIndex = testOtherPosNone(v0,v1,v2))>= 0) {
						// Does that constraint lie inside or outside of the triangle?
						if ( testConstraintInside(v0,v1,v2, cIndex) ){
							return Markup.Partial;
						} else if( (hs = (Halfspace)_halfspaces[cIndex]).contains(v0) ){
							// Does the triangle lie completely within that constr?
							return Markup.Partial;
						} else {
							return Markup.Reject; // FIXED:was: Markup.Partial;
						}
					} else {
						if(_sign == Halfspace.Sign.Positive || 
							_sign == Halfspace.Sign.Zero){
							return Markup.Partial;
						} else {
							return Markup.Dontknow;
						}
					}
				} else {
					if (_sign == Halfspace.Sign.Positive || 
						_sign == Halfspace.Sign.Zero) {
						// Does the smallest lie inside or outside the triangle?
						if( testConstraintInside(v0,v1,v2, 0)){
							return Markup.Partial;
						} else {
							return Markup.Reject;
						}
					} else {
						return Markup.Dontknow;
					}
				}
			} else if (_sign == Halfspace.Sign.Zero) {
				if (_halfspaces.Count > 0 && testEdge0(v0,v1,v2)){
					return Markup.Partial;
				} else {
					return Markup.Reject;
				}
			}
			return Markup.Partial;
		}
		// Added May 31, 2005
		// 
		public bool isVertexInsideAllHalfspaces(double x, double y, double z) {

			Halfspace hs;
			for (int i = 0; i < _halfspaces.Count; i++) {
				hs = (Halfspace)_halfspaces[i];
				// if (v.dot(hs.sv) > hs.d) {
				if (hs.sv.x * x + hs.sv.y * y + hs.sv.z * z > hs.d){
					return false;
				}
			}
			return true;
		}
 
		 
		/// <summary>
		/// This test fails if the vertex v is inside any halfspace
		/// in this convex. In other words, it succeeds if v is
		/// not inside any of them
		/// </summary>
		/// <param name="v">vertex to test</param>
		/// <returns></returns>
		private bool isVertexOutsideAllHalfspaces(Cartesian v) {
			Halfspace hs;
			for ( int i = 0; i < _halfspaces.Count; i++){
				hs = (Halfspace) _halfspaces[i];
				if ( v.dot(hs.sv)  < hs.d ){
					return false;
				}
			}
			return true;
		}
		private bool testHole(Cartesian v0, Cartesian v1, Cartesian v2){
			Cartesian r1, r2, r3;
			bool test = false;
			Halfspace hs;
			r1 = new Cartesian();
			r2 = new Cartesian();
			r3 = new Cartesian();
			for(int i = 0; i < _halfspaces.Count; i++) {
				hs = (Halfspace) _halfspaces[i];
				if ( hs.sign < 0) {  
					// test only 'holes'
					// If (a ^ b * c) < 0, vectors abc point clockwise.
					// -> center c not inside triangle, since vertices a,b are ordered
					// counter-clockwise. The comparison here is the other way
					// round because c points into the opposite direction as the hole
					//
					// The old code said this:
					//					if ( ( ( v0 ^ v1 ) *
					//						_halfspaces[i].a_) > 0.0L ) continue;
					//					if ( ( ( v1 ^ v2 ) *
					//						_halfspaces[i].a_) > 0.0L ) continue;
					//					if ( ( ( v2 ^ v0 ) *
					//						_halfspaces[i].a_) > 0.0L ) continue;
					
					r1.assign(v0);
					r1.crossMe(v1);
					if ( r1.dot( hs.sv) > 0.0) continue;

					r1.assign(v1);
					r1.crossMe(v2);
					if ( r1.dot( hs.sv) > 0.0) continue;

					r1.assign(v2);
					r1.crossMe(v0);
					if ( r1.dot( hs.sv) > 0.0) continue;
					test = true;
					break;
				}
			}
			return test;
		}
		/////////////TESTEDGE0////////////////////////////////////
		// testEdge0: 
		//

		/// <summary>
		/// Test if the edges intersect with the ZERO convex.
		//  The edges are given by the vertex vectors e[0-2]
		//	All constraints are great circles, so test if their intersect
		//  with the edges is inside or outside the convex.
		//  If any edge intersection is inside the convex, return true.
		//  If all edge intersections are outside, check whether one of
		//  the corners is inside the triangle. If yes, all of them must be
		//  inside -> return true.
		/// </summary>
		/// <param name="v0">vertex vector e[0]</param>
		/// <param name="v1">vertex vector e[1]</param>
		/// <param name="v2">vertex vector e[2]</param>
		/// <returns></returns>
		private bool testEdge0(Cartesian v0, Cartesian v1, Cartesian v2){
			// We have constructed the corners_ array in a certain direction.
			// now we can run around the convex, check each side against the 3
			// triangle edges. If any of the sides has its intersection INSIDE
			// the side, return true. At the end test if a corner lies inside
			// (because if we get there, none of the edges intersect, but it
			// can be that the convex is fully inside the triangle. so to test
			// one single edge is enough)
			edgeStruct[] edge = new edgeStruct[3];

			if (_corners.Count < 1) {
				throw new Exception("testEdge0: There are no corners");
			}
			// fill the edge structure for each side of this triangle
			Cartesian c01 = new Cartesian(v0);
			Cartesian c12 = new Cartesian(v1);
			Cartesian c20 = new Cartesian(v2);
			c01.crossMe(v1);
			c12.crossMe(v2);
			c20.crossMe(v0);
			edge[0].e = c01;
			edge[0].e1 = v0;
			edge[0].e2 = v1;
			edge[0].l = Math.Acos(v0.dot(v1));

			edge[1].e = c12;
			edge[1].e1 = v1;
			edge[1].e2 = v2;
			edge[1].l = Math.Acos(v1.dot(v2));

			edge[2].e = c20;
			edge[2].e1 = v2;
			edge[2].e2 = v0;
			edge[2].l = Math.Acos(v2.dot(v0));

			//			edge[0].e = v0 ^ v1; edge[0].e1 = &v0; edge[0].e2 = &v1;
			//			edge[1].e = v1 ^ v2; edge[1].e1 = &v1; edge[1].e2 = &v2;
			//			edge[2].e = v2 ^ v0; edge[2].e1 = &v2; edge[2].e2 = &v0;
			//			edge[0].l = Acos(v0 * v1);
			//			edge[1].l = Acos(v1 * v2);
			//			edge[2].l = Acos(v2 * v0);

			for(int i = 0; i < _corners.Count; i++) {
				int j = 0;
				if(i < _corners.Count - 1){
					j = i+1;
				}
				Cartesian a1 = new Cartesian();
				Cartesian tmp;
				double arg1, arg2;
				double l1,l2;   // lengths of the arcs from intersection to edge corners

				double cedgelen = Math.Acos(
					((Cartesian) _corners[i]).dot 
					((Cartesian) _corners[j]));  // length of edge of convex

				// calculate the intersection - all 3 edges
				for (int iedge = 0; iedge < 3; iedge++) {
					a1.assign(edge[iedge].e);
					tmp = 
						((Cartesian) _corners[i]).cross
						((Cartesian) _corners[j]);
					a1.crossMe(tmp);

					if (a1.normalizeMeSafely()){
						// WARNING
						// Keep your eye on this, used to crash here...
						//
						// if the intersection a1 is inside the edge of the convex,
						// its distance to the corners is smaller than the edgelength.
						// this test has to be done for both the edge of the convex and
						// the edge of the triangle.
						for(int k = 0; k < 2; k++) {
							l1 = Math.Acos(((Cartesian) _corners[i]).dot(a1));
							l2 = Math.Acos(((Cartesian) _corners[j]).dot(a1));
							if( l1 - cedgelen <= Cartesian.Epsilon && 
								l2 - cedgelen <= Cartesian.Epsilon){
								arg1 =  (edge[iedge].e1).dot(a1);
								arg2 =  (edge[iedge].e2).dot(a1);
								if (arg1 > 1.0) arg1 = 1.0;
								if (arg2 > 1.0) arg2 = 1.0;
								l1 = Math.Acos(arg1);
								l2 = Math.Acos(arg2);
								if( l1 - edge[iedge].l <= Cartesian.Epsilon &&
									l2 - edge[iedge].l <= Cartesian.Epsilon)
									return true;
							}
							a1.scaleMe(-1.0); // do the same for the other intersection
						}
					}
				}
			}
			return testVectorInside(v0,v1,v2,(Cartesian) _corners[0]);
		}

		/// <summary>
		/// Test if edges intersect with halfspace. This problem
		/// is solved by a quadratic equation. Return true if there is
		///	an intersection.
		/// </summary>
		/// <param name="v0"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		private bool testEdge(Cartesian v0, Cartesian v1, Cartesian v2){
			Halfspace hs;
			for(int i = 0; i < _halfspaces.Count; i++) {
				hs = (Halfspace) _halfspaces[i];
				if ( hs.sign < 0 ) {  // test only 'holes'
					if ( eSolve(v0, v1, i) ) return true;
					if ( eSolve(v1, v2, i) ) return true;
					if ( eSolve(v2, v0, i) ) return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Solve the quadratic eq. for intersection of an edge with a circle
		/// halfspace. Edge given by great circle running through v1, v2
		/// halfspace given by cIndex.
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <param name="cIndex"></param>
		/// <returns></returns>
		private bool eSolve(Cartesian v1, Cartesian v2, int cIndex){
			Halfspace hs;
			hs = (Halfspace) _halfspaces[cIndex];

			double gamma1 = v1.dot(hs.sv);
			double gamma2 = v2.dot(hs.sv);
			double mu     = v1.dot(v2);
			double u2     = (1.0 - mu) / (1.0 + mu);

			double a      = - u2 * (gamma1 + hs.d);
			double b      = gamma1 * (u2 - 1.0) + gamma2 * (u2 + 1.0);
			double c      = gamma1 - hs.d;

			double D      = b * b - 4 * a * c;

			if (D < 0.0){
				return false; // no intersection
			}

			// calculate roots a'la Numerical Recipes
			double SGN;
			if (b < 0.0){
				SGN = -1.0;
			} else {
				if (b > 0){
					SGN = 1.0;
				} else {
					SGN = 0.0;
				}
			}
			double q      = -0.5 * (b + (SGN * Math.Sqrt(D)));
			double root1 = -999.0, root2 = -999.0;
			int i = 0;

			if ( a > Cartesian.Epsilon || a < -Cartesian.Epsilon ) { root1 = q / a; i++; }
			if ( q > Cartesian.Epsilon || q < -Cartesian.Epsilon ) { root2 = c / q; i++; }

			// Check whether the roots lie within [0,1]. If not, the intersection
			// is outside the edge.

			if (i == 0) {
				return false; // no solution
			}
			// if i > 0, then root1 is assigned a value;
			if (root1 >= 0.0 && root1 <= 1.0){
				return true;
			}
			// If i == 2, then root2 was assigned a value;
			if (i == 2 && ((root1 >= 0.0 && root1 <= 1.0) ||
				(root2 >= 0.0 && root2 <= 1.0))){
				return true;
			}
			return false;
		} // METHOD


		/// <summary>
		/// Test for boundingCircles intersecting with halfspace
		/// </summary>
		/// <param name="v0"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		private bool testBoundingCircle(Cartesian v0, Cartesian v1, Cartesian v2){

			// Set the correct direction: The normal vector to the triangle plane
			//Cartesian c = ( v1 - v0 ) ^ ( v2 - v1 );
			//c.normalize();

			Cartesian c = new Cartesian(v1);
			c.subMe(v0);

			Cartesian t = new Cartesian(v2);
			t.subMe(v1);

			c.crossMe(t);
			c.normalizeMe();

			// Set the correct opening angle: Since the plane cutting out the triangle
			// also correctly cuts out the bounding cap of the triangle on the sphere,
			// we can take any corner to calculate the opening angle
			double d = Math.Acos (c.dot(v0));

			// for zero convexes, we have calculated a bounding circle for the convex.
			// only test with this single bounding circle.
			//
			// NOTE! Bounding circle gets changed by simplify0
			// If you did not run simplify, bounding circle may contain
			// unpredictable numbers.
			//
			if(_sign == Halfspace.Sign.Zero && _boundingCircle != null) {
				double tst;
				tst = (tst = c.dot(_boundingCircle.sv));
				if ((tst < -1.0 + Cartesian.Epsilon ? Cartesian.Pi : Math.Acos(tst)) >
					(d + _boundingCircle.hangle)) return false;
				return true;
			}

			// for all other convexes, test every constraint. If the bounding
			// circle lies completely outside of one of the constraints, reject.
			// else, accept.

			int i;
			double ftmp;
			Halfspace hs;
			for(i = 0; i < _halfspaces.Count; i++) {
				hs = (Halfspace) _halfspaces[i];
				if (c.dot(hs.sv) < -1.0 + Cartesian.Epsilon){
					ftmp = Cartesian.Pi;
				} else {
					ftmp = Math.Acos(c.dot(hs.sv));
				}
				if (ftmp > (d + hs.hangle)){
					return false;
				}
			}
			return true;
		} // end METHOD testBoundingCircle
		
		/// <summary>
		/// Test if edges intersect with a given constraint.
		/// </summary>
		/// <param name="v0"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <param name="cIndex"></param>
		/// <returns>true if edges intersect with the constraint</returns>
		private bool testEdgeConstraint(Cartesian v0, Cartesian v1, Cartesian v2, int cIndex){

			if ( eSolve(v0, v1, cIndex) ) return true;
			if ( eSolve(v1, v2, cIndex) ) return true;
			if ( eSolve(v2, v0, cIndex) ) return true;
			return false;
		}

		/// <summary>
		/// Test for other positive constraints that do
		//  not intersect with an edge. Return its index
		/// </summary>
		/// <param name="v0"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns>The index of a positive constraint which does not interspect with an edge</returns>
		private int testOtherPosNone(Cartesian v0, Cartesian v1, Cartesian v2){
			int i = 0;
			while ( i < _halfspaces.Count && ((Halfspace) _halfspaces[i]).sign == Halfspace.Sign.Positive){
				if ( !testEdgeConstraint(v0, v1, v2, i)){
					return i;
				}
				i++;
			}
			return -1;
		}
		/// <summary>
		/// Look if a constraint is inside the triangle
		/// </summary>
		/// <param name="v0"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <param name="i"></param>
		/// <returns>true if the constraint is inside the triage</returns>
		private bool testConstraintInside(	
			Cartesian v0, Cartesian v1, Cartesian v2, int i){
			Halfspace hs;
			hs = (Halfspace) _halfspaces[i];
			return testVectorInside(v0, v1, v2, hs.sv);
		}
		
		/// <summary>
		/// Look if a vector is inside the triangle 
		/// returns true if vector is inside
		/// </summary>
		/// <param name="v0"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <param name="v"></param>
		/// <returns>true if vector is inside the triangle</returns>
		private bool testVectorInside( // SUSPECT
			Cartesian v0, Cartesian v1, Cartesian v2, Cartesian v){

			// If (a ^ b * c) < 0, vectors abc point clockwise.
			// -> center c not inside triangle, since vertices are ordered
			// counter-clockwise.
			Cartesian c01 = new Cartesian(v0);
			Cartesian c12 = new Cartesian(v1);
			Cartesian c20 = new Cartesian(v2);
			c01.crossMe(v1);
			c12.crossMe(v2);
			c20.crossMe(v0);
			//			if (((( v0 ^ v1 ) * v) < 0 ) ||
			//				( (( v1 ^ v2 ) * v) < 0 ) ||
			//				( (( v2 ^ v0 ) * v) < 0 ) )
			//				return false;
			//			return true;
			if (c01.dot(v) < 0)
				return false;
			if (c12.dot(v) < 0)
				return false;
			if (c20.dot(v) < 0)
				return false;
			return true;
		}

		/// <summary>
		/// This is the main test of a triangle vs a Convex.
		/// </summary>
		/// <param name="index"></param>
		private void testTrixel(int index){
			Int64 hid;
			Cartesian v0, v1, v2;
			double[] fa, fb, fc;
			fa = new double[3];
			fb = new double[3];
			fc = new double[3];
			 
			//
			// Compute the vertices of
			// S0, S1, ... , N3
			//
			// Wee need to ask HtmTrixel for the initial
			// values of v0,...,v2 for HID tid;
			hid = 8 + index;
			// if you consider letting addlevel be zero,
			// then this top level trixel must be tested
			// too.
			_htm.level0vertices(index, fa, fb, fc);
			v0 = new Cartesian(fa[0], fa[1], fa[2]);
			v1 = new Cartesian(fb[0], fb[1], fb[2]);
			v2 = new Cartesian(fc[0], fc[1], fc[2]);
			if (_addlevel > 0){
				testPartial(_minlevel, _addlevel, hid, v0, v1, v2, 0); // Used to die here... no more
			} 
		}

		private void saveTrixel(Int64 hid, string cause, int level){
			Int64 lo, hi;
			int depth = HtmTrixel.levelOfHid(hid);
			_pseudoArea += (1L << (40 - 2*depth));
			_htm.extendhid(hid, 20, out lo, out hi);

			if(_mode == Mode.File){
				_savedfile.dump("{0,20}", hid);
				_extendedfile.dump("{0,20}  {1,20}", lo, hi);
			}
			/// Build the return results...
			/// _varlen controls whether or not to save extended HID lists
			/// _buildlist is where you stick the results
			/// if buildist is null, we don't build a list (just exercise the method)
			_numberSaved_++;

			if (_buildlist != null)
			{
				if (_varlen)
				{
					_buildlist.Add(hid);
				}
				else
				{
					_altSaved_++;
					// lo and hi already computed by extendhid...
					// if ( lo != hi | _buildlist.Contains(lo)) {
						_buildlist.Add(lo);
						_buildlist.Add(hi);
					// }
				}
			}
			else {
				throw new Exception("EMPTY BUILDLIST");
			}
			return;
		}

		private Convex.Markup testNode(
			Cartesian v0, Cartesian v1, Cartesian v2, Int64 hid){
			int vsum = 0;
			if (isVertexOutsideAllHalfspaces(v0)) vsum++;
			if (isVertexOutsideAllHalfspaces(v1)) vsum++;
			if (isVertexOutsideAllHalfspaces(v2)) vsum++;
			// 
			/// vsum is the number of vertices that are outside all other halfspaces
			/// 
			/// <LOG>: "testNode hid <vsum> VOAH vertices outside all halfspaces" </LOG>
			/// 
			if (_mode == Mode.File){
				this._tracefile.dump("testNode {0} (vsum {1}) vertices outside all halfspaces",
					hid, vsum);
			}
			Convex.Markup mark = testTriangle(v0, v1, v2, vsum);
			if (_mode == Mode.File){
				this._tracefile.dump("testNode {0} markup is {1}", hid, mark);
			}
			/// <LOG>: "testNode hid markup is <markup>" </LOG>
			/// 
			// since we cannot play games using the on-the-fly triangles,
			// substitute dontknow with partial.
			if (mark == Markup.Dontknow)
				mark = Markup.Partial;
			return mark;
		}

		/// <summary>
		/// Test a triangle's subtriangle whether they are partial.
		//  if level is nonzero, recurse, unless some conditions are met
		/// </summary>
		/// <param name="min_addlevel"></param>
		/// <param name="level"></param>
		/// <param name="hid"></param>
		/// <param name="v0"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <param name="PPrev"></param>
		private void testPartial(int min_addlevel, int level, Int64 hid,
			Cartesian v0, Cartesian v1, Cartesian v2, int PPrev){
			Int64[] ids = new Int64[4];
			Int64 id0;
			Convex.Savecode why;
			Convex.Markup[] m = new Convex.Markup[4];
			bool keepgoing; // force later termination of recorsion
			int P, F;// count number of partials and fulls

			// Console.WriteLine("testpartial level {0}", level);
			Cartesian w0 = new Cartesian(v1);
			w0.addMe(v2);
			w0.normalizeMe();

			
			Cartesian w1 = new Cartesian(v0);
			w1.addMe(v2);
			w1.normalizeMe();

			Cartesian w2 = new Cartesian(v1);
			w2.addMe(v0);
			w2.normalizeMe();

			ids[0] = id0 = hid << 2;
			ids[1] = id0 + 1;
			ids[2] = id0 + 2;
			ids[3] = id0 + 3;


			m[0] = testNode(v0, w2, w1, ids[0]);
			m[1] = testNode(v1, w0, w2, ids[1]);
			m[2] = testNode(v2, w1, w0, ids[2]);
			m[3] = testNode(w0, w1, w2, ids[3]); // [DEBUG] After 36 entries

			F = 0;
			if (m[0] == Markup.Full) F++;
			if (m[1] == Markup.Full) F++;
			if (m[2] == Markup.Full) F++;
			if (m[3] == Markup.Full) F++;

			P = 0;
			if (m[0] == Markup.Partial) P++;
			if (m[1] == Markup.Partial) P++;
			if (m[2] == Markup.Partial) P++;
			if (m[3] == Markup.Partial) P++;

			//
			// Several interesting cases for saving this (the parent) trixel.
			// Case P==4, all four children are partials, so pretend parent is full, we save and return
			// Case P==3, and F==1, most of the parent is in, so pretend that parent is full again
			// Case P==2 or 3, but the previous testPartial had three partials, so parent was in an arc
			// as opposed to previous partials being fewer, so parent was in a tiny corner...
			why = Savecode.Nosave;
			//
			// The Big Idea: several heuristics cut off further subdivisions
			// but this may cause huge overshoots!
			// So, I added an option to tweak the heuristics
			// with a flagarray to ignore certain heuristic on demand
			//
			// code  abbrev         summary
			// ----------------------------------------------------
			// p4    peq4	        all four children are partial
			// f2    fge2           more than two children are full
			// p3    peq3feq1       3 are partial one is full
			// p1    pgt1ppreveq3   more than 1 partial, parent had three partials
			// a good halfspace to test this on is
			// (1, 0.8, 0.5, 0.9998) normalized, of course.
			// 

			/* <EXP> */
			//if (HtmState.Instance.maxlevel - level >= this._smallestLevel) {
			//    why = Savecode.Reachedlevel;
			//} /* </EXP> */
			// if you remove EXP code, make sure that else is also removed below

			// We sometimes force the smallest level
			// maxlevel is usually 20 - level is levels yet to go.
			// _smalletstLevel 
			keepgoing = (HtmState.Instance.maxlevel - level >= this._smallestLevel);
			if (level <= 0) why = Savecode.Reachedlevel;
			else if (P == 4)               why = Savecode.Peq4;
			else if (F >= 2)               why = Savecode.Fge2;
			else if ((P == 3) && (F == 1)) why = Savecode.Peq3Feq1;
				// 
				// Feb 07, 2005, ruls
				// if the feature size (side) is in the same ballpark
				// as the radius of the smallest circle among the halfspaces
				// Use this condition to stop if current level is greater than
				// smallest desired level

			else if (keepgoing && (P > 1 && PPrev == 3))
			// WAS1 : else if ((P > 1 && PPrev == 3))
			// WAS2 : else if ((level <= this._smallestLevel) && (P > 1 && PPrev == 3))
			// level is levels yet to go, so currect level is HtmState.Instance.maxlevel - level
			{
				why = Savecode.Pgt1Ppreveq3;
			}

			// 
			// from the C++ source:
			//   if ((level-- <= 0) || ((P == 4) || (F >= 2) || (P == 3 && F == 1) || (P > 1 && PPrev == 3))){
			// in above legacy line, level-- was wrong, it should be decremented after
			// all the tests are made
			// GYF: added to ensure a minimum depth
			//
			if (min_addlevel > 0 && level > 0)
				why = Savecode.Nosave;		// Force recursion, unless 

			if (why != Savecode.Nosave){
				///
				/// <LOG>"Saving <hid> code <why> "</LOG>
				/// 
				if(_mode == Mode.File){
					this._tracefile.dump("Saving {0} why {1} (level to go {2})",
						hid, why, level);
				}
				saveTrixel(hid, "1", level);
				return;
			} else {
				// look at each child, see if some need saving;
				for(int i=0; i<4; i++){
					if (m[i] == Markup.Full){
						///
						/// <LOG>"saving <hid> code Child is full"</LOG>
						if (_mode == Mode.File){
							this._tracefile.dump("Saving {0} why {1} (level to go {2})",
								ids[i], Savecode.Childisfull, level);
						}
						saveTrixel(ids[i], "2", level);
					}
				}
				// look at the four kids again, for partials
				level--; // <--- moved here from above, see legacy comment
				min_addlevel--;
				if (m[0] == Markup.Partial) testPartial(min_addlevel, level, ids[0], v0, w2, w1, P);
				if (m[1] == Markup.Partial) testPartial(min_addlevel, level, ids[1], v1, w0, w2, P);
				if (m[2] == Markup.Partial) testPartial(min_addlevel, level, ids[2], v2, w1, w0, P);
				if (m[3] == Markup.Partial) testPartial(min_addlevel, level, ids[3], w0, w1, w2, P);
			}
		}


		/// <summary>
		/// calls testTrixel
		/// testPartial, testNode
		/// Trivial test covex with one halfspace (point)
		///
		/// (1, 0, 0, 1) should only only test positive with
		/// 4 level one trixels
		///
		/// Wrapper to the intersect function
		/// This form allows the setting to the minimum level of
		/// HID generation.
		/// </summary>
		/// <param name="varlen"></param>
		/// <param name="min_level"></param>
		/// <param name="cutoff_level"></param>
		/// <param name="lohis"></param>
		public void intersect(bool varlen, int min_level, 
			int cutoff_level, ArrayList lohis){
			this._minlevel = min_level;
			intersect(varlen, cutoff_level, lohis);
		}

		/// <summary>
		/// Generate HTMIDs that intersect the Convex.
		/// intersect generates the htmids that are guaranteed to cover the convex.
		/// The place where the result is built must be supplied by the caller.
		/// This is because the Region is the union of convexes, and each convex
		/// object's intersect method adds to the common pool.
		/// </summary>
		/// <param name="varlen">If set (true) then true,
		/// (variable level) HIDs are generated. When this flag is false, then
		/// all HIDs are padded out to level 20. This means,
		/// that each HID at a level less than 20 expands to a range
		/// of consecutive numbers. In this case, a list of 
		/// low and a high values is inserted in the output array</param>
		/// <param name="cutoff_level">Don't search HTM beyond this level</param>
		/// <param name="lohis">Array that hold result of either HTMIDs or ranges of HTMID ranges</param>
		public void intersect(bool varlen, int cutoff_level, ArrayList lohis){
			_varlen = varlen;
			_addlevel = cutoff_level;
			_buildlist = lohis;
			_pseudoArea = 0;

			Halfspace hs;
			// <EXP> override addlevel 
			// _addlevel = _cutoffLevel;

			if(_addlevel < 1) _addlevel = 1; // At least level 1 nodes...
			/* NEW June 15, 2005 GYF:
			 * Remove redundant whole globes *
			 * */

			simplify();	// This step must be made!
			//  has the side effect of modifying _corners in simplify0
			//  but if it is never called, then _corners is empty
			//

			if(_halfspaces.Count == 0)
				return;   // nothing to intersect!!

			// Start with root nodes (index = 0-7) and intersect triangles
			bool appendflag = HtmState.Instance._appendTrace;
			if(_mode == Mode.File){
				if (_directory == null){
					throw (new Exception("Undefined debug directory in Convex"));
				}
				this._tracefile = new Trace(String.Concat(_directory, "ix.txt"), appendflag);
				this._savedfile = new Trace(String.Concat(_directory, "i"), appendflag);
				this._extendedfile = new Trace(String.Concat(_directory, "e"), appendflag);
				this._halfspacesfile = new Trace(String.Concat(_directory, "c"), appendflag);
		
				for(int i=0; i<_halfspaces.Count; i++){
					hs = (Halfspace) _halfspaces[i];
					_halfspacesfile.dump(hs.sv.x, hs.sv.y, hs.sv.z, hs.d, 1);
				}
			}
			// The core part of the algorithm.
			// get the smallest radius, etc...
			//  
			this.updateExtremes();
			this._numberSaved_ = 0;
			this._altSaved_ = 0;
			this._pseudoArea = 0L;
			for(int i=0; i<8; i++){
				testTrixel(i);
			}
			//
			// BREAK here to see how many were saved
			// and what is the pseudoarea
			//int k = _numberSaved_;
			//k = _altSaved_;
			//k = _buildlist.Count;
			//k = 2;
			HtmState.Instance.tcount = _pseudoArea;
			if (_mode == Mode.File){
				_tracefile.close();
				_savedfile.close();
				_extendedfile.close();
				_halfspacesfile.close();
			}
		}

		/// <summary>
		/// Public wrapper to simplification
		///
		/// Simplification eliminates redundant Halfspaces.
		/// For example, a halfspace that completely surrounds
		/// another halfspace is eliminated. If two halfspaces
		/// in the Convex are disjoint, then the Convex is empty
		/// </summary>
		public void normalize(){
			simplify();	
		}

		
		
		//// ////////////////////////// SIMPLIFICATION /////////////////////
		//
		//
		
		// ///////////SIMPLIFY/////////////////////////////////////
		// simplify: 
		//

		/// <summary>
		/// We have the following decision tree for the
		///           simplification of convexes:
		///
		///  Always test two constraints against each other. We have
		///
		///  * If both halfspaces are Positive or Zero
		///    # If they intersect: keep both
		///     # If one lies inside the other: drop the *larger* one
		///     # Else: disjunct. Clear the convex, stop.
		///
		///  * If both constraints are Negative
		///     # If they intersect or are disjunct: ok
		///     # Else: one lies in the other, drop smaller 'hole'
		///
		///  * Mixed: one Positive one Negative
		///     # No intersection, disjunct: Positive is redundant
		///     # Intersection: keep both
		///     # POS inside NEG: empty convex, stop.
		///     # NEG inside POS: keep both.
		/// Eliminate reduntand Halfspaces
		///
		/// Simplification eliminates redundant Halfspaces.
		/// For example, a halfspace that completely surrounds
		/// another halfspace is eliminated. If two halfspaces
		/// in the Convex are disjoint, then the Convex is empty
		/// </summary>
		public void simplify() {

			prune();  // remove redundant halfspaces, adjust sign.
			if (_halfspaces.Count < 1)
				return;
			if(_sign == Halfspace.Sign.Zero) {
				simplify0();	// treat zERO convexes separately
				return;
			}

			int i,j;
			int clen;
			bool restart = true;

			while(restart) {
				restart = false;
				clen = _halfspaces.Count;
				for(i = 0; i < clen; i++) {
					Halfspace hi = (Halfspace) _halfspaces[i];
					for (j = 0; j < i; j++) {
						Halfspace hj = (Halfspace)_halfspaces[j];
						Halfspace.Position iPosj;

						// don't bother with two zero constraints
						if (hi.sign == Halfspace.Sign.Zero &&
							hj.sign == Halfspace.Sign.Zero)
							continue;

						// No longer need to look at sign of halfspace, relativeposition does the work
						iPosj = hi.relativePosition(hj);
						if (iPosj == Halfspace.Position.Intersecting)
							continue;
						if (iPosj == Halfspace.Position.Disjoint) {					// disjoint ! convex is empty
							_halfspaces.Clear();
							return;
						}
						if (iPosj == Halfspace.Position.Surrounds) {
							_halfspaces.RemoveAt(i);
							restart = true;
							break;
						} else if (iPosj == Halfspace.Position.Contained) {
							_halfspaces.RemoveAt(j);
							restart = true;
							break;
						}
					} // for j
					if(restart) break;
				} // for i
			}// while(redundancy)

			// reset the sign of the convex
			_sign = ((Halfspace) _halfspaces[0]).sign;
			for(i = 1; i < _halfspaces.Count; i++) {
				Halfspace hi = (Halfspace) _halfspaces[i];
				switch(_sign){
					case Halfspace.Sign.Negative:
						if(hi.sign == Halfspace.Sign.Positive)
							_sign = Halfspace.Sign.Mixed;
						break;
					case Halfspace.Sign.Positive:
						if(hi.sign == Halfspace.Sign.Negative)
							_sign = Halfspace.Sign.Mixed;
						break;
					case Halfspace.Sign.Zero:
						_sign = hi.sign;
						break;
					case Halfspace.Sign.Mixed:
						break;
				}
			}

			if (_halfspaces.Count == 1) // for one constraint, it is itself the BC
				_boundingCircle.copy((Halfspace) _halfspaces[0]);
			else if (_sign == Halfspace.Sign.Positive)
				_boundingCircle.copy((Halfspace) _halfspaces[0]);

		}
		public void OLDsimplify() {

			prune();  // remove redundant halfspaces, adjust sign.
			if (_halfspaces.Count < 1)
				return;
			if (_sign == Halfspace.Sign.Zero) {
				simplify0();	// treat ZERO convexes separately
				return;
			}

			int i, j;
			int clen;
			bool redundancy = true;

			while (redundancy) {
				redundancy = false;
				clen = _halfspaces.Count;
				for (i = 0; i < clen; i++) {
					Halfspace hi = (Halfspace)_halfspaces[i];
					for (j = 0; j < i; j++) {
						Halfspace hj = (Halfspace)_halfspaces[j];
						Halfspace.Position iPosj;

						// don't bother with two zero constraints
						if (hi.sign == Halfspace.Sign.Zero &&
							hj.sign == Halfspace.Sign.Zero)
							continue;

						// both pos or zero
						if ((hi.sign == Halfspace.Sign.Positive
							|| hi.sign == Halfspace.Sign.Zero) &&
							(hj.sign == Halfspace.Sign.Positive ||
							hj.sign == Halfspace.Sign.Zero)) {

							// was: if ( (iPosj = testConstraints(i,j)) == 0 ) continue; // intersection
							if ((iPosj = hi.relativePosition(hj)) == Halfspace.Position.Intersecting) continue;
							if (iPosj == Halfspace.Position.Disjoint) {					// disjoint ! convex is empty
								_halfspaces.Clear();
								return;
							}
							// one is redundant
							if (iPosj == Halfspace.Position.Surrounds) {
								// was: constraints_.erase(constraints_.en d()-i-1);
								// was later:constraints_.erase(constraints_.begin()+i);
								_halfspaces.RemoveAt(i);
							} else if (iPosj == Halfspace.Position.Contained) {
								// was: constraints_.erase(constraints_.en d()-j-1);
								// later: was constraints_.erase(constraints_.begin()+j);
								_halfspaces.RemoveAt(j);
							} else
								continue;     // intersection
							redundancy = true; // we did cut out a constraint -> do the loop again
							break;
						}

						// both neg
						if ((hi.sign == Halfspace.Sign.Negative) &&
							(hj.sign == Halfspace.Sign.Negative)) {
							//was: if ( (iposj = testConstraints(i,j)) <= 0 ) continue; // ok
							iPosj = hi.relativePosition(hj);
							if (iPosj == Halfspace.Position.Intersecting || iPosj == Halfspace.Position.Disjoint)
								continue;
							// one is redundant
							if (iPosj == Halfspace.Position.Surrounds) {
								//was: constraints_.erase(constraints_.en d()-1-j);
								// later was:constraints_.erase(constraints_.begin()+j);
								_halfspaces.RemoveAt(j);
							} else if (iPosj == Halfspace.Position.Contained) {
								//was : constraints_.erase(constraints_.en d()-1-i);
								// later was: constraints_.erase(constraints_.begin()+i);
								_halfspaces.RemoveAt(i);
							} else
								continue; // intersection
							redundancy = true; // we did cut out a constraint -> do the loop again
							break;
						}

						// one neg, one pos/zero
						// was:if( (iposj = testConstraints(i,j)) == 0) continue; // ok: intersect
						if ((iPosj = hi.relativePosition(hj)) == Halfspace.Position.Intersecting)
							continue;
						if (iPosj == Halfspace.Position.Disjoint) { // neg is redundant
							if (hi.sign == Halfspace.Sign.Negative) {
								//was: constraints_.erase(constraints_.en d()-1-i);
								//was later:constraints_.erase(constraints_.begin()+i);
								_halfspaces.RemoveAt(i);
							} else {
								//was: constraints_.erase(constraints_.en d()-1-j);
								// was later: constraints_.erase(constraints_.begin()+j);
								_halfspaces.RemoveAt(j);
							}
							redundancy = true; // we did cut out a constraint -> do the loop again
							break;
						}
						// if the negative constraint is inside the positive: continue
						if ((hi.sign == Halfspace.Sign.Negative && iPosj == Halfspace.Position.Contained) ||
							(hj.sign == Halfspace.Sign.Negative && iPosj == Halfspace.Position.Surrounds))
							continue;

						// positive constraint in negative: convex is empty!
						_halfspaces.Clear();
						return;
					} // for (i...)
					if (redundancy) break;
				}
			}// while(redundancy)

			// reset the sign of the convex
			_sign = ((Halfspace)_halfspaces[0]).sign;
			for (i = 1; i < _halfspaces.Count; i++) {
				Halfspace hi = (Halfspace)_halfspaces[i];
				switch (_sign) {
					case Halfspace.Sign.Negative:
						if (hi.sign == Halfspace.Sign.Positive)
							_sign = Halfspace.Sign.Mixed;
						break;
					case Halfspace.Sign.Positive:
						if (hi.sign == Halfspace.Sign.Negative)
							_sign = Halfspace.Sign.Mixed;
						break;
					case Halfspace.Sign.Zero:
						_sign = hi.sign;
						break;
					case Halfspace.Sign.Mixed:
						break;
				}
			}

			if (_halfspaces.Count == 1) // for one constraint, it is itself the BC
				_boundingCircle.copy((Halfspace)_halfspaces[0]);
			else if (_sign == Halfspace.Sign.Positive)
				_boundingCircle.copy((Halfspace)_halfspaces[0]);

		}
		/////////////SIMPLIFY0////////////////////////////////////
		// 
		//

		/// <summary>
		/// Simplify ZERO convexes. calculate corners of convex
		/// and the bounding circle.
		///
		/// ZERO convexes are made up of constraints which are all great
		/// circles. It can happen that some of the constraints are redundant.
		/// For example, if 3 of the great circles define a triangle as the convex
		/// which lies fully inside the half sphere of the fourth constraint,
		/// that fourth constraint is redundant and will be removed.
		///
		/// The algorithm is the following:
		///
		/// zero-constraints are half-spheres, defined by a single normalized
		/// vector v, pointing in the direction of that half-sphere.
		///
		/// Two zero-constraints intersect at
		///
		///    i    =  +- v  x v
		///     1,2        1    2
		///
		/// the vector cross product of their two defining vectors.
		///
		/// The two vectors i1,2 are tested against every other constraint in
		/// the convex if they lie within their half-spheres. Those
		/// intersections i which lie within every other constraint, are stored
		/// into corners_.
		///
		/// Constraints that do not have a single corner on them, are dropped.
		/// </summary>
		private void simplify0() {
			int i;
			int j,k;
			Cartesian vi1, vi2;
			//typedef std::vector<size_t> ValueVectorSzt;
			//ValueVectorSzt cornerConstr1, cornerConstr2, removeConstr;
			//ValueVectorSpvec corner;
			// bool ruledout;
			ArrayList corner = new ArrayList();
			
			ArrayList cornerConstr1 = new ArrayList();
			ArrayList cornerConstr2 = new ArrayList();
			ArrayList removeConstr = new ArrayList();

			bool vi1ok, vi2ok;
			bool[] ruledout = null;

			if (_halfspaces.Count == 1) { // for one constraint, it is itself the BC
				_boundingCircle.copy((Halfspace) _halfspaces[0]);
				return;
			} else if(_halfspaces.Count == 2) {
				// For 2 constraints, take the bounding circle a 0-constraint...
				// this is by no means optimal, but the code is optimized for at least
				// 3 ZERO constraints... so this is acceptable.

				// test for constraints being identical - rule 1 out
				Halfspace hs0, hs1;
				hs0 = (Halfspace) _halfspaces[0];
				hs1 = (Halfspace) _halfspaces[1];
				if(hs0.sv.eq(hs1.sv)){
					// delete 1
					_halfspaces[1] = null; // was:constraints_.erase(constraints_.end()-1);
					_boundingCircle.copy(hs0);
					return;
				}
				// test for constraints being two disjoint half spheres - empty convex!
				if(hs0.sv.opposite(hs1.sv)){
					_halfspaces.Clear();
					return;
				}
				_boundingCircle = new Halfspace(); // TODO: halfspace constructor from sum of two vectors
				_boundingCircle.d = 0;
				_boundingCircle.sv.assign(hs0.sv);
				_boundingCircle.sv.addMe(hs1.sv);
				_boundingCircle.sv.normalizeMe();

				//	SpatialConstraint(constraints_[0].v() +
				//	constraints_[1].v(),0);
				return;
			}

			// Go over all pairs of constraints
			ruledout = new bool[_halfspaces.Count];
			for(i=0; i<(int)_halfspaces.Count; i++){
				ruledout[i] = true;
			}
			for(i = 0; i < _halfspaces.Count - 1; i++) {
				// ruledout = true;
				// was:
				Halfspace hi, hj;
				hi = (Halfspace) _halfspaces[i];
				for(j = i+1; j < _halfspaces.Count; j ++) {
					hj = (Halfspace) _halfspaces[j];
					// test for constraints being identical - rule i out
					if(hi.sv.eq(hj.sv))		//constraints_[i].a_ == constraints_[j].a_) 
						break;
					// test for constraints being two disjoint half spheres - empty convex!
					if(hi.sv.opposite(hj.sv)){
						_halfspaces.Clear();
						return;
					}
					
					// vi1 and vi2 are their intersection points
//					vi1 = constraints_[i].a_ ^ constraints_[j].a_ ;
//					vi1.normalize();
//					vi2 = (-1.0) * vi1;

					vi1 = new Cartesian(hi.sv);
					vi1.crossMe(hj.sv);
					vi1.normalizeMe();
					vi2 = new Cartesian(vi1);
					vi2.scaleMe(-1.0);

					vi1ok = true;
					vi2ok = true;
					//
					// now test whether vi1 or vi2 or both are inside every constraint.
					// other than i or j
					// if yes, store them in the corner array.
					// vi1ok is false, if vi1 is outisde even one constraint
					//
					for(k = 0; k < _halfspaces.Count; k++) {
						Halfspace hk;
						if( k == i || k == j) continue;
						hk = (Halfspace)_halfspaces[k];
						if(vi1ok && vi1.dot(hk.sv) <= 0.0) {
							vi1ok = false;
						}
						if(vi2ok && vi2.dot(hk.sv) <= 0.0) {
							vi2ok = false;
						}
						if(!vi1ok && !vi2ok) // don't look further
							break;
					}
					if(vi1ok) { // vi1 is inside all other constraints
						corner.Add(vi1);
						// was: corner.push_back(vi1);
						cornerConstr1.Add(i);
						cornerConstr2.Add(j);

						// was: cornerConstr1.push_back(i);
						// was: cornerConstr2.push_back(j);
	
						ruledout[i] = false;
						ruledout[j] = false;
						// ruledout = false;
					}
					if(vi2ok) {
//						corner.push_back(vi2);
//						cornerConstr1.push_back(i);
//						cornerConstr2.push_back(j);
						corner.Add(vi2);
						cornerConstr1.Add(i);
						cornerConstr2.Add(j);
						ruledout[i] = false;
						ruledout[j] = false;
	
						// ruledout = false;
					}
				} // END of J loop
				// is this constraint ruled out? i.e. none of its intersections
				// with other constraints are corners... remove it from constraints_ list.
				/******************** WAS:
				if(ruledout) {
					removeConstr.push_back(i);
				}
				**********************************************/
			} // END of i loop

			// See if you can rule out a constraint
			//
			for(i=0; i< (int) _halfspaces.Count; i++){
				if (ruledout[i]){
					removeConstr.Add(i);
				}
			}
			ruledout = null; // delete ruledout;


			// Now set the corners into their correct order, which is an
			// anti-clockwise walk around the polygon.
			//
			// start at any corner. so take the first.
			if (corner.Count == 0){
				_halfspaces.Clear();
				return;
			}
 
			_corners.Clear();
			_corners.Add(corner[0]);

			// The trick is now to start off into the correct direction.
			// this corner has two edges it can walk. we have to take the
			// one where the convex lies on its left side.
			i = (int) cornerConstr1[0];		// the i'th constraint and j'th constraint
			j = (int) cornerConstr2[0];		// intersect at 0'th corner
			int c1,c2,k1,k2;
			// Now find the other corner where the i'th and j'th constraints intersect.
			// Store the corner in vi1 and vi2, and the other constraint indices
			// in c1,c2.
			c1 = c2 = k1 = k2 = -1;
			vi1 = null;
			vi2 = null;
			for( k = 1; k < cornerConstr1.Count; k ++) {
				if((int) cornerConstr1[k] == i) {
					vi1 = (Cartesian) corner[k];
					c1 = (int) cornerConstr2[k];
					k1 = k;
				}
				if((int) cornerConstr2[k] == i) {
					vi1 = (Cartesian) corner[k];
					c1 = (int) cornerConstr1[k];
					k1 = k;
				}
				if((int) cornerConstr1[k] == j) {
					vi2 = (Cartesian) corner[k];
					c2 = (int) cornerConstr2[k];
					k2 = k;
				}
				if((int) cornerConstr2[k] == j) {
					vi2 = (Cartesian) corner[k];
					c2 = (int) cornerConstr1[k];
					k2 = k;
				}
			}
			// Now test i'th constraint-edge ( corner 0 and corner k ) whether
			// it is on the correct side (left)
			//
			//  ( (corner(k) - corner(0)) x constraint(i) ) * corner(0)
			//
			// is >0 if yes, <0 if no...
			//
			int c,currentCorner;
			Cartesian va;
			va = new Cartesian(vi1);
			va.subMe((Cartesian) corner[0]);
			va.crossMe( ((Halfspace) _halfspaces[i]).sv);
			if (va.dot((Cartesian) corner[0]) > 0.0){
				if (k1 == -1) {
					throw (new Exception("Simplify0:k1 uninitialized"));
				}
				_corners.Add(vi1);
				c = c1;
				currentCorner = k1;
			} else {
				if (k2 == -1) {
					throw (new Exception("Simplify0:k2 uninitialized"));
				}
				_corners.Add(vi2);
				c = c2;
				currentCorner = k2;
			}
//			if( ((vi1 - corner[0]) ^ constraints_[i].a_) * corner[0] > 0 ) {
//				corners_.push_back(vi1);
//				c = c1;
//				currentCorner = k1;
//			} else {
//				corners_.push_back(vi2);
//				c = c2;
//				currentCorner = k2;
//			}

			
			// Now append the corners that match the index c until we got corner 0 again
			// currentCorner holds the current corners index
			// c holds the index of the constraint that has just been intersected with
			// So:
			// x We are on a constraint now (i or j from before), the second corner
			//   is the one intersecting with constraint c.
			// x Find other corner for constraint c.
			// x Save that corner, and set c to the constraint that intersects with c
			//   at that corner. Set currentcorner to that corners index.
			// x Loop until 0th corner reached.
			while( currentCorner > 0 ) {
				for (k = 0; k < cornerConstr1.Count; k++) {
					if(k == currentCorner)continue;
					if( (int) cornerConstr1[k] == c) {
						if( (currentCorner = k) == 0) break;
						_corners.Add(corner[k]);
						c = (int) cornerConstr2[k];
						break;
					}
					if((int) cornerConstr2[k] == c) {
						if( (currentCorner = k) == 0) break;
						_corners.Add(corner[k]);
						c = (int) cornerConstr1[k];
						break;
					}
				}
			}
			// Remove all redundant constraints
			for ( i = removeConstr.Count - 1; i>=0; i--){ //Start from END
				// WAS earlier: constraints_.erase(constraints_.end()-removeConstr[i]-1); 
				/// in C++:constraints_.erase(constraints_.begin()+removeConstr[i]);
				_halfspaces.RemoveAt( (int) removeConstr[i]);
			}
			// Now calculate the bounding circle for the convex.
			// We take it as the bounding circle of the triangle with
			// the widest opening angle. All triangles made out of 3 corners
			// are considered.
			_boundingCircle.d = 1.0;
			if (_halfspaces.Count >=3 ) {
				for(i = 0; i < (int) _corners.Count; i++){
					for(j = i+1; j < _corners.Count; j++){
						for(k = j+1; k < _corners.Count; k++) {
							Cartesian v = new Cartesian();
							Cartesian tv = new Cartesian();
							v.assign((Cartesian) _corners[j]);
							v.subMe((Cartesian) _corners[i]);
							tv.assign((Cartesian) _corners[k]);
							tv.subMe((Cartesian) _corners[j]);
							v.crossMe(tv);
							v.normalizeMe();

							//	SpatialVector v = ( corners_[j] - corners_[i] ) ^
							//	( corners_[k] - corners_[j] );
							//	v.normalize();
							// Set the correct opening angle: Since the plane cutting
							// out the triangle also correctly cuts out the bounding cap
							// of the triangle on the sphere, we can take any corner to
							// calculate the opening angle
							double d = v.dot((Cartesian) _corners[i]);
							if(_boundingCircle.d > d){
								_boundingCircle.d = d;
								_boundingCircle.sv.assign(v);
								//new Halfspace(v,d);
							}
						}
					}
				}
			}
		}
	}
}
