IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'SpottedException' 
	AND	[column].name = 'SqlServerDateTime'
) BEGIN
	ALTER TABLE dbo.SpottedException ADD SqlServerDateTime datetime not null default getdate()

END
