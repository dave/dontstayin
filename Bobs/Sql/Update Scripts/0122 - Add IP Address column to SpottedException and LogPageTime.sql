IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'SpottedException' 
	AND	[column].name = 'IpAddress'
) BEGIN
	ALTER TABLE dbo.SpottedException ADD IpAddress varchar(15)
	EXECUTE sp_addextendedproperty N'MS_Description', N'User''s IP address', N'SCHEMA', N'dbo', N'TABLE', N'SpottedException', N'COLUMN', N'IpAddress'

END

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'LogPageTime' 
	AND	[column].name = 'DsiGuid'
) BEGIN
	ALTER TABLE dbo.LogPageTime ADD DsiGuid uniqueidentifier null
	EXECUTE sp_addextendedproperty N'MS_Description', N'DSI Browser Guid', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'DsiGuid'
	
END

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'LogPageTime' 
	AND	[column].name = 'IpAddress'
) BEGIN
	ALTER TABLE dbo.LogPageTime ADD IpAddress varchar(15)
	EXECUTE sp_addextendedproperty N'MS_Description', N'User''s IP address', N'SCHEMA', N'dbo', N'TABLE', N'LogPageTime', N'COLUMN', N'IpAddress'
	
END
