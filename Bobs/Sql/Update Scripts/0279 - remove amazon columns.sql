DECLARE @TableName NVARCHAR(MAX) SET @TableName = 'Group'
DECLARE @ColumnName NVARCHAR(MAX) SET @ColumnName = 'AverageCommentDateTime'
DECLARE @PropertyName NVARCHAR(MAX) SET @PropertyName = 'IsNotNull'
IF EXISTS (
	SELECT * 
	FROM sys.extended_properties ep 
	JOIN sys.tables t ON ep.major_id = t.object_id
	JOIN sys.columns c ON t.object_id = c.object_id AND ep.minor_id = c.column_id
	WHERE c.Name = @ColumnName AND t.Name = @TableName and ep.Name = @PropertyName
)	BEGIN
	EXEC sys.sp_dropextendedproperty @name=@PropertyName , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=@ColumnName
END

GO
DECLARE @TableName NVARCHAR(MAX) SET @TableName = 'Group'
DECLARE @ColumnName NVARCHAR(MAX) SET @ColumnName = 'LastPost'
DECLARE @PropertyName NVARCHAR(MAX) SET @PropertyName = 'IsNotNull'
IF EXISTS (
	SELECT * 
	FROM sys.extended_properties ep 
	JOIN sys.tables t ON ep.major_id = t.object_id
	JOIN sys.columns c ON t.object_id = c.object_id AND ep.minor_id = c.column_id
	WHERE c.Name = @ColumnName AND t.Name = @TableName and ep.Name = @PropertyName
)	BEGIN
	EXEC sys.sp_dropextendedproperty @name=@PropertyName , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=@ColumnName
END
GO
/*
DECLARE @TableNames  TABLE(Counter INT, TableName VARCHAR(MAX))
DECLARE @ColumnName NVARCHAR(MAX) SET @ColumnName = 'Pic'
INSERT INTO @TableNames VALUES(1, 'Article')
INSERT INTO @TableNames VALUES(2, 'Brand')
INSERT INTO @TableNames VALUES(3, 'Comp')
INSERT INTO @TableNames VALUES(4, 'Event')
INSERT INTO @TableNames VALUES(5, 'Group')
INSERT INTO @TableNames VALUES(6, 'Para')
INSERT INTO @TableNames VALUES(7, 'Place')
INSERT INTO @TableNames VALUES(8, 'Usr')
INSERT INTO @TableNames VALUES(9, 'Venue')
INSERT INTO @TableNames VALUES(10, 'Promoter')


DECLARE @Counter INT SET @Counter = 1
DECLARE @PropertyName NVARCHAR(MAX) SET @PropertyName = 'IsNotNull'
WHILE EXISTS (SELECT * FROM @TableNames) BEGIN
	DECLARE @TableName VARCHAR(MAX) SELECT @TableName = TableName FROM @TableNames WHERE Counter = @Counter
	PRINT @TableName
	
	IF EXISTS (
		SELECT * 
		FROM sys.extended_properties ep 
		JOIN sys.tables t ON ep.major_id = t.object_id
		JOIN sys.columns c ON t.object_id = c.object_id AND ep.minor_id = c.column_id
		WHERE c.Name = @ColumnName AND t.Name = @TableName and ep.Name = @PropertyName
	)	BEGIN
		EXEC sys.sp_dropextendedproperty @name=@PropertyName , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=@ColumnName
	END
	EXEC ('UPDATE [' + @TableName + '] SET Pic = NULL WHERE Pic = ''00000000-0000-0000-0000-000000000000''')
	DELETE FROM @TableNames WHERE Counter = @Counter
	SET @Counter = @Counter + 1
END
*/
GO



WHILE EXISTS(SELECT * FROM sys.columns c INNER JOIN sys.tables t ON t.object_id = c.object_id AND c.Name IN ('DoneAmazonPix', 'FailedAmazonPix', 'FailedAmazonCheck'))
BEGIN 
	DECLARE @TableName VARCHAR(MAX) 
	DECLARE @ColumnName VARCHAR(MAX)
	SELECT @TableName = t.Name, @ColumnName = c.Name FROM sys.columns c INNER JOIN sys.tables t ON t.object_id = c.object_id AND c.Name IN ('DoneAmazonPix', 'FailedAmazonPix', 'FailedAmazonCheck')
	PRINT @TableName + '.' + @ColumnName
	EXEC( 'ALTER TABLE dbo.[' + @TableName + ']	DROP COLUMN ' + @ColumnName)
END
