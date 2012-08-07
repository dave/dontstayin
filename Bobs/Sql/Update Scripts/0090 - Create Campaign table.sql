
IF EXISTS(
	SELECT * FROM sys.tables [table] 
	WHERE [table].name = 'Campaign' 
) BEGIN
DROP TABLE Campaign
END 

CREATE TABLE dbo.Campaign
	(
	K int NOT NULL IDENTITY (1, 1),
	Name varchar(250) NOT NULL,
	PromoterK int NOT NULL,
	DateTimeCreated datetime NOT NULL
	)  ON [PRIMARY]
	
ALTER TABLE dbo.Campaign ADD CONSTRAINT
	PK_Campaign_1 PRIMARY KEY CLUSTERED 
	(
	K
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
DECLARE @v sql_variant 
SET @v = N'A campaign object used for grouping banners'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Campaign', NULL, NULL
SET @v = N'The auto incrementing primary key'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Campaign', N'COLUMN', N'K'
SET @v = N'The name of the campaign'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Campaign', N'COLUMN', N'Name'
SET @v = N'The primary key of the promoter which owns the campaign'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Campaign', N'COLUMN', N'PromoterK'
SET @v = N'When the campaign object was first created'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Campaign', N'COLUMN', N'DateTimeCreated'
GO
IF NOT EXISTS (
	SELECT * FROM sys.tables t INNER JOIN sys.columns c on t.object_id = c.object_id WHERE c.Name = 'CampaignK' AND t.Name = 'Banner'
)BEGIN
	ALTER TABLE dbo.Banner ADD
		CampaignK int NULL
	DECLARE @v sql_variant 
	SET @v = N'The K of the campaign to which the banner belongs'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'CampaignK'
END
