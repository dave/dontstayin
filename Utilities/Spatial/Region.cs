using System;
using System.Collections;
using System.Text;

/*=====================================================================

  File:      Region.cs for Spatial Sample
  Summary:   Implements a union of convexes
  Date:	     August 16, 2005

---------------------------------------------------------------------
  This spatial search library was developed by Alexander Szalay, Robert Brunner, 
  Peter Kunszt, and Gyorgy Fekete at the Department of Physics and Astronomy, 
  The Johns Hopkins University, in collaboration with Jim Gray, Microsoft Research.
  Details of the algorithms can be found at http://www.sdss.jhu.edu/htm/.
 
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.
======================================================= */


///
///  This is region, the normal form of everything
///  
///
namespace Microsoft.Samples.SqlServer
{
	/// <summary>
	/// Region is a union of convexes.
	/// </summary>
	public class Region {
		ArrayList _convexes;
		HtmRange  _range;
		string    _directory = null; // The place for debugging, if any
		private void init(){
			_range = new HtmRange();
			_convexes = new ArrayList();
		}
		protected bool selfdebug(){
			return (_directory != null);
		}
		public Region(string dir){
			_directory = dir;
			init();
		}
		public Region() {
			_directory = null;
			init();
			//
			// TODO: Add constructor logic here
			//
		}
		public int Count {
			get {
				return _convexes.Count;
			}
		}
		public Convex getNth(int n){
			if (n < _convexes.Count){
				return (Convex) _convexes[n];
			} else {
				return null;
			}
		}
		public void Clear(){
			//
			// For C++ programmers, you must take care to free
			// local stuff in convex and range objects. Write
			// destructors carefully
			//
			_convexes.Clear();
			_range.Clear();
		}
		public void add(Convex c){
			_convexes.Add(c);
		}
		public void normalize(){
			//
			for(int i=0; i<_convexes.Count; i++){
				Convex c = (Convex) _convexes[i];
				c.normalize();
			}
			return;
		}
		
		public void smartintersect(bool varlen, int minlevel, int cutoff_level, ArrayList lohis) {
			//
			// forms the per convex based intersect, eac of which gets
			// added to the global list;
			//
			ArrayList templist = new ArrayList();
			_range.Clear();
			for (int i = 0; i < _convexes.Count; i++) {
				Convex c = (Convex)_convexes[i];
				c.notrace(true);
				c.intersect(varlen, minlevel, cutoff_level, templist);
				if (templist.Count > 1) { // Do it again with the rules
					int fudle;
					int hlevel;
					int magic = HtmState.Instance.magicnumber;
					// make magic 31 for tight fit
					// make magic 30 for relaxed fit (unlike jeans)
					// 
					Int64 blorp = HtmState.Instance.tcount;
					for (fudle = 0; blorp > 0; fudle++) {
						blorp >>= 2;
					}
					hlevel = (magic - fudle) / 2;
					HtmState.Instance.minlevel = hlevel; // Do you reset this anytime?
					HtmState.Instance.maxlevel = hlevel + HtmState.Instance.hdelta; // reasonable cutoff? <EXP> 
					templist.Clear();
					/* 
					 * This is the one that counts: previous intersect should never dump to file 
					 */
					if (selfdebug()) {
						c.set_debugdirectory(_directory);
					}

					HtmState.Instance._appendTrace = (i != 0); // read below what I mean. append, unless i == 0
					//if (i == 0) {
					//    HtmState.Instance._appendTrace = false;
					//} else {
					//    HtmState.Instance._appendTrace = true;
					//}
					c.intersect(varlen,
						HtmState.Instance.minlevel,
						HtmState.Instance.maxlevel, templist);
				}
				// we are programming to an interface:
				// lohis is supplied by caller.
				// internally, we keep a Range objects, which contains a sorted 
				// list of lows and highs.
				// temporary lohis returned from intersect must be merged with
				// range. The final contents of _range is converted to ArrayList
				// of lo-hi value pairs. This object is reusable, but mist be
				// Clear-ed after each use.
				//
				_range.merge(templist);
				templist.Clear();
			}
			//
			// Transfer result to output
			//
			int k = 0;
			for (int i = 0; i < _range.Count; i++) {
				lohis.Add((Int64)_range.lows.GetKey(i));
				lohis.Add((Int64)_range.highs.GetKey(i));
				k += 2;
			}
			return;
		}
		// END Smart interswect
		public void intersect(bool varlen, int minlevel, int cutoff_level, ArrayList lohis) {
			//
			// forms the per convex based intersect, eac of which gets
			// added to the global list;
			//
			ArrayList templist = new ArrayList();
			_range.Clear();
			for (int i = 0; i < _convexes.Count; i++) {
				Convex c = (Convex) _convexes[i];
				if (selfdebug()){
					c.set_debugdirectory(_directory);
				}
				HtmState.Instance._appendTrace = (i != 0); // read below what I mean. append, unless i == 0
				//if (i == 0) {
				//    HtmState.Instance._appendTrace = false;
				//} else {
				//    HtmState.Instance._appendTrace = true;
				//}
				c.intersect(varlen, minlevel, cutoff_level, templist);
				// we are programming to an interface:
				// lohis is supplied by caller.
				// internally, we keep a Range objects, which contains a sorted 
				// list of lows and highs.
				// temporary lohis returned from intersect must be merged with
				// range. The final contents of _range is converted to ArrayList
				// of lo-hi value pairs. This object is reusable, but mist be
				// Clear-ed after each us.e
				//
				_range.merge(templist);
				templist.Clear();
			}
			//
			// Transfer result to output
			//
			int k=0;
			for(int i=0; i<_range.Count; i++){
				lohis.Add((Int64) _range.lows.GetKey(i));
				lohis.Add((Int64) _range.highs.GetKey(i));
				k+=2;
			}
			return;
		}
		public override string ToString(){
			StringBuilder sb = new StringBuilder();
			sb.Append("REGION ");
			for(int i=0; i<_convexes.Count; i++){
				Convex c = (Convex)_convexes[i];
				if (c.Count > 0) {
					sb.Append("CONVEX CARTESIAN ");
					for (int j = 0; j < c.size; j++) {
						Halfspace hs = c.hsAt(j);
						sb.Append(hs.ToString());
					}
				}
			}
			return sb.ToString();
		}
		//public void WriteConsole() {
		//    Convex c;
		//    Console.WriteLine("Region::");
		//    for (int i = 0; i < _convexes.Count; i++) {
		//        c = (Convex)this._convexes[i];
		//        c.WriteConsole();
		//    }
		//}
	}
}
