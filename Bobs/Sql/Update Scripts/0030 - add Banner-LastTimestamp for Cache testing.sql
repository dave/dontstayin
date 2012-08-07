IF EXISTS (
	SELECT * FROM sysobjects WHERE id = object_id(N'[DF_Banner_LastTimestamp]')
) BEGIN
	ALTER TABLE dbo.Banner drop CONSTRAINT DF_Banner_LastTimestamp
END
GO

IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'LastTimestamp'
) BEGIN
	ALTER TABLE dbo.Banner drop column LastTimestamp
END
GO

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Banner' 
	AND	[column].name = 'LastTimestamp'
) BEGIN
	ALTER TABLE dbo.Banner ADD LastTimestamp timestamp

	DECLARE @v sql_variant 
	SET @v = N'Last updated timestamp for cache testing'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Banner', N'COLUMN', N'LastTimestamp'

END

GO
