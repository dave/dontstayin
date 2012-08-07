IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Place_SpatialIndex_Population') BEGIN 	DROP INDEX IX_Place_SpatialIndex_Population ON Place END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Place_SpatialIndex_Name') BEGIN 	DROP INDEX IX_Place_SpatialIndex_Name ON Place END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Venue_SpatialIndex_Name') BEGIN 	DROP INDEX IX_Venue_SpatialIndex_Name ON Venue END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Venue_SpatialIndex_Capacity') BEGIN 	DROP INDEX IX_Venue_SpatialIndex_Capacity ON Venue END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Venue_SpatialIndex_TotalEvents') BEGIN 	DROP INDEX IX_Venue_SpatialIndex_TotalEvents ON Venue END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Gallery_SpatialIndex_LastLiveDateTime') BEGIN 	DROP INDEX IX_Gallery_SpatialIndex_LastLiveDateTime ON Gallery END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Thread_SpatialIndex_LastPost') BEGIN 	DROP INDEX IX_Thread_SpatialIndex_LastPost ON Thread END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Article_SpatialIndex_AddedDateTime') BEGIN 	DROP INDEX IX_Article_SpatialIndex_AddedDateTime ON Article END
IF EXISTS (SELECT TOP 1 * FROM sys.indexes WHERE name = 'IX_Event_SpatialIndex_DateTime_Name') BEGIN 	DROP INDEX IX_Event_SpatialIndex_DateTime_Name ON Event END

IF EXISTS (SELECT TOP 1 * FROM sys.tables t inner join sys.columns c on t.object_id = c.object_id WHERE t.name = 'Article' and c.Name = 'PosZ') BEGIN 
	ALTER TABLE dbo.Article	DROP COLUMN HtmId, Lat, Lon, PosX, PosY, PosZ
END
IF EXISTS (SELECT TOP 1 * FROM sys.tables t inner join sys.columns c on t.object_id = c.object_id WHERE t.name = 'Gallery' and c.Name = 'PosZ') BEGIN 
	ALTER TABLE dbo.Gallery	DROP COLUMN HtmId, Lat, Lon, PosX, PosY, PosZ
END
IF EXISTS (SELECT TOP 1 * FROM sys.tables t inner join sys.columns c on t.object_id = c.object_id WHERE t.name = 'Thread' and c.Name = 'PosZ') BEGIN 
	ALTER TABLE dbo.Thread	DROP COLUMN HtmId, Lat, Lon, PosX, PosY, PosZ
END
IF EXISTS (SELECT TOP 1 * FROM sys.tables t inner join sys.columns c on t.object_id = c.object_id WHERE t.name = 'Venue' and c.Name = 'PosZ') BEGIN 
	ALTER TABLE dbo.Venue	DROP COLUMN PosX, PosY, PosZ
END
IF EXISTS (SELECT TOP 1 * FROM sys.tables t inner join sys.columns c on t.object_id = c.object_id WHERE t.name = 'Place' and c.Name = 'PosZ') BEGIN 
	ALTER TABLE dbo.Place	DROP COLUMN PosX, PosY, PosZ
END
IF EXISTS (SELECT TOP 1 * FROM sys.tables t inner join sys.columns c on t.object_id = c.object_id WHERE t.name = 'Event' and c.Name = 'PosZ') BEGIN 
	ALTER TABLE dbo.Event	DROP COLUMN PosX, PosY, PosZ
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveFunnyChars]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[RemoveFunnyChars]
go
CREATE FUNCTION dbo.RemoveFunnyChars (@INPUT VARCHAR(MAX)) RETURNS VARCHAR(MAX)
AS
BEGIN
	RETURN REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(@INPUT, 'â', 'a'),'ã', 'a'),'-', '-'),'û', 'u'),'ñ', 'n'),'é', 'e'),'ü', 'u'),'´', ''),	'à', 'a'),	'ì', 'i'),	'á', 'a'),	'ô', 'o'),	'ú', 'u'),	'è', 'e'),	'ä', 'a'),	'ö', 'o'),	'å', 'a'),	'ø', 'o'),	'ó', 'o'),	'í', 'i'),	'î', 'i')
END
GO

go

GO
IF EXISTS (SELECT * FROM Sys.triggers WHERE NAME = 'PlaceTrigger') DISABLE TRIGGER PlaceTrigger ON Place

GO
--UPDATE P with (rowlock) SET MeridianFeatureID = (
--	SELECT TOP 1
--		f.id FROM MeridianWorldData..Feature f 
--		inner JOIN MeridianWorldData.dbo.Country mc ON mc.ISO3166NumericCode = c.Code3Number
--		WHERE f.Name = dbo.RemoveFunnyChars(p.Name) collate Latin1_General_CI_AS 
--		ORDER BY (f.lat - p.latitudedegreesNorth) * (f.lat - p.latitudedegreesNorth) + 
--		(f.lon + p.longitudedegreeswest) * (f.lon + p.longitudedegreeswest)
--)
--FROM Place p INNER JOIN Country c (NOLOCK) ON p.CountryK = c.K
--WHERE p.MeridianFeatureID IS NULL
--GO

Update Place SET MeridianFeatureId = 4254948 WHERE K = 61068
UPDATE Place SET MeridianFeatureId = 1519583 WHERE K = 64401


--UPDATE	p WITH(ROWLOCK)
--SET		p.Lat = f.Lat,
--		p.Lon = f.Lon,
--		p.HtmId = dbo.fHtmLatLon(f.Lat, f.Lon)
--FROM	Place p
--INNER JOIN MeridianWorldData..Feature f (NOLOCK) ON p.MeridianFeatureId = f.Id
--where HtmID is null
--
--GO

ALTER TABLE Place ALTER COLUMN MeridianFeatureId int not null
ALTER TABLE Place ALTER COLUMN HtmId bigint not null
ALTER TABLE Place ALTER COLUMN Lat float not null
ALTER TABLE Place ALTER COLUMN Lon float not null

GO
IF EXISTS (SELECT * FROM Sys.triggers WHERE NAME = 'PlaceTrigger') ENABLE TRIGGER PlaceTrigger ON Place
go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveFunnyChars]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[RemoveFunnyChars]
go
IF EXISTS (SELECT * FROM Sys.triggers WHERE NAME = 'VenueTrigger') DISABLE TRIGGER VenueTrigger ON Venue
go

UPDATE v
	SET v.HtmId = p.HtmId,
		v.Lat = p.Lat,
		v.Lon = p.Lon
FROM Venue v inner join Place p on v.PlaceK = p.K WHERE v.HtmID IS NULL

go
delete from Venue where placek in (29100,29248,29671,34489,41216,47104,50001,54691,54791,56015,56757,59373,64927,66554)
go


ALTER TABLE Venue ALTER COLUMN HtmId bigint not null
ALTER TABLE Venue ALTER COLUMN Lat float not null
ALTER TABLE Venue ALTER COLUMN Lon float not null

GO 
IF EXISTS (SELECT * FROM Sys.triggers WHERE NAME = 'VenueTrigger') ENABLE TRIGGER VenueTrigger ON Venue
go

IF EXISTS (SELECT * FROM Sys.triggers WHERE NAME = 'EventTrigger') DISABLE TRIGGER EventTrigger ON Event
gO
UPDATE v
	SET v.HtmId = p.HtmId,
		v.Lat = p.Lat,
		v.Lon = p.Lon
FROM Event v inner join Venue p on v.VenueK = p.K WHERE v.HtmID IS NULL OR v.Lat IS NULL Or v.Lon IS NULL

go

IF EXISTS (SELECT * FROM Sys.triggers WHERE NAME = 'EventTrigger') ENABLE TRIGGER EventTrigger ON Event
GO

ALTER TABLE Event ALTER COLUMN HtmId bigint not null
ALTER TABLE Event ALTER COLUMN Lat float not null
ALTER TABLE Event ALTER COLUMN Lon float not null

go



IF NOT EXISTS (SELECT * FROM sys.tables AS tbl
	INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
	INNER JOIN sys.extended_properties AS p ON p.major_id=clmns.object_id AND p.minor_id=clmns.column_id AND p.class=1
	WHERE (p.name=N'MS_Description')and((clmns.name=N'MeridianFeatureId')and((tbl.name=N'Place' and SCHEMA_NAME(tbl.schema_id)=N'dbo'))))

	EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FeatureId in MeridianWorldData database' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Place', @level2type=N'COLUMN',@level2name=N'MeridianFeatureId'
	
