IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialIndexToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialIndexToTable]
go

create proc [Spatial.AddSpatialIndexToTable](@TableName varchar(max), @col1 varchar(max) = null, @col2 varchar(max) = null)
as

	declare @sql varchar(max)
	DECLARE @IndexName VARCHAR(MAX) 
	SET @IndexName = 
	'IX_' + @TableName + '_SpatialIndex' + 
	case when @col1  is null then '' else '_' + @col1 end +
	case when @col2  is null then '' else '_' + @col2 end 

	IF EXISTS(SELECT * FROM sys.indexes WHERE Name =@IndexName ) BEGIN

		SET @Sql = ('DROP INDEX ' + @IndexName + ' ON dbo.' + @TableName )
		EXEC(@Sql)
	END

	
	SET @sql = 'CREATE NONCLUSTERED INDEX ' + @IndexName + ' ON ' + @TableName + ' (HtmId' + 	case when @col1  is null then '' else ', ' + @col1 end +	case when @col2  is null then '' else ', ' + @col2 end	+ ') '
	EXEC (@SQL)

go


DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Place'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Population'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Name'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Venue'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Name'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Capacity'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'TotalEvents'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Thread'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'LastPost'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Article'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'AddedDateTime'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Gallery'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'CreateDateTime'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Event'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'DateTime', 'Name'

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialIndexToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialIndexToTable]

GO

