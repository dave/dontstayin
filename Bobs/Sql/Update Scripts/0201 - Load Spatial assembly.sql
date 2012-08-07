/*=====================================================================

  File:      spHtmCsharp.sql for Spatial Sample
  Summary:   Implements the Transact-SQL interface to the Spatial library
  Date:	     August 19, 2005

---------------------------------------------------------------------
  This spatial search library was developed by Alexander Szalay, Robert Brunner, 
  Peter Kunszt, and Gyorgy Fekete at the Department of Physics and Astronomy, 
  The Johns Hopkins University, in collaboration with Jim Gray, Microsoft Research.
  Details of the algorithms can be found at http://www.sdss.jhu.edu/htm/.
 
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.
======================================================= */

--=================================================================================
-- spHtmCsharp.sql 
--   Jim Gray, Alex Szalay, Gyorgy Fekete, May 2005
-----------------------------------------------------------------------------------
-- Define load the Spatial Assembly (dll) and define the entry points to the HTM functions.
-- Replaces spHtm.sql of the SQL2000 version. 
--  
-- Defines: 
--		fHtmVersion
--		fHtmEq					-- convert HTM to coordinates or name
--		fHtmXyz
--		fHtmLatLon
--		fHtmToString 
--		fHtmXyzToEq				-- coordinate transforms
--		fHtmXyzToLatLon
--		fHtmEqToXyz
--		fHtmLatLonToXyz
--      fDistanceEq				-- compute distances
--      fDistanceLatLon
--      fDistanceXyz 
--      fHtmToCenterPoint		-- HTM trixel attributes
--      fHtmToCornerPoints
--		fHtmCoverCircleEq		-- HTM covers of various shapes
--		fHtmCoverCircleLatLon
--		fHtmCoverCircleXyz
--		fHtmCoverRegion
--		fHtmRegionToNormalFormString	-- manipulate region strings
--		fHtmRegionToTable
--      fHtmRegionObjects
--		fHtmRegionError
--      fHtmNearbyLatLon		-- nearby objects
--      fHtmNearbyEq
--      fHtmNearbyXYZ
--      fHtmNearestLatLon		-- nearest object
--      fHtmNearestEq
--      fHtmNearestXYZ
----------------------------------------------------------------------------------
-- History:
--* 2005-05-01 Jim:  started
--* 2005-05-02 Jim:  removed fHtmLookup and fHtmLookupError
--*                  added fHtmToString
--* 2005-05-05 GYF:  added .pdb to assembly for symbolic debugging
--*                  added fHtmToName (faster than fHtmToString and reports error)
--* 2005-05-09 Jim:  Added more documentation.  
--*                  Replaced fHtmtoString with fHtmToName
--* 2005-05-11 Jim:  Added fDistanceEq, fDistanceXyz
--*	                 Changed fHtmToPoint to fHtmToCenterPoint
--*                          fHtmToVertices to fHtmToCornerPoints
--*                  Defaulted to release version of library.
--* 2005-05-17 GYF:  Added fHtmLatLon, fDistanceLatLon, fHtmCoverCircleLatLon
--* 2005-05-19 Jim:  Fixed comments re fHtmToPoint,fHtmToCenterPoint 
--* 2005-05-22 Jim:  Replace fHtmCoverError,  fHtmToNormalFormError() 
--                        with fHtmRegionError()
--                   Added LatLon to region syntax comments. 
--                   Added fHtmNearbyLatLon, fHtmNearbyEq, fHtmNearbyXYZ
--                   Added fHtmNearestLatLon, fHtmNearestEq, fHtmNearestXYZ
--* 2005-05-31 GYF:	 Added fHtmXyzToEq, fHtmXyzTolatLon,
--						   fHtmEqToXyz, fHtmLatLonToXyz
--* 2005-06-02 Jim:  Minor changes to comments.
--* 2005-06-04 Jim:  renamed fHtmGetNearbyXXX to fHtmNearbyXXX 
--                       and fHtmNearestXXX  to fHtNearestXXX 
--                     for XXX in {LatLon, Eq, Xyz}
--                   optimized distance test in the "near" routines. 
--* 2005-06-22 GYF:  added (fHtmCoverToHalfspaces see next:)
--* 2005-06-28 GYF:  changed the name to fHtmRegionToTable
--* 2005-06-29 Jim:  fixed code to have new-new function names.
--*                  fixed comments to reflect 21-deep (not 20 deep) HTMs
--* 2005-06-30 Jim:  Added fHtmRegionObjects
--* 2005-07-01 GYF:	 fHtmRegionObjects dropped properly
----------------------------------------------------------------------------------
--* todo:   
----------------------------------------------------------------------------------
-- EDIT database name as appropriate
-- Don't need to add HTM.pdb to ASSEMBLY, unless you want to
--      use the symbolic debugger of VS
--
--create database testHtm
--go
-- The sample database is called "Spatial" and should be attached. 
-- These procedures are installed in the sample database already. 
SET NOCOUNT ON
go
exec sp_configure 'clr enabled', 1
go
if exists (select * from sys.objects where [name] = N'fHtmVersion') 
			drop function  fHtmVersion
if exists (select * from sys.objects where [name] = N'fHtmLatLon') 
			drop function  fHtmLatLon
if exists (select * from sys.objects where [name] = N'fHtmEq') 
			drop function  fHtmEq
if exists (select * from sys.objects where [name] = N'fHtmXyz') 
			drop function  fHtmXyz
if exists (select * from sys.objects where [name] = N'fHtmToString') 
			drop function  fHtmToString 
if exists (select * from sys.objects where [name] = N'fHtmXyzToEq') 
			drop function fHtmXyzToEq
if exists (select * from sys.objects where [name] = N'fHtmXyzToLatLon') 
			drop function fHtmXyzToLatLon
if exists (select * from sys.objects where [name] = N'fHtmEqToXyz') 
			drop function fHtmEqToXyz
if exists (select * from sys.objects where [name] = N'fHtmLatLonToXyz') 
			drop function fHtmLatLonToXyz
if exists (select * from sys.objects where [name] = N'fDistanceLatLon') 
			drop function fDistanceLatLon
if exists (select * from sys.objects where [name] = N'fDistanceEq') 
			drop function fDistanceEq
if exists (select * from sys.objects where [name] = N'fDistanceXyz') 
			drop function fDistanceXyz
if exists (select * from sys.objects where [name] = N'fHtmIDToSquareArcmin') 
			drop function fHtmIDToSquareArcmin
if exists (select * from sys.objects where [name] = N'fHtmToCenterPoint') 
			drop function fHtmToCenterPoint
if exists (select * from sys.objects where [name] = N'fHtmToCornerPoints') 
			drop function fHtmToCornerPoints
if exists (select * from sys.objects where [name] = N'fHtmCoverCircleLatLon') 
			drop function  fHtmCoverCircleLatLon
if exists (select * from sys.objects where [name] = N'fHtmCoverCircleEq') 
			drop function  fHtmCoverCircleEq
if exists (select * from sys.objects where [name] = N'fHtmCoverCircleXyz') 
			drop function  fHtmCoverCircleXyz
if exists (select * from sys.objects where [name] = N'fHtmCoverRegion') 
			drop function  fHtmCoverRegion
if exists (select * from sys.objects where [name] = N'fHtmRegionToNormalFormString') 
			drop function fHtmRegionToNormalFormString
if exists (select * from sys.objects where [name] = N'fHtmCoverRegion') 
			drop function fHtmCoverRegion
if exists (select * from sys.objects where [name] = N'fHtmRegionToTable') 
			drop function fHtmRegionToTable
if exists (select * from sys.objects where [name] = N'fHtmRegionError') 
			drop function fHtmRegionError
if exists (select * from sys.objects where [name] = N'fHtmRegionObjects')
			drop function fHtmRegionObjects
if exists (select * from sys.objects where [name] = N'fHtmNearbyLatLon') 
			drop function fHtmNearbyLatLon
if exists (select * from sys.objects where [name] = N'fHtmNearbyEq') 
			drop function fHtmNearbyEq
if exists (select * from sys.objects where [name] = N'fHtmNearbyXYZ') 
			drop function fHtmNearbyXYZ
if exists (select * from sys.objects where [name] = N'fHtmNearestLatLon') 
			drop function fHtmNearestLatLon
if exists (select * from sys.objects where [name] = N'fHtmNearestEq') 
			drop function fHtmNearestEq
if exists (select * from sys.objects where [name] = N'fHtmNearestXYZ') 
			drop function fHtmNearestXYZ
--

		
go
------------------------------------------------------------------
/* release version */
-- Add the assembly which contains the CLR methods we want to invoke on the server.
--DECLARE @SamplesPath nvarchar(1024)
-- You may need to modify the value of the this variable if you have installed the sample someplace other than the default location.
--SELECT @SamplesPath = replace(physical_name, 'Microsoft SQL Server\MSSQL.1\MSSQL\DATA\master.mdf', 'Microsoft SQL Server\90\Samples\Engine\Programmability\CLR\') 
--	FROM master.sys.database_files 
--	WHERE name = 'master';
--
--CREATE ASSEMBLY Spatial  
-- 	FROM @SamplesPath + 'Spatial\CS\Spatial\bin\release\Spatial.dll'
--     WITH permission_set = safe;   
--GO
------------------------------------------------------------------
--/* debug version */
-- Add the assembly which contains the CLR methods we want to invoke on the server.
if NOT exists (select * from sys.assemblies where [name] = N'Spatial') 
	CREATE ASSEMBLY Spatial  
 		FROM @WorkingDirectory + '\..\..\Spatial\bin\Spatial.dll'
		WITH permission_set = safe;  

go
------------------------------------------------------------------------
-- Now define the functions in the HTM assembly
-----------------------------------------------
GO create function fHtmVersion() returns nvarchar(max)
------------------------------------------------------------- 
--/H Returns version of installed Returns Hierarchical Triangular Mesh (HTM) library as a string.  
--/H <br> Typically 'C# HTM.DLL V.1.0.0 15 May 2005'
------------------------------------------------------------- 
--/T Sample call to  
--/T <br><samp> 
--/T <br> declare @version nvarchar(max)
--/T <br> select @version = dbo.fHtmVersion() 
--/T </samp>  
------------------------------------------------------------- 
	as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmVersion
go
-- select dbo.fHtmVersion() -- 'C# HTM.DLL V.1.0.0 15 May 2005'
go
GO create function fHtmLatLon(@Lat float, @Lon float) returns bigint
------------------------------------------------------------- 
--/H Returns Hierarchical Triangular Mesh (HTM) ID of a given point.  
--/H <br> given right ascencion and declination in J2000 reference frame
------------------------------------------------------------- 
--/T <li> Lat float NOT NULL,          --/D Latitude will be nomalized to [0..360] --/U deg --/K POS_LIMIT
--/T <li> Lon float NOT NULL,          --/D Longitude will be nomalized to [-90..90] --/U deg --/K POS_LIMIT
--/T <li> returns htmID bigint         --/D 21-deep hierarchical trangular mesh ID of this point --/K CODE_HTM
--/T <br> 
--/T Sample call    
--/T <br><samp> 
--/T <br> declare @htmID bigint
--/T <br> select @htmID = dbo.fHtmEq(0, 195) 
--/T </samp>  
--/T <br> see also fHtmXyz, fHtmEq
------------------------------------------------------------- 
	as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmLatLon
go
GO create function fHtmEq(@ra float, @dec float) returns bigint
------------------------------------------------------------- 
--/H Returns Hierarchical Triangular Mesh (HTM) ID of a given point.  
--/H <br> given right ascencion and declination in J2000 reference frame
------------------------------------------------------------- 
--/T <li> ra  float NOT NULL,          --/D J2000 right ascension will be nomalized to [0..360] --/U deg --/K POS_LIMIT
--/T <li> dec float NOT NULL,          --/D J2000 right declination will be nomalized to [-90..90] --/U deg --/K POS_LIMIT
--/T <li> returns htmID bigint         --/D 21-deep hierarchical trangular mesh ID of this point --/K CODE_HTM
--/T <br> 
--/T Sample call    
--/T <br><samp> 
--/T <br> declare @htmID bigint
--/T <br> select @htmID = dbo.fHtmEq(195,0) 
--/T </samp>  
--/T <br> see also fHtmXyz 
------------------------------------------------------------- 
	as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmEq
go
-- select dbo.fHtmEq(180, 0) -- 14843406974976
go
GO create function fHtmXyz(@x float, @y float, @z float) returns bigint
------------------------------------------------------------- 
--/H Returns Hierarchical Triangular Mesh (HTM) ID of a given point. 
--/H <br> given x,y,z in cartesian (galactic) reference frame.
--/H <br> x,y,z will be normalized to unit vector if non-zero and to 1,0,0 if zero. 
------------------------------------------------------------- 
--/T <li> x float NOT NULL, --/D unit vector for ra+dec --/K POS_EQ_CART_X
--/T <li> y float NOT NULL, --/D unit vector for ra+dec --/K POS_EQ_CART_Y
--/T <li> z float NOT NULL, --/D unit vector for ra+dec --/K POS_EQ_CART_Z
--/T <li> returns htmID bigint         --/D 21-deep hierarchical trangular mesh ID of this object --/K CODE_HTM
--/T <br> 
--/T Sample call    
--/T <br><samp> 
--/T <br> declare @htmID bigint
--/T <br> select @htmID = dbo.fHtmXyz(1,0,0) 
--/T </samp>  
--/T <br> see also fHtmEq 
------------------------------------------------------------- 
	as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmXyz
go
-- select dbo.fHtmXyz(1,0,0) -- 17042430230528
go
--=======================================================
 
GO create function fHtmToString (@HtmID bigint) returns nvarchar(max)
---------------------------------------------------------------------
--/H  Converts an HTM ID to a string representaion of that HTM ID
---------------------------------------------------------------------
--/T <br>Parameter:
--/T <li>htmID bigint --/D 21-deep hierarchical trangular mesh ID of this point --/K CODE_HTM
--/T <li> returns varchar(max) The string format is (N|S)[0..3]* 
--/T  <br> For example S130000013 is on the second face of the southern hemisphere.
--/T  <br> i.e. ra is between 6h and 12h  
--/T  <li>Example: 
--/T  <samp>print  dbo.fHtmToString(dbo.fHtmEq(195,2.5))
--/T  <br> gives: N120131233021223031323 </samp>
--/T  <br>
----------------------------------------------------- 
	as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmToString
go
GO create function fDistanceLatLon(@Lat1 float, @Lon1 float, @Lat2 float, @Lon2 float) 
-------------------------------------------------------------
--/H returns distance (arc minutes) between two points (Lat1, Lon1) and (Lat2, Lon2)
-------------------------------------------------------------
--/T <br> Lat1, Lon1, Lat2, Lon2 are in degrees
--/T <br>
-------------------------------------------------------------
    returns float
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fDistanceLatLon
go
GO create function fDistanceEq(@ra1 float, @dec1 float, @ra2 float, @dec2 float) 
-------------------------------------------------------------
--/H returns distance (arc minutes) between two points (ra1,dec1) and (ra2,dec2)
-------------------------------------------------------------
--/T <br> ra1, dec1, ra2, dec2 are in degrees
--/T <br>
--/T <samp>select top 10 objid, dbo.fDistanceEq(185,0,ra,dec) from PhotoObj </samp>
-------------------------------------------------------------
    returns float
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fDistanceEq
go
GO create function fDistanceXyz(@x1 float, @y1 float, @z1 float, @x2 float, @y2 float, @z2 float ) 
-------------------------------------------------------------
--/H returns distance (arc minutes) between two points (x1,y1,z1) and (x2,y1,z2)
-------------------------------------------------------------
--/T <br> x1,y1,z1 and x2,y2,z2 are cartesian points.  
--/T <br>
--/T <samp>select top 10 objid, dbo.fDistanceXyz(1,0,0,cx,cy,cz) from PhotoObj </samp>
-------------------------------------------------------------
    returns float
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fDistanceXyz
go
GO create function fHtmIDToSquareArcmin(@HtmID bigint) 
-------------------------------------------------------------
--/H returns area (square arc minutes) of trixel HtmID
-------------------------------------------------------------
--/T <br>Parameter:
--/T <li>htmID bigint --/D hierarchical trangular mesh ID  --/K CODE_HTM
--/T <li> returns float area in square arc minutes  
--/T  <li>Example: 
--/T  <samp>select fHtmIDToSquareArcmin(8)
--/T  <br> gives: 18563832.50
--/T  </samp>
--/T  <br>
-------------------------------------------------------------
    returns float
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmIDToSquareArcmin
go
GO create function fHtmToCenterPoint(@HtmID bigint)
---------------------------------------------------------------------
--/H  Converts an HTM ID to a (x,y,z) vector of the HTM centerpoint.   
---------------------------------------------------------------------
--/T <br>Parameter:
--/T <li>htmID bigint --/D hierarchical trangular mesh ID  --/K CODE_HTM
--/T <li> returns VertexTable(x float, y float, z float) with one row contining the HTM triangle centerpoint.  
--/T  <li>Example: 
--/T  <samp>select * from  fHtmToCenterPoint(dbo.fHtmXyz(.57735,.57735,.57735))
--/T  <br> gives: 0.577350269189626, 0.577350269189626, 0.577350269189626 
--/T  </samp>
--/T  <br>
----------------------------------------------------- 
				returns table (x float, y float, z float)
as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmToCenterPoint
go
GO create function fHtmToCornerPoints(@HtmID bigint)
---------------------------------------------------------------------
--/H  Converts an HTM ID to the three (x,y,z) vector of the triangle's corner points.   
---------------------------------------------------------------------
--/T <br>Parameter:
--/T <li>htmID bigint --/D 21-deep hierarchical trangular mesh ID of the point --/K CODE_HTM
--/T <li> returns VertexTable(x float, y float, z float) with one row contining the HTM triangle centerpoint.  
--/T  <li>Example: 
--/T  <samp>select * from  fHtmToCornerPoints(8)
--/T  <br> gives: x y z
--/T  <br>        1 0 0
--/T  <br>        0 0 0
--/T  <br>        0 1 0
--/T  </samp>
--/T  <br>
----------------------------------------------------- 
				returns table (x float, y float, z float)
as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmToCornerPoints
go
GO create function fHtmEqToXyz(@ra float, @dec float)
------------------------------------------------------------------
--/H Convert Ra, Dec to Cartesian coordinates (x, y, z)
------------------------------------------------------------------
--/T <br>Parameters:
--/T <li>ra float --D/ Right Ascension
--/T <li>dec float --D/ Declination
--/T <li> returns Table of one row containing the values (x, y, z)
--/T <samp>select * from fHtmEqToXyz(-180.0, 0.0)
--/T <br> gives:  x  y  z
--/T <br>        -1  0  0
--/T </samp>
--/T <br>
-----------------------------------------------------------------
				returns table (x float, y float, z float)
as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmEqToXyz
go
GO create function fHtmXyzToEq(@x float, @y float, @z float) 
------------------------------------------------------------------
--/H Convert Cartesian coordinates (x, y, z) to Ra, Dec.
--/H <br> (x, y, z) will be normalized unless (x, y, z) is close to (0,0,0)
--/H <br> 
------------------------------------------------------------------
--/T <br>Parameters:
--/T <li>x float --D/ 
--/T <li>y float --D/ 
--/T <li>z float --D/ 
--/T <li> returns Table of one row containing the values (ra, dec)
--/T <samp>select * from fHtmXyzToEq(0.0, 0.0, -1.0)
--/T <br> gives:  ra  dec
--/T <br>          0  -90
--/T </samp>
--/T <br>
-----------------------------------------------------------------
				returns table (ra float, dec float)
as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmXyzToEq
go
GO create function fHtmLatLonToXyz(@ra float, @dec float)
------------------------------------------------------------------
--/H Convert Lat, Lon to Cartesian coordinates (x, y, z)
------------------------------------------------------------------
--/T <br>Parameters:
--/T <li>Lat float --D/ Latitude in degrees
--/T <li>Lon float --D/ Longitude in degrees
--/T <li> returns Table of one row containing the values (x, y, z)
--/T <samp>select * from fHtmLatLonToXyz(0.0, 270.0)
--/T <br> gives:  x  y  z
--/T <br>         0 -1  0
--/T </samp>
--/T <br>
-----------------------------------------------------------------
				returns table (x float, y float, z float)
as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmLatLonToXyz
go
GO create function fHtmXyzToLatLon(@x float, @y float, @z float)
------------------------------------------------------------------
--/H Convert Cartesian coordinates (x, y, z) to Lat/Lon pair.
--/H <br> (x, y, z) will be normalized unless (x, y, z) is close to (0,0,0)
--/H <br> 
------------------------------------------------------------------
--/T <br>Parameters:
--/T <li>x float --D/ 
--/T <li>y float --D/ 
--/T <li>z float --D/ 
--/T <li> returns Table of one row containing the values (Lat, Lon)
--/T <samp>select * from fHtmXyzToLatLon(0.0, 0.0, -1.0)
--/T <br> gives:  Lat Lon
--/T <br>         -90   0
--/T </samp>
--/T <br>
-----------------------------------------------------------------
				returns table (Lat float, Lon float)
as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmXyzToLatLon
go
GO create function fHtmCoverCircleLatLon (@Lat float, @Lon float, @radiusArcMinutes float) 
	           returns table  (HtmIDStart bigint, HtmIDEnd bigint)
------------------------------------------------------------- 
--/H Returns HTM TrixelTable covering a circle centered at J2000 Lon,Lat (in degrees)
--/H <br> within  @radiusArcMinutes arcminutes of that centerpoint.
------------------------------------------------------------- 
--/T <li> Lat float NOT NULL,          --/D Latitude will be nomalized to [-90..90] --/U deg --/K POS_LIMIT
--/T <li> Lon float NOT NULL,         --/D Longitude will be nomalized to [0..360] --/U deg --/K POS_LIMIT
--/T <li> radiusArcMinutes float NOT NULL,  --/D radius in arcminutes --/U arcmin --/K POS_LIMIT
--/T <li> returns trixel table(HtmIDStart bigint, HtmIDEnd bigint)
--/T <br>	HtmIDStart bigint        --/D 21-deep HtmID range start --/K CODE_HTM
--/T <br>	HtmIDEnd   bigint        --/D 21-deep HtmID range end --/K CODE_HTM
--/T <br> 
--/T Sample call    
--/T <br><samp> 
--/T <br> select * from fHtmCoverCircleEq( 0, 190, 1)
--/T </samp>  
--/T <br> see also fHtmCoverCircleXyz, fHtmCover
------------------------------------------------------------- 
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmCoverCircleLatLon 
-- select * from fHtmCoverCircleEq( 0, 190,1)
go
GO create function fHtmCoverCircleEq (@ra float, @dec float, @radiusArcMinutes float) 
	           returns table  (HtmIDStart bigint, HtmIDEnd bigint)
------------------------------------------------------------- 
--/H Returns HTM TrixelTable covering a circle centered at J2000 ra,dec (in degrees)
--/H <br> within  @radiusArcMinutes arcminutes of that centerpoint.
------------------------------------------------------------- 
--/T <li> ra  float NOT NULL,          --/D J2000 right ascension will be nomalized to [0..360] --/U deg --/K POS_LIMIT
--/T <li> dec float NOT NULL,          --/D J2000 right declination will be nomalized to [-90..90] --/U deg --/K POS_LIMIT
--/T <li> radiusArcMinutes float NOT NULL,  --/D radius in arcminutes --/U arcmin --/K POS_LIMIT
--/T <li> returns trixel table(HtmIDStart bigint, HtmIDEnd bigint)
--/T <br>	HtmIDStart bigint        --/D 21-deep HtmID range start --/K CODE_HTM
--/T <br>	HtmIDEnd   bigint        --/D 21-deep HtmID range end --/K CODE_HTM
--/T <br> 
--/T Sample call    
--/T <br><samp> 
--/T <br> select * from fHtmCoverCircleEq( 190,0,1)
--/T </samp>  
--/T <br> see also fHtmCoverCircleXyz, fHtmCover
------------------------------------------------------------- 
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmCoverCircleEq 
-- select * from fHtmCoverCircleEq( 190,0,1)
go
GO create function fHtmCoverCircleXyz(@x float, @y float, @z float, @r float) 
	           returns table  (HtmIDStart bigint, HtmIDEnd bigint)
------------------------------------------------------------- 
--/H Returns HTM TrixelTable covering a circle centered at CARTESIAN x,y,z 
--/H <br> within  @radiusArcMinutes arcminutes of that centerpoint.
------------------------------------------------------------- 
--/T <li> x float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_X
--/T <li> y float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_Y
--/T <li> z float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_Z
--/T <li> r float NOT NULL,      --/D radius in arcminutes --/U arcmin --/K POS_LIMIT
--/T <li> returns trixel table(HtmIDStart bigint, HtmIDEnd bigint)
--/T <br>	HtmIDStart bigint        --/D 21-deep HtmID range start --/K CODE_HTM
--/T <br>	HtmIDEnd   bigint        --/D 21-deep HtmID range end   --/K CODE_HTM
--/T <br> 
--/T Sample call    
--/T <br><samp> 
--/T <br> declare @htmID bigint
--/T <br> select * from fHtmCoverCircleXyz( 1,0,0, 1)
--/T </samp>  
--/T <br> see also fHtmCoverCircleEq fHtmCover 
------------------------------------------------------------- 
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmCoverCircleXyz
-- select * from fHtmCoverCircleXyz( 1,0,0,1)
go
GO create function fHtmCoverRegion(@region nvarchar(max)) 
---------------------------------------------------------------------
--/H Returns HTM TrixelTable covering a designated region.
-------------------------------------------------------------
--/T Regions have the syntax
--/T <pre>
--/T circleSpec  =>     CIRCLE LATLON    lat lon radArcMin  
--/T            |       CIRCLE CARTESIAN ra dec  radArcMin  
--/T            |       CIRCLE CARTESIAN x y z   radArcMin
--/T rectSpec    =>     RECT LATLON     {lat lon}2
--/T            |       RECT J2000      {ra dec }2
--/T            |       RECT CARTESIAN  {x y z  }2
--/T polySpec    =>     POLY LATLON     {lat lon}3+
--/T            |       POLY J2000      {ra dec }3+
--/T            |       POLY CARTESIAN  {x y z  }3+
--/T hullSpec    =>     CHULL LATLON    {lat lon}3+
--/T            |       CHULL J2000     {ra dec }3+
--/T            |       CHULL CARTESIAN {x y z  }3+
--/T convexSpec	 =>     CONVEX { x y z D}*
--/T coverSpec	 =>     circleSpec | rectSpec | polySpec | hullSpec | convexSpec
--/T regionSpec	 =>     REGION {coverSpec}* | coverspec 
--/T </pre> 
--/T <p>returned table:  
--/T <li> returns trixel table(start bigint, end bigint)
--/T <br>	HtmIDStart bigint        --/D 21-deep HtmID range start --/K CODE_HTM
--/T <br>	HtmIDEnd   bigint        --/D 21-deep HtmID range end   --/K CODE_HTM
--/T <br> 
--/T <br> Sample call to find htm triangles covering area withn 5 arcminutes of xyz -.0996,-.1,0
--/T <br><samp>
--/T <br>select * from fHtmRegionCover('REGION CIRCLE CARTESIAN -.996 -.1 0 5')  
--/T </samp>  
--/T <br>see also fHtmRegionError 
	           returns table  (HtmIDStart bigint, HtmIDEnd bigint)
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmCoverRegion
go
GO create function fHtmRegionToTable (@cover nvarchar(max))
---------------------------------------------------------------------
--/H Returns a table of 6 columns for each halfspace in the cover.
-------------------------------------------------------------
--/T The cover has the same syntax as for fHtmCover
--/T <p>returned table:  
--/T <li> returns halfspace table(
--/T <br>         convexID bigint, halfspaceID bigint, -- convex and halfspace IDs
--/T <br>         x float, y float, z float, D float) -- the halfspace normal vector
--/T <br> Sample call 
--/T <br><samp>
--/T <br>select * from fHtmRegionToTable('REGION CIRCLE CARTESIAN -.996 -.1 0 5')  
--/T </samp>  
--/T <br>see also fHtmCoverError, fHtmCover
returns table (convexID bigint, halfspaceID bigint, x float, y float, z float, D float)
as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmRegionToTable
go
GO create function fHtmRegionToNormalFormString(@region nvarchar(max)) returns nvarchar(max)
---------------------------------------------------------------------
--/H  Returns normal form of a region definition in terms of convex hulls unions of convexes of halfspaces.
--/H  Each convex is simplified (no redundant constraints). 
---------------------------------------------------------------------
--/T Regions have the syntax
--/T <pre>
--/T circleSpec  =>     CIRCLE LATLON    lat lon radArcMin  
--/T            |       CIRCLE CARTESIAN ra dec  radArcMin  
--/T            |       CIRCLE CARTESIAN x y z   radArcMin
--/T rectSpec    =>     RECT LATLON     {lat lon}2
--/T            |       RECT J2000      {ra dec }2
--/T            |       RECT CARTESIAN  {x y z  }2
--/T polySpec    =>     POLY LATLON     {lat lon}3+
--/T            |       POLY J2000      {ra dec }3+
--/T            |       POLY CARTESIAN  {x y z  }3+
--/T hullSpec    =>     CHULL LATLON    {lat lon}3+
--/T            |       CHULL J2000     {ra dec }3+
--/T            |       CHULL CARTESIAN {x y z  }3+
--/T convexSpec	 =>     CONVEX { x y z D}*
--/T coverSpec	 =>     circleSpec | rectSpec | polySpec | hullSpec | convexSpec
--/T regionSpec	 =>     REGION {coverSpec}* | coverspec 
--/T </pre>
--/T <br> returns nvarchar(max) string of the form REGION CONVEX x1 y1 z1 x2 y2 z2 x3 y3 z3 CONVEX ...
--/T <br>                or REGION  if region is null
--/T <br>  or empty string if region definition is in error.  
--/T  <li>Example: 
--/T  <samp>select dbo.fHtmRegionToNormalFormString('CIRCLE CARTESIAN -.996 -.1 0 2')</samp>
--/T  <br>
--/T <br> see also fHtmRegionError 
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmRegionToNormalFormString
go
GO create function fHtmRegionObjects(@region nvarchar(max), @type char(1))
---------------------------------------------------------------------
--/H Returns list of objects of give type that are in the region.
-------------------------------------------------------------
--/T Regions have the syntax
--/T <pre>
--/T circleSpec  =>     CIRCLE LATLON    lat lon radArcMin  
--/T            |       CIRCLE CARTESIAN ra dec  radArcMin  
--/T            |       CIRCLE CARTESIAN x y z   radArcMin
--/T rectSpec    =>     RECT LATLON     {lat lon}2
--/T            |       RECT J2000      {ra dec }2
--/T            |       RECT CARTESIAN  {x y z  }2
--/T polySpec    =>     POLY LATLON     {lat lon}3+
--/T            |       POLY J2000      {ra dec }3+
--/T            |       POLY CARTESIAN  {x y z  }3+
--/T hullSpec    =>     CHULL LATLON    {lat lon}3+
--/T            |       CHULL J2000     {ra dec }3+
--/T            |       CHULL CARTESIAN {x y z  }3+
--/T convexSpec	 =>     CONVEX { x y z D}*
--/T coverSpec	 =>     circleSpec | rectSpec | polySpec | hullSpec | convexSpec
--/T regionSpec	 =>     REGION {coverSpec}* | coverspec 
--/T </pre> 
--/T <br> 
--/T Second parameter is Type char(1) S: station (river flow),  P: Place (city)
--/T <p>returned table:  
--/T <li> returns object table(objID bigint primary key)
--/T <br>	ObjID bigint             --/D objID is Station Number for Type='S' (Station)
--/T <br>                            --/D objID is HtmID for type = 'P' (Place) 
--/T <br> 
--/T <br> Sample call to find places in Colorado
--/T <br><samp>
--/T <br> select *			-- find Colorado places.
--/T <br> from Place    
--/T <br> where HtmID in
--/T <br>  (select objID 
--/T <br>  from fHtmRegionObjects('RECT LATLON 37 -109.55  41 -102.05','P'))  
--/T </samp>  
--/T <br>see also fHtmRegionError 
   returns @Objects TABLE (objID bigint primary key)
as	begin
	insert @Objects
		select distinct SI.ObjID
		from fHtmCoverRegion(@region)  
			loop join SpatialIndex SI
				on SI.HtmID between HtmIDStart and HtmIDEnd 
					and SI.Type = @type
			cross join fHtmRegionToTable(@region) Poly
		group by SI.ObjID, Poly.convexID 
		having min(SI.x *Poly.x + SI.y * Poly.y + SI.z * Poly.z - Poly.D) >= 0	
		OPTION(FORCE ORDER)
	return 
	end
go
GO create function fHtmRegionError(@region nvarchar(max)) returns nvarchar(max)
---------------------------------------------------------------------
--/H Returns error message describing whyat is wrong with @region. 
-------------------------------------------------------------
--/T Regions have the syntax
--/T <pre>
--/T circleSpec  =>     CIRCLE LATLON    lat lon radArcMin  
--/T            |       CIRCLE CARTESIAN ra dec  radArcMin  
--/T            |       CIRCLE CARTESIAN x y z   radArcMin
--/T rectSpec    =>     RECT LATLON     {lat lon}2
--/T            |       RECT J2000      {ra dec }2
--/T            |       RECT CARTESIAN  {x y z  }2
--/T polySpec    =>     POLY LATLON     {lat lon}3+
--/T            |       POLY J2000      {ra dec }3+
--/T            |       POLY CARTESIAN  {x y z  }3+
--/T hullSpec    =>     CHULL LATLON    {lat lon}3+
--/T            |       CHULL J2000     {ra dec }3+
--/T            |       CHULL CARTESIAN {x y z  }3+
--/T convexSpec	 =>     CONVEX { x y z D}*
--/T coverSpec	 =>     circleSpec | rectSpec | polySpec | hullSpec | convexSpec
--/T regionSpec	 =>     REGION {coverSpec}* | coverspec 
--/T </pre> 
--/T <li> returns "OK" if string is OK,  
--/T      returns string giving the above syntax if the region description is in error. 
--/T <br> 
--/T Sample call    
--/T <br><samp> 
--/T <br> declare @errorMsg nvarchar(max)
--/T <br> select @errorMsg = dbo.fHtmRegionError('CIRCLE LATLON 190')
--/T </samp>  
--/T <br>see also fHtmRegionCover, fHtmRegionToNormalForm, fHtmRegionToTable
    as external name Spatial.[Microsoft.Samples.SqlServer.Sql].fHtmRegionError
go
GO create function fHtmNearbyLatLon(@type char(1), @Lat float, @Lon float, @r float)
-------------------------------------------------------------
--/H Returns table of objects of the given type within @r arcmins of an xyz point (@Lat,@Lon).
-------------------------------------------------------------
--/T <li> type char(1) NOT NULL, --/D type of station: either 'P' for place or 'S' for stream flow station
--/T <li> Lat float NOT NULL,    --/D Latitude of center point  
--/T <li> Lon float NOT NULL,    --/D Longitude of center point 
--/T <li> r float NOT NULL,      --/D radius in arcminutes --/U arcmin --/K POS_LIMIT
--/T There is no limit on the number of objects returned.
--/T <p>returned table has the same format as the spatail index (minus the type field):  
--/T <li> htmID bigint,               -- Hierarchical Trangular Mesh id of this object
--/T <li> Lat, Lon float not null,    -- Latitude and Longitude (dec/ra) of point.   
--/T <li> x,y,z float not null,       -- x,y,z of unit vector to this object
--/T <li> objID bigint,               -- object ID in SpatialIndex table. 
--/T <li> distance float              -- distance in arc minutes to this object from the ra,dec.
--/T <br> Sample call to find places within 50 arcminutes of SQL Server Development. 
--/T <br><samp>
--/T <br>select *
--/T <br> from fHtmNearbyLatLon('P',47.5,-122) 
--/T </samp>  
--/T <br>see also fHtmNearbyEq, fHtmNearbyXYZ, fHtmNearestLatLon
-------------------------------------------------------------
  returns @SpatialIndex table (
					HtmID	bigint NOT NULL,
					Lat		float  NOT NULL,
					Lon		float  NOT NULL,
					x		float  NOT NULL,
					y		float  NOT NULL,
					z		float  NOT NULL,
 					ObjID	bigint NOT NULL,
					distance float NOT NULL -- distance in arc minutes
  ) as begin
	if (@r<0) RETURN
	-- compute x,y,z to make distance test fast.
	declare @x float, @y float, @z float
	select	@x = cos(radians(@Lat))*cos(radians(@Lon)),
			@y = cos(radians(@Lat))*sin(radians(@Lon)),
			@z = sin(radians(@Lat))
	-- do the spatial join using the HTM cover. 
	insert @SpatialIndex select 
	    HtmID, 
	    Lat,Lon,
	    x,y,z,
	    ObjID,
 	    2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60 
	    --sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/PI()/3 
	    from fHtmCoverCircleXyz(@x,@y,@z, @r) join SpatialIndex 
			on HtmID BETWEEN  HtmIDStart AND HtmIDEnd  
            and [Type]  = @type
--          this clause is simplified since it is innner loop.
--	        and( (2*DEGREES(ASIN(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60)< @r)
			and (power(@x-x,2)+power(@y-y,2)+power(@z-z,2)) < power(2*sin(radians(@r/120)),2) 
	order by (2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60) asc
	OPTION(FORCE ORDER, LOOP JOIN)
	return
	end
go
GO create function fHtmNearbyEq (@type char(1), @ra float, @dec float, @r float)
-------------------------------------------------------------
--/H Returns table of objects of the given type within @r arcmins of a ra/dec point.
-------------------------------------------------------------
--/T <li> type char(1) NOT NULL,      --/D type of station: either 'P' for place or 'S' for stream flow station
--/T <li> ra float NOT NULL,          --/D right ascension --/K POS_EQ_RA
--/T <li> dec float NOT NULL,         --/D declination --/K POS_EQ_DEC
--/T There is no limit on the number of objects returned.
--/T <p>returned table has the same format as the spatail index (minus the type field):  
--/T <li> htmID bigint,               -- Hierarchical Trangular Mesh id of this object
--/T <li> ra, dec float not null,     -- ra/dec of point.   
--/T <li> x,y,z float not null,       -- x,y,z of unit vector to this object
--/T <li> objID bigint,               -- object ID in SpatialIndex table. 
--/T <li> distance float              -- distance in arc minutes to this object from the ra,dec.
--/T <br> Sample call to find places within 50 arcminutes of SQL Server Development. 
--/T <br><samp>
--/T <br>select *
--/T <br> from fHtmNearbyEq('P', -122, 47.5) 
--/T </samp>  
--/T <br>see also fHtmNearbyLatLon, fHtmNearbyXYZ, fHtmNearestXYZ
-------------------------------------------------------------
  returns @SpatialIndex table (
					HtmID	bigint NOT NULL,
					ra		float  NOT NULL,
					dec		float  NOT NULL,
					x		float  NOT NULL,
					y		float  NOT NULL,
					z		float  NOT NULL,
 					ObjID	bigint NOT NULL,
					distance float NOT NULL -- distance in arc minutes
  ) as begin
	if (@r<0) RETURN
	-- compute x,y,z to make distance test fast.
	declare @x float, @y float, @z float
	select	@x = cos(radians(@dec))*cos(radians(@ra)),
			@y = cos(radians(@dec))*sin(radians(@ra)),
			@z = sin(radians(@dec))
	-- do the spatial join using the HTM cover. 
	insert @SpatialIndex select 
	    HtmID, 
	    Lon,Lat,
	    x,y,z,
	    ObjID,
 	    2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60 
	    --sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/PI()/3 
	    from fHtmCoverCircleXyz(@x,@y,@z, @r) join SpatialIndex 
			on HtmID BETWEEN  HtmIDStart AND HtmIDEnd  
            and [Type]  = @type
--          this clause is simplified since it is innner loop.
--	        and( (2*DEGREES(ASIN(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60)< @r)
			and (power(@x-x,2)+power(@y-y,2)+power(@z-z,2)) < power(2*sin(radians(@r/120)),2) 
	order by (2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60) asc
	OPTION(FORCE ORDER, LOOP JOIN)
	return
	end
go
GO create function fHtmNearbyXYZ (@type char(1), @x float, @y float, @z float, @r float)
-------------------------------------------------------------
--/H Returns table of objects of the given type within @r arcmins of an xyz point (@x,@y,@z).
-------------------------------------------------------------
--/T <li> type char(1) NOT NULL, --/D type of station: either 'P' for place or 'S' for stream flow station
--/T <li> x float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_X
--/T <li> y float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_Y
--/T <li> z float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_Z
--/T <li> r float NOT NULL,      --/D radius in arcminutes --/U arcmin --/K POS_LIMIT
--/T There is no limit on the number of objects returned.
--/T <p>returned table has the same format as the spatail index (minus the type field):  
--/T <li> htmID bigint,               -- Hierarchical Trangular Mesh id of this object
--/T <li> Lat, Lon float not null,    -- Latitude and Longitude (dec/ra) of point.   
--/T <li> x,y,z float not null,       -- x,y,z of unit vector to this object
--/T <li> objID bigint,               -- object ID in SpatialIndex table. 
--/T <li> distance float              -- distance in arc minutes to this object from the ra,dec.
--/T <br> Sample call to find places within 50 arcminutes of Baltimore. 
--/T <br><samp>
--/T <br>select *
--/T <br> from fHtmNearbyXYZ('P', 0.179195, -0.752798, 0.633392, 50) 
--/T </samp>  
--/T <br>see also fHtmNearbyLatLon, fHtmNearbyEq, fHtmNearestXYZ
-------------------------------------------------------------
  returns @SpatialIndex table (
					HtmID	bigint NOT NULL,
					Lat		float  NOT NULL,
					Lon		float  NOT NULL,
					x		float  NOT NULL,
					y		float  NOT NULL,
					z		float  NOT NULL,
 					ObjID	bigint NOT NULL,
					distance float NOT NULL -- distance in arc minutes
  ) as begin
	if (@r<0) RETURN
	-- do the spatial join using the HTM cover. 
	insert @SpatialIndex select 
	    HtmID, 
	    Lat,Lon,
	    x,y,z,
	    ObjID,
 	    2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60 
	    --sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/PI()/3 
	    from fHtmCoverCircleXyz(@x,@y,@z, @r) join SpatialIndex 
			on HtmID BETWEEN  HtmIDStart AND HtmIDEnd  
            and [Type]  = @type
--          this clause is simplified since it is innner loop.
--	        and( (2*DEGREES(ASIN(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60)< @r)
			and (power(@x-x,2)+power(@y-y,2)+power(@z-z,2)) < power(2*sin(radians(@r/120)),2) 
	order by (2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60) asc
	OPTION(FORCE ORDER, LOOP JOIN)
	return
	end
go
GO create function fHtmNearestLatLon(@type char(1), @Lat float, @Lon float)
-------------------------------------------------------------
--/H Returns table of objects of the given type within @r arcmins of a ra/dec point.
-------------------------------------------------------------
--/T <li> Lat float NOT NULL,          --/D Latitude (decimal degrees) 
--/T <li> Lon float NOT NULL,         --/D Longitude (decimal degrees) 
--/T <p> One object is returned. 
--/T <br>returned table has the same format as the spatail index (minus the type field):  
--/T <li> htmID bigint,               -- Hierarchical Trangular Mesh id of this object
--/T <li> Lat, Lon float not null,    -- Latitude and Longitude (dec/ra) of point.   
--/T <li> x,y,z float not null,       -- x,y,z of unit vector to this object
--/T <li> objID bigint,               -- object ID in SpatialIndex table. 
--/T <li> distance float              -- distance in arc minutes to this object from the ra,dec.
--/T <br> Sample call to find nearest place to Baltimore (which is Baltimore (!)). 
--/T <br><samp>
--/T <br> select distance, Place.*
--/T <br> from fHtmNearestLatLon('P', 39.3, -76.6) I join Place on I.objID = Place.HtmID
--/T </samp>  
--/T <br>see also fHtmNearbyLatLon, fHtmNearbyEq, fHtmNearestXYZ
-------------------------------------------------------------
  returns @SpatialIndex table (
					HtmID	bigint NOT NULL,
					Lat		float  NOT NULL,
					Lon		float  NOT NULL,
					x		float  NOT NULL,
					y		float  NOT NULL,
					z		float  NOT NULL,
 					ObjID	bigint NOT NULL,
					distance float NOT NULL -- distance in arc minutes
  ) as begin
	declare @x float, @y float, @z float, @r  float
	select	@x = cos(radians(@Lat))*cos(radians(@Lon)),
			@y = cos(radians(@Lat))*sin(radians(@Lon)),
			@z = sin(radians(@Lat)),	
			@r = 1
-- Try r = 1, 4, 14,.... till you find a non null set.
-- do the spatial join using the HTM cover. 
retry:
	insert @SpatialIndex 
		select top 1 
	    HtmID, 
	    Lat,Lon,
	    x,y,z,
	    ObjID,
 	    2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60 
	    --sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/PI()/3 
	    from fHtmCoverCircleXyz(@x,@y,@z, @r) join SpatialIndex 
			on HtmID BETWEEN  HtmIDStart AND HtmIDEnd  
            and [Type]  = @type
--          this clause is simplified since it is innner loop.
--	        and( (2*DEGREES(ASIN(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60)< @r)
			and (power(@x-x,2)+power(@y-y,2)+power(@z-z,2)) < power(2*sin(radians(@r/120)),2) 
		order by (2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60) asc
		OPTION(FORCE ORDER, LOOP JOIN)
		
	if (@@rowcount = 0)
		begin
		set @r = @r * 4
		goto retry
		end
	return
	end
go
GO create function fHtmNearestEq (@type char(1), @ra float, @dec float)
-------------------------------------------------------------
--/H Returns table of objects of the given type within @r arcmins of a ra/dec point.
-------------------------------------------------------------
--/T <li> ra float NOT NULL,          --/D right ascension --/K POS_EQ_RA
--/T <li> dec float NOT NULL,         --/D declination     --/K POS_EQ_DEC
--/T <p> One object is returned. 
--/T <br>returned table has the same format as the spatail index (minus the type field):  
--/T <li> htmID bigint,               -- Hierarchical Trangular Mesh id of this object
--/T <li> Lat, Lon float not null,    -- Latitude and Longitude (dec/ra) of point.   
--/T <li> x,y,z float not null,       -- x,y,z of unit vector to this object
--/T <li> objID bigint,               -- object ID in SpatialIndex table. 
--/T <li> distance float              -- distance in arc minutes to this object from the ra,dec.
--/T <br> Sample call to find nearest place to Baltimore (which is Baltimore (!)). 
--/T <br><samp>
--/T <br> select distance, Place.*
--/T <br> from fHtmNearestEq('P', -76.6, 39.3) I join Place on I.objID = Place.HtmID
--/T </samp>  
--/T <br>see also fHtmNearbyLatLon, fHtmNearbyEq, fHtmNearestXYZ
-------------------------------------------------------------
  returns @SpatialIndex table (
					HtmID	bigint NOT NULL,
					ra		float  NOT NULL,
					dec		float  NOT NULL,
					x		float  NOT NULL,
					y		float  NOT NULL,
					z		float  NOT NULL,
 					ObjID	bigint NOT NULL,
					distance float NOT NULL -- distance in arc minutes
  ) as begin
	declare @x float, @y float, @z float, @r  float 
	select	@x = cos(radians(@dec))*cos(radians(@ra)),
			@y = cos(radians(@dec))*sin(radians(@ra)),
			@z = sin(radians(@dec)),	
			@r = 1
-- Try r = 1, 4, 14,.... till you find a non null set.
-- do the spatial join using the HTM cover. 
retry:
	insert @SpatialIndex 
		select top 1 
	    HtmID, 
	    Lon, Lat,
	    x,y,z,
	    ObjID,
 	    2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60 
	    --sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/PI()/3 
	    from fHtmCoverCircleXyz(@x,@y,@z, @r) join SpatialIndex 
			on HtmID BETWEEN  HtmIDStart AND HtmIDEnd  
            and [Type]  = @type
--          this clause is simplified since it is innner loop.
--	        and( (2*DEGREES(ASIN(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60)< @r)
			and (power(@x-x,2)+power(@y-y,2)+power(@z-z,2)) < power(2*sin(radians(@r/120)),2) 
		order by (2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60) asc
		OPTION(FORCE ORDER, LOOP JOIN)
		
	if (@@rowcount = 0)
		begin
		set @r = @r * 4
		goto retry
		end
	return
	end
go
GO create function fHtmNearestXYZ (@type char(1), @x float, @y float, @z float)
-------------------------------------------------------------
--/H Returns table containing object of the given type closest to the Cartesian point (@x,@y,@z).
-------------------------------------------------------------
--/T <li> type char(1) NOT NULL, --/D type of station: either 'P' for place or 'S' for stream flow station
--/T <li> x float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_X
--/T <li> y float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_Y
--/T <li> z float NOT NULL,      --/D unit vector for ra+dec --/K POS_EQ_CART_Z
--/T <P>One object is returned. 
--/T <br>returned table has the same format as the spatail index (minus the type field):  
--/T <li> htmID bigint,               -- Hierarchical Trangular Mesh id of this object
--/T <li> Lat, Lon float not null,    -- Latitude and Longitude (dec/ra) of point.   
--/T <li> x,y,z float not null,       -- x,y,z of unit vector to this object
--/T <li> objID bigint,               -- object ID in SpatialIndex table. 
--/T <li> distance float              -- distance in arc minutes to this object from the ra,dec.
--/T <br> Sample call to find nearest place to Baltimore (which is Baltimore (!)). 
--/T <br><samp>
--/T <br> select distance, Place.*
--/T <br> from fHtmNearestXYZ('P', 0.179195, -0.752798, 0.633392) I join Place on I.objID = Place.HtmID
--/T </samp>  
--/T <br>see also fHtmNearbyLatLon, fHtmNearbyEq, fHtmNearestXYZ
-------------------------------------------------------------
  returns @SpatialIndex table (
					HtmID	bigint NOT NULL,
					Lat		float  NOT NULL,
					Lon		float  NOT NULL,
					x		float  NOT NULL,
					y		float  NOT NULL,
					z		float  NOT NULL,
 					ObjID	bigint NOT NULL,
					distance float NOT NULL -- distance in arc minutes
  ) as begin
	declare @r  float
	set @r = 1
	-- Try r = 1, 4, 14,.... till you find a non null set.
	-- do the spatial join using the HTM cover. 
retry:
	insert @SpatialIndex 
		select top 1 
	    HtmID, 
	    Lat,Lon,
	    x,y,z,
	    ObjID,
 	    2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60 
	    --sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/PI()/3 
	    from fHtmCoverCircleXYZ(@x,@y,@z, @r) join SpatialIndex 
			on HtmID BETWEEN  HtmIDStart AND HtmIDEnd  
            and [Type]  = @type
--          this clause is simplified since it is innner loop.
--	        and( (2*DEGREES(ASIN(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60)< @r)
			and (power(@x-x,2)+power(@y-y,2)+power(@z-z,2)) < power(2*sin(radians(@r/120)),2) 
		order by (2*degrees(asin(sqrt(power(@x-x,2)+power(@y-y,2)+power(@z-z,2))/2))*60) asc
		OPTION(FORCE ORDER, LOOP JOIN)
		
	if (@@rowcount = 0)
		begin
		set @r = @r * 4
		goto retry
		end
	return
	end
go
/*
select dbo.fHtmEq(195,0)
select * from fHtmCover('CIRCLE J2000 195 0 1')
select * from fHtmCoverCircleEq(195,0,1)
select * from fHtmCover('REGION CARTESIAN CONVEX 1 0 0 -0.6 0 1 0 -0.6 0 0 1 -0.6 -1 0 0 -0.6 0 -1 0 -0.6 0 0 -1 -0.6')
select dbo.fHtmToNormalForm('REGION CARTESIAN CONVEX 1 0 0 -0.6 0 1 0 -0.6 0 0 1 -0.6 -1 0 0 -0.6 0 -1 0 -0.6 0 0 -1 -0.6')
*/
go
declare @version varchar(max)
select @version = dbo.fHtmVersion()
PRINT '[spHtmCsharp.sql]: Version: ' + @version + ' of the HTM library successfully installed.'
GO
