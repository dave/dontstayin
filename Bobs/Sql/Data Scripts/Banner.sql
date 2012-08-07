 

DELETE FROM Banner
TRUNCATE TABLE [Banner]
SET IDENTITY_INSERT [Banner] ON

DECLARE @Leaderboard INT SET @Leaderboard = 1
DECLARE @Hotbox INT SET	@Hotbox = 2
DECLARE @EmailBanner INT SET @EmailBanner = 3
DECLARE @PhotoBanner INT SET @PhotoBanner = 4
DECLARE @Skyscraper INT SET @Skyscraper = 5

DECLARE @New INT SET @New = 1
DECLARE @Enabled INT SET @Enabled = 2
DECLARE @Disabled INT SET @Disabled = 3

DECLARE @MinDate DATETIME SET @MinDate = '01 Jan 1900'
DECLARE @MaxDate DATETIME SET @MaxDate = '31 Jan 2200'

DECLARE @ThreeDaysInPast DATETIME SET @ThreeDaysInPast = DateAdd(day, -3, Getdate())
DECLARE @ThreeDaysInFuture DATETIME SET @ThreeDaysInFuture = DateAdd(day, 3, Getdate())

--																																																																K, [Position],	 [FirstDay],				[LastDay],				Statuses	[TotalRequiredImpressions],	[DisplayType],	[MiscK],	[IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK],	[Name],		[PromoterK]
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	1, @Leaderboard, @ThreeDaysInPast,			@ThreeDaysInFuture,		1, 1, 1,	30000,						4,				1,			0,					0,					1,					'Banner1'	,1)
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	2, @Hotbox,		 @ThreeDaysInPast,			@ThreeDaysInFuture,		1, 1, 1,	30000,						4,				2,			0,					0,					1,					'Banner2'	,1)
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	3, @Leaderboard, @ThreeDaysInPast,			@ThreeDaysInFuture,		1, 1, 1,	30000,						4,				3,			0,					0,					1,					'Banner3'	,1)
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	4, @Leaderboard, @MinDate,					@MaxDate,				0, 0, 0,	30000,						4,				4,			0,					0,					1,					'Banner4'	,1)
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	5, @Hotbox,		 @MinDate,					@MaxDate,				0, 0, 0,	30000,						4,				5,			0,					0,					1,					'Banner5'	,1)
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	6, @Leaderboard, @MinDate,					@MaxDate,				0, 0, 0,	30000,						4,				6,			0,					0,					2,					'Banner6'	,1)
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	7, @Hotbox, 	 @MinDate,					DATEADD(d,7, @MinDate), 1, 1, 1,	30000,						4,				7,			0,					0,					2,					'Banner7'	,1)
INSERT INTO [dbo].[Banner] (K, [Position], [FirstDay], [LastDay], [StatusArtwork], [StatusBooked], [StatusEnabled], [TotalRequiredImpressions], [DisplayType], [MiscK], [IsPlaceTargetted], [IsMusicTargetted], [BannerFolderK], [Name], [PromoterK]) VALUES (	8, @Leaderboard, DATEADD(d, -7, @MaxDate),	@MaxDate,				1, 1, 1,	30000,						4,				8,			0,					0,					2,					'Banner8'	,1)




insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (9 , 'Banner9' , 1, 1, 1, 2, 1, 20000000, 3)
insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (10, 'Banner10', 1, 1, 1, 2, 1, 50000, 3)
insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (11, 'Banner11', 1, 1, 1, 2, 1, 30000, 3)
insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (12, 'Banner12', 1, 1, 1, 2, 1, 70000, 4)
insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (13, 'Banner13', 1, 1, 1, 2, 1, 20000, 4)
insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (14, 'Banner14', 1, 1, 1, 2, 2, 5000000, 4)
insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (15, 'Banner15', 1, 1, 1, 2, 3, 30000, 5)
insert into Banner (K, Name, [StatusArtwork], [StatusBooked], [StatusEnabled], PromoterK, Position, TotalRequiredImpressions, [BannerFolderK]) values (16, 'Banner16', 1, 1, 1, 2, 4, 7000000, 5)

                      
SET IDENTITY_INSERT [Banner] OFF
