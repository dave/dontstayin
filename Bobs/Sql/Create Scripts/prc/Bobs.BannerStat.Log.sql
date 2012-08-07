if exists (SELECT * FROM sys.procedures WHERE name = 'Bobs.BannerStat.Log') BEGIN
	DROP PROC [Bobs.BannerStat.Log]
END

GO

CREATE PROC [Bobs.BannerStat.Log] (@BannerK INT, @Date DATETIME, @Hits INT, @UniqueVisitors INT, @Clicks INT)
AS 
UPDATE BannerStat 
SET	Hits = Hits + @Hits, 
	UniqueVisitors = UniqueVisitors + @UniqueVisitors,
	Clicks = Clicks + @Clicks
WHERE Date = @Date AND BannerK = @BannerK

IF @@RowCount = 0 BEGIN 
	INSERT INTO BannerStat 
		(BannerK, Date,	Hits,	Clicks,		UniqueVisitors)
	VALUES (
		@BannerK, @Date, @Hits,	@Clicks,	@UniqueVisitors
	)
END
