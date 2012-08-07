IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Phone' 
	AND	[column].name = 'IpAddress'
) BEGIN

ALTER TABLE dbo.Phone ADD IpAddress varchar(100) NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The external internet IP of the phone', N'SCHEMA', N'dbo', N'TABLE', N'Phone', N'COLUMN', N'IpAddress'

END


