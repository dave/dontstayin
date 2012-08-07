IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialIndexToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialIndexToTable]
go

create proc [Spatial.AddSpatialIndexToTable](@TableName varchar(max), @indexCols varchar(max) = null, @IncludedCols varchar(max) = null)
as

	declare @sql varchar(max)
	DECLARE @IndexName VARCHAR(MAX) 
	SET @IndexName = 
	'IX_' + @TableName + '_SpatialIndex' + 
	case when @indexCols is null then '' else '__' + REPLACE( @indexCols, ',', '_') end +
	case when @IncludedCols  is null then '' else '__' + REPLACE(@IncludedCols, ',', '_') end 

	IF EXISTS(SELECT * FROM sys.indexes WHERE Name =@IndexName ) BEGIN

		SET @Sql = ('DROP INDEX ' + @IndexName + ' ON dbo.' + @TableName )
		EXEC(@Sql)
	END

	
	SET @sql = 'CREATE NONCLUSTERED INDEX ' + @IndexName + ' ON ' + @TableName + ' (HtmId' + 	case when @indexCols  is null then '' else ', ' + @indexCols end + ') '
	SET @sql = @Sql + case when @IncludedCols  is null then '' else 'INCLUDE( ' + @IncludedCols + ')' end
	PRINT @Sql
	EXEC (@SQL)


	

go



 

GO
--NOW DO THE WORK


EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] 'Event', 'DateTime', 'Lat,Lon'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] 'Venue', 'Name', 'Lat,Lon'

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

