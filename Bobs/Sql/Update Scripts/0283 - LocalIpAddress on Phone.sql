IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Phone' 
	AND	[column].name = 'LocalIpAddress'
) BEGIN

ALTER TABLE dbo.Phone ADD LocalIpAddress varchar(100) NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The IP of the phone on the local network', N'SCHEMA', N'dbo', N'TABLE', N'Phone', N'COLUMN', N'LocalIpAddress'

END


