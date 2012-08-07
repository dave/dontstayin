IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'LogPageTime' 
	AND	[column].name = 'SqlServerDateTime'
) BEGIN
	ALTER TABLE dbo.LogPageTime ADD SqlServerDateTime datetime not null default getdate()

END
