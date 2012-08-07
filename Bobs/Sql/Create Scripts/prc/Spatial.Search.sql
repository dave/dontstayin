
if exists (SELECT * FROM sys.procedures WHERE name = 'Spatial.Search') BEGIN
	DROP PROC [Spatial.Search]
END

GO

CREATE PROC [Spatial.Search] (@TableName VARCHAR(50), @WhereClause VARCHAR(MAX), @CustomVariableDeclarationSql VARCHAR(MAX), @OrderBy VARCHAR(200), @FirstRowIndex INT, @LastRowIndex INT, @North FLOAT, @East FLOAT, @South FLOAT, @West FLOAT)
AS 

if (@WhereClause is null) SET @WhereClause  = '1=1'
if (@OrderBy is null) SET @OrderBy = 'ORDER BY K'

DECLARE @SQL NVARCHAR(MAX) SET @SQL = @CustomVariableDeclarationSql + N' 
DECLARE @MidLat FLOAT SET @MidLat = @South + (@North - @South) / 2
DECLARE @MidLon FLOAT SET @MidLon = @West + (@East - @West) / 2
DECLARE @Radius Float SET @Radius = dbo.fDistanceLatLon(@North, @West, @South, @East) / 2;



WITH Items (K, RowNum) AS (
		SELECT TOP ' + convert(varchar(max), @LastRowIndex + 1) + ' K, - 1 + ROW_NUMBER() OVER(' + @OrderBy + ') as RowNum
		FROM dbo.[fHtmCoverCircleLatLon](@MidLat,  @MidLon,  @Radius) f 
		JOIN ' + @TableName + ' p ON ' + @WhereClause + ' AND p.HtmId BETWEEN HtmIdStart AND HtmIdEnd 
					AND	Lat BETWEEN @South AND @North AND Lon BETWEEN @West AND @East
	)

SELECT TOP ' + convert(varchar(max), @LastRowIndex + 1 - @FirstRowIndex) + ' t.*
from Items
	INNER JOIN ' + @TableName + ' t ON t.k = items.K
WHERE RowNum BETWEEN @FirstRowIndex AND @LastRowIndex
ORDER BY RowNum'

EXECUTE sp_executesql  
	@SQL, 
	N'@North FLOAT, @East FLOAT, @South FLOAT, @West FLOAT, @FirstRowIndex  INT, @LastRowIndex INT',
    @North = @North, @East = @East, @South = @South, @West = @West, @FirstRowIndex = @FirstRowIndex, @LastRowIndex = @LastRowIndex

GO
