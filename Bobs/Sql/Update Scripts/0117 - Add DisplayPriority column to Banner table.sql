IF NOT EXISTS(SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.name = 'Banner' and c.name = 'Priority') BEGIN
	ALTER TABLE Banner 
		ADD Priority INT NOT NULL DEFAULT 0
	DECLARE @v sql_variant 
	
	SET @v = N'Higher priority banners will always be shown before those with lower priorities'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'Priority'

END
GO
IF NOT EXISTS(SELECT * FROM sys.columns c INNER JOIN sys.tables t ON c.object_id = t.object_id WHERE t.name = 'Banner' and c.name = 'AlwaysShow') BEGIN
	ALTER TABLE Banner 
		ADD AlwaysShow BIT NOT NULL DEFAULT 0
	DECLARE @v sql_variant 
	
	SET @v = N'Setting this bit ensures that this banner will be shown if suitable for request'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'AlwaysShow'

END
