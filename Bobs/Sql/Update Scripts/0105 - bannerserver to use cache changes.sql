IF EXISTS(SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'banner' and c.Name ='MostRecentlySavedTimeslotStart') BEGIN
	ALTER TABLE dbo.Banner DROP COLUMN
		MostRecentlySavedTimeslotStart 
END
GO
IF EXISTS(SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'banner' and c.Name ='ImpressionDateTimes') BEGIN
	EXEC ('ALTER TABLE dbo.Banner DROP CONSTRAINT DF_Banner_TotalImpressionsUpUntilToday')
END
GO
IF EXISTS(SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'banner' and c.Name ='ImpressionDateTimes') BEGIN
	EXEC ('ALTER TABLE dbo.Banner DROP CONSTRAINT DF_Banner_TotalImpressionsSoFarToday')
END
GO
IF EXISTS(SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'banner' and c.Name ='ImpressionDateTimes') BEGIN
	ALTER TABLE dbo.Banner
		DROP COLUMN ImpressionDateTimes, TotalImpressionsUpUntilToday, TotalImpressionsSoFarToday
END
GO
IF EXISTS (SELECT * FROM Sys.procedures WHERE NAME = 'BannerServer.Banner.LogImpressions') BEGIN
	DROP PROC [BannerServer.Banner.LogImpressions]
END
GO
IF EXISTS (SELECT * FROM sys.procedures WHERE NAME = 'BannerServer.Banner.ClearBannerIdentityTempDataFromBeforeTodayIntoDailyStatsTable') BEGIN
	DROP PROC [BannerServer.Banner.ClearBannerIdentityTempDataFromBeforeTodayIntoDailyStatsTable]
END
GO
IF EXISTS(SELECT * FROM sys.tables WHERE NAME = 'BannerIdentityTempData') BEGIN
	DROP TABLE BannerIdentityTempData
END
GO
IF NOT EXISTS(SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE t.Name = 'BannerStat' and c.Name ='UniqueVisitors') BEGIN
	ALTER TABLE dbo.BannerStat ADD
		UniqueVisitors int NULL
END

GO

IF EXISTS(SELECT * FROM sys.tables WHERE NAME = 'BannerDailyStats') BEGIN
	DROP TABLE BannerDailyStats
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bobs.Banner.LogHits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Bobs.Banner.LogHits]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bobs.Banner.LogImpressions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Bobs.Banner.LogImpressions]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bobs.BannerDailyStats.SyncDataToBannerTotalHits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Bobs.BannerDailyStats.SyncDataToBannerTotalHits]

GO
IF NOT EXISTS(SELECT * FROM sys.INdexes WHERE Name = 'PK_BannerStat') BEGIN
	ALTER TABLE dbo.BannerStat ADD CONSTRAINT
		PK_BannerStat PRIMARY KEY NONCLUSTERED 
		(
		BannerK,
		Date
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO
IF EXISTS(SELECT * FROM sys.extended_properties ep INNER JOIN sys.tables t ON ep.major_id = t.object_id WHERE t.name = 'banner' AND ep.Name = 'MS_Description' AND ep.Value = 'Number of slots occupied by this banner. Value of 2 indicates double the normal number of hits.') 
BEGIN
	
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHits'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalClicks'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHitsTargetMatch'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHitsPlaceMatch'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalHitsMusicMatch'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalClicksPlaceMatch'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'TotalClicksMusicMatch'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'DateLastHit'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'HitsToday'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'Weight'
	EXEC sp_dropextendedproperty  N'MS_Description', N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'HitsTodayNormalised'
	
		
END
