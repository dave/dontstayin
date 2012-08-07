/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Place
	(
	K int NOT NULL IDENTITY (1, 1),
	Name varchar(100) NOT NULL,
	UniqueName AS RTRIM(Name + ' ' + ISNULL(RegionAbbreviation, '')) PERSISTED ,
	Population float(53) NULL,
	LatitudeDegreesNorth float(53) NOT NULL,
	LongitudeDegreesWest float(53) NOT NULL,
	SubCountry int NOT NULL,
	CountryK int NOT NULL,
	Enabled bit NULL,
	Pic uniqueidentifier NULL,
	DetailsHtml text NULL,
	TotalEvents int NULL,
	TotalComments int NULL,
	TotalCommentsShallow int NULL,
	LastPostShallow datetime NULL,
	LastPost datetime NULL,
	AverageCommentDateTime datetime NULL,
	AverageCommentDateTimeShallow datetime NULL,
	RegionAbbreviation varchar(10) NULL,
	RegionK int NOT NULL,
	Code varchar(10) NULL,
	Type varchar(10) NULL,
	IsRegionCapital bit NULL,
	IsCountryCapital bit NULL,
	ExcludeFromMap bit NULL,
	UrlName varchar(100) NOT NULL,
	PicState varchar(100) NULL,
	PicPhotoK int NULL,
	PicMiscK int NULL,
	UrlFragment varchar(255) NOT NULL,
	DoneAmazonPix bit NULL,
	FailedAmazonPix bit NULL,
	FailedAmazonCheck bit NULL,
	MeridianFeatureId int NOT NULL,
	Lat float(53) NOT NULL,
	Lon float(53) NOT NULL,
	HtmId  AS ([dbo].[fHtmLatLon]([Lat],[Lon])) PERSISTED 
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
DECLARE @v sql_variant 
SET @v = N'e.g. Southampton, London'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', NULL, NULL
GO
DECLARE @v sql_variant 
SET @v = N'The primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'K'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Name'
SET @v = N'Place name'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Name'
GO
DECLARE @v sql_variant 
SET @v = N'Unique place name e.g. Springfield TX'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'UniqueName'
GO

DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Population'
SET @v = N'Population of the place in 1000''s'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Population'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'LatitudeDegreesNorth'
SET @v = N'Latitude (degrees north)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'LatitudeDegreesNorth'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'LongitudeDegreesWest'
SET @v = N'Longitude (degrees west)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'LongitudeDegreesWest'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'SubCountry'
SET @v = N'Country (1=England, 2=Scotland, 3=Wales, 4=Northern Ireland)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'SubCountry'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'CountryK'
SET @v = N'Link to the country table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'CountryK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Enabled'
SET @v = N'Whether the place is displayed in the full place list.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Enabled'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Pic'
SET @v = N'Cropped image between 75*75 and 100*100'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Pic'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'DetailsHtml'
SET @v = N'Details displayed on the place page'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'DetailsHtml'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'TotalEvents'
SET @v = N'The total number of events'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'TotalEvents'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'TotalComments'
SET @v = N'The total number of comments'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'TotalComments'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'TotalCommentsShallow'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'LastPostShallow'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'LastPost'
SET @v = N'The date/time of the last post that was posted in this board (including child objects)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'LastPost'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'AverageCommentDateTime'
SET @v = N'The average date.time of all comments posted in this board (including child objects)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'AverageCommentDateTime'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'AverageCommentDateTimeShallow'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'RegionAbbreviation'
SET @v = N'Appended to end of FriendlyName. Usually US State abbreviation.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'RegionAbbreviation'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'RegionK'
SET @v = N'Link to Region table'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'RegionK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Code'
SET @v = N'Any regional place code (e.g. US FIPS code)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Code'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Type'
SET @v = N'Place type - e.g. US CDP/Town/City'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Type'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'IsRegionCapital'
SET @v = N'Is this the capital of the region?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'IsRegionCapital'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'IsCountryCapital'
SET @v = N'Is this the capital of the country?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'IsCountryCapital'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'ExcludeFromMap'
SET @v = N'Leave this place off when drawing the map? (Usefull for outlying islands)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'ExcludeFromMap'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'UrlName'
SET @v = N'Name used in the URL...'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'UrlName'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'PicState'
SET @v = N'State var used to reconstruct cropper when re-cropping'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'PicState'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'PicPhotoK'
SET @v = N'The Photo that was used to create the Pic.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'PicPhotoK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'PicMiscK'
SET @v = N'The Misc that was used to create the Pic.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'PicMiscK'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'UrlFragment'
SET @v = N'The url fragment - so that the url can be generated without accessing parent database records'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'UrlFragment'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'DoneAmazonPix'
SET @v = N'Has the Pix file been uploaded to Amazon?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'DoneAmazonPix'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'FailedAmazonPix'
SET @v = N'Has the Pix file failed to upload to Amazon?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'FailedAmazonPix'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'FailedAmazonCheck'
SET @v = N'Has the Pix file failed to upload to Amazon?'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'FailedAmazonCheck'
GO
DECLARE @v sql_variant 
SET @v = N'true'
EXECUTE sp_addextendedproperty N'IsNotNull', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'MeridianFeatureId'
SET @v = N'FeatureId in MeridianWorldData database'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'MeridianFeatureId'
GO
DECLARE @v sql_variant 
SET @v = N'Latitude'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Lat'
GO
DECLARE @v sql_variant 
SET @v = N'Longitude'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Tmp_Place', N'COLUMN', N'Lon'
GO
SET IDENTITY_INSERT dbo.Tmp_Place ON
GO
IF EXISTS(SELECT * FROM dbo.Place)
	 EXEC('INSERT INTO dbo.Tmp_Place (K, Name, Population, LatitudeDegreesNorth, LongitudeDegreesWest, SubCountry, CountryK, Enabled, Pic, DetailsHtml, TotalEvents, TotalComments, TotalCommentsShallow, LastPostShallow, LastPost, AverageCommentDateTime, AverageCommentDateTimeShallow, RegionAbbreviation, RegionK, Code, Type, IsRegionCapital, IsCountryCapital, ExcludeFromMap, UrlName, PicState, PicPhotoK, PicMiscK, UrlFragment, DoneAmazonPix, FailedAmazonPix, FailedAmazonCheck, MeridianFeatureId, Lat, Lon)
		SELECT K, Name, Population, LatitudeDegreesNorth, LongitudeDegreesWest, SubCountry, CountryK, Enabled, Pic, DetailsHtml, TotalEvents, TotalComments, TotalCommentsShallow, LastPostShallow, LastPost, AverageCommentDateTime, AverageCommentDateTimeShallow, RegionAbbreviation, RegionK, Code, Type, IsRegionCapital, IsCountryCapital, ExcludeFromMap, UrlName, PicState, PicPhotoK, PicMiscK, UrlFragment, DoneAmazonPix, FailedAmazonPix, FailedAmazonCheck, MeridianFeatureId, Lat, Lon FROM dbo.Place WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Place OFF
GO
DROP TABLE dbo.Place
GO
EXECUTE sp_rename N'dbo.Tmp_Place', N'Place', 'OBJECT' 
GO
ALTER TABLE dbo.Place ADD CONSTRAINT
	PK_Town_1 PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX Place40 ON dbo.Place
	(
	K,
	CountryK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX Place31 ON dbo.Place
	(
	CountryK,
	UrlName
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX Place39 ON dbo.Place
	(
	K,
	Pic
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX Place48 ON dbo.Place
	(
	CountryK
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX Place56 ON dbo.Place
	(
	CountryK,
	Enabled,
	Name,
	K,
	RegionAbbreviation
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX Place35 ON dbo.Place
	(
	CountryK,
	TotalEvents DESC,
	Population DESC,
	K,
	Name,
	Pic,
	RegionAbbreviation,
	UrlName,
	UrlFragment
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX Place27 ON dbo.Place
	(
	CountryK,
	Enabled,
	K,
	Name,
	RegionAbbreviation
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Place_SpatialIndex__Name__Lat_Lon ON dbo.Place
	(
	HtmId,
	Name
	) INCLUDE (Lat, Lon) 
 WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX _dta_index_Place_7_71671303__K7_K11_K8_K1_K4_2_18_25_29 ON dbo.Place
	(
	CountryK,
	TotalEvents,
	Enabled,
	K,
	LatitudeDegreesNorth
	) INCLUDE (Name, RegionAbbreviation, UrlName, UrlFragment) 
 WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX _dta_index_Place_8_71671303__K7_K1_2_18_25 ON dbo.Place
	(
	CountryK,
	K
	) INCLUDE (Name, RegionAbbreviation, UrlName) 
 WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
if EXISTS(SELECT * FROM sys.assemblies WHERE name = 'CacheTriggers') BEGIN
	EXEC ('CREATE TRIGGER dbo.PlaceTrigger ON dbo.Place 
	AFTER UPDATE , DELETE , INSERT 
	AS 
		EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].PlaceTrigger')
END
GO
CREATE NONCLUSTERED INDEX IX_Place_UniqueName ON dbo.Place
	(
	UniqueName
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
