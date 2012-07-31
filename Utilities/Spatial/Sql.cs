using System;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server; //Use with MSSQLS2005April or later

/*=====================================================================

  File:      Sql.cs for Spatial Sample
  Summary:   Implements the CLR integration interface to the HTM procedures.
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
	///  ------------------------------------------------------
	/// <h1>
	/// Sql Class     File: Sql.cs
	/// </h1>
	/// <br><strong>Sql</strong> Encapsulates the HTM procedures for use from SQL2005
	/// <br>Assemblies built using these routines access HTM functions.
	/// <br>All HTMs are done 20-deep for the SQL interface, starting is level 0.
	/// <br> fHtmVersion()					returns varchar(max) version string of the DLL.
	/// <br> fHtmLatLon(lat, lon)			returns HtmID    of Lat,Lon location (point)
	/// <br> fHtmEq(ra,dec)					returns HtmID    of equatorial point
	/// <br> fHtmXyz(x,y,z)					returns HtmID    of cartesian point
	/// <br> fHtmLatLonToXyz			    returns xyzTable of Lat,Lon location (point)
	///	<br> fHtmEqToXyz			        returns xyzTable of equatorial point
	///	<br> fHtmXyzToLatLon			    returns LatLonTable of cartesian point
	///	<br> fHtmXyzToEq                    returns RaDecTable of cartesian point
	/// <br> fHtmToString(HtmID)			returns varchar(max) string description of HtmID
	/// <br> fHtmToCenterPoint(HTM)			returns xyzTable 
	/// <br> fHtmToCornerPoints(HTM)		returns xyzTable
	/// <br> fHtmCoverCircleLatLon(lat, lon, R)	returns HtmTable of htm triangles covering circle</br>
	/// <br> fHtmCoverCircleEq(ra,dec,R)	returns HtmTable of htm triangles covering circle
	/// <br> fHtmCoverCircleXyz(x,y,z,R)	returns HtmTable of htm triangles covering circle
	/// <br> fHtmCoverRegion(regionSpec)	returns HtmTable of htm triangles covering area or region
	/// <br> fHtmRegionToNormalFormString(regionSpec)	returns normal form of region string.  
	/// <br> fHtmRegionToTable(regionSpec)	returns table of convexes and halfspaces of region string.  
	/// <br> fHtmRegionError(regionSpec)    returns errorMessage for region string. 
	///          
	///  <br> Version: 1.0,  June 2005
	///
	///  <br> Authors: Jim Gray, Microsoft, Gyorgy Fekete, JHU
	///  <br>          Gray@Microsoft.com,  gyuri@pha.jhu.edu 
	///  <br>
	///		  Change history: 
	///			10 May 2005  Jim: renamed fHtmToName      to fHtmToString
	///									  fHtmToPoint     to fHtmToCenterPoint
	///                                   fHtmtoVerticies to fHtmToCornerPoints
	///						      dropped fHtmToPointError
	///					                  fHtmToVerticesError
	///							  added   fDistanceEq
	///						              fDistanceXyz
	///			17 May 2005 GYF: added    fDistanceLatLon
	///									  fHtmLatLon
	///									  fHtmCoverCircleLatLon
	///			22 May 2005 Jim: replaced fHtmCoverError and fHtmToNormalFormError with fHtmRegionError
	///							 fixed comments on region syntax.
	///							 reprderd routines to match comments and "logical order"
	/// 		May 31 2005 GYF  added	  fHtmLatLonToXyz,
	///									  fHtmEqToXyz,
	///									  fHtmXyzToLatLon
	///									  fHtmXyzToEq
	///         June 2 2005 Jim    fixed comments in header to include new functions.
	///                            added intellisense comments for fHtmLatLonToXyz,fHtmEqToXyz,fHtmXyzToLatLon, fHtmXyzToEq,
	///			June 18 2005 Jim   fixed fHtm...Xyz to cast cartestian 0 0 0 to 0 0 1. 
	///								modified commments and fRegionError to show new syntax for Regions. 
	///			Jun 21  2005 GYF	added fHtmCoverToHalfspaces
	///			Jun 28  2005 GYF	merged Jim's Sql.cs renamed above to fHtmRregionStringToTable
	///			Jun 29  2005 Jim    more name changes and comments.
	///								fixed fHtmRegionToTable to return convexes and halfspaces numbered from zero. 
	///			July  4	2005 GYF	Added fHtmIDToSquareArcminutes
	///			July 15 2005 GYF	Added interface to "magic numbers"
	///  </br>
	/// </summary>
	/// -----------------------------------------------------
	/// <remarks>
	///   
	/// </remarks>
	///
	public partial class Sql
	{
		#region Globals
		/// <summary>
		/// Global constants:  all HTMs for SQL are done with 20-deep HTMs
		/// if a cartesian vector length is less than 1e-9 set it to 1,0,0
		/// </summary>
		//DSI likes it 23
		const int level = 20;			// 20-deep htm is our standard.
		const long minHtmID = 8;			// smallest HTM ID (face zero, trixel zero)
		const double epsilon = 1e-9;		// one billionth is small.
		#endregion
		#region Pair
		/// <summary>
		/// Pair is a pair of HtmID pair.  
		/// <br>It defines the format of IEnumerable list elements returned by the htm cover functions. 
		/// <br>These lists are recast as tables of the form T(HTM_start bigint, HTM_end bigint)
		/// </summary>
		class Pair : Object
		{
			public SqlInt64 lo, hi;
			public Pair(SqlInt64 lo, SqlInt64 hi) { this.lo = lo; this.hi = hi; }
		}
		#endregion
		#region DPair
		/// <summary>
		/// DPair is a pair of doubles
		/// <br>It defines the format of IEnumerable list elements returned by the ..ToLatLon functions. 
		/// <br>These lists are recast as tables of the form T(ra float, dec float) as well as lat and lon
		/// </summary>
		class DPair : Object
		{
			public SqlDouble d1, d2;
			public DPair(SqlDouble in1, SqlDouble in2) { this.d1 = in1; this.d2 = in2; }
		}
		#endregion
		#region Triple
		/// <summary>
		/// Triple is a is an X, Y, Z vector in Cartesian space.  
		/// <br>It defines the format of IEnumerable list elements returned by the htmToVerticies function. 
		/// <br>These lists are recast a a table  of the form T(x float, y float, z float)
		/// </summary>
		class Triple : Object
		{
			public SqlDouble x, y, z;
			public Triple(SqlDouble x, SqlDouble y, SqlDouble z)
			{
				this.x = x;
				this.y = y;
				this.z = z;
			}
		}
		#endregion
		#region HSEntry
		/// <summary>
		/// HSentry is a halfspace entry in a row: convexID halfspaceID x y z D 
		/// </summary>
		class HSEntry : Object
		{
			public SqlDouble x, y, z, D;
			public SqlInt64 cid, hid;
			public HSEntry(int cid, int hid, double x, double y, double z, double D)
			{
				this.cid = (SqlInt16)cid;
				this.hid = (SqlInt16)hid;
				this.x = (SqlDouble)x;
				this.y = (SqlDouble)y;
				this.z = (SqlDouble)z;
				this.D = (SqlDouble)D;
			}
		}

		#endregion
		#region fHtmVersion
		/// <summary>
		/// fHtmVersion() returns the version number of this htm library as a string
		/// </summary>
		/// <returns> sqlString: a string telling the version number.  </returns>
		/// <br> Typically: "C# HTM.DLL V.1.0.0 30 July 2005 "
		/// <example><code>
		///         create function fHtmVersion() returns nvarchar(max)
		///                 as external name HTM.Sql.fHtmVersion
		///         declare @version nvarchar(max)
		///			select @version = dbo.fHtmVersion()
		///  </code></example>  
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlString fHtmVersion()
		{
			return new SqlString(HtmState.Instance.getVersion());
		}
		#endregion
		#region fHtmXyz
		/// <summary>
		/// fHtmXyz(x,y,z) returns the 20-deep HtmID of the given cartesian point.
		/// <br> There are no error cases. All vectors are nomalized and 0,0,0 maps to 1,0,0</br>
		/// </summary>
		/// <param name="x"> double, x Cartesian coordinate</param>
		/// <param name="y"> double, y Cartesian coordinate</param>
		/// <param name="z"> double, z Cartesian coordinate</param>
		/// <returns> HtmID: Int64 20-deep HTM id of the point  </returns>
		/// <example><code>
		///         create function fHtmXyz(@x float, @y float, @z float) 
		///					returns bigint 
		///                 as external name HTM.Sql.fHtmXyz
		///         declare @HtmID bigint
		///			select @HtmID = dbo.fHtmXyz(1,0,0)
		///  </code></example> 
		/// <seealso cref="fHtmEq()">  uses eqatorial. </seealso>
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static long fHtmXyz(double x, double y, double z)
		{	// avoid zero vectors by setting them to 1,0,0.
			if (Cartesian.Epsilon2 > (x * x + y * y + z * z)) { x = 0; y = 0; z = 1; }
			HtmTrixel ht = new HtmTrixel();
			return ht.cartesian2HID(x, y, z, level);
		}
		#endregion
		#region fHtmEq
		/// <summary>
		/// fHtmEq(ra,dec) returns the 20-deep HtmID of the given equatorial point.
		/// <br> There are no error cases. all RA, folded to [0..360] and dec to [0...90]</br>
		/// </summary>	
		/// <param name="ra"> double, right ascencion coordinate (degrees)</param>
		/// <param name="dec"> double, declination coordinate (degrees)</param>
		/// <returns> HtmID: Int64 20-deep HTM id of the point  </returns> 
		/// <example><code>
		///         create function fHtmEq(@ra float, @dec float ) 
		///					returns bigint 
		///                 as external name HTM.Sql.fHtmEq
		///         declare @HtmID bigint
		///			select @HtmID = dbo.fHtmEq(195,5) -- output defaults to ''
		///  </code></example>  
		/// <seealso cref="fHtmXyz()">  uses cartesian. </seealso>
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static long fHtmEq(double ra, double dec)
		{
			double x, y, z;
			HtmTrixel ht = new HtmTrixel();
			SpatialVector.radec2cartesian(ra, dec, out x, out y, out z);
			return ht.cartesian2HID(x, y, z, level);
		}
		#endregion
		#region fHtmLatLon
		/// <summary>
		/// fHtm(lat,lon) returns the 20-deep HtmID of the given location.
		/// <br> There are no error cases. all RA, folded to [0..360] and dec to [0...90]</br>
		/// </summary>	
		/// <param name="lat"> double, latitude coordinate (degrees)</param>
		/// <param name="lon"> double, longitude coordinate (degrees)</param>
		/// <returns> HtmID: Int64 20-deep HTM id of the point  </returns> 
		/// <example><code>
		///         create function fHtmLatLon(@lat float, @lon float ) 
		///					returns bigint 
		///                 as external name HTM.Sql.fHtmLatLon
		///         declare @HtmID bigint
		///			select @HtmID = dbo.fHtmLatLon(5, 195) -- output defaults to ''
		///  </code></example>  
		/// <seealso cref="fHtmXyz()">  uses cartesian. </seealso>
		/// <seealso cref="fHtmEq()">  uses J2000 (ra/dec). </seealso>
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static long fHtmLatLon(double lat, double lon)
		{
			double x, y, z;
			HtmTrixel ht = new HtmTrixel();
			SpatialVector.radec2cartesian(lon, lat, out x, out y, out z);
			return ht.cartesian2HID(x, y, z, level);
		}
		#endregion
		#region fDistanceEq
		/// <summary>
		/// double fDistanceEq(ra1,dec1,ra2,dec2) 
		///  returns distance in ArcMinutes between two points. 
		/// </summary>	
		/// <param name="ra1">  double, right ascencion coordinate (degrees)</param>
		/// <param name="dec1"> double, declination coordinate (degrees)</param>
		/// <param name="ra2">  double, right ascencion coordinate (degrees)</param>
		/// <param name="dec2"> double, declination coordinate (degrees)</param>
		/// <returns> double distance in arc minutes. 
		/// <example><code>
		///         create function fDistanceEq(@ra1 float, @dec1 float, @ra2 float, @dec2 float) 
		///					returns float
		///						 as external name HTM.Sql.fDistanceEq
		///			select dbo.fDistanceEq(0,0,30,30)
		///  </code></example> 
		/// --------------------------------------------------------------

		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlDouble fDistanceEq(double ra1, double dec1, double ra2, double dec2)
		{
			double d2r = Math.PI / 180;
			double nx1 = Cartesian.Cos(dec1 * d2r) * Cartesian.Cos(ra1 * d2r);
			double ny1 = Cartesian.Cos(dec1 * d2r) * Math.Sin(ra1 * d2r);
			double nz1 = Math.Sin(dec1 * d2r);
			double nx2 = Cartesian.Cos(dec2 * d2r) * Cartesian.Cos(ra2 * d2r);
			double ny2 = Cartesian.Cos(dec2 * d2r) * Math.Sin(ra2 * d2r);
			double nz2 = Math.Sin(dec2 * d2r);
			return (60 * 2 * (1 / d2r) * Math.Asin(
										  Math.Sqrt((nx1 - nx2) * (nx1 - nx2)
												   + (ny1 - ny2) * (ny1 - ny2)
												   + (nz1 - nz2) * (nz1 - nz2)
												   ) / 2
												 )
					);
		}
		#endregion
		#region fDistanceLatLon
		/// <summary>
		/// double fDistanceLatLon(lat1,lon1,lat2,lon2) 
		///  returns distance in ArcMinutes between two points. 
		/// </summary>	
		/// <param name="lat1"> double, latitude coordinate (degrees)</param>
		/// <param name="lon1"> double, longitude coordinate (degrees)</param>
		/// <param name="lat2"> double, latitude coordinate (degrees)</param>
		/// <param name="lon2"> double, longitude coordinate (degrees)</param>
		/// <returns> double distance in arc minutes. 
		/// <example><code>
		///         create function fDistanceLatLon(@lat1 float, @lon1 float, @lat2 float, @lon2 float) 
		///					returns float
		///						 as external name HTM.Sql.fDistanceLatLon
		///			select dbo.fDistanceLatLon(0,0,30,30)
		///  </code></example> 
		/// --------------------------------------------------------------

		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlDouble fDistanceLatLon(double lat1, double lon1, double lat2, double lon2)
		{
			double d2r = Math.PI / 180;
			double nx1 = Cartesian.Cos(lat1 * d2r) * Cartesian.Cos(lon1 * d2r);
			double ny1 = Cartesian.Cos(lat1 * d2r) * Math.Sin(lon1 * d2r);
			double nz1 = Math.Sin(lat1 * d2r);
			double nx2 = Cartesian.Cos(lat2 * d2r) * Cartesian.Cos(lon2 * d2r);
			double ny2 = Cartesian.Cos(lat2 * d2r) * Math.Sin(lon2 * d2r);
			double nz2 = Math.Sin(lat2 * d2r);
			return (60 * 2 * (1 / d2r) * Math.Asin(
										  Math.Sqrt((nx1 - nx2) * (nx1 - nx2)
												   + (ny1 - ny2) * (ny1 - ny2)
												   + (nz1 - nz2) * (nz1 - nz2)
												   ) / 2
												 )
					);
		}
		#endregion
		#region fHtmIDToSquareArcmin
		/// <summary>
		/// double fHtmIDToSquareArcmin(Int64 HTMID) 
		///  returns the area of the trixel with given HTM ID. 
		/// </summary>	
		/// <param name="HTNID"> Int64, the HTMID</param>
		/// <returns> double area in square arc minutes. 
		/// <example><code>
		///         create function fHtmIDToSquareArcmin(HTMID bigint) 
		///					returns float
		///						 as external name HTM.Sql.fHtmIDToSquareArcmin
		///			select fHtmIDToSquareArcmin(8) returns 18,563,832.50
		///  </code></example> 
		/// --------------------------------------------------------------

		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlDouble fHtmIDToSquareArcmin(SqlInt64 hid)
		{
			HtmTrixel htm = new HtmTrixel();

			double t1, t2, t3;
			double area;
			if (htm.getAngles((Int64)hid, out  t1, out  t2, out  t3))
			{
				// Area in stearadians A = R^2[(t1 + t2 + t3 ) - Pi], but R=1.
				area = (t1 + t2 + t3) - Cartesian.Pi;
				area *= Cartesian.RTOD * Cartesian.RTOD; // to get square degrees;
				area *= 3600.0; // square arc minutes
			}
			else
			{
				area = 0.0;
			}
			return (area);
		}
		#endregion
		#region fDistanceXyz
		/// <summary>
		/// double fDistanceXyz(x1,y1,z1,x2,y2,z2) 
		///  returns distance in ArcMinutes between two points. 
		/// </summary>	
		/// <param name="x1"> double, first x Cartesian coordinate</param>
		/// <param name="y1"> double, first y Cartesian coordinate</param>
		/// <param name="z1"> double, first z Cartesian coordinate</param>
		/// <param name="x2"> double, second x Cartesian coordinate</param>
		/// <param name="y2"> double, second y Cartesian coordinate</param>
		/// <param name="z2"> double, second z Cartesian coordinate</param>
		/// <returns> double distance in arc minutes. 
		/// <example><code>
		///         create function fDistanceXyz(@x1 float, @y1 float, @z1 float, 
		///                                      @x2 float, @y2 float, @z2 float) 
		///					returns float
		///						 as external name HTM.Sql.fDistanceXyz
		///			select dbo.fDistanceEq(0,0,0,0,0,1)
		///  </code></example> 
		/// --------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlDouble fDistanceXyz(double x1, double y1, double z1, double x2, double y2, double z2)
		{
			double d2r = Math.PI / 180;
			if (Cartesian.Epsilon2 > (x1 * x1 + y1 * y1 + z1 * z1)) { x1 = 0; y1 = 0; z1 = 1; } // replace zero vector with 0,0,1
			if (Cartesian.Epsilon2 > (x2 * x2 + y2 * y2 + z2 * z2)) { x2 = 0; y2 = 0; z2 = 1; } // replace zero vector with 0,0,1
			return (60 * 2 * (1 / d2r) * Math.Asin(
										  Math.Sqrt((x1 - x2) * (x1 - x2)
												   + (y1 - y2) * (y1 - y2)
												   + (z1 - z2) * (z1 - z2)
												   ) / 2
												 )
					);
		}
		#endregion
		#region fHtmToString
		/// <summary>
		/// fHtmToString(HtmID) returns varchar(max)a string describing the HtmID
		/// <br> There are no error cases. All vectors are nomalized and 0,0,0 maps to 1,0,0</br>
		/// </summary>
		/// <param name="HtmID"> the ID to be translated</param>
		/// <returns> varchar(max)   </returns>
		/// <example><code>
		///         create function fHtmToString(HtmID) 
		///					returns varchar(max) 
		///                 as external name HTM.Sql.fHtmToString
		///         
		///			print dbo.fHtmToString(dbo.fHtmXyz(1,0,0))
		///  </code></example> 
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlString fHtmToString(SqlInt64 HtmID)
		{
			HtmTrixel htm = new HtmTrixel();
			char[] name = new char[HtmTrixel.eMaxNameSize];
			int level = htm.hid2name(name, (long)HtmID);
			if (level < 0)
			{
				return (new SqlString("Invalid HTM Identifier."));
			}
			else
			{
				return (new SqlString(new string(name, 0, level)));
			}
		}
		#endregion
		#region fHtmLatLonToXyz
		/// <summary>
		/// XyzTable(x,y,z) fHtmLatLonToXyz(lat, lon) converts an lat,lon point
		/// to a table with one row containing the cartesian point (x,y,z)  
		/// </summary>	
		/// <param name="lat"> double, latitude coordinate (degrees)</param>
		/// <param name="lon"> double, longitude coordinate (degrees)</param>
		/// <returns> IEnummerable VertexTable(x float, y float, z float) 
		/// <br> with a row contining the (x,y,z) of a lat,lon point.</returns>
		/// <example><code>
		///         create function fHtmLatLonToXyz(lat float, lon float) 
		///					returns VertexTable(x float, y float, z float) 
		///						 as external name HTM.Sql.fHtmLatLonToXyz
		///			select * from  fHtmLatLonToXyz(115, 38)
		///  </code></example> 
		/// <seealso cref="fHtmXyzToLatLon()"> converts xyz to lat,lon. </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true, FillRowMethodName = "FillTriple",
			TableDefinition = "X float, Y float, Z float")]
		public static IEnumerable fHtmLatLonToXyz(SqlDouble lat, SqlDouble lon)
		{
			double x, y, z;
			ArrayList point = new ArrayList();
			SpatialVector.radec2cartesian((double)lon, (double)lat, out x, out y, out z);
			point.Add(new Triple(x, y, z));
			return (IEnumerable)point;
		}
		#endregion
		#region fHtmEqToXyz
		/// <summary>
		/// XyzTable(x,y,z) fHtmEqToXyz(ra, dec) converts an equitorial point
		/// to a table with one row containing the cartesian point (x,y,z)  
		/// </summary>	
		/// <param name="ra"> double, right ascencion coordinate (degrees)</param>
		/// <param name="dec"> double, declination coordinate (degrees)</param>
		/// <returns> IEnummerable VertexTable(x float, y float, z float) 
		/// <br> with a row contining the (x,y,z) of a ra,dec point.</returns>
		/// <example><code>
		///         create function fHtmEqXyz(lat float, lon float) 
		///					returns VertexTable(x float, y float, z float) 
		///						 as external name HTM.Sql.fHtmEqToXyz
		///			select * from  fHtmEqToXyz(115, 38)
		///  </code></example> 
		/// <seealso cref="fHtmXyzToEq()"> converts xyz to ra, dec. </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true, FillRowMethodName = "FillTriple",
			TableDefinition = "X float, Y float, Z float")]
		public static IEnumerable fHtmEqToXyz(SqlDouble ra, SqlDouble dec)
		{
			double x, y, z;
			ArrayList point = new ArrayList();
			SpatialVector.radec2cartesian((double)ra, (double)dec, out x, out y, out z);
			point.Add(new Triple(x, y, z));
			return (IEnumerable)point;
		}
		#endregion
		#region fHtmXyzToLatLon
		/// <summary>
		/// LatLonTable(lat,lon) fHtmXyzToLatLon(x,y,z) converts the cartesian point (x,y,z)  
		/// to a table with one row containing the equivalent lat,lon point  
		/// </summary>	
		/// <param name="x"> double, unit vector cartesian "x" </param>
		/// <param name="y"> double, unit vector cartesian "x" </param>
		/// <param name="z"> double, unit vector cartesian "x" </param>
		/// <returns> IEnummerable LatLonTable(lat float, lon float) 
		/// <br> with a row contining the (lat, lon) of the cartesian xyz point.</returns>
		/// <example><code>
		///         create function fHtmXyzToEq(x float, y float, z float) 
		///					returns LatLonTable(lat float, lon float) 
		///						 as external name HTM.Sql.fHtmXyzToLatLon
		///			select * from fHtmXyzToLatLon(1,0,0)
		///  </code></example> 
		/// <seealso cref="fHtmLatLonToXyz()"> converts lat, lon to xyz.  </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true, FillRowMethodName = "FillDPair",
			TableDefinition = "LAT float, LON float")]
		public static IEnumerable fHtmXyzToLatLon(SqlDouble x, SqlDouble y, SqlDouble z)
		{
			double lat, lon;
			ArrayList point = new ArrayList();
			double norm2, norm;
			norm2 = (double)x * (double)x + (double)y * (double)y + (double)z * (double)z;
			if (norm2 < Cartesian.Epsilon2)
			{
				x = 0; y = 0; z = 1;
			}
			else
			{	// cast zero vector as north pole
				norm = System.Math.Sqrt(norm2);
				x /= norm;
				y /= norm;
				z /= norm;
			}
			SpatialVector.xyz2radec((double)x, (double)y, (double)z, out lon, out lat);
			point.Add(new DPair(lat, lon));
			return (IEnumerable)point;
		}
		#endregion
		#region fHtmXyzToEq
		/// <summary>
		/// EqTable(ra, dec) fHtmXyzToEq(x,y,z) converts the cartesian point (x,y,z)  
		/// to a table with one row containing the equivalent equitorial (ra,dec) point  
		/// </summary>	
		/// <param name="x"> double, unit vector cartesian "x" </param>
		/// <param name="y"> double, unit vector cartesian "x" </param>
		/// <param name="z"> double, unit vector cartesian "x" </param>
		/// <returns> IEnummerable EqTable(ra float, dec float) 
		/// <br> with a row contining the (ra, dec) of the cartesian xyz point.</returns>
		/// <example><code>
		///         create function fHtmXyzToEq(x float, y float, z float) 
		///					returns EqTable(ra float, dec float) 
		///						 as external name HTM.Sql.fHtmXyzToEq
		///			select * from  fHtmXyzToEq(1,0,0)
		///  </code></example> 
		/// <seealso cref="fHtmEqToXyz()"> converts ra, dec to xyz.  </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true, FillRowMethodName = "FillDPair",
			TableDefinition = "RA float, DEC float")]
		public static IEnumerable fHtmXyzToEq(SqlDouble x, SqlDouble y, SqlDouble z)
		{
			double ra, dec;
			ArrayList point = new ArrayList();
			double norm2, norm;
			norm2 = (double)x * (double)x + (double)y * (double)y + (double)z * (double)z;
			if (norm2 < Cartesian.Epsilon2) { x = 0; y = 0; z = 1; } // cast zero vector as north pole
			norm = System.Math.Sqrt(norm2);
			x /= norm;
			y /= norm;
			z /= norm;
			SpatialVector.xyz2radec((double)x, (double)y, (double)z, out ra, out dec);
			point.Add(new DPair(ra, dec));
			return (IEnumerable)point;
		}
		#endregion
		#region fHtmToCenterPoint
		/// <summary>
		/// fHtmToCenterPoint(HtmID) converts an HTM triangle ID to an (x,y,z) vector of the HTM triangle centerpoint.
		/// <br>and returns that vector as the only row of a table.  
		/// </summary>	
		/// <param name="HtmID">  long: the htm ID of the triangle. an unsigned bigint.   </param>
		/// <returns> IEnummerable VertexTable(x float, y float, z float) 
		/// <br> with one row contining the HTM triangle centerpoint.</returns>
		/// <example><code>
		///         create function fHtmToCenterPoint(@htmID bigint.) 
		///					returns VertexTable(x float, y float, z float) 
		///						 as external name HTM.Sql.fHtmToCenterPoint
		///			select * from  fHtmToCenterPoint(dbo.fHtmXyz(.57735,.57735,.57735))
		/// <br> gives: 0.577350269189626, 0.577350269189626, 0.577350269189626 
		///  </code></example> 
		/// <seealso cref="fHtmToCornerPoints()"> gives triangle corner points. </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true, FillRowMethodName = "FillTriple",
			TableDefinition = "X float, Y float, Z float")]
		public static IEnumerable fHtmToCenterPoint(SqlInt64 HtmID)
		{
			double x, y, z;
			ArrayList point = new ArrayList();
			HtmTrixel htm = new HtmTrixel();
			char[] name = new char[HtmTrixel.eMaxNameSize];
			double[] v0, v1, v2;
			double norm;
			v0 = new double[3];
			v1 = new double[3];
			v2 = new double[3];
			if (htm.hid2name(name, (long)HtmID) > 1)
			{
				htm.name2Triangle(name, v0, v1, v2);
				x = v0[0] + v1[0] + v2[0];
				y = v0[1] + v1[1] + v2[1];
				z = v0[2] + v1[2] + v2[2];
				norm = x * x + y * y + z * z;
				norm = Math.Sqrt(norm);
				x /= norm;
				y /= norm;
				z /= norm;
				point.Add(new Triple(x, y, z));
			}
			else { /* return empty */ }
			return (IEnumerable)point;
		}
		#endregion
		#region fHtmToCornerPoints
		/// <summary>
		/// fHtmToCornerPoints(HtmID) converts an HTM triangle ID to an table of three 
		/// (x,y,z) vectors of the HTM triangle corners. 
		/// </summary>	
		/// <param name="HtmID"> long: the htm ID of the triangle. an unsigned bigint. </param>  
		/// <returns> IEnummerable VertexTable(x float, y float, z float) 
		/// <br> with each row contining the (x,y,z)  of a triangle corner point.</returns>
		/// <example><code>
		///         create function fHtmTovertices(@htmID bigint.) 
		///					returns VertexTable(x float, y float, z float) 
		///						 as external name HTM.Sql.fHtmTovertices
		///			select * from  fHtmTovertices(8)
		///			gives: x y z
		///			       1 0 0
		///			       0 0 0
		///			       0 1 0
		///  </code></example> 
		/// <seealso cref="fHtmToCenterPoint()"> gives triangle center point. </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true, FillRowMethodName = "FillTriple",
			TableDefinition = "X float, Y float, Z float")]
		public static IEnumerable fHtmToCornerPoints(SqlInt64 HtmID)
		{
			ArrayList point = new ArrayList();
			HtmTrixel htm = new HtmTrixel();
			char[] name = new char[HtmTrixel.eMaxNameSize];
			double[] v0, v1, v2;
			v0 = new double[3];
			v1 = new double[3];
			v2 = new double[3];
			if (htm.hid2name(name, (long)HtmID) > 1)
			{
				htm.name2Triangle(name, v0, v1, v2);
				point.Add(new Triple(v0[0], v0[1], v0[2]));
				point.Add(new Triple(v1[0], v1[1], v1[2]));
				point.Add(new Triple(v2[0], v2[1], v2[2]));
			}
			else { /* return empty */ }
			return (IEnumerable)point;
		}
		#endregion
		#region fHtmCoverCircleLatLon
		/// <summary>
		/// fHtmCoverCircleLatLon(ra,dec,radiusArcMin) returns a trixel table (a list) covering 
		/// <br> the circle centered at that lat,lon, within that arc-minute radius. 
		/// <br> A trixel table is a list of HtmStart,HtmStop pairs that describe 
		/// <br> the HTM triangles (all points of a triangle are between its HtmStart and HtmStop.
		/// </summary>	
		/// <param name="lat"> double, latitude of centerpoint (degrees)</param>
		/// <param name="lon"> double, longitude of centerpoint (degrees)</param>
		/// <param name="radiusArcMin"> double, radius in arcminutes</param>
		/// <returns> IEnummerable(HtmIDStart bigint, HtmIDend bigint)</returns>
		/// <example><code>
		///         create function fHtmCoverCircleLatLon(@lat float, @lon float, @radiusArcMin float) 
		///					returns bigint 
		///                 as external name HTM.Sql.fHtmCoverCircleLatLon
		///         declare @HtmID bigint
		///			select * from fHtmCoverCircLatLon(5,195,1) 
		///  </code></example>  
		/// <seealso cref="fHtmCoverCircleXyz()">  uses cartesian. </seealso>
		/// <seealso cref="fHtmCoverCircleEq()">  uses J2000. </seealso>
		/// <seealso cref="fHtmCover()">  uses string definition. </seealso>
		/// --------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true,
			FillRowMethodName = "FillPair",
			TableDefinition = "HtmIDstart bigint, HtmIDend bigint")]
		public static IEnumerable fHtmCoverCircleLatLon(SqlDouble lat, SqlDouble lon, SqlDouble radiusArcMin)
		{
			radiusArcMin = Math.Max(0.0, Math.Min(180 * 3600, (double)radiusArcMin));	// range limit the radius
			Int64[,] table = HtmCover.Circle(level, (double)lon, (double)lat, (double)radiusArcMin);
			ArrayList trixelTable = new ArrayList();	// ArrayLists are enumerable 
			if (table != null)
			{
				int n = table.Length / 2;
				for (int j = 0; j < n; j++)
				{	// that is they have a stream itterator 
					Int64 hlo = table[j, 0];
					Int64 hhi = table[j, 1];
					trixelTable.Add(new Pair(hlo, hhi));
				}
			}
			return (IEnumerable)trixelTable; 		// return list  
		}
		#endregion
		#region fHtmCoverCircleEq
		/// <summary>
		/// fHtmCoverCircleEq(ra,dec,radiusArcMin) returns a trixel table (a list) covering 
		/// <br> the circle centered at that J2000 ra,dec, within that arc-minute radius. 
		/// <br> A trixel table is a list of HtmStart,HtmStop pairs that describe 
		/// <br> the HTM triangles (all points of a triangle are between its HtmStart and HtmStop.
		/// </summary>	
		/// <param name="ra"> double, right ascencion of centerpoint (degrees)</param>
		/// <param name="dec"> double, declination of centerpoint (degrees)</param>
		/// <param name="radiusArcMin"> double, radius in arcminutes</param>
		/// <returns> IEnummerable(HtmIDStart bigint, HtmIDend bigint)</returns>
		/// <example><code>
		///         create function fHtmCoverCircleEq(@ra float, @dec float, @radiusArcMin float) 
		///					returns bigint 
		///                 as external name HTM.Sql.fHtmCoverCircleEq
		///         declare @HtmID bigint
		///  </code></example>  
		/// <seealso cref="fHtmCoverCircleXyz()">  uses cartesian. </seealso>
		/// <seealso cref="fHtmCover()">  uses string definition. </seealso>
		/// --------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true,
			FillRowMethodName = "FillPair",
			TableDefinition = "HtmIDstart bigint, HtmIDend bigint")]
		public static IEnumerable fHtmCoverCircleEq(SqlDouble ra, SqlDouble dec, SqlDouble radiusArcMin)
		{
			radiusArcMin = Math.Max(0.0, Math.Min(180 * 3600, (double)radiusArcMin));	// range limit the radius
			Int64[,] table = HtmCover.Circle(level, (double)ra, (double)dec, (double)radiusArcMin);
			ArrayList trixelTable = new ArrayList();	// ArrayLists are enumerable 
			if (table != null)
			{
				int n = table.Length / 2;
				for (int j = 0; j < n; j++)
				{	// that is they have a stream itterator 
					Int64 hlo = table[j, 0];
					Int64 hhi = table[j, 1];
					trixelTable.Add(new Pair(hlo, hhi));
				}
			}
			return (IEnumerable)trixelTable; 		// return list  
		}
		#endregion
		#region fHtmCoverCircleXyz
		/// <summary>
		/// fHtmCoverCircleXyz(x,y,z,radiusArcMin) returns a trixel table (a list) covering 
		/// <br> the circle centered at that x,y,z, within that arc-minute radius. 
		/// <br> A trixel table is a list of HtmStart,HtmStop pairs that describe 
		/// <br> the HTM triangles (all points of a triangle are between its HtmStart and HtmStop. 
		/// </summary>	
		/// <param name="x"> double, x Cartesian coordinate of centerpoint</param>
		/// <param name="y"> double, y Cartesian coordinate of centerpoint</param>
		/// <param name="z"> double, z Cartesian coordinate of centerpoint</param>
		/// <param name="radiusArcMin"> double, radius in arcminutes</param>
		/// <returns>IEnummerable TrixelTable(HtmIDStart bigint, HtmIDend bigint) </returns>
		/// <example><code>
		///         create function fHtmCoverCircleXyz(@x float, @y float, @z float, @radiusArcMin float) 
		///					returns bigint 
		///                 as external name HTM.Sql.fHtmCoverCircleXyz
		///         declare @HtmID bigint
		///			select * from fHtmCircleXyz(1,0,0,1) 
		///  </code></example>  
		/// <seealso cref="fHtmCoverCircleEq()">  uses J2000. </seealso>
		/// <seealso cref="fHtmCover()">  uses string definition. </seealso>
		/// --------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true,
			FillRowMethodName = "FillPair",
			TableDefinition = "HtmIDstart bigint, HtmIDend bigint")]
		public static IEnumerable fHtmCoverCircleXyz(SqlDouble x, SqlDouble y, SqlDouble z, SqlDouble radiusArcMin)
		{
			double ra, dec;
			if (Cartesian.Epsilon2 > (x * x + y * y + z * z)) { x = 0; y = 0; z = 1; } // replace zero vector with 0,0,1
			SpatialVector.xyz2radec((double)x, (double)y, (double)z, out ra, out dec);
			radiusArcMin = Math.Max(0, Math.Min(180 * 3600, (double)radiusArcMin));	// range limit the radius
			Int64[,] table = HtmCover.Circle(level, (double)ra, (double)dec, (double)radiusArcMin);
			ArrayList trixelTable = new ArrayList();	// ArrayLists are enumerable 
			if (table != null)
			{
				int n = table.Length / 2;
				for (int j = 0; j < n; j++)
				{	// that is they have a stream itterator 
					Int64 hlo = table[j, 0];
					Int64 hhi = table[j, 1];
					trixelTable.Add(new Pair(hlo, hhi));
				}
			}
			return (IEnumerable)trixelTable; 		// return list  
		}
		#endregion
		#region fHtmMagicNumber
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlString fHtmMagicNumber(SqlInt32 magic, SqlInt32 hdel)
		{
			HtmState.Instance.hdelta = (int)hdel;
			HtmState.Instance.magicnumber = (int)magic;
			return new SqlString("ok");
		}
		#endregion

		#region fHtmCoverRegion
		/// <summary>
		/// fHtmCoverRegion(region) returns a list of HtmStart,HtmStop pairs 
		/// <br>that describe the HTM triangles covering the region. 
		/// </summary>	
		/// <param name="region"> a string satisfying the region syntax
		/// <li>                   regionSpec :=     REGION {areaSpec}* 
		///										|    areaSpec
		/// <li>                   areaSpec   :=     circleSpec | rectSpec | polySpec | hullSpec | convexSpec  
		/// <li>                   circleSpec :=     CIRCLE J2000        ra dec  radArcMin  
		/// <li>                                |    CIRCLE LATLON       lat lon radArcMin  
		/// <li>                                |    CIRCLE [CARTESIAN ] x y z   radArcMin  
		/// <li>                   rectSpec   :=     RECT LATLON        {lat lon}2  
		/// <li>                                |    RECT J2000         {ra dec }2   
		/// <li>                                |    RECT [CARTESIAN ]  {x y z  }2  
		/// <li>                   polySpec   :=     POLY LATLON        {lat lon}3+  
		/// <li>                                |    POLY J2000         {ra dec }3+  
		/// <li>                                |    POLY [CARTESIAN ]  {x y z  }3+  
		/// <li>                   hullSpec   :=     CHULL LATLON       {lat lon}3+  
		/// <li>                                |    CHULL J2000        {ra dec }3+  
		/// <li>                                |    CHULL [CARTESIAN ] {x y z  }3+  
		/// <li>                   convexSpec :=     CONVEX LATLON      {lat lon}*  
		/// <li>                                |    CONVEX J2000       {ra dec }* 
		/// <li>                                |    CONVEX [CARTESIAN ]{x y z  }*  
		/// </param>

		/// <returns> IEnummerable(HtmIDStart bigint, HtmIDend bigint)</returns>
		/// <example><code>
		///         create function fHtmCoverRegion(@region nvarchar(max)) 
		///					returns table  (HtmIDstart bigint, HtmIDend bigint)
		///						 as external name HTM.Sql.fHtmCover
		///			select * from fHtmCoverRegion('CIRCLE J2000 195 0 1')
		///  </code></example> 
		/// <seealso cref="fHtmCoverError()"> gives diagnostic message. </seealso>
		/// <seealso cref="fHtmCircleEq()"> Equatorial circle. </seealso>
		/// <seealso cref="fHtmCircleXyz()"> Cartesian circle. </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true,
			FillRowMethodName = "FillPair",
			TableDefinition = "HtmIDstart bigint, HtmIDend bigint")]
		public static IEnumerable fHtmCoverRegion(SqlString coverspec)
		{
			// May 25, 2005 experimenting with a heuristic for
			// making a tighter fit.
			// uncomment whichever way you want it...
			// Int64[,] table = HtmCover.Region(coverspec.ToString(), null); /* classis */
			HtmState.Instance.minlevel = HtmState._D_minlevel;
			Int64[,] table = HtmCover.RegionTighter(
				coverspec.ToString(), null); /* tight-fit heuristic */
			ArrayList list = new ArrayList();	// ArrayLists are enumerable 
			if (table != null)
			{
				int n = table.Length / 2;
				for (int j = 0; j < n; j++)
				{	// that is they have a stream itterator 
					Int64 hlo = table[j, 0];
					Int64 hhi = table[j, 1];
					list.Add(new Pair(hlo, hhi));
				}
			}
			return (IEnumerable)list; 		// return list  
		}
		#endregion
		#region fHtmRegionToTable
		/// <summary>
		/// fHtmRegionToTable(region) converts a region definiton a table of halfspaces
		/// </summary>	
		/// <param name="region"> a string satisfying the region syntax
		/// <li>                   circleSpec :=     CIRCLE J2000        ra dec  radArcMin  
		/// <li>                                |    CIRCLE LATLON       lat lon radArcMin  
		/// <li>                                |    CIRCLE [CARTESIAN ] x y z   radArcMin  
		/// <li>                   rectSpec   :=     RECT LATLON        {lat lon}2  
		/// <li>                                |    RECT J2000         {ra dec }2   
		/// <li>                                |    RECT [CARTESIAN ]  {x y z  }2  
		/// <li>                   polySpec   :=     POLY LATLON        {lat lon}3+  
		/// <li>                                |    POLY J2000         {ra dec }3+  
		/// <li>                                |    POLY [CARTESIAN ]  {x y z  }3+  
		/// <li>                   hullSpec   :=     CHULL LATLON       {lat lon}3+  
		/// <li>                                |    CHULL J2000        {ra dec }3+  
		/// <li>                                |    CHULL [CARTESIAN ] {x y z  }3+  
		/// <li>                   convexSpec :=     CONVEX LATLON      {lat lon}*  
		/// <li>                                |    CONVEX J2000       {ra dec }* 
		/// <li>                                |    CONVEX [CARTESIAN ]{x y z  }*  
		/// <li>                   areaSpec   :=     circleSpec | rectSpec | polySpec | hullSpec | convexSpec  
		/// <li>                   regionSpec :=     REGION {areaSpec}* 
		///										|    areaSpec
		/// </param>	 
		/// <returns> Table of Halfspaces:
		/// <br> This looks something like REGION CONVEX 1 0 0 0 0 1 0 0 CONVEX 0 0 1 0 </returns>
		/// <example><code>
		///         create function fHtmRegionToTable(@region nvarchar(max)) 
		///					returns table  (convexid int, halfspaceid int, x double, y double, z double, D double)
		///						 as external name HTM.Sql.fHtmRegionToTable
		///			select * from dbo.fHtmRegionToTable('CIRCLE J2000 195 0 1')
		///  </code></example> 
		/// --------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true,
			FillRowMethodName = "FillHalfspace",
			TableDefinition = "convexId bigint, halfspaceID bigint, x float, y float, z float, D float")]
		public static IEnumerable fHtmRegionToTable(SqlString coverspec)
		{
			double[,] table = HtmCover.CoverToHalfspaces(coverspec.ToString());
			ArrayList list = new ArrayList();
			if (table != null)
			{
				int n = table.Length / 6;
				for (int j = 0; j < n; j++)
				{
					int cid = (int)table[j, 0];
					int hid = (int)table[j, 1];
					double x = table[j, 2];
					double y = table[j, 3];
					double z = table[j, 4];
					double D = table[j, 5];
					list.Add(new HSEntry(cid, hid, x, y, z, D));
				}
			}
			/** The following hack by JG ensured that each row (halfspace entry)
			 * starts with an id = 0, the code was restored to it orinigal state
			 * after CoverTohalfSpaces was modofied to do this
			 */
			//if (table != null) {
			//    int n = table.Length / 6;
			//    int cid, hid = 0;		
			//    int oldCid = -1;	// machinery to make hid restart at zero for each new cid.
			//    for (int j = 0; j < n; j++) {
			//        cid = (int) table[j, 0];
			//        if (cid == oldCid) hid++;
			//        else {
			//            hid = 0;
			//            oldCid = cid;
			//        }
			//        //hid = (int) table[j, 1];
			//        double x = table[j, 2];
			//        double y = table[j, 3];
			//        double z = table[j, 4];
			//        double D = table[j, 5];
			//        list.Add(new HSEntry(cid, hid, x, y, z, D));
			//    }
			//}
			return (IEnumerable)list;
		}
		#endregion
		#region fHtmRegionToNormalFormString
		/// <summary>
		/// fHtmRegionToNormalFormString(region) converts a region definiton to normal form
		/// </summary>	
		/// <param name="region"> a string satisfying the region syntax
		/// <li>                   circleSpec :=     CIRCLE J2000        ra dec  radArcMin  
		/// <li>                                |    CIRCLE LATLON       lat lon radArcMin  
		/// <li>                                |    CIRCLE [CARTESIAN ] x y z   radArcMin  
		/// <li>                   rectSpec   :=     RECT LATLON        {lat lon}2  
		/// <li>                                |    RECT J2000         {ra dec }2   
		/// <li>                                |    RECT [CARTESIAN ]  {x y z  }2  
		/// <li>                   polySpec   :=     POLY LATLON        {lat lon}3+  
		/// <li>                                |    POLY J2000         {ra dec }3+  
		/// <li>                                |    POLY [CARTESIAN ]  {x y z  }3+  
		/// <li>                   hullSpec   :=     CHULL LATLON       {lat lon}3+  
		/// <li>                                |    CHULL J2000        {ra dec }3+  
		/// <li>                                |    CHULL [CARTESIAN ] {x y z  }3+  
		/// <li>                   convexSpec :=     CONVEX LATLON      {lat lon}*  
		/// <li>                                |    CONVEX J2000       {ra dec }* 
		/// <li>                                |    CONVEX [CARTESIAN ]{x y z  }*  
		/// <li>                   areaSpec   :=     circleSpec | rectSpec | polySpec | hullSpec | convexSpec  
		/// <li>                   regionSpec :=     REGION {areaSpec}* 
		///										|    areaSpec
		/// </param>
		/// <returns> region String SqlString region in normal form ( a union of convexes)
		/// <br> This looks something like REGION CONVEX 1 0 0 0 0 1 0 0 CONVEX 0 0 1 0 </returns>
		/// <example><code>
		///         create function fHtmRegionToNormalFormString(@region nvarchar(max)) 
		///					returns table  (HtmID_start bigint, HtmID_end bigint)
		///						 as external name HTM.Sql.fHtmToNormalForm
		///			select dbo.fHtmRegionToNormalFormString('CIRCLE J2000 195 0 1')
		///  </code></example> 
		/// --------------------------------------------------------------

		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlString fHtmRegionToNormalFormString(SqlString regionString)
		{
			return (HtmCover.NormalForm(regionString.ToString()));
		}
		#endregion
		#region fHtmRegionError
		/// <summary>
		/// fHtmRegionError(region) returns diagnostic messge for an HTM region string 
		/// </summary>	
		/// <param name="region"> a string satisfying the region syntax
		/// <li>                   circleSpec :=     CIRCLE J2000        ra dec  radArcMin  
		/// <li>                                |    CIRCLE LATLON       lat lon radArcMin  
		/// <li>                                |    CIRCLE [CARTESIAN ] x y z   radArcMin  
		/// <li>                   rectSpec   :=     RECT LATLON        {lat lon}2  
		/// <li>                                |    RECT J2000         {ra dec }2   
		/// <li>                                |    RECT [CARTESIAN ]  {x y z  }2  
		/// <li>                   polySpec   :=     POLY LATLON        {lat lon}3+  
		/// <li>                                |    POLY J2000         {ra dec }3+  
		/// <li>                                |    POLY [CARTESIAN ]  {x y z  }3+  
		/// <li>                   hullSpec   :=     CHULL LATLON       {lat lon}3+  
		/// <li>                                |    CHULL J2000        {ra dec }3+  
		/// <li>                                |    CHULL [CARTESIAN ] {x y z  }3+  
		/// <li>                   convexSpec :=     CONVEX LATLON      {lat lon}*  
		/// <li>                                |    CONVEX J2000       {ra dec }* 
		/// <li>                                |    CONVEX [CARTESIAN ]{x y z  }*  
		/// <li>                   areaSpec   :=     circleSpec | rectSpec | polySpec | hullSpec | convexSpec  
		/// <li>                   regionSpec :=     REGION {areaSpec}* 
		///										|    areaSpec
		/// </param>
		/// <returns> diagnostic message: varchar(max) 
		/// <br>    OK if ok
		/// <br>	else message and a syntax string. 
		/// </returns>
		/// <example><code>
		///         create function fHtmRegionError(@region nvarchar(max)) 
		///					returns varchar(max)
		///						 as external name HTM.Sql.fHtmRegionError
		///			print dbo.fHtmRegionError('CIRCLE LATLON 195 ')
		///  </code></example> 
		/// <seealso cref="fHtmCover()"> the main routine </seealso>
		///--------------------------------------------------------------
		[SqlFunction(IsDeterministic = true, IsPrecise = true)]
		public static SqlString fHtmRegionError(SqlString coverspec)
		{
			// return syntax only if not ok after reason
			String errmsg;
			String syntax = "\n Syntax is:"
						  + "\n regionSpec :=     REGION {areaSpec}*              "
						  + "\n              |    areaSpec "
						  + "\n areaSpec   :=     circleSpec | rectSpec | polySpec | hullSpec | convexSpec"
			+ "\n circleSpec :=     CIRCLE J2000         ra dec  radArcMin "
			+ "\n              |    CIRCLE LATLON        lat lon radArcMin "
			+ "\n              |    CIRCLE [CARTESIAN ]  x y z   radArcMin "
			+ "\n rectSpec   :=     RECT LATLON         {lat lon}2  "
			+ "\n              |    RECT J2000          {ra dec }2  "
			+ "\n              |    RECT [CARTESIAN ]   {x y z  }2  "
			+ "\n polySpec   :=     POLY LATLON         {lat lon}3+ "
			+ "\n              |    POLY J2000          {ra dec }3+ "
			+ "\n              |    POLY [CARTESIAN ]   {x y z  }3+ "
			+ "\n hullSpec   :=     CHULL LATLON        {lat lon}3+ "
			+ "\n              |    CHULL J2000         {ra dec }3+ "
			+ "\n              |    CHULL [CARTESIAN ]  {x y z  }3+ "
			+ "\n convexSpec :=     CONVEX  LATLON      {lat lon}*  "
			+ "\n              |    CONVEX J2000        {ra dec }*  "
			+ "\n              |    CONVEX [CARTESIAN ] {x y z  }*  ";
			// Strings and SqlStrings are special, ToString() converts a null to "Null"
			//
			if (coverspec.IsNull)
			{
				return new SqlString("Null is not a valid region specification");
			}
			errmsg = HtmCover.Error(coverspec.ToString());
			if (errmsg.CompareTo("ok") == 0)
			{
				return new SqlString("OK");
			}
			else
			{
				return new SqlString(errmsg + syntax);
			}
		}
		#endregion
		#region FillPair
		/// <summary>
		// FillPair() for this release, we have to provide a deserializer that converts a IEnumeable object to a row.
		// SQL calls this when extracting the (HtmStart, HtmEnd) IEnumberable elements. 
		// SQL will learn how to do this automatically someday
		/// </summary>
		private static void FillPair(object source, out SqlInt64 lo, out SqlInt64 hi)
		{
			Pair p = (Pair)source;			// down-cast IEnumerable objects
			lo = p.lo; hi = p.hi;				// do the extraction/serialziation
		}
		#endregion
		#region FillDPair
		/// <summary>
		// FillDPair() for this release, we have to provide a deserializer that converts a IEnumeable object to a row.
		// SQL calls this when extracting the (ra, dec) IEnumberable elements. 
		// SQL will learn how to do this automatically someday
		/// </summary>
		private static void FillDPair(object source, out SqlDouble o1, out SqlDouble o2)
		{
			DPair p = (DPair)source;			// down-cast IEnumerable objects
			o1 = p.d1; o2 = p.d2;				// do the extraction/serialziation
		}
		#endregion
		#region FillTriple
		/// <summary>
		// FillTripple() for this release, we have to provide a deserializer that converts a IEnumeable object to a row.
		// SQL calls this when extracting the (x,y, z) IEnumberable elements. 
		// SQL will learn how to do this automatically someday
		/// </summary>
		private static void FillTriple(object source, out SqlDouble x, out SqlDouble y, out SqlDouble z)
		{
			Triple t = (Triple)source;
			x = t.x;
			y = t.y;
			z = t.z;
		}
		#endregion
		#region FillHalfspace
		/// <summary>
		// FillHalfspace() 	/// </summary>

		private static void FillHalfspace(object source, out SqlInt64 convexID, out SqlInt64 halfSpaceID,
							out SqlDouble x, out SqlDouble y, out SqlDouble z, out SqlDouble D)
		{
			HSEntry t = (HSEntry)source;
			convexID = t.cid;
			halfSpaceID = t.hid;
			x = t.x;
			y = t.y;
			z = t.z;
			D = t.D;
		}
		#endregion
		#region fHtmCallback
		//	Please try using SqlFunction(DataAccess=DataAccessKind.Read)] in the custom attribute? 
		//

		//[SqlFunction(DataAccess=DataAccessKind.Read)] 
		//public static SqlInt32 CheckMaxDepth() {
		//    string version = "";
		//    using (SqlConnection connection = new SqlConnection("context connection=true")) {
		//        try {
		//            connection.Open();
		//            SqlCommand command = new SqlCommand("SELECT @@version", connection);
		//            version = command.ExecuteScalar().ToString();
		//        } catch (Exception ex) {
		//            version = ex.Message;
		//        } finally {
		//            if (connection != null) connection.Close();
		//        }
		//        //return new SqlString(version); 
		//        return new SqlInt32(version.Length);
		//    } 
		//} 

		#endregion
		///**************************************************************
	}
}
