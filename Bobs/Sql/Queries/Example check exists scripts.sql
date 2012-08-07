/*example data existence check*/
IF NOT EXISTS(
	--INSERT SELECT QUERY THAT WOULD BRING BACK DATA
)BEGIN 
	PRINT 'INSERT OR UPDATE DATA HERE'
END


/*example table existence check*/
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'TableName' 
) BEGIN
	PRINT 'INSERT TABLE HERE'
END


/*example column existance check*/
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'TableName' 
	AND	[column].name = 'ColumnName'
) BEGIN
	PRINT 'INSERT COLUMN HERE'
END


/* example column length check */
IF NOT EXISTS(
	SELECT * FROM sys.tables [table] INNER JOIN sys.columns [column] ON [table].object_id = [column].object_id 	
	WHERE [table].name = 'TableName' 
	AND	[column].name = 'ColumnName'
	AND [column].max_length = 'new data column length')
) BEGIN
	PRINT 'UPDATE COLUMN LENGTH HERE'
END



/* example index existance script check */

IF NOT EXISTS(
	SELECT * FROM sys.indexes i INNER JOIN sys.tables t On t.object_id = i.object_id
	WHERE t.name = 'tableName' AND i.name = 'indexName' ) 
) BEGIN
	PRINT 'CREATE INDEX HERE'
END

