IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Phone' 
	AND	[column].name = 'NatPort'
) BEGIN

ALTER TABLE dbo.Phone ADD NatPort varchar(100) NULL 
	
EXECUTE sp_addextendedproperty N'MS_Description', N'The nat redirect port', N'SCHEMA', N'dbo', N'TABLE', N'Phone', N'COLUMN', N'NatPort'

END
