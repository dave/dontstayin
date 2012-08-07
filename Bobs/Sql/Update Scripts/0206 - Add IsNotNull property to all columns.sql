DECLARE @Templates TABLE([Order] INT, Sql VARCHAR(MAX))
INSERT INTO @Templates VALUES(0, '--{TableName} {ColumnName}')
INSERT INTO @Templates VALUES(1, 'EXEC sys.sp_addextendedproperty @name=N''IsNotNull'', @value=N''true'' , @level0type=N''SCHEMA'',@level0name=N''dbo'', @level1type=N''TABLE'',@level1name=N''{TableName}'', @level2type=N''COLUMN'',@level2name=N''{ColumnName}''')

DECLARE @Exclude TABLE(TableName VARCHAR(MAX), ColumnName VARCHAR(MAX))




DECLARE @Commands Table(Id INT, Sql VARCHAR(MAX))

INSERT INTO @Commands
SELECT ROW_NUMBER() OVER (ORDER BY t.[object_id], c.[Column_id], te.[Order] DESC),  REPLACE(REPLACE(
			te.Sql, 
		'{TableName}', t.Name), '{ColumnName}', c.Name) 
FROM sys.tables t 
INNER JOIN sys.columns c ON t.object_id = c.Object_id 
--INNER JOIN @Mapping m ON c.system_type_id = m.system_type_id
CROSS JOIN @Templates te 
LEFT JOIN @Exclude ex ON ex.TableName = t.Name AND ex.ColumnName = c.Name
WHERE	NOT  C.Name IN ('Lat', 'Lon', 'HtmId', 'PosX', 'PosY', 'PosZ', 'K' ) 
AND		t.Name NOT LIKE '%Quartz%' 



DECLARE @Counter INT SELECT @Counter = COUNT(*) FROM @Commands
WHILE (@Counter > 0) BEGIN
	DECLARE @Command VARCHAR(MAX)
	SELECT @Command = Sql FROM @Commands WHERE Id = @Counter
	PRINT (@Command)
	EXEC (@Command)
	SET @Counter = @Counter - 1
END
