IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Phone' 
	AND	[column].name = 'LocalGateway'
) BEGIN

ALTER TABLE dbo.Phone ADD LocalGateway varchar(100) NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The default gateway on the local network', N'SCHEMA', N'dbo', N'TABLE', N'Phone', N'COLUMN', N'LocalGateway'

END




IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Phone' 
	AND	[column].name = 'LocalDns'
) BEGIN

ALTER TABLE dbo.Phone ADD LocalDns varchar(100) NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The dns server on the local network', N'SCHEMA', N'dbo', N'TABLE', N'Phone', N'COLUMN', N'LocalDns'

END

 
