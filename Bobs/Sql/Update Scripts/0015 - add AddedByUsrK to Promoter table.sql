IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Promoter' 
	AND	[column].name = 'AddedByUsrK'
) BEGIN
	ALTER TABLE dbo.Promoter ADD
	AddedByUsrK int NULL

	DECLARE @v sql_variant 
	SET @v = N'Who was the promoter added by (e.g. for sales admins)'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Promoter', N'COLUMN', N'AddedByUsrK'

END

 


