IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Transfer' 
	AND	[column].name = 'Company'
) BEGIN

ALTER TABLE dbo.Transfer ADD Company int NOT NULL DEFAULT 0
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Which company did the transfer go to / come from', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'Company'
EXECUTE sp_addextendedproperty N'EnumProperty', N'CompanyEnum', N'SCHEMA', N'dbo', N'TABLE', N'Transfer', N'COLUMN', N'Company'

END
