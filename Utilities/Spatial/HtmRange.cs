using System;
using System.Collections;
using System.Text;

/*=====================================================================

  File:      HtmRange.cs for Spatial Sample
  Summary:   Implements a sorted list of intervals.
  Date:	     August 16, 2005

---------------------------------------------------------------------
  This spatial search library was developed by Alexander Szalay, Robert Brunner, 
  Peter Kunszt, and Gyorgy Fekete at the Department of Physics and Astronomy, 
  The Johns Hopkins University, in collaboration with Jim Gray, Microsoft Research.
  Details of the algorithms can be found at http://www.sdss.jhu.edu/htm/.
 
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.
======================================================= */


//
// Gyorgy Fekete 
// Johns Hopkins University
// Physics and Astronomy
namespace Microsoft.Samples.SqlServer
{

	/// <summary>
	/// Manipulate Ranges
	/// A <i>Range</i> in this context is a
	/// sorted list of intervals, and an interval is and
	/// ordered pair. The motivation for this class came
	/// from the need to manage a potentially large number of HID ranges.
	/// It is important, that the ordered pairs represent disjoint
	/// intervals of numbers, that is, if (a, b) and (c, d) is in the
	/// Range, then b less than c. One of the tasks that this class performs
	/// is ensuring that the above constraint is always satisifed.
	/// If two overlapping intervals are encountered, then
	/// they would be merged, for example.
	/// </summary>
	public class HtmRange {
		public SortedList lows; // array of interval starts
		public SortedList highs;  // array of interval ends
		private SortedList new_lows;
		private SortedList new_highs;
		private SortedList tmp;

		/// <summary>
		/// Default constructor
		/// </summary>
		public HtmRange ( ) {
			lows = new SortedList ( );
			highs = new SortedList ( );
			new_lows = new SortedList ( );
			new_highs = new SortedList ( );
		}
		private enum Mode {
			abFirst, lohiFirst, abOnly, lohiOnly, allGone
		};
		private enum Feed {
			abFeed, lohiFeed, noFeed
		};
		private enum RangeOrder {
			Lessthan, Inside, Contains, Greaterthan, Leftadjacent, Rightadjacent, Intersect,
			Equal,
			Yes, No,// this one is computed by predicates like 'disjoint'
			// those are not boolean, because 2 valued is not enough.
			// must allow for underfined also
			Undefined
		};
		private void sortlist ( ArrayList list ) {
			list.Sort ( 0, list.Count, null );

			ArrayList hold = new ArrayList ( list );
			Int64 lo, hi, nextlo, nexthi;
			list.Clear ( );
			Boolean more;
			int k;
			k = 0;
			more = hold.Count > 1;
			lo = ( Int64 ) hold[0];
			hi = ( Int64 ) hold[1];
			k += 2;
			while ( more ) {
				if ( k >= hold.Count ) {
					list.Add ( lo );
					list.Add ( hi );
					break;
				}
				nextlo = ( Int64 ) hold[k++];
				nexthi = ( Int64 ) hold[k++];
				if ( hi + 1 == nextlo ) {
					hi = nexthi;
				} else {
					list.Add ( lo );
					list.Add ( hi );
					lo = nextlo;
					hi = nexthi;
				}
			}

			//
			// and merge adjacent ranges.

			//
			// Brute force sort, optimize later after it works
			//Int64 a, b;
			//int i, j;
			//int nrab = list.Count;
			//if (nrab < 2)
			//    return;
			//for(i = 0; i<nrab-1; i++){
			//    a = (Int64) list[i];
			//    for (j = i + 1; j < nrab; j++) {
			//        b = (Int64)list[j];
			//        if (a > b) {
			//            list[i] = b;
			//            list[j] = a;
			//            a = b;
			//        }
			//    }
			//}
		}
		private void checklist ( ArrayList list ) {
			Int64 a, b;
			int k = 0;
			int nrab = list.Count;
			if ( nrab < 2 )
				return;
			a = ( Int64 ) list[0];
			for ( k=1; k<nrab; k++ ) {
				b = ( Int64 ) list[k];
				if ( a > b ) {
					StringBuilder sb = new StringBuilder ( );
					sb.AppendFormat ( "checklist fail at k = {0}", k );
					throw ( new Exception ( sb.ToString ( ) ) );
				}
				a = b;
			}
			return;
		}
		private RangeOrder disjoint ( RangeOrder rel ) {
			if ( rel == RangeOrder.Undefined )
				return RangeOrder.Undefined;
			if ( rel == RangeOrder.Greaterthan ||
				rel == RangeOrder.Lessthan ) {
				return RangeOrder.Yes;
			} else {
				return RangeOrder.No;
			}
		}
		private RangeOrder compare ( Int64 a, Int64 b, Int64 ap, Int64 bp ) {
			if ( b < ap - 1 ) {
				return RangeOrder.Lessthan;
			}
			if ( bp < a - 1 ) {
				return RangeOrder.Greaterthan;
			}
			if ( a == ap && b == bp ) {
				return RangeOrder.Contains; // Explicitly favor ab over (ab)p
			}
			if ( a <= ap && b >= bp ) {
				return RangeOrder.Contains;
			}
			if ( ap <= a && bp >= b ) {
				return RangeOrder.Inside;
			}
			if ( b + 1 == ap ) {
				return RangeOrder.Leftadjacent;
			}
			if ( bp + 1 == a ) {
				return RangeOrder.Rightadjacent;
			}
			return RangeOrder.Intersect;

		}
		public void merge ( ArrayList newlist ) {
			if ( newlist.Count == 0 )
				return;

			try {
				sortlist ( newlist );
				checklist ( newlist );
			} catch ( Exception ee ) {
				throw ee;
			}
			//
			// Pop ranges from the new list and the old list
			// make a provisional range
			// write provisional range when current poped old and
			// popped new are disjoint from provisional range
			//
			Int64 olda, oldb;
			Int64 newa, newb;
			Int64 pa, pb; // This is the provisional stuff
			bool haveold, havenew;
			bool moreold, morenew;
			bool writepab, freepab;
			new_lows.Clear ( );
			new_highs.Clear ( );

			int nrnew = newlist.Count;	// this is by pairs
			int nrold = lows.Count;		// same as highs.Count (hope)
			int iold = 0;				// remember to bump this by 1
			int inew = 0;				// remember to bump this by 2
			int rejected = 0;
			haveold = false;		// use defaul values if list is exhausted
			havenew = false;
			olda = 0;
			oldb = 0;
			newa = 0;
			newb = 0;
			pa = 0;
			pb = 0;
			writepab = false;
			freepab = true;
			while ( true ) {				// we break out of this
				moreold = ( iold < nrold );
				morenew = ( inew < nrnew );
				if ( !haveold && moreold ) {
					olda = ( Int64 ) lows.GetKey ( iold );
					oldb = ( Int64 ) highs.GetKey ( iold );
					iold++;
					haveold = true;
				}
				if ( !havenew && morenew ) {
					newa = ( Int64 ) newlist[inew++];
					newb = ( Int64 ) newlist[inew++];
					havenew = true;
				}

				// update provisional
				// if old < new and provisional is undefined
				// then provisional = old.
				// if new < old and provisional is undefined
				// then provisional = new
				// etc..
				if ( freepab ) {
					RangeOrder ord;
					ord = compare ( olda, oldb, newa, newb );
					if ( havenew && !haveold ) {
						pa = newa;
						pb = newb;
						freepab = false;
						havenew = false;
					} else if ( !havenew && haveold ) {
						pa = olda;
						pb = oldb;
						freepab = false;
						haveold = false;
					} else if ( !havenew && !haveold ) {
						// Nothing to do. keep it free
					} else if ( havenew && haveold ) {
						switch ( ord ) {
							case RangeOrder.Lessthan:
								pa = olda;
								pb = oldb;
								haveold = false;
								break;
							case RangeOrder.Greaterthan:
								pa = newa;
								pb = newb;
								havenew = false;
								break;
							case RangeOrder.Inside:
								// old is inside new, old may be discarded
								pa = newa;
								pb = newb;
								havenew = false;
								break;
							case RangeOrder.Contains:
								// new is inside ol, new may be discarded
								pa = olda;
								pb = oldb;
								haveold = false;
								break;
							case RangeOrder.Leftadjacent:
								// may be cobined, both are discarder
								pa = olda;
								pb = newb;
								havenew = false;
								haveold = false;
								break;
							case RangeOrder.Rightadjacent:
								// may be combined, both are discarded
								pa = newa;
								pb = oldb;
								havenew = true;
								haveold = true;
								break;
							case RangeOrder.Intersect:
								pa = Math.Min ( newa, olda );
								pb = Math.Max ( newb, oldb );
								havenew = true;
								haveold = true;
								break;
							default:
								break;
						} // switch
						freepab = false;
					} // else havenew and haveold 
				} // if freepa

				// have defined provisional at this point
				// examine provisional in terms of old and new
				// several things are possible
				// old may disappear, new may disappear
				// provisional may get written if:
				// (1) both old and new are greater than provisional
				// (2) there is no more new and old > provisional
				// (3) there is no more old and new > provisional
				// (4) there is mo more old and no more new

				if ( freepab ) {
					break;
				}
				RangeOrder pRelnew, pRelold;
				pRelnew = RangeOrder.Undefined;
				pRelold = RangeOrder.Undefined;
				if ( haveold )
					pRelold = compare ( pa, pb, olda, oldb );
				if ( havenew )
					pRelnew = compare ( pa, pb, newa, newb );
				writepab = false;
				if ( havenew && haveold && pRelold == RangeOrder.Lessthan && pRelnew == RangeOrder.Lessthan ) {
					writepab = true;
				} else if ( havenew && !haveold && pRelnew == RangeOrder.Lessthan ) {
					writepab = true;
				} else if ( !havenew && haveold && pRelold == RangeOrder.Lessthan ) {
					writepab = true;
				} else if ( !havenew && !haveold ) {
					writepab = true;
				}
				if ( writepab ) {
					if ( !new_highs.Contains ( pb ) && !new_lows.Contains ( pa ) ) {
						new_highs.Add ( pb, null );
						new_lows.Add ( pa, null );
					} else {
						rejected++;
					}
					writepab = false;
					freepab = true;
				} else { // nothing was written, merge something into pa, pb
					if ( disjoint ( pRelnew ) == RangeOrder.No ) {
						havenew = false;
						pa = Math.Min ( pa, newa );
						pb = Math.Max ( pb, newb );
					} else if ( disjoint ( pRelold ) == RangeOrder.No ) {
						haveold = false;
						pa = Math.Min ( pa, olda );
						pb = Math.Max ( pb, oldb );
					}
				}
				// if provosional is defined
				//		if old, provisional overlap, update provisional
				//		if new, provisinal overlap, update provisional
				//  At this point always have privisional, new and old ranges
				//		defined (unless one of the lists is exhausted)
			} // While true
			if ( rejected > 0 ) {
				rejected = 0; // debug stop here
			}
			tmp = lows;
			lows = new_lows;
			new_lows = tmp;

			tmp = highs;
			highs = new_highs;
			new_highs = tmp;

		}


	

		/// In general, we have to consider, whether or not the lo-hi
		/// range is already in the saved range, but for the HTM intersect
		/// algorithhm, we don't have to. Why? Because ranges generfated by saveTrixel
		/// are guaranteed to be disjoint. Theay are also self-sorting by the nature of the
		/// algorithm, so it may be, that soon I will replace sortedlists with
		/// something faster.
		/// Not so fast, says George. What about domain? True, each convex intersect
		/// does produce inherently ordered htmid ranges, but domains are unions
		/// of independent convexes, and they can no be sorted.
		/// There is no partial ordering on convexes.
		/// The paper on convex/htm theory has several fundamental theorems, one
		/// of which is, that the convex.intersect algorithm generates htmids
		/// that are ordered. A total order on htmids can be defined easily.
		/// Define the less than relation so that C1 less than C2 <--> for each h1 in IDSET(C1)
		/// and h2 in IDSET(C2) h1 less than h2.
		/// Define less than on H so that for h1, h2 in H, h1 less than h2, <--> all decendants
		/// of hd1 of h1 and all decendants hd2 of h2 that are at the same level
		/// hd1 less than hd2. Lemma: less than is not a partial order on {C}. Theorem less than is a partial
		/// order (nay, total order) on Intersect{C_i} for any collection
		/// {C_i}. But I ramble, this should be written up neatly.

		/// <summary>
		/// Add an interval specified by the start (lo)
		/// and end (hi) value.
	    /// </summary>
		/// <param name="lo">start of interval</param>
		/// <param name="hi">end of interval</param>
		public void addRange ( Int64 lo, Int64 hi ) {
			lows.Add ( lo, null );
			highs.Add ( hi, null );
			return;
		}

		/// <summary>
		/// Clear everything
		/// Toss everything, reset to empty. Get ready for reuse
		/// </summary>
		public void Clear ( ) {
			lows.Clear ( );
			highs.Clear ( );
		}

		/// <summary>
		/// Compact this Range if possible.
		/// Merge ranges that are "adjacent" for example,
		/// (u, v) (v+1, w) can be safely replaced by (u, w)
		/// in other words, we punt the high value (v) and the low
		/// value (v+1)
		/// </summary>
		public void compact ( ) {

			// Since Kelist is a read-only view of the key list,
			// it  is changed as soon
			// as the lows and highs change, 
			// count must be adjusted 
			// with every deletion


			int count;
			count = lows.Count;
			IList larr = lows.GetKeyList ( );
			IList harr = highs.GetKeyList ( );
			count--; // because we compar i to t+1
			for ( int i=0; i<count; ) { // third part purposely empty
				if ( ( Int64 ) harr[i] + 1L == ( Int64 ) larr[i+1] ) {
					// remove the high at i, 
					// and remove the low at i+1
					highs.RemoveAt ( i );
					lows.RemoveAt ( i+1 );
					count--;
				} else {
					i++;
				}
			}
			return;
		}
		// READONLY
		public const Int64 Superbig = ( 1L << 60 );
		public int Count {
			get {
				return lows.Count;
			}
		}
	}
}
