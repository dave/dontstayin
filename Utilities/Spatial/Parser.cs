using System;
/*=====================================================================

  File:      Parser.cs for Spatial Sample
  Summary:   Implements a parser which converts text to various kinds of regions.
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
/// This is the object, that parses geometries,
/// and builds region formal form.t
/// 
/// Knows about the text specification
/// Converts text to the specific object,
/// One of Circle, Rectangle, Polygon, Convex hull, Region, Convex, Halfspace
/// CRICLE, RECT, POLY, CHULL, REGION
///
/// Usage:	Parser p = new Parser(); p.set("CONVEX 1 2 3 4");
///			Parser p = new Parser("CONVEX 1 2 3");
///			 
namespace Microsoft.Samples.SqlServer
{
	/// <summary>
	/// Summary description for Parser.
	/// </summary>
	public class Parser {
		private enum Format {
			J2000,
			Cartesian,
			Latlon,
			Null,
			Unkown
		}
		public enum Geometry {
			Region,
			Convex,
			Halfspace, /* n/a */
			Rect,
			Circle,
			Poly,
			Chull,
			Null
		}
		private enum Error {
			Ok,
			errLevel,
			errIllegalNumber,
			errFormat,
			errEol,
			errNot2forRect,
			errNotenough4Poly,
			errMissingConvex,
			errPolyBowtie,
			errPolyZeroLength,
			errPolyToomanyPoints,
			errBadedgePoly,
			errChullTooBig,
			errNullSpecification,
			errUnknown
		}
		private const int INITIAL_CAPACITY = 102;
		private int _current_capacity = INITIAL_CAPACITY - 2;
		private double[] xs, ys, zs, ras, decs;
		private string _spec;
		private char[] _delims;
		private string[] _tokens;
		private int _ct;
		private Error _error;
		private Format _format;
		private Geometry _geometry; // The kind of object we are building
		private Region _targetregion;		// Everything is a region, it will be built to here....
		private void init(){
			_delims = " \t\n\r".ToCharArray();
			xs  = new double[INITIAL_CAPACITY]; // Should be made to grow eventually
			ys  = new double[INITIAL_CAPACITY];
			zs  = new double[INITIAL_CAPACITY];
			ras = new double[INITIAL_CAPACITY];
			decs = new double[INITIAL_CAPACITY];
		}
		private string __prematureeol = "Not enough arguments for this specification";
		public string errmsg(){
			switch(_error){
				case Error.Ok: return "Ok" ;
				case Error.errLevel: return "Level specification error";
				case Error.errIllegalNumber: return "Illegal number format"; 
				case Error.errFormat: return "Format must be CARTESIAN, J2000 or LATLON"; 
				case Error.errUnknown: return "Unrecognized area name, or syntax error";
				case Error.errEol: return this.__prematureeol;
				case Error.errNot2forRect: return "Not 2 points given (exactly 2 needed) for a rectangle)";
				case Error.errMissingConvex: return "Missing a keyword: 'CONVEX'";
				case Error.errPolyBowtie: return "Polygon has a bowtie or is concave";
				case Error.errPolyToomanyPoints: return "Too many points in polygon";
				case Error.errPolyZeroLength: return "Polygon had zero length edges";
				case Error.errNotenough4Poly: return "Need 3 or more points for a polygon";
				case Error.errChullTooBig: return "Points for convex hull cover more than half the globe";
				case Error.errNullSpecification: return "Empty string is an incorrect specification";
			}
			return "Unknown error (3). Call George";
		}
		public Parser() {
			//
			// TODO: Add constructor logic here
			//
			init();
			input = null;
		}
		public void buildto(Region o){
			_targetregion = o;
		}
		/* *******************process tokens******************************/
		private void advance(){
			_ct++;
		}
		public  bool isok {
			get {
				return _error == Error.Ok;
			}
		}
		/// <summary>
		/// True, if there are any tokens left to parse. ismore() skips over white spaces
		/// </summary>
		protected bool ismore {
			get {
				if (_tokens == null)
					return false;
				if (_tokens[0] == null)
					return false;
				while( _ct < _tokens.Length && _tokens[_ct].Length == 0){
					_ct++;
				}
				return _ct < _tokens.Length;
			}
		}
		private int getint(int skip){
			int res;
			if (!ismore){
				_error = Error.errEol;
				throw new Exception(__prematureeol);
			}
			res = int.Parse(_tokens[_ct].Substring(skip));
			advance();
			return res;
		}
		private int getint() {
			int res;
			if (!ismore){
				_error = Error.errEol;
				throw new Exception(__prematureeol);
			}
			res = int.Parse(_tokens[_ct]);
			advance();
			return res;
		}
		private double getdouble() {
			double res;
			if (!ismore){
				_error = Error.errEol;
				throw new Exception(__prematureeol);
			}
			res = double.Parse(_tokens[_ct]);
			advance();
			return res;
		}
		private bool match_current(string pattern, bool casesensitive){
			if (!ismore){
				return false;
			}
			if (casesensitive){
				return (_tokens[_ct].Equals(pattern));
			} else {
				return (_tokens[_ct].ToLower().Equals(pattern));
			}
		}
		private bool isprefix(string prefix){
			if (!ismore)
				return false;
			return _tokens[_ct].Substring(0, prefix.Length).Equals(prefix);
		}
		public string input {
			get {
				return _spec;
			}
			set {
				_error = Error.Ok;
				_format = Format.Unkown;
				_geometry = Geometry.Null;
				_ct = 0;
				_spec = value;
				_targetregion = null;
				if (_spec == null){
					_tokens = null;
				} else if (_spec.Length > 0){
					_tokens = _spec.Split(_delims);

				} else {
					_tokens = null;
				}		
			}
		}
		private void rewind(){
			_ct = 0;
		}
		public Parser(char[] in_spec) {
			//
			// Break spec into words (tokens)
			// First word must be recognized as one of the keys
			// Case insensitive
			init();
			input = new string(in_spec);
		}
		public Parser(string in_spec){
			init();
			input = in_spec;
		}
		
		/*************************************                   *************************/
		/*************************************                   *************************/
		/************************************* CONVEX            *************************/
		/************************************* REGION            *************************/
		/*************************************                   *************************/
		/************************************* RECTANGLE         *************************/
		/*************************************                   *************************/
		/************************************* CIRCLE            *************************/
		/*************************************                   *************************/
		/************************************* POLYGON           *************************/
		/*************************************                   *************************/
		/************************************* CHULL             *************************/
		/************************************* convex hull       *************************/
		private Format peekFormat() {
			// Peek into the string, and return the type of geometric object
			// the parser will build.

			if (_tokens == null)
				return Format.Null;
			if (match_current("cartesian", false)) {
				return Format.Cartesian;
			} else if (match_current("j2000", false)) {
				return Format.J2000;
			} else if (match_current("latlon", false)) {
				return Format.Latlon;
			} else {
				return Format.Null;
			}
			// return Format.Null;
		}
		public Geometry peekGeometry() {
			// Peek into the string, and return the type of geometric object
			// the parser will build.
			if (_tokens == null)
				return Geometry.Null;
			if (match_current("convex", false))
				return Geometry.Convex;
			if (match_current("region", false))
				return Geometry.Region;
			if (match_current("halfspace", false))
				return Geometry.Halfspace;
			if (match_current("poly", false))
				return Geometry.Poly;
			if (match_current("chull", false))
				return Geometry.Chull;
			if (match_current("rect", false))
				return Geometry.Rect;
			if (match_current("circle", false))
				return Geometry.Circle;
			return Geometry.Null;
		}
		public bool parse() {
			// smarter parser, allows mixed geometries and object
			// Skip the first 'REGION', else assume that
			bool multiple = false; // parse a region, use multiple, else just one object

			if (!ismore) {
				_error = Error.errNullSpecification;
				return isok;
			}
			_geometry = peekGeometry();
			if (_geometry == Geometry.Region) {
				advance();
				multiple = true;
			}
			while (ismore) {			// We loop on each CONVEX, whatever it may be...
				_geometry = peekGeometry();
				switch (_geometry) {
					case Geometry.Circle:
						if (parse_circle() == false) {
							return isok; // Adds circle convex to _targetreion
						}
						break;
					case Geometry.Convex:
						if (parse_convex() == false) {
							return isok;
						}
						break;
					case Geometry.Rect:
						if (parse_rectangle() == false) {
							return isok;
						}
						break;
					case Geometry.Poly:
						if (parse_polygon() == false) {
							return isok;
						}
						break;
					case Geometry.Chull:
						if (parse_chull() == false) {
							return isok;
						}
						break;
					case Geometry.Null:
						_error = Error.errUnknown;
						return isok;
						// break;
				}
				if (!multiple)
					break;
			}
			return true;
		}
		/************************************* ***************** *************************/
		/************************************* SPECIALTY PARSERS *************************/
		/************************************* ***************** *************************/
		#region parse_RECTANGLE
		/* ******************************************** RECT ***/
		private bool parse_rectangle() {

			Region region = _targetregion;
			Rectangle rect = null;
			int npoints = 0;
			double ra, dec, x, y, z;

			if (region == null)
				return true;
			advance(); // Skip over 'RECT'
			_format = peekFormat();
			if (ismore && _format != Format.Null) {
				advance();
			}
			while (ismore) {
				switch (_format) {
					case Format.Null:
					case Format.Cartesian:
						try {
							x = this.getdouble();
							y = this.getdouble();
							z = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						xs[npoints] = x;
						ys[npoints] = y;
						zs[npoints] = z;
						SpatialVector.xyz2radec(x, y, z, out ra, out dec);
						ras[npoints] = ra;
						decs[npoints] = dec;
						npoints++;
						break;
					case Format.J2000:
					case Format.Latlon:
						try {
							ra = this.getdouble();
							dec = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						ras[npoints] = ra;
						decs[npoints] = dec;
						npoints++;
						break;
					default:
						break;
				}
				if (npoints >= 2) {
					break;
				}
			}
			if (npoints != 2) {
				_error = Error.errNot2forRect;
				return isok;
			}
			rect = new Rectangle(region);
			if (_format == Format.Latlon) {
				rect.make(decs[0], ras[0], decs[1], ras[1]);
			} else {
				rect.make(ras[0], decs[0], ras[1], decs[1]);
			}
			return true;
		}
		/************************** END RECT *******************************/
		#endregion
		#region parse_POLYGON
		/* ******************************************** POLY ***/
		private bool parse_polygon() {

			Region region = _targetregion;
			Polygon poly = null;
			int npoints = 0;
			double ra, dec, x, y, z;

			if (region == null)
				return true;
			advance(); // Skip over 'RECT'
			_format = peekFormat();
			if (ismore && _format != Format.Null) {
				advance();
			}
			while (ismore) {
				if (npoints >= this._current_capacity) {
					_error = Error.errPolyToomanyPoints;
					return isok;
				}
				switch (_format) {
					case Format.Null:
					case Format.Cartesian:
						try {
							x = this.getdouble();
							y = this.getdouble();
							z = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						xs[npoints] = x;
						ys[npoints] = y;
						zs[npoints] = z;
						npoints++;
						break;
					case Format.J2000:
					case Format.Latlon:
						try {
							ra = this.getdouble();
							dec = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						if (_format == Format.Latlon) {
							SpatialVector.radec2cartesian(dec, ra, out x, out y, out z);
						} else {
							SpatialVector.radec2cartesian(ra, dec, out x, out y, out z);
						}
						xs[npoints] = x;
						ys[npoints] = y;
						zs[npoints] = z;
						npoints++;
						break;
					default:
						break;
				}
			}
			if (npoints < 3) {
				_error = Error.errNot2forRect;
				return isok;
			}
			poly = new Polygon(region);
			// WARNING!!! more than one kinf of error. The other one is for
			// degenerate edges, two points are too close... or you can eliminate them!
			Polygon.Error err = poly.add(xs, ys, zs, npoints);
			if (err == Polygon.Error.errBowtieOrConcave) {
				_error = Error.errPolyBowtie;
				return isok;
			} else if (err == Polygon.Error.errZeroLength){
				_error = Error.errPolyZeroLength;
				return isok;
			}
			return true;
		}
		/************************** END RECT *******************************/
		#endregion
		#region parse_CHULL
		/* ******************************************** CHULL ***/
		private bool parse_chull() {

			Region region = _targetregion;
			Chull plate = null;
			int npoints = 0;
			double ra, dec, x, y, z;

			if (region == null)
				return true;
			advance(); // Skip over 'RECT'
			_format = peekFormat();
			if (ismore && _format != Format.Null) {
				advance();
			}
			while (ismore) {
				if (npoints >= this._current_capacity) {
					_error = Error.errPolyToomanyPoints;
					return isok;
				}
				switch (_format) {
					case Format.Null:
					case Format.Cartesian:
						try {
							x = this.getdouble();
							y = this.getdouble();
							z = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						xs[npoints] = x;
						ys[npoints] = y;
						zs[npoints] = z;
						npoints++;
						break;
					case Format.J2000:
					case Format.Latlon:
						try {
							ra = this.getdouble();
							dec = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						if (_format == Format.Latlon) {
							SpatialVector.radec2cartesian(dec, ra, out x, out y, out z);
						} else {
							SpatialVector.radec2cartesian(ra, dec, out x, out y, out z);
						}
						xs[npoints] = x;
						ys[npoints] = y;
						zs[npoints] = z;
						npoints++;
						break;
					default:
						break;
				}
			}
			if (npoints < 3) {
				_error = Error.errNot2forRect;
				return isok;
			}
			plate = new Chull(region);
			if (plate.addConvex(xs, ys, zs, npoints) == false) {
				_error = Error.errChullTooBig;
				return isok;
			}
			return true;
		}
		/************************** END CHULL *******************************/
		#endregion
		#region parse_circle
		/* ******************************************** CIRCLE ***/
		private bool parse_circle() {
	
			Region region = _targetregion;
			Circle circle = null;
			double ra, dec, x, y, z, rad;

			if (region == null)
				return true;
			advance(); // Skip over 'CIRCLE'
			_format = peekFormat();
			if (ismore && _format != Format.Null) {
				advance();
			}
			if (ismore) {
				switch (_format){
					case Format.Null:
					case Format.Cartesian:
						try {
							x = this.getdouble();
							y = this.getdouble();
							z = this.getdouble();
							rad = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						circle = new Circle(region);
						circle.make(x, y, z, rad);
						break;
					case Format.J2000:
					case Format.Latlon:
						try {
							ra = this.getdouble();
							dec = this.getdouble();
							rad = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						circle = new Circle(region);
						if (_format == Format.J2000) {
							circle.make(ra, dec, rad);
						} else {
							circle.make(dec, ra, rad);
						}
						break;
					default:
						break;
				}
			}
			return true;
		}
		/************************** END CIRCLE *******************************/
		#endregion
		#region parse_convex
		// ////////////////////////////// parse CONVEX
		private bool parse_convex() {

			Region region = _targetregion;
			Convex convex = null;
			Geometry nextitem;
			double ra, dec, x, y, z, D;

			if (region == null)
				return true;
			advance(); // Skip over 'CONVEX'
			_format = peekFormat();
			if (ismore && _format != Format.Null) {
				advance();
			}
			convex = new Convex();
			region.add(convex);
			while (ismore) {
				nextitem = peekGeometry();
				if (nextitem != Geometry.Null)
					break;
				switch (_format) {
					case Format.Null:
					case Format.Cartesian:
						try {
							x = this.getdouble();
							y = this.getdouble();
							z = this.getdouble();
							D = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						convex.add(x, y, z, D);
						break;
					case Format.J2000:
					case Format.Latlon:
						try {
							ra = this.getdouble();
							dec = this.getdouble();
							D = this.getdouble();
						} catch {
							if (_error != Error.errEol)
								_error = Error.errIllegalNumber;
							return isok;
						}
						if (_format == Format.J2000) {
							SpatialVector.radec2cartesian(ra, dec, out x, out y, out z);
						} else {
							SpatialVector.radec2cartesian(dec, ra, out x, out y, out z);
						}
						convex.add(x, y, z, D);
						break;
					default:
						break;
				}
			}
			return true;
		}
		#endregion
		
	}

}
