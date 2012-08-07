using System;
using System.Collections;

/*=====================================================================

  File:      Chull.cs for Spatial Sample
  Summary:   Computes the convex hull of a collection of points
	         on the surface of the sphere.
  Date:	     August 10, 2005

---------------------------------------------------------------------
  This spatial search library was developed by Alexander Szalay, Robert Brunner, 
  Peter Kunszt, and Gyorgy Fekete at the Department of Physics and Astronomy, 
  The Johns Hopkins University, in collaboration with Jim Gray, Microsoft Research.
  Details of the algorithms can be found at http://www.sdss.jhu.edu/htm/.
 
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.
======================================================= */

// My legal resposibilities:
// Copyright 2001, softSurfer (www.softsurfer.com)
// This code may be freely used and modified for any purpose
// providing that this copyright notice is included with it.
// SoftSurfer makes no warranty for this code, and cannot be held
// liable for any real or imagined damage resulting from its use.
// Users of this code must verify correctness for their application.
//
// Requires 
// Point with coordinates {double x, y;}
// Modified by Gyorgy (George) Fekete, Johns Hopkins University
// Center for Astrophysical Sciences, Baltimore, MD
//
//
namespace Microsoft.Samples.SqlServer {
	
	
	/// <summary>
	/// 
	/// Computes the convex hull of a collection of points
	/// on the surface of the sphere.
	///
	/// This is a somewhat clever (read hacky), albeit not completely tested
	/// way of computing the convex hull on a half-spehere.
	/// 
	/// Some assumptions that must hold true for this to work.
	/// All the given points must fit on a hemisphere. This halfsphere
	/// is a Halfspace, whose directional vector points to the centroid of the
	/// collection of points. The algorithm used here is actually a modified version
	/// of a 2D convex hull algorithm (Andrews) and what makes it work, is that we
	/// restrict the point to a hemishphere. Either a parallel or a radial
	/// projection from the 
	/// hemisphere to the tangent plane turns the problem into "2D"
	/// 
	/// </summary>
	public class Chull : HtmShape , IComparer {

		private double  rot00, rot01, rot02,	// a rigid
				rot10, rot11, rot12,	// rotation matrix 
				rot20, rot21, rot22;

		public class CPoint : Cartesian {
			public bool keeper;
			public double x_in;
			public double y_in;
			public double z_in;
		}
		ArrayList input;
		ArrayList output;

		int nPoints;
		int nrKept;
		private void chinit(){
			input = new ArrayList();
			output = new ArrayList();
			nPoints = 0;
			makeRotator(); // initializes rotXY variables
		}
		private void makeRotator(){
			rot00 = rot11 = rot22 = 1.0;
			rot01 = rot02 = 0.0;
			rot10 = rot12 = 0.0;
			rot20 = rot21 = 0.0;
		}
		//
		//  creates a rotation matrix and saves it in the
		//  instance's internal variables
		private void makeRotator(CPoint v) {
			double theta, phi;
			double m00, m01, m02, m10, m11, m12, m20, m21, m22;
			double n00, n01, n02, n10, n11, n12, n20, n21, n22;
						
			// WA: theta = acos(v.x_in / sqrt(norm));
			theta = Math.Atan2(v.y_in, v.x_in);
			phi = Math.Acos(v.z_in);
						
			m00 = m11 = Cartesian.Cos(theta);
			m10 = -(m01 = Math.Sin(theta));
			m02 = m12 = m20 = m21 = 0.0;
			m22 = 1.0;
						
			n00 = n22 = v.z_in; // cos(phi);
			n02 = -(n20 = Math.Sin(phi));
			n01 = n10 = n21 = n12 = 0.0;
			n11 = 1.0;
						
			// Multiply n.m for the rotator
						  
			rot00 = n00*m00 + n01*m10 + n02*m20;
			rot01 = n00*m01 + n01*m11 + n02*m21;
			rot02 = n00*m02 + n01*m12 + n02*m22;
						
			rot10 = n10*m00 + n11*m10 + n12*m20;
			rot11 = n10*m01 + n11*m11 + n12*m21;
			rot12 = n10*m02 + n11*m12 + n12*m22;
						
			rot20 = n20*m00 + n21*m10 + n22*m20;
			rot21 = n20*m01 + n21*m11 + n22*m21;
			rot22 = n20*m02 + n21*m12 + n22*m22;
						
			//   use this code if you need to check the determinant
			//   it should be 1 for a rigid rotation
			//   {
			//     double det;
			//     det = rot00 * (rot11 * rot22 - rot21 * rot12);
			//     det -= rot01 * (rot10 * rot22 - rot20 * rot12);
			//     det += rot02 * (rot10 * rot21 - rot11 * rot20);
			//     write the determinant
			//   }
			return;
		}
		public Chull(){
			init();
			chinit();
			_reg = null;
		}
		/// <summary>
		/// Constructor for remembering where to build result
		/// </summary>
		/// <param name="reg">The Region into which to build things.</param>
		public Chull(Region reg){
			//
			// TODO: Add constructor logic here
			//
			init();
			chinit();
			_reg = reg;
		}
		/// <summary>
		/// 2D convex hull without building a Region
		/// in and out of this object, and sorting.
		/// </summary>
		/// <param name="x">array of X coordinates</param>
		/// <param name="y">array of Y coordinates</param>
		/// <param name="len">number of 2D points in input</param>
		/// <param name="ox">array of output X coordinates</param>
		/// <param name="oy">array of output Y coordinates</param>
		/// <returns>the number of points in output</returns>
		public int testHull(double[] x, double[] y, int len, 
			double[] ox, double[] oy){
			// Bypasses a lot of stuff, really only used for testing the 2D
			// Andrews algorithm
			// Observe the preconditions to chainHull_2D()
			CPoint p;
			int k;
			output.Clear();
			input.Clear();
			for(int i=0; i<len; i++){
				p = new CPoint();
				p.x_in = p.x = x[i];
				p.y_in = p.y = y[i];
				p.z_in = p.z = 0.0;
				p.keeper = false;
				input.Add(p);
			}
			input.Sort(this);
			k = chainHull_2D();
			for(int i=0; i<k; i++){
				ox[i] = ((CPoint) output[i]).x;
				oy[i] = ((CPoint) output[i]).y;
			}
			return k;
		}

		/// <summary>
		/// Convex hull from RA/DEC values
		/// </summary>
		/// <param name="ra">array of RA values</param>
		/// <param name="dec">array of DEC value</param>
		/// <param name="len">number of 2D points in input</param>
		/// <param name="ora">array of output RA values</param>
		/// <param name="odec">array of output DEC values</param>
		/// <returns>the number of points in output</returns>
		public int getHull(double[] ra, double[] dec, int len, 
			double[] ora, double[] odec){
			// Ra/dec wrapper around the cartesian version
			double[] x, y, z, ox, oy, oz;
			int olen;
			x = new double[len];
			y = new double[len];
			z = new double[len];
			ox = new double[len+1];
			oy = new double[len+1];
			oz = new double[len+1];
			ora = new double [len+1];
			odec = new double[len+1];
			for(int i=0; i<len; i++){ // input lenth
				SpatialVector.radec2cartesian(ra[i], dec[i],
					out x[i], out y[i], out z[i]);
			}
			olen = getHull(x, y, z, len, ox, oy, oz);
			for(int i=0; i<olen; i++){ // output length
				SpatialVector.xyz2radec(x[i], y[i],z[i],
					out	ra[i], out dec[i]);
			}
			return olen;
		}
		public int getHull(double[] x, double[] y, double[] z, int len,
			double[] ox, double[] oy, double[] oz){
			//
			// All the input points are in x,y,z...
			// To make a convex hull, follow these steps:
			// 1. add all the points to the internal array
			// 2. compute the centroid
			// 3. make the rotation matrix, that rotates the centroid to (x,y,0)
			// 4. rotate the points (call xform, returns error if more than hemisphere)
			// 5. sort (by x,y)
			// 6. compute the convex hull, result is a polygon
			// 7. Call the polygoner to make the convex
			// todo: encapsulate Polygon into Chull, or use inheritance?
			//
			CPoint centroid = new CPoint();
			for(int i=0; i<len; i++){
				addPoint(x[i], y[i], z[i]);
			}
			this.nPoints = len;
			this.getCentroid( centroid);
			this.makeRotator(centroid);
			if (this.xform()){
				this.sort();
				nrKept = this.chainHull_2D(); 
				CPoint p;
				for(int i=0; i<nrKept; i++){
					p = (CPoint) output[i];
					ox[i] = p.x_in;
					oy[i] = p.y_in;
					oz[i] = p.z_in;
				}
			
			} else {
				//
				// Error, points were more than a hemisphere
				//
				nrKept = 0;
			}
								
			return nPoints;
		}
		public bool addConvex(double[] x, double[] y, double[] z, int len){
			//
			// All the input points are in x,y,z...
			// To make a convex hull, follow these steps:
			// 1. add all the points to the internal array
			// 2. compute the centroid
			// 3. make the rotation matrix, that rotates the centroid to (x,y,0)
			// 4. rotate the points (call xform, returns error if more than hemisphere)
			// 5. sort (by x,y)
			// 6. compute the convex hull, result is a polygon
			// 7. Call the polygoner to make the convex
			// todo: encapsulate Polygon into Chull, or use inheritance?
			//
			if (_reg == null)
				return true;
			CPoint centroid = new CPoint();
			for(int i=0; i<len; i++){
				addPoint(x[i], y[i], z[i]);
			}
			this.nPoints = len;
			this.getCentroid( centroid);
			this.makeRotator(centroid);
			if (this.xform()){
				this.sort();
				nrKept = this.chainHull_2D(); 
				// output has it all
				// allmost like Polygon, except first point is included at end
				// We can now make a polygon out of it. Luckily it takes
				// care of the winding error
				//
				// First, move over the x,y,z points to double arrays;
				//
				double[] xa, ya, za;
				CPoint p;
				xa = new double[nrKept];
				ya = new double[nrKept];
				za = new double[nrKept];
				for(int i=0; i<nrKept; i++){
					p = (CPoint) output[i];
					xa[i] = p.x_in;
					ya[i] = p.y_in;
					za[i] = p.z_in;
				}
				Polygon poly = new Polygon(this._reg);
				poly.add(xa, ya, za, nrKept-1); // because first connects to last
			
			} else {
				//
				// Error, points were more than a hemisphere
				//
				return false;
			}
			return true;
		}
		void addPoint(double ax, double ay, double az){
			CPoint p = new CPoint();
			p.keeper = false;
			p.x_in = ax;
			p.y_in = ay;
			p.z_in = az;
			input.Add(p);
		}
		void getCentroid(CPoint c) {
			int nelts;
			nelts = this.nPoints;
			double sumx, sumy, sumz;
			double norm;
			sumx = sumy = sumz = 0.0;
			for(int i=0; i<nelts; i++){
				sumx += ((CPoint) input[i]).x_in;
				sumy += ((CPoint) input[i]).y_in;
				sumz += ((CPoint) input[i]).z_in;
			}
			norm = sumx * sumx;
			norm += sumy * sumy;
			norm += sumz * sumz;
			norm = Math.Sqrt(norm);
			if (norm > Cartesian.Epsilon){
				sumx /= norm;
				sumy /= norm;
				sumz /= norm;
			} else {
				sumx = sumy = sumz = 0.0;
			}
			c.x_in = sumx;
			c.y_in = sumy;
			c.z_in = sumz;
			return;
		}
		void rotate(CPoint p){
			p.x = rot00 * p.x_in + rot01 * p.y_in + rot02 * p.z_in;
			p.y = rot10 * p.x_in + rot11 * p.y_in + rot12 * p.z_in;
			p.z = rot20 * p.x_in + rot21 * p.y_in + rot22 * p.z_in;
			return;
		}
		bool fakeform() { // Use for debuggin only, copy x_in into x, no div
			CPoint p;
			int nelts;
			nelts = this.nPoints;
			for(int i = 0; i<nelts; i++){
				p = (CPoint) input[i];
				p.x = p.x_in;
				p.y = p.y_in;
			}
			return true;	
		}
		bool xform() {
			CPoint p;
			int nelts;
			nelts = this.nPoints;
			for(int i = 0; i<nelts; i++){
				p = (CPoint) input[i];
				rotate(p); // sets p->x,y,z from p->x_in, etc and the rotator
				if(p.z <= Cartesian.Epsilon){
					return false;			// Can not work with this object
				}
				// else
				p.x /= p.z;
				p.y /= p.z;
			}
			return true;
		}
						
		void sort(){ 		// First by x, then by y, then ...
			//size_t nelts;
			//size_t siz;
			//nelts = this.nPoints;
			//siz = sizeof (Point);
			input.Sort(this);
			//qsort(input, nelts, siz, pointComp);
		}
		public int Compare(Object a, Object b){

		  CPoint pa = (CPoint) a;
		  CPoint pb = (CPoint) b;
		
		  if (pa.x < pb.x){
		    return -1;
		  }
		  if (pa.x > pb.x){
		    return 1;
		  }
		  if (pa.y < pb.y){
		    return -1;
		  }
		  if (pa.y > pb.y){
		    return 1;
		  }
		  return 0;
		}
		
		double isLeft( Object obj0, Object obj1, Object obj2){
			CPoint P0 = ((CPoint) obj0);
			CPoint P1 = ((CPoint) obj1);
			CPoint P2 = ((CPoint) obj2);
			return (P1.x - P0.x)*(P2.y - P0.y) - (P2.x - P0.x)*(P1.y -  P0.y);
		}
		//===================================================================
		// chainHull_2D(): Andrew's monotone chain 2D convex hull algorithm
