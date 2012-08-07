IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialColumnsToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialColumnsToTable]
go

create proc dbo.[Spatial.AddSpatialColumnsToTable] (@TableName varchar(max))
as
IF NOT EXISTS(SELECT * FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id WHERE c.name = 'HtmId' And t.Name = @TableName) BEGIN
	declare @sql varchar(max)
	set @sql = '
			ALTER TABLE ' + @TableName + ' ADD
			HtmId bigint NULL,
			Lat float(53) NULL,
			Lon float(53) NULL,
			PosX float(53) NULL,
			PosY float(53) NULL,
			PosZ float(53) NULL
		'
	EXEC (@sql)
	
		EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Hierarchical triangular mesh index' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE', @level1name=@TableName, @level2type=N'COLUMN',@level2name=N'HtmId'
		EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Latitude' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=N'Lat'
		EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Longitude' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=N'Lon'
		EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cartesian x-coordinate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=N'PosX'
		EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cartesian y-coordinate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=N'PosY'
		EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cartesian z-coordinate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=@TableName, @level2type=N'COLUMN',@level2name=N'PosZ'
END
go
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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Spatial.AddSpatialDataToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Spatial.AddSpatialDataToTable]
go




 

GO
--NOW DO THE WORK

DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Place'
EXECUTE [dbo].[Spatial.AddSpatialColumnsToTable] @TableName
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Population'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Name'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Venue'
EXECUTE [dbo].[Spatial.AddSpatialColumnsToTable] @TableName
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Name'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'Capacity'
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'TotalEvents'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Thread'
EXECUTE [dbo].[Spatial.AddSpatialColumnsToTable] @TableName
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'LastPost'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Article'
EXECUTE [dbo].[Spatial.AddSpatialColumnsToTable] @TableName
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'AddedDateTime'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Gallery'
EXECUTE [dbo].[Spatial.AddSpatialColumnsToTable] @TableName
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'CreateDateTime'
GO
DECLARE @TableName VARCHAR(MAX) SET @TableName = 'Event'
EXECUTE [dbo].[Spatial.AddSpatialColumnsToTable] @TableName
EXECUTE [dbo].[Spatial.AddSpatialIndexToTable] @TableName, 'DateTime', 'Name'
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

