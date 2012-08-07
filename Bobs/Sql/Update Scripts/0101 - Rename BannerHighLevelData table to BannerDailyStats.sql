IF EXISTS (SELECT * FROM sys.tables WHERE Name = 'BannerHighLevelData') BEGIN
EXEC sp_rename 'BannerHighLevelData', 'BannerDailyStats'

END

IF EXISTS (SELECT * FROM sys.procedures WHERE Name = 'BannerServer.Banner.ClearBannerIdentityTempDataFromBeforeTodayIntoHighLevelDataTable') BEGIN
DROP proc [BannerServer.Banner.ClearBannerIdentityTempDataFromBeforeTodayIntoHighLevelDataTable]
END
