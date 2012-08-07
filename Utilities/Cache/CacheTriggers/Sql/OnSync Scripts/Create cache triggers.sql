
DECLARE @TablesWithOneKeyAndADescription TABLE(NAME VARCHAR(250), object_id INT)
INSERT INTO @TablesWithOneKeyAndADescription

SELECT 
	kcu.TABLE_NAME
	,t.object_id 
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu
INNER JOIN sys.indexes i on i.name = CONSTRAINT_NAME AND is_Primary_key = 1
INNER JOIN sys.tables t ON i.object_id = t.object_id AND kcu.TABLE_NAME = t.name AND t.name = kcu.TABLE_NAME
INNER JOIN sys.extended_properties ep ON ep.major_id = t.object_id and ep.minor_id = 0 and ep.Name = 'MS_Description'
INNER JOIN sys.columns c ON t.object_id = c.object_id AND c.Name = 'K'
where table_name in ('Buddy', 'GroupPhoto', 'UsrPhotoMe', 'UsrPhotoFavourite') OR (table_name not in (
	select table_name from INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
	where ordinal_position > 1
) AND ordinal_position = 1  AND table_name NOT LIKE 'SpottedException'
)

DECLARE @Scripts TABLE(RowNumber INT, SQL VARCHAR(500))
INSERT INTO @Scripts
SELECT ROW_NUMBER() OVER (ORDER BY [SQL]), [SQl] FROM 
(
	select 'CREATE TRIGGER ' + t.Name + 'Trigger ON [' + t.Name + '] FOR UPDATE, INSERT, DELETE AS EXTERNAL NAME CacheTriggers.[CacheTriggers.Triggers].' + t.Name + 'Trigger' as [Sql]
	From @TablesWithOneKeyAndADescription t
	LEFT JOIN sys.triggers tr1 ON tr1.parent_id = t.object_id AND tr1.name = t.Name + 'Trigger'
	WHERE tr1.name is null
) as t 

IF EXISTS(SELECT * FROM sys.Triggers WHERE Name = 'SpottedExceptionTrigger') BEGIN
	DROP TRIGGER SpottedExceptionTrigger
END




DECLARE @Counter INT SELECT @Counter = MAX(RowNumber) FROM @Scripts
DECLARE @Sql VARCHAR(500)
WHILE @Counter > 0 BEGIN
	SELECT @Sql = Sql FROM @Scripts WHERE RowNumber = @Counter
	EXEC( @Sql)
	SET @Counter = @Counter - 1
END
