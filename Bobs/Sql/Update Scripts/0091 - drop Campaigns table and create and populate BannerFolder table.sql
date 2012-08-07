IF EXISTS(
	SELECT * FROM sys.tables [table] 
	WHERE [table].name = 'Campaign' 
) BEGIN
DROP TABLE Campaign
END 

GO
IF EXISTS(
	SELECT * FROM sys.tables [table] 
	WHERE [table].name = 'BannerFolder' 
) BEGIN
DROP TABLE BannerFolder
END 
GO
IF EXISTS(
	SELECT * FROM sys.tables t INNER JOIN sys.Columns c ON t.object_id = c.object_id
	WHERE t.name = 'Banner' AND c.Name = 'CampaignK'
) BEGIN 
	ALTER TABLE dbo.Banner DROP	COLUMN CampaignK 

END

GO
CREATE TABLE dbo.BannerFolder
	(
	K int NOT NULL IDENTITY (1, 1),
	Name varchar(250) NOT NULL,
	PromoterK int NOT NULL,
	DateTimeCreated datetime NOT NULL,
	EventK INT NULL
	)  ON [PRIMARY]
	
ALTER TABLE dbo.BannerFolder ADD CONSTRAINT
	PK_BannerFolder_1 PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
DECLARE @v sql_variant 
SET @v = N'A BannerFolder object used for grouping banners'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerFolder', NULL, NULL
SET @v = N'The auto incrementing primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerFolder', N'COLUMN', N'K'
SET @v = N'The name of the BannerFolder'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerFolder', N'COLUMN', N'Name'
SET @v = N'The primary key of the promoter which owns the BannerFolder'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerFolder', N'COLUMN', N'PromoterK'
SET @v = N'When the BannerFolder object was first created'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerFolder', N'COLUMN', N'DateTimeCreated'
SET @v = N'The evnet the folder corresponds to, if any'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'BannerFolder', N'COLUMN', N'EventK'
GO
IF NOT EXISTS (
	SELECT * FROM sys.tables t INNER JOIN sys.columns c on t.object_id = c.object_id WHERE c.Name = 'BannerFolderK' AND t.Name = 'Banner'
)BEGIN
	ALTER TABLE dbo.Banner ADD
		BannerFolderK int NULL
	DECLARE @v sql_variant 
	SET @v = N'The K of the BannerFolder to which the banner belongs'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'BannerFolderK'
END
GO
DELETE FROM BannerFolder
UPDATE Banner SET BannerFolderK = null

DELETE FROM Banner WHERE FirstDay IS NULL;

UPDATE b SET EventK = 0
FROM Banner B 
LEFT JOIN Event e ON B.EventK = e.K 
WHERE e.K IS NULL AND b.EventK <> 0

SELECT ROW_NUMBER() OVER (ORDER BY FirstDay) as BannerFolderK, *
INTO #BannerFolderEvent
FROM (
	SELECT null as BannerK, EventK, PromoterK, MIN(FirstDay) FirstDay 
	FROM Banner 
	WHERE EventK <> 0  AND Banner.BannerFolderK is null 
	GROUP BY EventK, PromoterK
	UNION SELECT K as BannerK, null as EventK, PromoterK, FirstDay 
	FROM Banner 
	WHERE EventK = 0 AND Banner.BannerFolderK is null 
	
) BannerFoldersToBeCreated


SET IDENTITY_INSERT BannerFolder ON
INSERT INTO BannerFolder
(K, Name, PromoterK, DateTimeCreated, EventK)
SELECT ce.BannerFolderK, 'Event: ' + e.Name collate database_default as Name, PromoterK, FirstDay, ce.EventK
FROM #BannerFolderEvent ce 
INNER JOIN Event e ON ce.EventK = e.K
UNION 
SELECT ce.BannerFolderK, 'Banner: ' + b.Name collate database_default as Name, ce.PromoterK, b.FirstDay, null
FROM #BannerFolderEvent ce 
INNER JOIN Banner b ON ce.BannerK = b.K
WHERE ce.EventK is null 
SET IDENTITY_INSERT BannerFolder OFF

UPDATE b
SET b.BannerFolderK = ce.BannerFolderK
FROM Banner b
LEFT JOIN #BannerFolderEvent ce  ON b.EventK = ce.EventK and b.PromoterK = ce.PromoterK  WHERE b.BannerFolderK is null

UPDATE b
SET b.BannerFolderK = ce.BannerFolderK
FROM Banner b
LEFT JOIN #BannerFolderEvent ce  ON b.K = ce.BannerK WHERE b.BannerFolderK is null



DROP TABLE #BannerFolderEvent


SELECT * FROM BannerFolder
