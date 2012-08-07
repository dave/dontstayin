
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'InvitedViaContactImporter'
) BEGIN
	ALTER TABLE dbo.Usr ADD
	InvitedViaContactImporter bit NULL

	DECLARE @v sql_variant 
	SET @v = N'Was this usr invited using the contact importer device?'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'InvitedViaContactImporter'

END

go
