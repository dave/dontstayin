IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Usr' 
	AND	[column].name = 'NeedsCaptcha'
) BEGIN

ALTER TABLE dbo.Usr ADD NeedsCaptcha bit NULL
	
EXECUTE sp_addextendedproperty N'MS_Description', N'Does this user need a captcha for security?', N'SCHEMA', N'dbo', N'TABLE', N'Usr', N'COLUMN', N'NeedsCaptcha'

END
