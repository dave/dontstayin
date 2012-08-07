IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddSpatialColumnsToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddSpatialColumnsToTable]
go
 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddSpatialIndexToTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddSpatialIndexToTable]
go
 
