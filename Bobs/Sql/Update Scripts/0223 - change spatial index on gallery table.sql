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



IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Gallery]') AND name = N'IX_Gallery_SpatialIndex_CreateDateTime')
DROP INDEX [IX_Gallery_SpatialIndex_CreateDateTime] ON [dbo].[Gallery] WITH ( ONLINE = OFF )
 

GO
--NOW DO THE WORK

DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Gallery'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'LastLiveDateTime'
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialIndexToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialIndexToTable]
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialColumnsToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialColumnsToTable]
go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialDataToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialDataToTable]
go

