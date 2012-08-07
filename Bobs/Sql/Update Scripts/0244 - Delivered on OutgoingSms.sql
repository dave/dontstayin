IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'OutgoingSms' 
	AND	[column].name = 'Delivered'
) BEGIN

ALTER TABLE dbo.OutgoingSms ADD
	Delivered bit NULL
	
DECLARE @v sql_variant 
SET @v = N'The message was successfully delivered (and charged if it is a premium rate sms)'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'OutgoingSms', N'COLUMN', N'Delivered'

END
