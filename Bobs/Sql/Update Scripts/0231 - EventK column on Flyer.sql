IF EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'Flyer' 
	AND	[column].name = 'EventK'
) BEGIN

ALTER TABLE Flyer DROP COLUMN EventK

END

ALTER TABLE dbo.Flyer ADD
	EventK int NULL
	
DECLARE @v sql_variant 
SET @v = N'Target users who attended / are attending a particular event'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'Flyer', N'COLUMN', N'EventK'
