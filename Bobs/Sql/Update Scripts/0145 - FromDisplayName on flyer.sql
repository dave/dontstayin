

IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'MailFromDisplayName'
) BEGIN
	ALTER TABLE dbo.Flyer ADD MailFromDisplayName varchar(50) NULL

	DECLARE @v sql_variant 
	SET @v = N'Optional display name in the From field on the sent email'
	EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'MailFromDisplayName'

END
